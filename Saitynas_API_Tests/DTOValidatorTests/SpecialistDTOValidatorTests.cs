using Moq;
using NUnit.Framework;
using Saitynas_API.Exceptions;
using Saitynas_API.Models.Entities.Specialist;
using Saitynas_API.Models.Entities.Specialist.DTO;
using Saitynas_API.Services;
using Saitynas_API.Services.Validators;
using static NUnit.Framework.Assert;

namespace Saitynas_API_Tests.DTOValidatorTests;

[TestFixture]
public class SpecialistDTOValidatorTests
{
    [SetUp]
    public void SetUp()
    {
        _validator = new SpecialistDTOValidator(EntityValidatorMock);
    }

    private ISpecialistDTOValidator _validator;

    private static IEntityValidator EntityValidatorMock
    {
        get
        {
            var entityValidatorMock = new Mock<IEntityValidator>();

            entityValidatorMock
                .Setup(v => v.IsWorkplaceIdValid(It.IsAny<int?>()))
                .Returns(true);

            entityValidatorMock
                .Setup(v => v.IsWorkplaceIdValid(0))
                .Returns(false);

            entityValidatorMock
                .Setup(v => v.IsSpecialityIdValid(
                    It.IsInRange(1, 10, Range.Inclusive)
                ))
                .Returns(true);

            entityValidatorMock
                .Setup(v => v.IsSpecialityIdValid(null))
                .Returns(true);

            return entityValidatorMock.Object;
        }
    }

    private static CreateSpecialistDTO ValidCreateDTO => new()
    {
        FirstName = "Test",
        LastName = "User",
        SpecialityId = 1,
        City = "Test City"
    };

    private static EditSpecialistDTO ValidEditDTO => new()
    {
        FirstName = "Edited Name",
        LastName = "Edited Surname",
        Address = "Edited Address",
        WorkplaceId = null
    };

    [Test]
    public void TestValidCreateDto()
    {
        var dto = ValidCreateDTO;

        _validator.ValidateCreateSpecialistDTO(dto);

        var specialist = new Specialist(dto);

        IsNotNull(specialist);
    }

    [Test]
    public void TestInvalidCreateName()
    {
        var dto = ValidCreateDTO;
        dto.FirstName = "";
        dto.LastName = "";

        Throws<DTOValidationException>(() => _validator.ValidateCreateSpecialistDTO(dto));
    }

    [Test]
    public void TestInvalidCreateSpecialityId()
    {
        var dto = ValidCreateDTO;
        dto.SpecialityId = 0;

        Throws<DTOValidationException>(() => _validator.ValidateCreateSpecialistDTO(dto));
    }

    [Test]
    public void TestValidEditDto()
    {
        var dto = ValidEditDTO;

        _validator.ValidateEditSpecialistDTO(dto);
    }

    [Test]
    public void TestInvalidEditAddress()
    {
        var dto = ValidEditDTO;
        dto.Address = new string('x', 256);

        Throws<DTOValidationException>(() => _validator.ValidateEditSpecialistDTO(dto));
    }
}
