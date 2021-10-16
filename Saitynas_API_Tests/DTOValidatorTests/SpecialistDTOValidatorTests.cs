using NUnit.Framework;
using Saitynas_API.Exceptions;
using Saitynas_API.Models.SpecialistEntity;
using Saitynas_API.Models.SpecialistEntity.DTO;
using Saitynas_API.Models.SpecialistEntity.DTO.Validator;
using static NUnit.Framework.Assert;

namespace Saitynas_API_Tests.DTOValidatorTests
{
    public class SpecialistDTOValidatorTests
    {
        private ISpecialistDTOValidator _validator;
        
        private static CreateSpecialistDTO ValidCreateDTO => new()
        {
            FirstName = "Test",
            LastName = "User",
            SpecialityId = 1,
            Address = "Test Address",
            WorkplaceId = 1
        };
        
        private static EditSpecialistDTO ValidEditDTO => new()
        {
            FirstName = "Edited Name",
            LastName = "Edited Surname",
            Address = "Edited Address",
            WorkplaceId = null
        };

        [SetUp]
        public void SetUp()
        {
            _validator = new SpecialistDTOValidator();
        }
        
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

            Pass();
        }

        [Test]
        public void TestInvalidEditAddress()
        {
            var dto = ValidEditDTO;
            dto.Address = new string('x', 256);

            Throws<DTOValidationException>(() => _validator.ValidateEditSpecialistDTO(dto));
        }
    }
}
