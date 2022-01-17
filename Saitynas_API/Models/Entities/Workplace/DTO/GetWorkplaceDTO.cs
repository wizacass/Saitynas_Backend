using Newtonsoft.Json;

namespace Saitynas_API.Models.Entities.Workplace.DTO;

public class GetWorkplaceDTO : WorkplaceDTO
{
    [JsonProperty("id")]
    public int Id { get; set; }

    public GetWorkplaceDTO() { }

    public GetWorkplaceDTO(Workplace w)
    {
        Id = w.Id;
        Address = w.Address;
        City = w.City;
    }
}