using Newtonsoft.Json;

namespace Saitynas_API.Models.Authentication.DTO
{
    public class ChangePasswordDTO
    {
        [JsonProperty("oldPassword")]
        public string OldPassword { get; set; }

        [JsonProperty("newPassword")]
        public string NewPassword { get; set; }
    }
}
