namespace Saitynas_API.Models.WorkplaceEntity.DTO.Validator
{
    public interface IWorkplaceDTOValidator
    {
        public void ValidateCreateWorkplaceDTO(CreateWorkplaceDTO dto);

        public void ValidateEditWorkplaceDTO(EditWorkplaceDTO dto);
    }
}
