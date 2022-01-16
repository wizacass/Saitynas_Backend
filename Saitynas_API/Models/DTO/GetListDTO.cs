using System.Collections.Generic;
using Newtonsoft.Json;

namespace Saitynas_API.Models.DTO;

public class GetListDTO<T>
{
    [JsonProperty("meta")]
    public Meta Meta { get; set; }

    [JsonProperty("data")]
    public IEnumerable<T> Data { get; set; }

    public GetListDTO(IEnumerable<T> data)
    {
        Meta = Meta.Empty;
        Data = data;
    }
}