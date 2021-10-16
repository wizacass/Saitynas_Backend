namespace Saitynas_API.Models.SpecialistEntity.DTO.Validator
{
    public interface ISpecialistDTOValidator
    {
        public void ValidateCreateSpecialistDTO(CreateSpecialistDTO dto);

        public void ValidateEditSpecialistDTO(EditSpecialistDTO dto);
    }
}
