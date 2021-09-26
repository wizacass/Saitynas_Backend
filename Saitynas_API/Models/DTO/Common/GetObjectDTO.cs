using Newtonsoft.Json;
using Saitynas_API.Models.Common;

namespace Saitynas_API.Models.DTO.Common
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
