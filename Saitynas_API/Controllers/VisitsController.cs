using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Saitynas_API.Models.DTO;
using Saitynas_API.Models.Entities.Visit.DTO;

namespace Saitynas_API.Controllers;

[Route($"{RoutePrefix}/[Controller]")]
[ApiController]
[Produces(ApiContentType)]
[Obsolete]
public class VisitsController : ApiControllerBase
{
    protected override string ModelName => "visit";

    [HttpGet]
    public ActionResult<GetListDTO<GetVisitDTO>> GetVisits()
    {
        var evaluations = new List<GetVisitDTO>
        {
            new()
            {
                Id = 1,
                SpecialistName = "Good Doctor",
                PatientName = "John Doe",
                VisitStart = DateTime.Now.ToString("O"),
                VisitEnd = DateTime.Now.AddHours(1).ToString("O")
            },
            new()
            {
                Id = 2,
                SpecialistName = "Good Doctor",
                PatientName = "John Doe",
                VisitStart = DateTime.Now.ToString("O"),
                VisitEnd = DateTime.Now.AddHours(1).ToString("O")
            }
        };

        var dto = new GetListDTO<GetVisitDTO>(evaluations);

        return Ok(dto);
    }

    [HttpGet("{id:int}")]
    public ActionResult<GetObjectDTO<GetVisitDTO>> GetVisit(int id)
    {
        if (id != 1) return ApiNotFound();

        var dto = new GetVisitDTO
        {
            Id = 1,
            SpecialistName = "Good Doctor",
            PatientName = "John Doe",
            VisitStart = DateTime.Now.ToString("O"),
            VisitEnd = DateTime.Now.AddHours(1).ToString("O")
        };

        return Ok(dto);
    }

    [HttpPost]
    public ActionResult<GetObjectDTO<GetVisitDTO>> CreateVisit([FromBody] VisitDTO dto)
    {
        var evaluation = new GetVisitDTO
        {
            Id = 10,
            SpecialistName = dto.SpecialistName,
            PatientName = dto.PatientName,
            VisitStart = dto.VisitStart,
            VisitEnd = dto.VisitEnd
        };

        return ApiCreated(new GetObjectDTO<GetVisitDTO>(evaluation));
    }

    [HttpPut("{id:int}")]
    public ActionResult<GetObjectDTO<VisitDTO>> EditVisit(
        int id,
        [FromBody] VisitDTO dto
    )
    {
        var evaluation = new GetVisitDTO
        {
            Id = 10,
            SpecialistName = dto.SpecialistName,
            PatientName = dto.PatientName,
            VisitStart = dto.VisitStart,
            VisitEnd = dto.VisitEnd
        };

        return Ok(new GetObjectDTO<GetVisitDTO>(evaluation));
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteVisit(int id)
    {
        return NoContent();
    }
}
