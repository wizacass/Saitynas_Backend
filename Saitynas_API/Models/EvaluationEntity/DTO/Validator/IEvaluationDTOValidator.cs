namespace Saitynas_API.Models.EvaluationEntity.DTO.Validator
{
    public interface IEvaluationDTOValidator
    {
        public void ValidateCreateEvaluationDTO(EvaluationDTO dto);

        public void ValidateEditEvaluationDTO(EditEvaluationDTO dto);
    }
}
