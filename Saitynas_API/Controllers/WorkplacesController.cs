using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saitynas_API.Models.DTO;
using Saitynas_API.Models.SpecialistEntity.DTO;
using Saitynas_API.Models.WorkplaceEntity;
using Saitynas_API.Models.WorkplaceEntity.DTO;
using Saitynas_API.Repositories;
using Saitynas_API.Services.Validators;

namespace Saitynas_API.Controllers
{
    [Route(RoutePrefix + "/[Controller]")]
    [ApiController]
    [Produces(ApiContentType)]
    public class WorkplacesController : ApiControllerBase
    {
        protected override string ModelName => "workplace";

        private readonly IWorkplacesRepository _repository;
        private readonly IWorkplaceDTOValidator _validator;

        public WorkplacesController(IWorkplacesRepository repository, IWorkplaceDTOValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        [HttpGet]
        [Authorize(Roles = AllRoles)]
        public async Task<ActionResult<GetListDTO<GetWorkplaceDTO>>> GetWorkplaces()
        {
            var workplaces = (await _repository.GetAllAsync())
                .Select(w => new GetWorkplaceDTO(w));

            var dto = new GetListDTO<GetWorkplaceDTO>(workplaces);
            return Ok(dto);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = AllRoles)]
        public async Task<ActionResult<GetObjectDTO<GetWorkplaceDTO>>> GetWorkplace(int id)
        {
            var workplace = await _repository.GetAsync(id);

            if (workplace == null) return ApiNotFound();

            var dto = new GetWorkplaceDTO(workplace);
            return Ok(new GetObjectDTO<GetWorkplaceDTO>(dto));
        }

        [HttpGet("{id:int}/specialists")]
        [Authorize(Roles = AllRoles)]
        [Obsolete]
        public async Task<ActionResult<GetListDTO<GetSpecialistDTO>>> GetWorkplaceSpecialists(int id)
        {
            // var specialists = await Context.Specialists
            //     .Where(s => s.WorkplaceId == id)
            //     .Include(s => s.Speciality)
            //     .Include(s => s.Workplace)
            //     .Select(s => new GetSpecialistDTO(s))
            //     .ToListAsync();
            //
            // var dto = new GetListDTO<GetSpecialistDTO>(specialists);
            // return Ok(dto);

            return StatusCode(500);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Specialist")]
        public async Task<IActionResult> CreateWorkplace(
            [FromBody] CreateWorkplaceDTO dto
        )
        {
            _validator.ValidateCreateWorkplaceDTO(dto);
            await _repository.InsertAsync(new Workplace(dto));

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
            await _repository.UpdateAsync(id, new Workplace(id, dto));

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteWorkplace(int id)
        {
            await _repository.DeleteAsync(id);

            return NoContent();
        }
    }
}
