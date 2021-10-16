using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Saitynas_API.Models;
using Saitynas_API.Models.DTO.Common;
using Saitynas_API.Models.EvaluationEntity;
using Saitynas_API.Models.EvaluationEntity.DTO;
using Saitynas_API.Models.EvaluationEntity.Repository;

namespace Saitynas_API.Controllers
{
    [Route(RoutePrefix + "/[Controller]")]
    [ApiController]
    [Produces(ApiContentType)]
    public class EvaluationsController : ApiControllerBase
    {
        protected override string ModelName => "evaluation";

        private readonly IEvaluationsRepository _repository;

        public EvaluationsController(ApiContext context, IEvaluationsRepository repository) : base(context)
        {
            _repository = repository;
        }
        
        [Obsolete]
        [HttpGet]
        public async Task<ActionResult<GetListDTO<GetEvaluationDTO>>> GetEvaluations()
        {
            var evaluations = (await _repository.GetAllAsync())
                .Select(e => new GetEvaluationDTO(e));

            return Ok(new GetListDTO<GetEvaluationDTO>(evaluations));
        }

        [Obsolete]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetObjectDTO<GetEvaluationDTO>>> GetEvaluation(int id)
        {
            var evaluation = await _repository.GetAsync(id);
            
            if (evaluation == null) return ApiNotFound();
            
            var dto = new GetObjectDTO<GetEvaluationDTO>(new GetEvaluationDTO(evaluation));

            return Ok(dto);
        }

        [Obsolete]
        [HttpPost]
        public ActionResult<GetObjectDTO<GetEvaluationDTO>> CreateEvaluation([FromBody] EvaluationDTO dto)
        {
            var evaluation = new Evaluation(dto);

            _repository.InsertAsync(evaluation);

            return ApiCreated(new GetObjectDTO<GetEvaluationDTO>(new GetEvaluationDTO(evaluation)));
        }
        
        [Obsolete]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<GetObjectDTO<GetEvaluationDTO>>> EditEvaluation(int id, [FromBody] EvaluationDTO dto)
        {
            var evaluation = new Evaluation(id, dto);
            
            await _repository.UpdateAsync(id, evaluation);

            return Ok(new GetObjectDTO<GetEvaluationDTO>(new GetEvaluationDTO(evaluation)));
        }

        [Obsolete]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEvaluation(int id)
        {
            await _repository.DeleteAsync(id);
                
            return NoContent();
        }
    }
}
