using Microsoft.AspNetCore.Mvc;
using Saitynas_API.Models;
using Saitynas_API.Models.Common;
using Saitynas_API.Models.DTO.Common;

namespace Saitynas_API.Controllers
{
    public abstract class ApiControllerBase : ControllerBase
    {
        protected const string ApiContentType = "application/json";
        protected const string RoutePrefix = "api/v1";

        protected abstract string ModelName { get; }

        protected ApiContext Context { get; }

        protected ApiControllerBase(ApiContext context)
        {
            Context = context;
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
    }
}
