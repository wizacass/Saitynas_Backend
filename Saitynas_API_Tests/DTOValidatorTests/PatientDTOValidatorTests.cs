using NUnit.Framework;
using Saitynas_API.Exceptions;
using Saitynas_API.Models.Entities.Patient;
using Saitynas_API.Models.Entities.Patient.DTO;
using Saitynas_API.Services.Validators;
using static NUnit.Framework.Assert;

namespace Saitynas_API_Tests.DTOValidatorTests;

[TestFixture]
public class PatientDTOValidatorTests
{
    private IPatientDTOValidator _validator;
    
    [SetUp]
    public void SetUp()
    {
        _validator = new PatientDTOValidator();
    }
    
    private static PatientDTO ValidPatientDTO => new()
    {
        FirstName = "Test",
        LastName = "User",
        City = "Kaunas",
        BirthDate = "1990-04-12",
    };
    
    [Test]
    public void TestValidPatientDto()
    {
        var dto = ValidPatientDTO;

        _validator.ValidatePatientDTO(dto);

        var patient = new Patient(dto);

        IsNotNull(patient);
    }

    [Test]
    public void TestInvalidPatientName()
    {
        var dto = ValidPatientDTO;
        dto.FirstName = "";
        dto.LastName = "";

        Throws<DTOValidationException>(() => _validator.ValidatePatientDTO(dto));
    }
    
    [Test]
    public void TestInvalidPatientBirthDate()
    {
        var dto = ValidPatientDTO;
        dto.BirthDate = "";

        Throws<DTOValidationException>(() => _validator.ValidatePatientDTO(dto));
    }
    
    [Test]
    public void TestInvalidPatientCity()
    {
        var dto = ValidPatientDTO;
        dto.City = "";

        Throws<DTOValidationException>(() => _validator.ValidatePatientDTO(dto));
    }
}
