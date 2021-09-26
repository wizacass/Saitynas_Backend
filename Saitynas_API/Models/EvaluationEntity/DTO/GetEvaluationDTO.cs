using Newtonsoft.Json;

namespace Saitynas_API.Models.EvaluationEntity.DTO
{
    public class GetEvaluationDTO : EvaluationDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        public GetEvaluationDTO() { }

        public GetEvaluationDTO(Evaluation e)
        {
            Id = e.Id;
            Value = e.Value;
            Comment = e.Comment;
        }
    }
}
