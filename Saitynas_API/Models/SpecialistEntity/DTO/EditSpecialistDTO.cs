using Newtonsoft.Json;

namespace Saitynas_API.Models.SpecialistEntity.DTO
{
    public class EditSpecialistDTO
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        
        [JsonProperty("specialityId")]
        public int? SpecialityId { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }
        
        [JsonProperty("workplaceId")]
        public int? WorkplaceId { get; set; }
    }
}
