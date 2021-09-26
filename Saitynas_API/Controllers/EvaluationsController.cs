using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Saitynas_API.Models;
using Saitynas_API.Models.EvaluationEntity.DTO;
using Saitynas_API.Models.MessageEntity.DTO;

namespace Saitynas_API.Controllers
{
    [Route(RoutePrefix + "/[Controller]")]
    [ApiController]
    [Produces(ApiContentType)]
    public class EvaluationsController : ApiControllerBase
    {
        protected override string ModelName => "evaluation";
        
        public EvaluationsController(ApiContext context) : base(context) { }
        
        [HttpGet]
        public ActionResult<GetListDTO<GetEvaluationDTO>> GetEvaluations()
        {
            var evaluations = new List<GetEvaluationDTO>
            {
                GetEvaluationDTO.Mocked,
                new()
                {
                    Id = 2,
                    Evaluation = 3,
                    Comment = null
                }
            };

            var dto = new GetListDTO<GetEvaluationDTO>(evaluations);

            return Ok(dto);
        }

        [HttpGet("{id:int}")]
        public ActionResult<GetObjectDTO<GetEvaluationDTO>> GetEvaluation(int id)
        {
            if (id != 1) return ApiNotFound();
            
            var dto = new GetObjectDTO<GetEvaluationDTO>(GetEvaluationDTO.Mocked);

            return Ok(dto);
        }

        [HttpPost]
        public ActionResult<GetObjectDTO<GetEvaluationDTO>> CreateEvaluation([FromBody] EvaluationDTO dto)
        {
            var evaluation = new GetEvaluationDTO
            {
                Id = 10,
                Evaluation = dto.Evaluation,
                Comment = dto.Comment
            };

            return ApiCreated(new GetObjectDTO<GetEvaluationDTO>(evaluation));
        }
        
        [HttpPut("{id:int}")]
        public ActionResult<GetObjectDTO<GetEvaluationDTO>> EditEvaluation(int id, [FromBody] EditEvaluationDTO dto)
        {
            var evaluation = GetEvaluationDTO.Mocked;
            evaluation.Evaluation = (dto.Evaluation ?? evaluation.Evaluation);
            evaluation.Comment = (dto.Comment ?? evaluation.Comment);

            return Ok(new GetObjectDTO<GetEvaluationDTO>(evaluation));
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteEvaluation(int id)
        {
            return NoContent();
        }
    }
}
