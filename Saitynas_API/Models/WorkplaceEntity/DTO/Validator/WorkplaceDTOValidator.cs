using Saitynas_API.Exceptions;
using Saitynas_API.Models.Common;

namespace Saitynas_API.Models.WorkplaceEntity.DTO.Validator
{
    public class WorkplaceDTOValidator : DTOValidator, IWorkplaceDTOValidator
    {
        public void ValidateCreateWorkplaceDTO(CreateWorkplaceDTO dto)
        {
            ValidateWorkplaceDTO(dto);
        }

        public void ValidateEditWorkplaceDTO(EditWorkplaceDTO dto)
        {
            ValidateStringLength(dto.Address, "address");
            ValidateStringLength(dto.City, "city");
        }

        private static void ValidateWorkplaceDTO(WorkplaceDTO dto)
        {
            ValidateString(dto.Address, "address");
            ValidateString(dto.City, "city");
        }
    }
}
