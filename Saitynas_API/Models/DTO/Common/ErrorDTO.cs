using Newtonsoft.Json;

namespace Saitynas_API.Models.DTO.Common
{
    public class ErrorDTO
    {
        [JsonProperty("type")]
        public long Type { get; set; }
        
        [JsonProperty("title")]
        public string Title { get; set; }
        
        [JsonProperty("details")]
        public string Details { get; set; }
    }
}
