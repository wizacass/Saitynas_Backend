using Newtonsoft.Json;

namespace Saitynas_API.Models.DTO
{
    public class ErrorDTO
    {
        [JsonProperty("type")]
        public long Type { get; set; }
        
        [JsonProperty("title")]
        public string Title { get; set; }
        
        [JsonProperty("details")]
        public string Details { get; set; }

        public ErrorDTO() { }

        public ErrorDTO(long type, string title, string details = "")
        {
            Type = type;
            Title = title;
            Details = details;
        }
    }
}
