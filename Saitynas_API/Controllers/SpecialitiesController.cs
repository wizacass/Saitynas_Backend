using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Saitynas_API.Models;
using Saitynas_API.Models.DTO.Common;
using Saitynas_API.Models.SpecialistEntity.DTO;
using Saitynas_API.Models.SpecialityEntity;

namespace Saitynas_API.Controllers
{
    [Route(RoutePrefix + "/[Controller]")]
    [ApiController]
    [Produces(ApiContentType)]
    [Obsolete]
    public class SpecialitiesController : ApiControllerBase
    {
        protected override string ModelName => "speciality";

        public SpecialitiesController(ApiContext context) : base(context) { }
        
        [HttpGet]
        public ActionResult<GetListDTO<GetEnumDTO>> GetSpecialities()
        {
            var specialities = Enum.GetValues(typeof(SpecialityId))
                .Cast<SpecialityId>()
                .Select(s => new GetEnumDTO
                {
                    Id = (int)s,
                    Name = s.ToString()
                })
                .ToList();

            var dto = new GetListDTO<GetEnumDTO>(specialities);

            return Ok(dto);
        }

        [HttpGet("{id:int}")]
        public ActionResult<GetObjectDTO<EnumDTO>> GetSpeciality(int id)
        {
            if (id != 1) return ApiNotFound();
            
            var dto = new EnumDTO
            {
                Name = SpecialityId.Allergologist.ToString()
            };

            return Ok(dto);
        }
        
        [HttpGet("{id:int}/specialists")]
        public ActionResult<GetListDTO<GetSpecialistDTO>> GetSpecialitySpecialists(int id)
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
        public ActionResult<GetObjectDTO<GetEnumDTO>> CreateSpeciality([FromBody] EnumDTO dto)
        {
            var speciality = new GetEnumDTO
            {
                Id = 100,
                Name = dto.Name
            };

            return ApiCreated(new GetObjectDTO<GetEnumDTO>(speciality));
        }
        
        [HttpPut("{id:int}")]
        public ActionResult<GetObjectDTO<GetEnumDTO>> EditEvaluation(int id, [FromBody] EnumDTO dto)
        {
            var speciality = new GetEnumDTO
            {
                Id = id,
                Name = dto.Name ?? "Other"
            };

            return Ok(new GetObjectDTO<GetEnumDTO>(speciality));
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteSpeciality(int id)
        {
            return NoContent();
        }
    }
}
