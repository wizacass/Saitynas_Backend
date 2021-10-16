using Saitynas_API.Models.Common;

namespace Saitynas_API.Models.SpecialistEntity.DTO.Validator
{
    public class SpecialistDTOValidator : DTOValidator, ISpecialistDTOValidator
    {
        public void ValidateCreateSpecialistDTO(CreateSpecialistDTO dto)
        {
            ValidateString(dto.FirstName, "firstName");
            ValidateString(dto.LastName, "lastName");
            ValidateIntegerIsPositive(dto.SpecialityId, "specialityId");
            ValidateStringLength(dto.Address, "address");
            ValidateIntegerIsPositive(dto.WorkplaceId, "workplaceId");
        }

        public void ValidateEditSpecialistDTO(EditSpecialistDTO dto)
        {
            ValidateStringLength(dto.FirstName, "firstName");
            ValidateStringLength(dto.LastName, "lastName");
            ValidateIntegerIsPositive(dto.SpecialityId, "specialityId");
            ValidateStringLength(dto.Address, "address");
            ValidateIntegerIsPositive(dto.WorkplaceId, "workplaceId");
        }
    }
}
