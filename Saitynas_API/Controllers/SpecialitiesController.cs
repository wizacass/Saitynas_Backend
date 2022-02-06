using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Saitynas_API.Models;
using Saitynas_API.Models.DTO;
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
    public async Task<ActionResult<GetListDTO<GetSpecialityDTO>>> GetSpecialities()
    {
        var specialitiesTask = _context.Specialities.Select(s => new GetSpecialityDTO(s)).ToListAsync();
        
        var dto = new GetListDTO<GetSpecialityDTO>(await specialitiesTask);
        
        return Ok(dto);
    }

   
    [HttpGet("{id:int}/specialists")]
    [Obsolete]
    public ActionResult<GetListDTO<GetSpecialistDTO>> GetSpecialitySpecialists(int id)
    {
        // TODO: Implement logic to retrieve specialists based on Speciality
        return ApiNotImplemented();
    }

    [HttpPost]
    [Obsolete]
    public ActionResult<GetObjectDTO<GetEnumDTO>> CreateSpeciality([FromBody] EnumDTO dto)
    {
        // TODO: Implement logic to allow for specialists to create new specialities
        return ApiNotImplemented();
    }
}
