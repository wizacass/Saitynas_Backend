using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Saitynas_API.Models.DTO;
using Saitynas_API.Models.Entities.Role;
using Saitynas_API.Services;

namespace Saitynas_API.Controllers;

[Route($"{RoutePrefix}/[Controller]")]
[ApiController]
[Produces(ApiContentType)]
public class ConsultationsController : ApiControllerBase
{
    protected override string ModelName => "Consultation";

    private readonly IConsultationsService _consultationsService;

    public ConsultationsController(IConsultationsService consultationsService)
    {
        _consultationsService = consultationsService;
    }

    [HttpPost]
    [Authorize(Roles = AuthRole.Patient)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult RequestConsultation(RequestConsultationDTO dto)
    {
        _consultationsService.EnqueuePatient(dto.DeviceToken);    
        
        return NoContent();
    }
}
