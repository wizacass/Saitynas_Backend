using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Saitynas_API.Models;
using Saitynas_API.Models.DTO;
using Saitynas_API.Models.SpecialistEntity;
using Saitynas_API.Models.SpecialistEntity.DTO;
using Saitynas_API.Models.VisitEntity.DTO;
using Saitynas_API.Repositories;
using Saitynas_API.Services.Validators;

namespace Saitynas_API.Controllers
{
    [Route(RoutePrefix + "/[Controller]")]
    [ApiController]
    [Produces(ApiContentType)]
    public class SpecialistsController : ApiControllerBase
    {
        protected override string ModelName => "specialist";

        private readonly ISpecialistsRepository _repository;
        private readonly ISpecialistDTOValidator _validator;

        public SpecialistsController(
            ApiContext context,
            ISpecialistsRepository repository,
            ISpecialistDTOValidator validator
        ) : base(context)
        {
            _repository = repository;
            _validator = validator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Patient")]
        public async Task<ActionResult<GetListDTO<GetSpecialistDTO>>> GetSpecialists()
        {
            var specialists = (await _repository.GetAllAsync())
                .Select(w => new GetSpecialistDTO(w));

            var dto = new GetListDTO<GetSpecialistDTO>(specialists);

            return Ok(dto);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin,Patient")]
        public async Task<ActionResult<GetObjectDTO<GetSpecialistDTO>>> GetSpecialist(int id)
        {
            var specialist = await _repository.GetAsync(id);

            if (specialist == null) return ApiNotFound();

            var dto = new GetSpecialistDTO(specialist);
            return Ok(dto);
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
            await _repository.InsertAsync(specialist);

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
            await _repository.UpdateAsync(id, new Specialist(id, dto));

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSpecialist(int id)
        {
            await _repository.DeleteAsync(id);

            return NoContent();
        }
    }
}
