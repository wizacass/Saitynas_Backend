using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Saitynas_API.Models.Common;
using Saitynas_API.Models.DTO;
using Saitynas_API.Models.Entities.Patient;
using Saitynas_API.Models.Entities.Patient.DTO;
using Saitynas_API.Models.Entities.User;
using Saitynas_API.Repositories;
using Saitynas_API.Services.Validators;

namespace Saitynas_API.Controllers;

[Route($"{RoutePrefix}/[Controller]")]
[ApiController]
[Produces(ApiContentType)]
public class PatientsController : ApiControllerBase
{
    private readonly IPatientsRepository _patientsRepository;
    private readonly IPatientDTOValidator _validator;
    protected override string ModelName => "patient";

    public PatientsController(
        UserManager<User> userManager,
        IPatientsRepository patientsRepository,
        IPatientDTOValidator validator
    ) : base(userManager)
    {
        _patientsRepository = patientsRepository;
        _validator = validator;
    }

    [HttpPost]
    [Authorize(Roles = "Patient")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreatePatient([FromBody] PatientDTO dto)
    {
        var user = await GetCurrentUser();

        if (user.HasProfile) return ApiBadRequest(ApiErrorSlug.EntityExists, ModelName);
        
        _validator.ValidatePatientDTO(dto);
        var patient = new Patient(dto, user);
        await _patientsRepository.InsertAsync(patient);

        return NoContent();
    }
}
