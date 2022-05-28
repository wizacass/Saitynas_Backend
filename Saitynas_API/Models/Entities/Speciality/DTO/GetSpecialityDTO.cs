using System.Linq;
using Newtonsoft.Json;
using Saitynas_API.Models.Entities.Specialist;

namespace Saitynas_API.Models.Entities.Speciality.DTO;

public class GetSpecialityDTO
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("activeSpecialists")]
    public int Count { get; set; }

    public GetSpecialityDTO() { }

    public GetSpecialityDTO(Speciality s)
    {
        Id = s.Id;
        Name = s.Name;
        Count = s.Specialists.Count(s => s.SpecialistStatusId == SpecialistStatusId.Available);
    }
}
