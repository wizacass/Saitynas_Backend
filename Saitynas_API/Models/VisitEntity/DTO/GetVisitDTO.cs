using Newtonsoft.Json;

namespace Saitynas_API.Models.VisitEntity.DTO
{
    public class GetVisitDTO : VisitDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
