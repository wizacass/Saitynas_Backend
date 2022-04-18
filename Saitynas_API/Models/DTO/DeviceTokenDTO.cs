using Newtonsoft.Json;

namespace Saitynas_API.Models.DTO;

public class DeviceTokenDTO
{
    [JsonProperty("deviceToken")]
    public string DeviceToken { get; set; }
}
