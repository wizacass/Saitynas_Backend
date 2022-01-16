using Newtonsoft.Json;

namespace Saitynas_API.Models.Entities.User.DTO;

public class UserDTO
{
    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("registrationDate")]
    public string RegistrationDate { get; set; }

    public UserDTO() { }

    public UserDTO(User u)
    {
        Email = u.Email;
        RegistrationDate = u.RegistrationDate.ToString("O");
    }
}