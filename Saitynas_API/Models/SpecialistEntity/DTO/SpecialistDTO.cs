using Newtonsoft.Json;
using Saitynas_API.Models.UserEntity.DTO;

namespace Saitynas_API.Models.SpecialistEntity.DTO
{
    public class SpecialistDTO : UserDTO
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        
        [JsonProperty("speciality")]
        public string Speciality { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        public SpecialistDTO() { }
    }
}
