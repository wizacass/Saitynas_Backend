using Newtonsoft.Json;

namespace Saitynas_API.Models.DTO;

public class CountDTO
{
    [JsonProperty("count")]
    public int Count { get; set; }
}
