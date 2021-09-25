using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Saitynas_API.Models;
using Saitynas_API.Models.Messages.DTO;

namespace Saitynas_API.Controllers
{
    [Route(RoutePrefix)]
    [ApiController]
    [Produces(ApiContentType)]
    public class WelcomeController : ApiControllerBase
    {
        protected override string ModelName => "message";

        public WelcomeController(ApiContext context) : base(context) { }

        [HttpGet]
        public async Task<ActionResult<MessageDTO>> GetMessage()
        {
            var message = await Context.Messages.FirstOrDefaultAsync(m => m.Id == 1);
            var dto = new MessageDTO(message.Text);

            return Ok(dto);
        }
    }
}
