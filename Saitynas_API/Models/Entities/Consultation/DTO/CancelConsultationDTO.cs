using Newtonsoft.Json;

namespace Saitynas_API.Models.Entities.Consultation.DTO;

public class CancelConsultationDTO
{
    [JsonProperty("consultationId")]
    public int ConsultationId { get; set; }
    
    [JsonProperty("deviceToken")]
    public string DeviceToken { get; set; }
}
