using Newtonsoft.Json;

namespace Saitynas_API.Models.EvaluationEntity.DTO
{
    public class GetEvaluationDTO : EvaluationDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("specialist")]
        public string Specialist { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }
        
        public GetEvaluationDTO() { }

        public GetEvaluationDTO(Evaluation e) : base(e)
        {
            Id = e.Id;
            Specialist = $"{e.Specialist?.FirstName} {e.Specialist?.LastName}";
            Author = e.User?.Email;
        }
    }
}
