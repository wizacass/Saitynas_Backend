using NUnit.Framework;
using Saitynas_API.Exceptions;
using Saitynas_API.Models.Entities.Workplace;
using Saitynas_API.Models.Entities.Workplace.DTO;
using Saitynas_API.Services.Validators;
using static NUnit.Framework.Assert;

namespace Saitynas_API_Tests.DTOValidatorTests;

[TestFixture]
public class WorkplaceDTOValidatorTests
{
    [SetUp]
    public void SetUp()
    {
        _validator = new WorkplaceDTOValidator();
    }

    private IWorkplaceDTOValidator _validator;

    private static CreateWorkplaceDTO ValidCreateDTO => new()
    {
        Address = "Test Address",
        City = "Kaunas"
    };

    private static EditWorkplaceDTO ValidEditDTO => new()
    {
        Address = "Test Address",
        City = "Kaunas"
    };

    [Test]
    public void TestValidCreateDto()
    {
        var dto = ValidCreateDTO;

        _validator.ValidateCreateWorkplaceDTO(dto);

        var workplace = new Workplace(dto);

        IsNotNull(workplace);
    }

    [Test]
    public void TestInvalidCreateAddress()
    {
        var dto = ValidCreateDTO;
        dto.Address = "";

        Throws<DTOValidationException>(() => _validator.ValidateCreateWorkplaceDTO(dto));
    }

    [Test]
    public void TestInvalidCreateCity()
    {
        var dto = ValidCreateDTO;
        dto.City = "";

        Throws<DTOValidationException>(() => _validator.ValidateCreateWorkplaceDTO(dto));
    }


    [Test]
    public void TestValidEditDto()
    {
        var dto = ValidEditDTO;

        _validator.ValidateEditWorkplaceDTO(dto);
    }

    [Test]
    public void TestInvalidEditAddress()
    {
        var dto = ValidEditDTO;
        dto.Address = new string('x', 256);

        Throws<DTOValidationException>(() => _validator.ValidateEditWorkplaceDTO(dto));
    }

    [Test]
    public void TestInvalidEditCity()
    {
        var dto = ValidEditDTO;
        dto.Address = new string('x', 256);

        Throws<DTOValidationException>(() => _validator.ValidateEditWorkplaceDTO(dto));
    }
}
