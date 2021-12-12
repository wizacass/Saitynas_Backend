using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Saitynas_API.Models.DTO;
using Saitynas_API.Models.Entities.Evaluation;
using Saitynas_API.Models.Entities.Evaluation.DTO;
using Saitynas_API.Models.Entities.Role;
using Saitynas_API.Models.Entities.User;
using Saitynas_API.Repositories;
using Saitynas_API.Services.Validators;

namespace Saitynas_API.Controllers
{
    [Route(RoutePrefix + "/[Controller]")]
    [ApiController]
    [Produces(ApiContentType)]
    public class EvaluationsController : ApiControllerBase
    {
        protected override string ModelName => "evaluation";

        private readonly IEvaluationsRepository _repository;
        private readonly IEvaluationDTOValidator _validator;

        public EvaluationsController(
            IEvaluationsRepository repository,
            IEvaluationDTOValidator validator,
            UserManager<User> userManager
        ) : base(userManager)
        {
            _repository = repository;
            _validator = validator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GetListDTO<GetEvaluationDTO>>> GetEvaluations()
        {
            var evaluations = (await _repository.GetAllAsync())
                .Select(e => new GetEvaluationDTO(e));

            return Ok(new GetListDTO<GetEvaluationDTO>(evaluations));
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GetObjectDTO<GetEvaluationDTO>>> GetEvaluation(int id)
        {
            var evaluation = await _repository.GetAsync(id);

            if (evaluation == null) return ApiNotFound();

            var dto = new GetObjectDTO<GetEvaluationDTO>(new GetEvaluationDTO(evaluation));

            return Ok(dto);
        }

        [HttpPost]
        [Authorize(Roles = "Patient")]
        public async Task<NoContentResult> CreateEvaluation([FromBody] EvaluationDTO dto)
        {
            _validator.ValidateCreateEvaluationDTO(dto);

            var user = await GetCurrentUser();
            var evaluation = new Evaluation(user, dto);

            await _repository.InsertAsync(evaluation);

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<NoContentResult> EditEvaluation(int id, [FromBody] EditEvaluationDTO dto)
        {
            _validator.ValidateEditEvaluationDTO(dto);
            var data = new Evaluation(dto);

            await _repository.UpdateAsync(id, data);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Patient,Admin")]
        public async Task<IActionResult> DeleteEvaluation(int id)
        {
            var user = await GetCurrentUser();
            var evaluation = await _repository.GetAsync(id);

            if (!CanDelete(user, evaluation))
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);

            return NoContent();
        }

        private static bool CanDelete(User user, Evaluation evaluation)
        {
            if (user.RoleId == RoleId.Admin) return true;

            return user.RoleId == RoleId.Patient && user.Id == evaluation.UserId;
        }
    }
}
