using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Saitynas_API.Models.Common;
using Saitynas_API.Models.DTO;
using Saitynas_API.Models.Entities.User;

namespace Saitynas_API.Controllers;

public abstract class ApiControllerBase : ControllerBase
{
    protected const string ApiContentType = "application/json";
    protected const string RoutePrefix = "api/v1";

    private readonly UserManager<User> _userManager;

    protected abstract string ModelName { get; }

    protected ApiControllerBase() { }

    protected ApiControllerBase(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    protected async Task<User> GetCurrentUser()
    {
        string email = User.FindFirst(ClaimTypes.Email)?.Value;
        var user = await _userManager.FindByEmailAsync(email);

        return user;
    }

    protected ActionResult ApiCreated(object data)
    {
        return StatusCode(201, data);
    }

    protected BadRequestObjectResult ApiBadRequest(string message, string details = null)
    {
        var error = new ErrorDTO
        {
            Type = 400,
            Title = message,
            Details = details
        };

        return BadRequest(error);
    }

    protected NotFoundObjectResult ApiNotFound()
    {
        var error = new ErrorDTO
        {
            Type = 404,
            Title = ApiErrorSlug.ResourceNotFound,
            Details = ModelName
        };

        return NotFound(error);
    }

    protected NotFoundObjectResult ApiNotFound(string title, string details = null)
    {
        var error = new ErrorDTO
        {
            Type = 404,
            Title = title,
            Details = details
        };

        return NotFound(error);
    }

    protected ActionResult ApiNotImplemented()
    {
        return StatusCode(501);
    }
}
