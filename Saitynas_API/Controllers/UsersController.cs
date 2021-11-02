using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Saitynas_API.Models.Common;
using Saitynas_API.Models.DTO;
using Saitynas_API.Models.Entities.User;
using Saitynas_API.Models.Entities.User.DTO;

namespace Saitynas_API.Controllers
{
    [Route(RoutePrefix + "/[Controller]")]
    [ApiController]
    [Produces(ApiContentType)]
    public class UsersController : ApiControllerBase
    {
        protected override string ModelName => "user";

        public UsersController(UserManager<User> userManager) : base(userManager) { }

        [HttpGet("me")]
        [Authorize(Roles = AllRoles)]
        public async Task<ActionResult<GetObjectDTO<UserDTO>>> GetUser()
        {
            var user = await GetCurrentUser();

            if (user == null) return ApiNotFound(ApiErrorSlug.ResourceNotFound, ModelName);

            var dto = new UserDTO(user);
            return Ok(new GetObjectDTO<UserDTO>(dto));
        }
    }
}
