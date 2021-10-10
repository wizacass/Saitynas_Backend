using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saitynas_API.Exceptions;
using Saitynas_API.Models;
using Saitynas_API.Models.Common;
using Saitynas_API.Models.DTO.Common;
using Saitynas_API.Models.SpecialistEntity.DTO;
using Saitynas_API.Models.SpecialityEntity;
using Saitynas_API.Models.WorkplaceEntity;
using Saitynas_API.Models.WorkplaceEntity.DTO;
using Saitynas_API.Models.WorkplaceEntity.DTO.Validator;
using Saitynas_API.Models.WorkplaceEntity.Repository;

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

        public WorkplacesController(
            ApiContext context,
            IWorkplacesRepository repository,
            IWorkplaceDTOValidator validator
        ) : base(context)
        {
            _repository = repository;
            _validator = validator;
        }

        [HttpGet]
        [Authorize(Roles = AllRoles)]
        public async Task<ActionResult<GetListDTO<GetWorkplaceDTO>>> GetWorkplaces()
        {
            var workplaces = await _repository.GetAllAsync();

            var dto = new GetListDTO<GetWorkplaceDTO>(
                workplaces.Select(w => new GetWorkplaceDTO(w))
            );

            return Ok(dto);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = AllRoles)]
        public async Task<ActionResult<GetObjectDTO<GetWorkplaceDTO>>> GetWorkplace(int id)
        {
            var workplace = await _repository.GetAsync(id);

            if (workplace == null) return ApiNotFound(ApiErrorSlug.ResourceNotFound, ModelName);
            
            var dto = new GetWorkplaceDTO(workplace);
            return Ok(new GetObjectDTO<GetWorkplaceDTO>(dto));
        }

        [HttpGet("{id:int}/specialists")]
        [Authorize(Roles = AllRoles)]
        public ActionResult<GetListDTO<GetSpecialistDTO>> GetWorkplaceSpecialists(int id)
        {
            if (id != 1) return ApiNotFound();

            var specialists = new List<GetSpecialistDTO>
            {
                new()
                {
                    Id = 3,
                    FirstName = "Test",
                    LastName = "Doctor",
                    Address = "Test str. 22",
                    Speciality = SpecialityId.Other.ToString()
                },
                new()
                {
                    Id = 2,
                    FirstName = "Good",
                    LastName = "Doktor",
                    Address = "Test str. 22",
                    Speciality = SpecialityId.Other.ToString()
                }
            };

            var dto = new GetListDTO<GetSpecialistDTO>(specialists);

            return Ok(dto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Specialist")]
        public async Task<ActionResult<GetObjectDTO<GetWorkplaceDTO>>> CreateWorkplace([FromBody] CreateWorkplaceDTO dto)
        {
            try
            {
                _validator.ValidateCreateWorkplaceDTO(dto);
                await _repository.InsertAsync(new Workplace(dto));
                
                return NoContent();
            }
            catch (DTOValidationException ex)
            {
                return ApiBadRequest(ex.Message, ex.Parameter);
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GetObjectDTO<GetWorkplaceDTO>>> EditWorkplace(int id, [FromBody] EditWorkplaceDTO dto)
        {
            try
            {
                _validator.ValidateEditWorkplaceDTO(dto);
                await _repository.UpdateAsync(id, new Workplace(id, dto));
                
                return NoContent();
            }
            catch (DTOValidationException ex)
            {
                return ApiBadRequest(ex.Message, ex.Parameter);
            }
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
