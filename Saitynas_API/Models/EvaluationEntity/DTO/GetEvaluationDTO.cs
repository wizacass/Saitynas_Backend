using Newtonsoft.Json;

namespace Saitynas_API.Models.EvaluationEntity.DTO
{
    public class GetEvaluationDTO : EvaluationDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        public GetEvaluationDTO() { }

        public GetEvaluationDTO(Evaluation e) : base(e)
        {
            Id = e.Id;
        }
    }
}
