using Newtonsoft.Json;

namespace Saitynas_API.Models.Workplaces.DTO
{
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

        public static GetWorkplaceDTO Mocked => new()
        {
            Id = 1,
            Address = "Test str. 22",
            City = "Kaunas"
        };
    }
}
