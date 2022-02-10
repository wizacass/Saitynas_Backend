using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Saitynas_API.Models.Common;
using Saitynas_API.Models.DTO;
using Saitynas_API.Models.Entities.Evaluation;
using Saitynas_API.Models.Entities.Evaluation.DTO;
using Saitynas_API.Models.Entities.Role;
using Saitynas_API.Models.Entities.User;
using Saitynas_API.Models.Entities.User.DTO;
using Saitynas_API.Repositories;
using Saitynas_API.Services;

namespace Saitynas_API.Controllers;

[Route($"{RoutePrefix}/[Controller]")]
[ApiController]
[Produces(ApiContentType)]
public class UsersController : ApiControllerBase
{
    private readonly IEvaluationsRepository _evaluationsRepository;
    private readonly ISpecialistsRepository _specialistsRepository;
    private readonly IPatientsRepository _patientsRepository;

    private readonly IApiUserStore _userStore;
    protected override string ModelName => "user";

    public UsersController(
        IApiUserStore userStore,
        UserManager<User> userManager,
        IEvaluationsRepository evaluationsRepository,
        ISpecialistsRepository specialistsRepository,
        IPatientsRepository patientsRepository
    ) : base(userManager)
    {
        _userStore = userStore;
        _evaluationsRepository = evaluationsRepository;
        _specialistsRepository = specialistsRepository;
        _patientsRepository = patientsRepository;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    [Obsolete]
    public async Task<ActionResult<GetListDTO<UserDTO>>> GetUsers()
    {
        var users = (await _userStore.GetAllUsers())
            .Select(u => new UserDTO(u));

        var dto = new GetListDTO<UserDTO>(users);

        return Ok(dto);
    }

    [HttpGet("me")]
    [Authorize(Roles = AllRoles)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetObjectDTO<UserDTO>>> GetUser()
    {
        var user = await GetCurrentUser();

        if (user == null) return ApiNotFound(ApiErrorSlug.ResourceNotFound, ModelName);

        var dto = new UserDTO(user);
        return Ok(new GetObjectDTO<UserDTO>(dto));
    }

    [HttpGet("me/profile")]
    [Authorize(Roles = AllRoles)]
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

    [HttpGet("me/evaluations")]
    [Authorize(Roles = "Specialist,Patient")]
    public async Task<ActionResult<GetListDTO<GetUserEvaluationDTO>>> GetUserEvaluations()
    {
        var user = await GetCurrentUser();

        if (user == null) return ApiNotFound(ApiErrorSlug.ResourceNotFound, ModelName);

        var evaluations = (await RetrieveEvaluations(user))
            .Select(e => new GetUserEvaluationDTO(e));

        var dto = new GetListDTO<GetUserEvaluationDTO>(evaluations);
        return Ok(dto);
    }

    private async Task<IEnumerable<Evaluation>> RetrieveEvaluations(User u)
    {
        return u.RoleId switch
        {
            RoleId.Patient => await _evaluationsRepository.GetByUserId(u.Id),
            RoleId.Specialist => await _evaluationsRepository.GetBySpecialistId(u.Id),
            _ => null
        };
    }
}
