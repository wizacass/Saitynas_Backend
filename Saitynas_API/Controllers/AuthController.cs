using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Saitynas_API.Models;
using Saitynas_API.Models.Authentication;
using Saitynas_API.Models.Authentication.DTO;
using Saitynas_API.Models.Authentication.DTO.Validator;
using Saitynas_API.Models.Common;
using Saitynas_API.Models.UserEntity;
using Saitynas_API.Services.AuthenticationService;
using Saitynas_API.Services.JwtService;

namespace Saitynas_API.Controllers
{
    [Route(RoutePrefix)]
    [ApiController]
    [Produces(ApiContentType)]
    public class AuthController : ApiControllerBase
    {
        protected override string ModelName => "user";

        private readonly ISignupDTOValidator _validator;
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationService _authService;
        private readonly IJwtService _jwt;

        public AuthController(ApiContext context, ISignupDTOValidator validator, UserManager<User> userManager, IAuthenticationService authService, IJwtService jwt) : base(context)
        {
            _validator = validator;
            _userManager = userManager;
            _authService = authService;
            _jwt = jwt;
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
        public async Task<ActionResult<object>> Login([FromBody] LoginDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null) return ApiBadRequest(ApiErrorSlug.InvalidCredentials);
            if (!await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                return ApiBadRequest(ApiErrorSlug.InvalidCredentials);
            }

            string token = GenerateAccessToken(user);
            return Ok(new
            {
                jwt = token
            });
        }

        private string GenerateAccessToken(User user)
        {
            return _jwt.GenerateSecurityToken(new JwtUser(user.Email, user.RoleId));
        }
    }
}
