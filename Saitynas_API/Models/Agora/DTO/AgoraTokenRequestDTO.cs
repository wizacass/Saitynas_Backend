using Newtonsoft.Json;

namespace Saitynas_API.Models.Agora.DTO;

public class AgoraTokenRequestDTO
{
    [JsonProperty("channel")]
    public string Channel { get; set; }
}
