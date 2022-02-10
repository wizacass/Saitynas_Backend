using Newtonsoft.Json;

namespace Saitynas_API.Models.Entities.User.DTO;

public class ProfileDTO
{
    [JsonProperty("firstName")]
    public string FirstName { get; set; }

    [JsonProperty("lastName")]
    public string LastName { get; set; }

    public ProfileDTO() { }

    public ProfileDTO(Specialist.Specialist s)
    {
        FirstName = s.FirstName;
        LastName = s.LastName;
    }

    public ProfileDTO(Patient.Patient p)
    {
        FirstName = p.FirstName;
        LastName = p.LastName;
    }
}
