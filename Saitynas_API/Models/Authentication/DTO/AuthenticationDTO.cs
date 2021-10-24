using Newtonsoft.Json;

namespace Saitynas_API.Models.Authentication.DTO
{
    public class AuthenticationDTO
    {
        [JsonProperty("jwt")]
        public string AccessToken { get; set; }

        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }

        public AuthenticationDTO() { }

        public AuthenticationDTO(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
