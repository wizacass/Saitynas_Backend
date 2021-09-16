using Microsoft.AspNetCore.Mvc;
using Saitynas_API.Models;
using Saitynas_API.Models.DTO.Common;

namespace Saitynas_API.Controllers
{
    [Route(RoutePrefix)]
    [ApiController]
    [Produces(ApiContentType)]
    public class WelcomeController : ApiControllerBase
    {
        public WelcomeController(ApiContext context) : base(context) { }

        [HttpGet]
        public ActionResult<MessageDTO> GetMessage()
        {
            var message = new MessageDTO("Hello world!");
            return Ok(message);
        }
    }
}
