using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Saitynas_API.Models.Common;
using Saitynas_API.Models.DTO;
using Saitynas_API.Models.Entities.Consultation.DTO;
using Saitynas_API.Models.Entities.Evaluation;
using Saitynas_API.Models.Entities.Evaluation.DTO;
using Saitynas_API.Models.Entities.Role;
using Saitynas_API.Models.Entities.Specialist;
using Saitynas_API.Models.Entities.Specialist.DTO;
using Saitynas_API.Models.Entities.User;
using Saitynas_API.Models.Entities.User.DTO;
using Saitynas_API.Repositories;
using Saitynas_API.Services;

namespace Saitynas_API.Controllers;

[Route($"{RoutePrefix}/[Controller]/me")]
[ApiController]
[Produces(ApiContentType)]
public class UsersController : ApiControllerBase
{
    private readonly IEvaluationsRepository _evaluationsRepository;
    private readonly ISpecialistsRepository _specialistsRepository;
    private readonly IPatientsRepository _patientsRepository;
    private readonly IConsultationsService _consultationsService;
    private readonly IConsultationsRepository _consultationsRepository;

    protected override string ModelName => nameof(User);

    public UsersController(
        UserManager<User> userManager,
        IEvaluationsRepository evaluationsRepository,
        ISpecialistsRepository specialistsRepository,
        IPatientsRepository patientsRepository,
        IConsultationsService consultationsService,
        IConsultationsRepository consultationsRepository
    ) : base(userManager)
    {
        _evaluationsRepository = evaluationsRepository;
        _specialistsRepository = specialistsRepository;
        _patientsRepository = patientsRepository;
        _consultationsService = consultationsService;
        _consultationsRepository = consultationsRepository;
    }

    [HttpGet]
    [Authorize(Roles = AuthRole.AnyRole)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetObjectDTO<UserDTO>>> GetUser()
    {
        var user = await GetCurrentUser();

        if (user == null) return ApiNotFound(ApiErrorSlug.ResourceNotFound, ModelName);

        var dto = new UserDTO(user);
        return Ok(new GetObjectDTO<UserDTO>(dto));
    }

    [HttpGet("profile")]
    [Authorize(Roles = AuthRole.AnyRole)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetObjectDTO<ProfileDTO>>> GetProfile()
    {
        var user = await GetCurrentUser();

        if (user == null) return ApiNotFound(ApiErrorSlug.ResourceNotFound, ModelName);

        var dto = await GetProfileDto(user);

        return dto == null
            ? ApiNotFound(ApiErrorSlug.ResourceNotFound, "profile")
            : Ok(new GetObjectDTO<ProfileDTO>(dto));
    }

    private async Task<ProfileDTO> GetProfileDto(User user)
    {
        if (!user.HasProfile) return null;

        return user.RoleId switch
        {
            RoleId.Patient => new ProfileDTO(await _patientsRepository.GetByUserId(user.Id)),
            RoleId.Specialist => new ProfileDTO(await _specialistsRepository.GetByUserId(user.Id)),
            _ => null
        };
    }

    [HttpGet("evaluations")]
    [Authorize(Roles = AuthRole.AnyRole)]
    public async Task<ActionResult<GetListDTO<GetEvaluationDTO>>> GetUserEvaluations()
    {
        var user = await GetCurrentUser();

        if (user == null) return ApiNotFound(ApiErrorSlug.ResourceNotFound, ModelName);

        var evaluations = (await RetrieveEvaluations(user))
            .Select(e => new GetEvaluationDTO(e));

        var dto = new GetListDTO<GetEvaluationDTO>(evaluations);
        return Ok(dto);
    }

    private async Task<IEnumerable<Evaluation>> RetrieveEvaluations(User u)
    {
        return u.RoleId switch
        {
            RoleId.Patient => await _evaluationsRepository.GetByUserId(u.Id),
            RoleId.Specialist => await _evaluationsRepository.GetBySpecialistId(u.SpecialistId ?? 0),
            _ => null
        };
    }

    [HttpGet("status")]
    [Authorize(Roles = AuthRole.Specialist)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetObjectDTO<GetEnumDTO>>> RetrieveSpecialistStatus()
    {
        var user = await GetCurrentUser();

        var status = user.Specialist.SpecialistStatusId;

        if (status == null) return ApiNotFound("ActivityStatus");

        var dto = new GetObjectDTO<GetEnumDTO>(new GetEnumDTO
        {
            Id = (int) status,
            Name = status.ToString()
        });

        return Ok(dto);
    }

    [HttpPut("status")]
    [Authorize(Roles = AuthRole.Specialist)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<NoContentResult> UpdateSpecialistStatus([FromBody] SpecialistStatusDto dto)
    {
        var user = await GetCurrentUser();

        var specialist = user.Specialist;
        specialist.SpecialistStatusId = dto.StatusId;

        await _specialistsRepository.UpdateAsync(specialist.Id, specialist);

        // TODO: Ping Queue
        if (specialist.SpecialistStatusId == SpecialistStatusId.Available)
        {
            await _consultationsService.EnqueueSpecialist(dto.DeviceToken, specialist.SpecialityId ?? 0);
        }
        else
        {
            await _consultationsService.DequeueSpecialist(dto.DeviceToken);
        }

        return NoContent();
    }

    [HttpGet("consultations")]
    [Authorize(Roles = AuthRole.Specialist)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetListDTO<GetConsultationDTO>>> GetSpecialistConsultations()
    {
        int? specialistId = (await GetCurrentUser()).SpecialistId;

        if (specialistId == null) return ApiNotFound(ApiErrorSlug.ResourceNotFound, nameof(Specialist));

        var consultations = (await _consultationsRepository.GetFinishedBySpecialistId(specialistId))
            .Select(c => new GetConsultationDTO(c));

        var dto = new GetListDTO<GetConsultationDTO>(consultations);

        return Ok(dto);
    }
}
