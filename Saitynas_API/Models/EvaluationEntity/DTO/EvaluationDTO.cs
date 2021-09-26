using Newtonsoft.Json;

namespace Saitynas_API.Models.EvaluationEntity.DTO
{
    public class EvaluationDTO
    {
        [JsonProperty("evaluation")]
        public int Evaluation { get; set; }
        
        [JsonProperty("comment")]
        public string Comment { get; set; }
    }
}
