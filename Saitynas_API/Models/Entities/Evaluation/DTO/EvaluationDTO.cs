using Newtonsoft.Json;

namespace Saitynas_API.Models.Entities.Evaluation.DTO;

public class EvaluationDTO
{
    [JsonProperty("value")]
    public int Value { get; set; }

    [JsonProperty("comment")]
    public string Comment { get; set; }

    [JsonProperty("specialistId")]
    public int SpecialistId { get; set; }

    public EvaluationDTO() { }

    public EvaluationDTO(Evaluation e)
    {
        Value = e.Value ?? 0;
        Comment = e.Comment;
        SpecialistId = e.SpecialistId;
    }
}
