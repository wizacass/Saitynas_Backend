using Newtonsoft.Json;

namespace Saitynas_API.Models.Authentication.DTO
{
    public class SignupDTO
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
        
        [JsonProperty("role")]
        public int Role { get; set; }
    }
}
