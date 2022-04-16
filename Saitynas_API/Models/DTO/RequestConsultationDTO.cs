using Newtonsoft.Json;

namespace Saitynas_API.Models.DTO;

public class RequestConsultationDTO
{
    [JsonProperty("deviceToken")]
    public string DeviceToken { get; set; } 
    
    [JsonProperty("specialityId")]
    public int? SpecialityId { get; set; }
}
