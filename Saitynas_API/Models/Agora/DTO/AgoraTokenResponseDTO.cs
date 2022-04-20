using Newtonsoft.Json;

namespace Saitynas_API.Models.Agora.DTO;

public class AgoraTokenResponseDTO
{
    [JsonProperty("channel")]
    public string Channel { get; set; }
    
    [JsonProperty("uid")]
    public string Uid { get; set; }
    
    [JsonProperty("token")]
    public string Token { get; set; }
}
