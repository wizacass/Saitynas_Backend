using Saitynas_API.Exceptions;
using Saitynas_API.Models.Common;

namespace Saitynas_API.Models.WorkplaceEntity.DTO.Validator
{
    public class WorkplaceDTOValidator : IWorkplaceDTOValidator
    {
        public void ValidateCreateWorkplaceDTO(CreateWorkplaceDTO dto)
        {
            ValidateWorkplaceDTO(dto);
        }

        public void ValidateEditWorkplaceDTO(EditWorkplaceDTO dto)
        {
            ValidateWorkplaceDTO(dto);
        }

        private static void ValidateWorkplaceDTO(WorkplaceDTO dto)
        {
            ValidateString(dto.Address, "address");
            ValidateString(dto.City, "city");
        }
        
        private static void ValidateString(string parameter, string name)
        {
            if (string.IsNullOrEmpty(parameter))
                throw new DTOValidationException(ApiErrorSlug.EmptyParameter, name);
        }
    }
}
