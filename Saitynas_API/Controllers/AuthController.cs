using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Saitynas_API.Models.Authentication.DTO;
using Saitynas_API.Models.DTO;
using Saitynas_API.Models.Entities.Role;
using Saitynas_API.Models.Entities.Specialist;
using Saitynas_API.Models.Entities.User;
using Saitynas_API.Repositories;
using Saitynas_API.Services;
using Saitynas_API.Services.Validators;

namespace Saitynas_API.Controllers;

[Route(RoutePrefix)]
[ApiController]
[Produces(ApiContentType)]
public class AuthController : ApiControllerBase
{
    private readonly IAuthenticationDTOValidator _validator;
    private readonly IAuthenticationService _authService;
    private readonly ISpecialistsRepository _specialistsRepository;
    
    protected override string ModelName => "user";

    public AuthController(
        IAuthenticationDTOValidator validator,
        IAuthenticationService authService,
        ISpecialistsRepository specialistsRepository,
        UserManager<User> userManager
    ) : base(userManager)
    {
        _validator = validator;
        _authService = authService;
        _specialistsRepository = specialistsRepository;
    }

    [HttpPost("signup")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuthenticationDTO>> Signup([FromBody] SignupDTO requestDto)
    {
        _validator.ValidateSignupDTO(requestDto);

        var responseDto = await _authService.Signup(requestDto);

        return ApiCreated(responseDto);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuthenticationDTO>> Login([FromBody] LoginDTO requestDto)
    {
        _validator.ValidateLoginDTO(requestDto);

        var responseDto = await _authService.Login(requestDto);

        return ApiCreated(responseDto);
    }

    [HttpPost("refresh-token")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuthenticationDTO>> RefreshToken([FromBody] RefreshTokenDTO requestDto)
    {
        _validator.ValidateRefreshTokenDTO(requestDto);

        var responseDto = await _authService.RefreshToken(requestDto.Token);

        return ApiCreated(responseDto);
    }

    [HttpPut("users/passwords")]
    [Authorize(Roles = AuthRole.AnyRole)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status400BadRequest)]
    public async Task<NoContentResult> ChangePassword([FromBody] ChangePasswordDTO dto)
    {
        _validator.ValidateChangePasswordDTO(dto);

        var user = await GetCurrentUser();

        await _authService.ChangePassword(dto, user);

        return NoContent();
    }

    [HttpPost("logout")]
    [Authorize(Roles = AuthRole.AnyRole)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<NoContentResult> Logout()
    {
        var user = await GetCurrentUser();

        if (user.RoleId == RoleId.Specialist)
        {
            var specialist = user.Specialist;
            
            specialist.SpecialistStatusId = SpecialistStatusId.Offline;
            await _specialistsRepository.UpdateAsync(specialist.Id, specialist);
        }
        
        return NoContent();
    }
}
