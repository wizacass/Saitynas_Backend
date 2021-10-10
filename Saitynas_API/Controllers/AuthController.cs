using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Saitynas_API.Models;
using Saitynas_API.Models.Authentication;
using Saitynas_API.Models.Authentication.DTO;
using Saitynas_API.Models.Common;
using Saitynas_API.Models.UserEntity;
using Saitynas_API.Services.JwtService;

namespace Saitynas_API.Controllers
{
    [Route(RoutePrefix)]
    [ApiController]
    [Produces(ApiContentType)]
    public class AuthController : ApiControllerBase
    {
        protected override string ModelName => "user";

        private readonly UserManager<User> _userManager;
        private readonly IJwtService _jwt;

        public AuthController(ApiContext context, UserManager<User> userManager, IJwtService jwt) : base(context)
        {
            _userManager = userManager;
            _jwt = jwt;
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<ActionResult<object>> Signup([FromBody] SignupDTO dto)
        {
            var user = new User(dto);
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                return ApiBadRequest(
                    ApiErrorSlug.AuthenticationError,
                    result.Errors.First().Description
                );
            }

            string token = GenerateToken(user);
            return ApiCreated(new
            {
                jwt = token
            });
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

            string token = GenerateToken(user);
            return Ok(new
            {
                jwt = token
            });
        }

        private string GenerateToken(User user)
        {
            return _jwt.GenerateSecurityToken(new JwtUser(user.Email, user.RoleId));
        }
    }
}
