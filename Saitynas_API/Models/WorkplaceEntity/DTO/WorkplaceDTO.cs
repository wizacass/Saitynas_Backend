using Newtonsoft.Json;

namespace Saitynas_API.Models.WorkplaceEntity.DTO
{
    public class WorkplaceDTO
    {
        [JsonProperty("address")]
        public string Address { get; set; }
        
        [JsonProperty("city")]
        public string City { get; set; }
    }
}
