using Saitynas_API.Exceptions;
using Saitynas_API.Models.Common;
using Saitynas_API.Services.EntityValidator;

namespace Saitynas_API.Models.EvaluationEntity.DTO.Validator
{
    public class EvaluationDTOValidator : DTOValidator, IEvaluationDTOValidator
    {
        private readonly IEntityValidator _entityValidator;

        public EvaluationDTOValidator(IEntityValidator validator)
        {
            _entityValidator = validator;
        }

        public void ValidateCreateEvaluationDTO(EvaluationDTO dto)
        {
            ValidateEvaluationValue(dto.Value, "value");
            ValidateStringLength(dto.Comment, "comment");
            ValidateEntityId(_entityValidator.IsSpecialistIdValid(dto.SpecialistId), "specialistId");
        }

        public void ValidateEditEvaluationDTO(EditEvaluationDTO dto)
        {
            ValidateEvaluationValue(dto.Value, "value");
            ValidateStringLength(dto.Comment, "comment");
        }

        private static void ValidateEvaluationValue(int? value, string parameter)
        {
            if (value == null) return;

            ValidateEvaluationValue((int)value, parameter);
        }

        private static void ValidateEvaluationValue(int value, string parameter)
        {
            if (value is <= 0 or > 10) throw new DTOValidationException(ApiErrorSlug.InvalidNumber, parameter);
        }
    }
}