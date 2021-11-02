using Newtonsoft.Json;

namespace Saitynas_API.Models.DTO
{
    public class EnumDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
