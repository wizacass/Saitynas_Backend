using Newtonsoft.Json;

namespace Saitynas_API.Models.Entities.Visit.DTO;

public class VisitDTO
{
    [JsonProperty("patientName")]
    public string PatientName { get; set; }

    [JsonProperty("specialistName")]
    public string SpecialistName { get; set; }

    [JsonProperty("visitStart")]
    public string VisitStart { get; set; }

    [JsonProperty("visitEnd")]
    public string VisitEnd { get; set; }
}