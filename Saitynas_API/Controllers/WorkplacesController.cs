using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Saitynas_API.Models;
using Saitynas_API.Models.Messages.DTO;
using Saitynas_API.Models.Workplaces.DTO;

namespace Saitynas_API.Controllers
{
    [Route(RoutePrefix + "/[Controller]")]
    [ApiController]
    [Produces(ApiContentType)]
    public class WorkplacesController : ApiControllerBase
    {
        protected override string ModelName => "workplace";

        public WorkplacesController(ApiContext context) : base(context) { }

        [HttpGet]
        public ActionResult<GetListDTO<GetWorkplaceDTO>> GetWorkplaces()
        {
            var workplaces = new List<GetWorkplaceDTO>
            {
                GetWorkplaceDTO.Mocked,
                new()
                {
                    Id = 2,
                    Address = "Test str. 22",
                    City = "Vilnius"
                }
            };

            var dto = new GetListDTO<GetWorkplaceDTO>(workplaces);

            return Ok(dto);
        }

        [HttpGet("{id:int}")]
        public ActionResult<GetObjectDTO<GetWorkplaceDTO>> GetWorkplace(int id)
        {
            if (id != 1) return ApiNotFound();
            
            var dto = new GetObjectDTO<GetWorkplaceDTO>(GetWorkplaceDTO.Mocked);

            return Ok(dto);
        }

        [HttpPost]
        public ActionResult<GetObjectDTO<GetWorkplaceDTO>> CreateWorkplace([FromBody] WorkplaceDTO dto)
        {
            var workplace = new GetWorkplaceDTO
            {
                Id = 10,
                Address = dto.Address,
                City = dto.City
            };

            return ApiCreated(new GetObjectDTO<GetWorkplaceDTO>(workplace));
        }
        
        [HttpPut("{id:int}")]
        public ActionResult<GetObjectDTO<GetWorkplaceDTO>> EditWorkplace(int id, [FromBody] WorkplaceDTO dto)
        {
            var workplace = GetWorkplaceDTO.Mocked;
            workplace.Address = (dto.Address ?? workplace.Address);
            workplace.City = (dto.City ?? workplace.City);

            return Ok(new GetObjectDTO<GetWorkplaceDTO>(workplace));
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteWorkplace(int id)
        {
            return NoContent();
        }
    }
}
