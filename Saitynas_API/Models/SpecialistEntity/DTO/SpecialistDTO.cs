using Newtonsoft.Json;
using Saitynas_API.Models.UsersEntity.DTO;

namespace Saitynas_API.Models.SpecialistsEntity.DTO
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
