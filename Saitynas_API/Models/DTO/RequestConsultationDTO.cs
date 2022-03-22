using Newtonsoft.Json;

namespace Saitynas_API.Models.DTO;

public class RequestConsultationDTO
{
    [JsonProperty("device_token")]
    public string DeviceToken { get; set; } 
    
    [JsonProperty("speciality_id")]
    public int? SpecialityId { get; set; }
}
