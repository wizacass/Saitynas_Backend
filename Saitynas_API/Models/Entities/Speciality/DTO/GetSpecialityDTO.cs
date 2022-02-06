using Newtonsoft.Json;

namespace Saitynas_API.Models.Entities.Speciality.DTO;

public class GetSpecialityDTO
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }

    public GetSpecialityDTO() { }

    public GetSpecialityDTO(Speciality s)
    {
        Id = s.Id;
        Name = s.Name;
    }
}
