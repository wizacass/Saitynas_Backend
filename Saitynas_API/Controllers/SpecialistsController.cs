using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saitynas_API.Models.DTO;
using Saitynas_API.Models.Entities.Evaluation.DTO;
using Saitynas_API.Models.Entities.Specialist;
using Saitynas_API.Models.Entities.Specialist.DTO;
using Saitynas_API.Models.Entities.Visit.DTO;
using Saitynas_API.Repositories;
using Saitynas_API.Services.Validators;

namespace Saitynas_API.Controllers;

[Route(RoutePrefix + "/[Controller]")]
[ApiController]
[Produces(ApiContentType)]
public class SpecialistsController : ApiControllerBase
{
    private readonly IEvaluationsRepository _evaluationsRepository;

    private readonly ISpecialistsRepository _specialistsRepository;
    private readonly ISpecialistDTOValidator _validator;
    protected override string ModelName => "specialist";

    public SpecialistsController(
        ISpecialistsRepository specialistsRepository,
        IEvaluationsRepository evaluationsRepository,
        ISpecialistDTOValidator validator
    )
    {
        _specialistsRepository = specialistsRepository;
        _evaluationsRepository = evaluationsRepository;
        _validator = validator;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Patient")]
    public async Task<ActionResult<GetListDTO<GetSpecialistDTO>>> GetSpecialists()
    {
        var specialists = (await _specialistsRepository.GetAllAsync())
            .Select(w => new GetSpecialistDTO(w));

        var dto = new GetListDTO<GetSpecialistDTO>(specialists);

        return Ok(dto);
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin,Patient")]
    public async Task<ActionResult<GetObjectDTO<GetSpecialistDTO>>> GetSpecialist(int id)
    {
        var specialist = await _specialistsRepository.GetAsync(id);

        if (specialist == null) return ApiNotFound();

        var dto = new GetSpecialistDTO(specialist);
        return Ok(new GetObjectDTO<GetSpecialistDTO>(dto));
    }

    [HttpGet("{id:int}/evaluations")]
    [Authorize(Roles = "Admin,Patient")]
    public async Task<ActionResult<GetListDTO<GetEvaluationDTO>>> GetSpecialistEvaluations(int id)
    {
        var evaluations = (await _evaluationsRepository.GetBySpecialistId(id))
            .Select(e => new GetEvaluationDTO(e));

        return Ok(new GetListDTO<GetEvaluationDTO>(evaluations));
    }

    [Obsolete("This is a mock implementation")]
    [HttpGet("{id:int}/visits")]
    public ActionResult<GetListDTO<GetVisitDTO>> GetSpecialistVisits(int id)
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

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateSpecialist([FromBody] CreateSpecialistDTO dto)
    {
        _validator.ValidateCreateSpecialistDTO(dto);
        var specialist = new Specialist(dto);
        await _specialistsRepository.InsertAsync(specialist);

        return NoContent();
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> EditSpecialist(
        int id,
        [FromBody] EditSpecialistDTO dto
    )
    {
        _validator.ValidateEditSpecialistDTO(dto);
        await _specialistsRepository.UpdateAsync(id, new Specialist(id, dto));

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteSpecialist(int id)
    {
        await _specialistsRepository.DeleteAsync(id);

        return NoContent();
    }
}