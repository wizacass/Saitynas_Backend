using Newtonsoft.Json;
using Saitynas_API.Models.Common;

namespace Saitynas_API.Models.Messages.DTO
{
    public class MessageDTO
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("data")]
        public MessageData Data { get; set; }
        
        public MessageDTO(string message)
        {
            Meta = Meta.Empty;
            Data = new MessageData
            {
                Message = message
            };
        }
    }

    public class MessageData
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
