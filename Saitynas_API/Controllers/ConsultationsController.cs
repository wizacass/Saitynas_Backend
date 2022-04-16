using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Saitynas_API.Models.Common;
using Saitynas_API.Models.DTO;
using Saitynas_API.Models.Entities.Consultation;
using Saitynas_API.Models.Entities.Consultation.DTO;
using Saitynas_API.Models.Entities.Role;
using Saitynas_API.Models.Entities.User;
using Saitynas_API.Services;

namespace Saitynas_API.Controllers;

[Route($"{RoutePrefix}/[Controller]")]
[ApiController]
[Produces(ApiContentType)]
public class ConsultationsController : ApiControllerBase
{
    protected override string ModelName => nameof(Consultation);

    private readonly IConsultationsService _consultationsService;

    public ConsultationsController(
        IConsultationsService consultationsService,
        UserManager<User> userManager
    ) : base(userManager)
    {
        _consultationsService = consultationsService;
    }

    [HttpPost]
    [Authorize(Roles = AuthRole.Patient)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<GetObjectDTO<IdDTO>>> RequestConsultation(RequestConsultationDTO dto)
    {
        var user = await GetCurrentUser();

        if (user.PatientId == null)
        {
            return ApiNotFound(ApiErrorSlug.ResourceNotFound, "patient");
        }
            
        var consultation = await _consultationsService.RequestConsultation((int) user.PatientId, dto.DeviceToken, dto.SpecialityId);
        var responseDto = new IdDTO { Id = consultation.Id };
        
        return ApiCreated(new GetObjectDTO<IdDTO>(responseDto));
    }
    
    [HttpPost("cancel")]
    [Authorize(Roles = AuthRole.Patient)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CancelConsultation(ConsultationDTO dto)
    {
        await _consultationsService.CancelConsultation(dto.ConsultationId, dto.DeviceToken);
        return NoContent();
    }
    
    [HttpPost("end")]
    [Authorize(Roles = AuthRole.Patient)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> EndConsultation(ConsultationDTO dto)
    {
        await _consultationsService.EndConsultation(dto.ConsultationId, dto.DeviceToken);
        return NoContent();
    }
    
    [HttpPost("start")]
    [Authorize(Roles = AuthRole.Patient)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> StartConsultation(ConsultationDTO dto)
    {
        await _consultationsService.StartConsultation(dto.ConsultationId, dto.DeviceToken);
        return NoContent();
    }
}
