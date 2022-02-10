using Newtonsoft.Json;

namespace Saitynas_API.Models.Entities.Patient.DTO;

public class PatientDTO
{
    [JsonProperty("firstName")]
    public string FirstName { get; set; }

    [JsonProperty("lastName")]
    public string LastName { get; set; }

    [JsonProperty("birthDate")]
    public string BirthDate { get; set; }

    [JsonProperty("city")]
    public string City { get; set; }

    public PatientDTO() { }

    public PatientDTO(Patient p)
    {
        FirstName = p.FirstName;
        LastName = p.LastName;
        BirthDate = p.BirthDate.ToString("O");
        City = p.City;
    }
}
