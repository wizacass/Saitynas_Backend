using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saitynas_API.Models.DTO;
using Saitynas_API.Models.Entities.Role;
using Saitynas_API.Models.Entities.Specialist.DTO;
using Saitynas_API.Models.Entities.Workplace;
using Saitynas_API.Models.Entities.Workplace.DTO;
using Saitynas_API.Repositories;
using Saitynas_API.Services.Validators;

namespace Saitynas_API.Controllers;

[Route($"{RoutePrefix}/[Controller]")]
[ApiController]
[Produces(ApiContentType)]
public class WorkplacesController : ApiControllerBase
{
    private readonly ISpecialistsRepository _specialistsRepository;
    private readonly IWorkplaceDTOValidator _validator;

    private readonly IWorkplacesRepository _workplacesRepository;
    protected override string ModelName => "workplace";

    public WorkplacesController(
        IWorkplacesRepository workplacesRepository,
        ISpecialistsRepository specialistsRepository,
        IWorkplaceDTOValidator validator)
    {
        _workplacesRepository = workplacesRepository;
        _specialistsRepository = specialistsRepository;
        _validator = validator;
    }

    [HttpGet]
    [Authorize(Roles = AuthRole.AnyRole)]
    public async Task<ActionResult<GetListDTO<GetWorkplaceDTO>>> GetWorkplaces()
    {
        var workplaces = (await _workplacesRepository.GetAllAsync())
            .Select(w => new GetWorkplaceDTO(w));

        var dto = new GetListDTO<GetWorkplaceDTO>(workplaces);
        return Ok(dto);
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = AuthRole.AnyRole)]
    public async Task<ActionResult<GetObjectDTO<GetWorkplaceDTO>>> GetWorkplace(int id)
    {
        var workplace = await _workplacesRepository.GetAsync(id);

        if (workplace == null) return ApiNotFound();

        var dto = new GetWorkplaceDTO(workplace);
        return Ok(new GetObjectDTO<GetWorkplaceDTO>(dto));
    }

    [HttpGet("{id:int}/specialists")]
    [Authorize(Roles = AuthRole.AnyRole)]
    public async Task<ActionResult<GetListDTO<GetSpecialistDTO>>> GetWorkplaceSpecialists(int id)
    {
        var specialists = (await _specialistsRepository.GetByWorkplace(id))
            .Select(s => new GetSpecialistDTO(s));

        var dto = new GetListDTO<GetSpecialistDTO>(specialists);

        return Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = "Admin, Specialist")]
    public async Task<IActionResult> CreateWorkplace(
        [FromBody] CreateWorkplaceDTO dto
    )
    {
        _validator.ValidateCreateWorkplaceDTO(dto);
        await _workplacesRepository.InsertAsync(new Workplace(dto));

        return NoContent();
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> EditWorkplace(
        int id,
        [FromBody] EditWorkplaceDTO dto
    )
    {
        _validator.ValidateEditWorkplaceDTO(dto);
        await _workplacesRepository.UpdateAsync(id, new Workplace(id, dto));

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteWorkplace(int id)
    {
        await _workplacesRepository.DeleteAsync(id);

        return NoContent();
    }
}
