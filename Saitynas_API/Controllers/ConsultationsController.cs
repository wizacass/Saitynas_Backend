using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Saitynas_API.Models.Common;
using Saitynas_API.Models.DTO;
using Saitynas_API.Models.Entities.Consultation;
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RequestConsultation(RequestConsultationDTO dto)
    {
        var user = await GetCurrentUser();

        if (user.PatientId == null)
        {
            return ApiNotFound(ApiErrorSlug.ResourceNotFound, "patient");
        }
            
        await _consultationsService.RequestConsultation((int) user.PatientId, dto.DeviceToken);

        return NoContent();
    }
}
