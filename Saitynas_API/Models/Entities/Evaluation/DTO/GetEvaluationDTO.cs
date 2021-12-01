using Newtonsoft.Json;

namespace Saitynas_API.Models.Entities.Evaluation.DTO
{
    public class GetEvaluationDTO : EvaluationDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("specialist")]
        public string Specialist { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        public GetEvaluationDTO() { }

        public GetEvaluationDTO(Evaluation e) : base(e)
        {
            Id = e.Id;
            Specialist = $"{e.Specialist?.FirstName} {e.Specialist?.LastName}";
            Author = e.User?.Email;
            CreatedAt = e.CreatedAt.ToString("O");
        }
    }
}
