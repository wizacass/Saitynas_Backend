using Newtonsoft.Json;

namespace Saitynas_API.Models.Entities.Evaluation.DTO
{
    public class GetUserEvaluationDTO
    {
        [JsonProperty("value")]
        public int Value { get; set; }
        
        [JsonProperty("comment")]
        public string Comment { get; set; }

        public GetUserEvaluationDTO() { }

        public GetUserEvaluationDTO(Evaluation e)
        {
            Value = e.Value ?? 0;
            Comment = e.Comment;
        }
    }
}
