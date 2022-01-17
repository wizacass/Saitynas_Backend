using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Saitynas_API.Models;
using Saitynas_API.Models.Entities.Message.DTO;

namespace Saitynas_API.Controllers;

[Route(RoutePrefix)]
[ApiController]
[Produces(ApiContentType)]
public class WelcomeController : ApiControllerBase
{
    private readonly ApiContext _context;
    protected override string ModelName => "message";

    public WelcomeController(ApiContext context)
    {
        _context = context;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<MessageDTO>> GetMessage()
    {
        var message = await _context.Messages.FirstOrDefaultAsync(m => m.Id == 1);
        var dto = new MessageDTO(message?.Text);

        return Ok(dto);
    }
}
