using Newtonsoft.Json;

namespace Saitynas_API.Models.Entities.Specialist.DTO;

public class GetSpecialistDTO : SpecialistDTO
{
    [JsonProperty("id")]
    public int Id { get; set; }

    public GetSpecialistDTO() { }

    public GetSpecialistDTO(Specialist s) : base(s)
    {
        Id = s.Id;
    }
}
