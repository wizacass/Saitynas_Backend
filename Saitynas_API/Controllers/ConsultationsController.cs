using System.Threading.Tasks;
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

    private readonly IApplePushNotificationService _apnService;

    public ConsultationsController(IApplePushNotificationService apnService)
    {
        _apnService = apnService;
    }

    [HttpPost]
    [Authorize(Roles = AuthRole.Patient)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RequestConsultation(RequestConsultationDTO dto)
    {
        await _apnService.PublishNotification(dto.DeviceToken, "You have a new message!");

        return NoContent();
    }
}
