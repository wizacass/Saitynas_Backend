using System;
using Newtonsoft.Json;

namespace Saitynas_API.Models.Entities.Consultation.DTO;

public class ConsultationDTO
{
    [JsonProperty("consultationId")]
    public Guid ConsultationId { get; set; }
    
    [JsonProperty("deviceToken")]
    public string DeviceToken { get; set; }
}
