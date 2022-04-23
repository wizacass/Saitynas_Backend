using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Saitynas_API.Models.DTO;
using Saitynas_API.Models.Entities.Evaluation;
using Saitynas_API.Models.Entities.Evaluation.DTO;
using Saitynas_API.Models.Entities.Role;
using Saitynas_API.Models.Entities.User;
using Saitynas_API.Repositories;
using Saitynas_API.Services.Validators;

namespace Saitynas_API.Controllers;

[Route($"{RoutePrefix}/[Controller]")]
[ApiController]
[Produces(ApiContentType)]
public class EvaluationsController : ApiControllerBase
{
    private readonly IEvaluationsRepository _evaluationsRepository;
    private readonly IConsultationsRepository _consultationsRepository;
    private readonly IEvaluationDTOValidator _validator;
    protected override string ModelName => "evaluation";

    public EvaluationsController(
        IEvaluationsRepository evaluationsRepository,
        IConsultationsRepository consultationsRepository,
        IEvaluationDTOValidator validator,
        UserManager<User> userManager
    ) : base(userManager)
    {
        _evaluationsRepository = evaluationsRepository;
        _consultationsRepository = consultationsRepository;
        _validator = validator;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<GetListDTO<GetEvaluationDTO>>> GetEvaluations()
    {
        var evaluations = (await _evaluationsRepository.GetAllAsync())
            .Select(e => new GetEvaluationDTO(e));

        return Ok(new GetListDTO<GetEvaluationDTO>(evaluations));
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<GetObjectDTO<GetEvaluationDTO>>> GetEvaluation(int id)
    {
        var evaluation = await _evaluationsRepository.GetAsync(id);

        if (evaluation == null) return ApiNotFound();

        var dto = new GetObjectDTO<GetEvaluationDTO>(new GetEvaluationDTO(evaluation));

        return Ok(dto);
    }

    [HttpPost("")]
    [Authorize(Roles = AuthRole.Patient)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status404NotFound)]
    public async Task<NoContentResult> CreateEvaluation([FromBody] EvaluationDTO dto)
    {
        _validator.ValidateCreateEvaluationDTO(dto);

        var user = await GetCurrentUser();
        var evaluation = new Evaluation(user, dto);

        if (dto.ConsultationId != null)
        {
            var consultation = await _consultationsRepository.FindByPublicID(new Guid(dto.ConsultationId));
            evaluation.ConsultationId = consultation.Id;
            evaluation.SpecialistId = consultation.SpecialistId;
        }
        
        await _evaluationsRepository.InsertAsync(evaluation);

        return NoContent();
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = AuthRole.Patient)]
    public async Task<IActionResult> EditEvaluation(int id, [FromBody] EditEvaluationDTO dto)
    {
        var user = await GetCurrentUser();
        var evaluation = await _evaluationsRepository.GetAsync(id);

        if (!CanDelete(user, evaluation)) return NotFound();

        _validator.ValidateEditEvaluationDTO(dto);
        var data = new Evaluation(dto);

        await _evaluationsRepository.UpdateAsync(id, data);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = AuthRole.Patient)]
    public async Task<IActionResult> DeleteEvaluation(int id)
    {
        var user = await GetCurrentUser();
        var evaluation = await _evaluationsRepository.GetAsync(id);

        if (!CanDelete(user, evaluation)) return NotFound();

        await _evaluationsRepository.DeleteAsync(id);

        return NoContent();
    }

    private static bool CanDelete(User user, Evaluation evaluation)
    {
        if (user.RoleId == RoleId.Admin) return true;

        return user.RoleId == RoleId.Patient && user.Id == evaluation.UserId;
    }
}
