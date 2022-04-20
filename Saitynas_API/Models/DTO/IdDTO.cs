using System;
using Newtonsoft.Json;

namespace Saitynas_API.Models.DTO;

public class IdDTO
{
    [JsonProperty("id")]
    public Guid Id { get; set; }
}
