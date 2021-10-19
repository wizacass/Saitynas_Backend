using Newtonsoft.Json;

namespace Saitynas_API.Models.EvaluationEntity.DTO
{
    public class EvaluationDTO
    {
        [JsonProperty("value")]
        public int Value { get; set; }
        
        [JsonProperty("comment")]
        public string Comment { get; set; }
        
        [JsonProperty("specialistId")]
        public int SpecialistId { get; set; }
        
        [JsonProperty("specialist")]
        public string Specialist { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        public EvaluationDTO() { }

        public EvaluationDTO(Evaluation e)
        {
            Value = e.Value;
            Comment = e.Comment;
            SpecialistId = e.SpecialistId;
            Specialist = $"{e.Specialist?.FirstName} {e.Specialist?.LastName}";
            Author = e.User?.Email;
        }
    }
}
