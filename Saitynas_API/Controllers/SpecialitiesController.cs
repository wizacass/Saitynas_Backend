using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Saitynas_API.Models;
using Saitynas_API.Models.DTO;
using Saitynas_API.Models.Entities.Role;
using Saitynas_API.Models.Entities.Specialist.DTO;
using Saitynas_API.Models.Entities.Speciality;
using Saitynas_API.Models.Entities.Speciality.DTO;

namespace Saitynas_API.Controllers;

[Route($"{RoutePrefix}/[Controller]")]
[ApiController]
[Produces(ApiContentType)]
public class SpecialitiesController : ApiControllerBase
{
    protected override string ModelName => "speciality";

    private readonly ApiContext _context;

    public SpecialitiesController(ApiContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Authorize(Roles = AuthRole.AnyRole)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<GetListDTO<GetSpecialityDTO>>> GetSpecialities()
    {
        var specialitiesTask = _context.Specialities
            .OrderBy(s => s.Name)
            .Select(s => new GetSpecialityDTO(s))
            .ToListAsync();

        var dto = new GetListDTO<GetSpecialityDTO>(await specialitiesTask);

        return Ok(dto);
    }
}
