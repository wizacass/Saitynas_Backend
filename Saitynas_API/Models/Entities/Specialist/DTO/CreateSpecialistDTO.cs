using Newtonsoft.Json;

namespace Saitynas_API.Models.Entities.Specialist.DTO;

public class CreateSpecialistDTO
{
    [JsonProperty("firstName")]
    public string FirstName { get; set; }

    [JsonProperty("lastName")]
    public string LastName { get; set; }
    
    [JsonProperty("city")]
    public string City { get; set; }

    [JsonProperty("specialityId")]
    public int SpecialityId { get; set; }
}
