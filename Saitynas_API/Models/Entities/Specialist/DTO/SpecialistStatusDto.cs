using Newtonsoft.Json;

namespace Saitynas_API.Models.Entities.Specialist.DTO;

public class SpecialistStatusDto
{
    [JsonProperty("specialistStatus")]
    public SpecialistStatusId StatusId { get; set; }
    
    [JsonProperty("deviceToken")]
    public string DeviceToken { get; set; }
}
