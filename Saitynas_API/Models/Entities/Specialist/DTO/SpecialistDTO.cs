using Newtonsoft.Json;

namespace Saitynas_API.Models.Entities.Specialist.DTO;

public class SpecialistDTO
{
    [JsonProperty("firstName")]
    public string FirstName { get; set; }

    [JsonProperty("lastName")]
    public string LastName { get; set; }

    [JsonProperty("speciality")]
    public string Speciality { get; set; }

    [JsonProperty("address")]
    public string Address { get; set; }

    public SpecialistDTO() { }

    public SpecialistDTO(Specialist s)
    {
        FirstName = s.FirstName;
        LastName = s.LastName;
        Address = s.Workplace?.Address ?? s.Address;
        Speciality = s.Speciality.Name;
    }
}