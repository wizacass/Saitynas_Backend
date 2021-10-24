using Newtonsoft.Json;

namespace Saitynas_API.Models.Authentication.DTO
{
    public class RefreshTokenDTO
    {
        [JsonProperty("refreshToken")]
        public string Token { get; set; }
    }
}
