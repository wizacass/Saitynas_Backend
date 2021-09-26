using Newtonsoft.Json;

namespace Saitynas_API.Models.EvaluationEntity.DTO
{
    public class EvaluationDTO
    {
        [JsonProperty("value")]
        public int Value { get; set; }
        
        [JsonProperty("comment")]
        public string Comment { get; set; }
    }
}
