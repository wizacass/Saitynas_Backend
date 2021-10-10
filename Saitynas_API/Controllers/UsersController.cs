using Microsoft.AspNetCore.Mvc;
using Saitynas_API.Models;

namespace Saitynas_API.Controllers
{
    [Route(RoutePrefix + "/[Controller]")]
    [ApiController]
    [Produces(ApiContentType)]
    public class UsersController : ApiControllerBase
    {
        protected override string ModelName => "user";

        public UsersController(ApiContext context) : base(context) { }
    }
}
