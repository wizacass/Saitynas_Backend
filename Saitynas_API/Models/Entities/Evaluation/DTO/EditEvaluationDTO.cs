using Newtonsoft.Json;

namespace Saitynas_API.Models.Entities.Evaluation.DTO
{
    public class EditEvaluationDTO
    {
        [JsonProperty("value")]
        public int? Value { get; set; }
        
        [JsonProperty("comment")]
        public string Comment { get; set; }
    }
}
