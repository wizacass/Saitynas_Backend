using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Saitynas_API.Models;
using Saitynas_API.Models.Database;
using Saitynas_API.Models.DTO.Common;
using Saitynas_API.Models.SpecialistEntity.DTO;
using Saitynas_API.Models.VisitEntity.DTO;

namespace Saitynas_API.Controllers
{
    [Route(RoutePrefix + "/[Controller]")]
    [ApiController]
    [Produces(ApiContentType)]
    public class SpecialistsController : ApiControllerBase
    {
        protected override string ModelName => "specialist";

        public SpecialistsController(ApiContext context) : base(context) { }

        [HttpGet]
        public ActionResult<GetListDTO<GetSpecialistDTO>> GetSpecialists()
        {
            var specialists = new List<GetSpecialistDTO>
            {
                new()
                {
                    Id = 1,
                    FirstName = "Test",
                    LastName = "Doctor",
                    Address = "Test str. 22",
                    Speciality = SpecialityId.GeneralPractician.ToString()
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
        
        [HttpGet("{id:int}")]
        public ActionResult<GetObjectDTO<GetSpecialistDTO>> GetSpecialist(int id)
        {
            if (id != 1) return ApiNotFound();
        
            var dto = new GetObjectDTO<GetSpecialistDTO>(new GetSpecialistDTO
            {
                Id = 1,
                FirstName = "Test",
                LastName = "Doctor",
                Address = "Test str. 22",
                Speciality = SpecialityId.GeneralPractician.ToString()
            });
        
            return Ok(dto);
        }
        
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
        public ActionResult<GetObjectDTO<GetSpecialistDTO>> CreateSpecialist([FromBody] SpecialistDTO dto)
        {
            var evaluation = new GetSpecialistDTO
            {
                Id = 10,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Address = dto.Address,
                Speciality = SpecialityId.Other.ToString()
            };
        
            return ApiCreated(new GetObjectDTO<GetSpecialistDTO>(evaluation));
        }
        
        [HttpPut("{id:int}")]
        public ActionResult<GetObjectDTO<SpecialistDTO>> EditSpecialist(
            int id,
            [FromBody] SpecialistDTO dto
        )
        {
            var evaluation = new GetSpecialistDTO
            {
                Id = id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Address = dto.Address,
                Speciality = SpecialityId.Other.ToString()
            };
        
            return Ok(new GetObjectDTO<SpecialistDTO>(evaluation));
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteSpecialist(int id)
        {
            return NoContent();
        }
    }
}
