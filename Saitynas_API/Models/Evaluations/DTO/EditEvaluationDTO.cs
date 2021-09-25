using Newtonsoft.Json;

namespace Saitynas_API.Models.Evaluations.DTO
{
    public class EditEvaluationDTO
    {
        [JsonProperty("evaluation")]
        public int? Evaluation { get; set; }
        
        [JsonProperty("comment")]
        public string Comment { get; set; }
    }
}
