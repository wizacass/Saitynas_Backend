using Newtonsoft.Json;

namespace Saitynas_API.Models.DTO
{
    public class GetObjectDTO<T>
    {
        [JsonProperty("meta")] 
        public Meta Meta { get; set; }

        [JsonProperty("data")] 
        public T Data { get; set; }
        
        public GetObjectDTO(T data)
        {
            Meta = Meta.Empty;
            Data = data;
        }
    }
}
