using Saitynas_API.Models.Common;
using Saitynas_API.Services;

namespace Saitynas_API.Models.SpecialistEntity.DTO.Validator
{
    public class SpecialistDTOValidator : DTOValidator, ISpecialistDTOValidator
    {
        private readonly IEntityValidator _entityValidator;

        public SpecialistDTOValidator(IEntityValidator entityValidator)
        {
            _entityValidator = entityValidator;
        }
        
        public void ValidateCreateSpecialistDTO(CreateSpecialistDTO dto)
        {
            ValidateString(dto.FirstName, "firstName");
            ValidateString(dto.LastName, "lastName");
            ValidateEntityId(_entityValidator.IsSpecialityIdValid(dto.SpecialityId), "specialityId");
            ValidateStringLength(dto.Address, "address");
            ValidateEntityId(_entityValidator.IsWorkplaceIdValid(dto.WorkplaceId), "workplaceId");
        }

        public void ValidateEditSpecialistDTO(EditSpecialistDTO dto)
        {
            ValidateStringLength(dto.FirstName, "firstName");
            ValidateStringLength(dto.LastName, "lastName");
            ValidateEntityId(_entityValidator.IsSpecialityIdValid(dto.SpecialityId), "specialityId");
            ValidateStringLength(dto.Address, "address");
            ValidateEntityId(_entityValidator.IsWorkplaceIdValid(dto.WorkplaceId), "workplaceId");
        }
    }
}
