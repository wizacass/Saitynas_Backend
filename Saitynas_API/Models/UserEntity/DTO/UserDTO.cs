using Newtonsoft.Json;

namespace Saitynas_API.Models.UsersEntity.DTO
{
    public class UserDTO
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        
        // [JsonProperty("birthDate")]
        // public string BirthDate { get; set; }

        public UserDTO() { }

        public UserDTO(User u)
        {
            FirstName = u.FirstName;
            LastName = u.LastName;
            // BirthDate = u.BirthDate.ToString("O");
        }
    }
}
