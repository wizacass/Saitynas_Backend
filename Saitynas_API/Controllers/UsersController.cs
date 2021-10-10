using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Saitynas_API.Models;
using Saitynas_API.Models.Common;
using Saitynas_API.Models.DTO.Common;
using Saitynas_API.Models.UserEntity;
using Saitynas_API.Models.UserEntity.DTO;

namespace Saitynas_API.Controllers
{
    [Route(RoutePrefix + "/[Controller]")]
    [ApiController]
    [Produces(ApiContentType)]
    public class UsersController : ApiControllerBase
    {
        protected override string ModelName => "user";

        private readonly UserManager<User> _userManager;

        public UsersController(ApiContext context, UserManager<User> userManager) : base(context)
        {
            _userManager = userManager;
        }

        [HttpGet("me")]
        [Authorize(Roles = AllRoles)]
        public async Task<IActionResult> GetUser()
        {
            var user = await GetCurrentUser();
            
            if (user == null) return ApiNotFound(ApiErrorSlug.ResourceNotFound, ModelName);

            var dto = new UserDTO(user);
            return Ok(new GetObjectDTO<UserDTO>(dto));
        }
        
        private async Task<User> GetCurrentUser()
        {
            string email = User.FindFirst(ClaimTypes.Email)?.Value;

            return await _userManager.FindByEmailAsync(email);
        }
    }
}
