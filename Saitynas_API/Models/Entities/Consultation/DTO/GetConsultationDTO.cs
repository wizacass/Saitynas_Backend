using Newtonsoft.Json;

namespace Saitynas_API.Models.Entities.Consultation.DTO;

public class GetConsultationDTO
{
    [JsonProperty("patientName")]
    public string PatientName { get; set; }
    
    [JsonProperty("startDate")]
    public string StartDate { get; set; }
    
    [JsonProperty("duration")]
    public int Seconds { get; set; }

    public GetConsultationDTO() { }

    public GetConsultationDTO(Consultation c)
    {
        PatientName = $"{c.Patient.FirstName} {c.Patient.LastName}";
        StartDate = c.StartedAt?.ToString("u");
        Seconds = (c.FinishedAt! - c.StartedAt!).Value.Seconds;
    }
}
