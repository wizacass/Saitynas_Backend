using Newtonsoft.Json;
using Saitynas_API.Models.UserEntity.DTO;

namespace Saitynas_API.Models.SpecialistEntity.DTO
{
    public class SpecialistDTO : UserDTO
    {
        [JsonProperty("speciality")]
        public string Speciality { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        public SpecialistDTO() { }
    }
}
