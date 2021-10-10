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
            ValidateStringLength(dto.Address, "address");
            ValidateStringLength(dto.City, "city");
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
            
            ValidateStringLength(parameter, name);
        }

        private static void ValidateStringLength(string parameter, string name)
        {
            if (parameter == null) return;

            if (parameter.Length > 255)
                throw new DTOValidationException(ApiErrorSlug.StringTooLong, name);
        }
    }
}
