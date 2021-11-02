using Moq;
using NUnit.Framework;
using Saitynas_API.Exceptions;
using Saitynas_API.Models.EvaluationEntity;
using Saitynas_API.Models.EvaluationEntity.DTO;
using Saitynas_API.Services;
using Saitynas_API.Services.Validators;
using static NUnit.Framework.Assert;

namespace Saitynas_API_Tests.DTOValidatorTests
{
    [TestFixture]
    public class EvaluationDTOValidatorTests
    {
        private IEvaluationDTOValidator _validator;

        private static IEntityValidator EntityValidatorMock
        {
            get
            {
                var entityValidatorMock = new Mock<IEntityValidator>();

                entityValidatorMock
                    .Setup(v => v.IsSpecialistIdValid(It.IsAny<int>()))
                    .Returns(true);

                entityValidatorMock
                    .Setup(v => v.IsSpecialistIdValid(0))
                    .Returns(false);

                return entityValidatorMock.Object;
            }
        }

        private static EvaluationDTO ValidCreateDTO => new()
        {
            Value = 10,
            Comment = "Very nice!",
            SpecialistId = 1
        };

        private static EditEvaluationDTO ValidEditDTO => new()
        {
           Value = 9
        };

        [SetUp]
        public void SetUp()
        {
            _validator = new EvaluationDTOValidator(EntityValidatorMock);
        }

        [Test]
        public void TestValidCreateDto()
        {
            var dto = ValidCreateDTO;

            _validator.ValidateCreateEvaluationDTO(dto);

            var evaluation = new Evaluation(dto);

            IsNotNull(evaluation);
        }

        [TestCase(0)]
        [TestCase(100)]
        public void TestInvalidCreateValue(int value)
        {
            var dto = ValidCreateDTO;
            dto.Value = value;

            Throws<DTOValidationException>(() => _validator.ValidateCreateEvaluationDTO(dto));
        }

        [Test]
        public void TestInvalidCreateSpecialistId()
        {
            var dto = ValidCreateDTO;
            dto.SpecialistId = 0;

            Throws<DTOValidationException>(() => _validator.ValidateCreateEvaluationDTO(dto));
        }

        [Test]
        public void TestValidEditDto()
        {
            var dto = ValidEditDTO;

            _validator.ValidateEditEvaluationDTO(dto);
        }

        [Test]
        public void TestInvalidEditComment()
        {
            var dto = ValidEditDTO;
            dto.Comment = new string('x', 256);

            Throws<DTOValidationException>(() => _validator.ValidateEditEvaluationDTO(dto));
        }
    }
}
