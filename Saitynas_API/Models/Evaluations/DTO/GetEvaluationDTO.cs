using Newtonsoft.Json;

namespace Saitynas_API.Models.Evaluations.DTO
{
    public class GetEvaluationDTO : EvaluationDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        public static GetEvaluationDTO Mocked => new()
        {
            Id = 1,
            Evaluation = 5,
            Comment = "Very nice!"
        };
    }
}
