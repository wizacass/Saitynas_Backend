using NUnit.Framework;
using Saitynas_API.Exceptions;
using Saitynas_API.Models.Authentication.DTO;
using Saitynas_API.Models.Authentication.DTO.Validator;
using static NUnit.Framework.Assert;

namespace Saitynas_API_Tests
{
    public class SignupDTOValidatorTests
    {
        private ISignupDTOValidator _validator;
        
        private static SignupDTO ValidCreateDTO =>  new()
        {
            Email = "test@test.com",
            Password = "Password0",
            Role = 3
        };

        [SetUp]
        public void SetUp()
        {
            _validator = new SignupDTOValidator();
        }
        
        [Test]
        public void TestValidCreateDto()
        {
            var dto = ValidCreateDTO;

            _validator.ValidateSignupDTO(dto);

            Pass();
        }

        [Test]
        public void TestInvalidEmail()
        {
            var dto = ValidCreateDTO;
            dto.Email = "";

            Throws<DTOValidationException>(() => _validator.ValidateSignupDTO(dto));
        }
        
        [Test]
        public void TestInvalidPassword()
        {
            var dto = ValidCreateDTO;
            dto.Password = new string('x', 256);

            Throws<DTOValidationException>(() => _validator.ValidateSignupDTO(dto));
        }
        
        [Test]
        public void TestInvalidRole()
        {
            var dto = ValidCreateDTO;
            dto.Role = 0;

            Throws<DTOValidationException>(() => _validator.ValidateSignupDTO(dto));
        }
    }
}
