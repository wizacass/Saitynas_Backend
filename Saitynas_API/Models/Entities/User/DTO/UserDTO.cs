using Newtonsoft.Json;

namespace Saitynas_API.Models.Entities.User.DTO;

public class UserDTO
{
    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("hasProfile")]
    public bool HasProfile { get; set; }

    public UserDTO() { }

    public UserDTO(User u)
    {
        Email = u.Email;
        HasProfile = u.HasProfile;
    }
}
