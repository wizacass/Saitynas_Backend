using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Saitynas_API.Models.Common;
using Saitynas_API.Models.DTO;
using Saitynas_API.Models.Entities.Evaluation.DTO;
using Saitynas_API.Models.Entities.Role;
using Saitynas_API.Models.Entities.Specialist;
using Saitynas_API.Models.Entities.Specialist.DTO;
using Saitynas_API.Models.Entities.User;
using Saitynas_API.Repositories;
using Saitynas_API.Services.Validators;

namespace Saitynas_API.Controllers;

[Route($"{RoutePrefix}/[Controller]")]
[ApiController]
[Produces(ApiContentType)]
public class SpecialistsController : ApiControllerBase
{
    private readonly IEvaluationsRepository _evaluationsRepository;

    private readonly ISpecialistsRepository _specialistsRepository;
    private readonly ISpecialistDTOValidator _validator;
    protected override string ModelName => "specialist";

    public SpecialistsController(
        UserManager<User> userManager,
        ISpecialistsRepository specialistsRepository,
        IEvaluationsRepository evaluationsRepository,
        ISpecialistDTOValidator validator
    ) : base(userManager)
    {
        _specialistsRepository = specialistsRepository;
        _evaluationsRepository = evaluationsRepository;
        _validator = validator;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Patient")]
    public async Task<ActionResult<GetListDTO<GetSpecialistDTO>>> GetSpecialists()
    {
        var specialists = (await _specialistsRepository.GetAllAsync())
            .Select(w => new GetSpecialistDTO(w));

        var dto = new GetListDTO<GetSpecialistDTO>(specialists);

        return Ok(dto);
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin,Patient")]
    public async Task<ActionResult<GetObjectDTO<GetSpecialistDTO>>> GetSpecialist(int id)
    {
        var specialist = await _specialistsRepository.GetAsync(id);

        if (specialist == null) return ApiNotFound();

        var dto = new GetSpecialistDTO(specialist);
        return Ok(new GetObjectDTO<GetSpecialistDTO>(dto));
    }

    [HttpGet("{id:int}/evaluations")]
    [Authorize(Roles = "Admin,Patient")]
    public async Task<ActionResult<GetListDTO<GetEvaluationDTO>>> GetSpecialistEvaluations(int id)
    {
        var evaluations = (await _evaluationsRepository.GetBySpecialistId(id))
            .Select(e => new GetEvaluationDTO(e));

        return Ok(new GetListDTO<GetEvaluationDTO>(evaluations));
    }

    [HttpPost]
    [Authorize(Roles = AuthRole.Specialist)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSpecialist([FromBody] CreateSpecialistDTO dto)
    {
        var user = await GetCurrentUser();

        if (user.HasProfile) return ApiBadRequest(ApiErrorSlug.EntityExists, ModelName);

        _validator.ValidateCreateSpecialistDTO(dto);
        var specialist = new Specialist(dto, user);
        await _specialistsRepository.InsertAsync(specialist);

        return NoContent();
    }

    [HttpGet("active")]
    [Authorize(Roles = AuthRole.Patient)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<GetObjectDTO<CountDTO>>> GetActiveSpecialistsCount()
    {
        int count = await _specialistsRepository.GetOnlineSpecialistsCount();

        var dto = new CountDTO {Count = count};
        return Ok(new GetObjectDTO<CountDTO>(dto));
    }
}
