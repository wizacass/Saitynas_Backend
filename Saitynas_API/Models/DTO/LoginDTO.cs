using Newtonsoft.Json;

namespace Saitynas_API.Models.DTO
{
    public class LoginDTO
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
