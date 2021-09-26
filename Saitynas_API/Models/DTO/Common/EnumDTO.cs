using Newtonsoft.Json;

namespace Saitynas_API.Models.DTO.Common
{
    public class EnumDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
