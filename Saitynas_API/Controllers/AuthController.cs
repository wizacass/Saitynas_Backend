using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saitynas_API.Models;
using Saitynas_API.Models.Authentication.DTO;
using Saitynas_API.Models.Authentication.DTO.Validator;
using Saitynas_API.Services.AuthenticationService;

namespace Saitynas_API.Controllers
{
    [Route(RoutePrefix)]
    [ApiController]
    [Produces(ApiContentType)]
    public class AuthController : ApiControllerBase
    {
        protected override string ModelName => "user";

        private readonly ISignupDTOValidator _validator;
        private readonly IAuthenticationService _authService;

        public AuthController(
            ApiContext context,
            ISignupDTOValidator validator,
            IAuthenticationService authService
        ) : base(context)
        {
            _validator = validator;
            _authService = authService;
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthenticationDTO>> Signup([FromBody] SignupDTO requestDto)
        {
            _validator.ValidateSignupDTO(requestDto);

            var responseDto = await _authService.Signup(requestDto);

            return ApiCreated(responseDto);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthenticationDTO>> Login([FromBody] LoginDTO requestDto)
        {
            var responseDto = await _authService.Login(requestDto);

            return ApiCreated(responseDto);
        }
    }
}
