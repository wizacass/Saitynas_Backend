using Saitynas_API.Models.Entities.Workplace.DTO;

namespace Saitynas_API.Services.Validators;

public interface IWorkplaceDTOValidator
{
    public void ValidateCreateWorkplaceDTO(CreateWorkplaceDTO dto);

    public void ValidateEditWorkplaceDTO(EditWorkplaceDTO dto);
}

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
