using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Saitynas_API.Models.DTO;
using Saitynas_API.Models.Entities.Role;
using Saitynas_API.Models.Entities.Specialist;

namespace Saitynas_API.Controllers;

[Route($"{RoutePrefix}/[Controller]")]
[ApiController]
[Produces(ApiContentType)]
public class ActivityStatusesController : ApiControllerBase
{
    protected override string ModelName => "SpecialistActivityStatus";

    [HttpGet]
    [Authorize(Roles = AuthRole.AnyRole)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<GetListDTO<GetEnumDTO>> GetActivityStatuses()
    {
        var statuses = Enum.GetValues(typeof(SpecialistStatusId))
            .Cast<SpecialistStatusId>()
            .Select(ss => new GetEnumDTO
            {
                Id = (int) ss,
                Name = ss.ToString()
            })
            .ToList();

        var dto = new GetListDTO<GetEnumDTO>(statuses);

        return Ok(dto);
    }
}
