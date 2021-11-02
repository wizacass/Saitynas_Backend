using Newtonsoft.Json;

namespace Saitynas_API.Models.DTO
{
    public class GetEnumDTO : EnumDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
