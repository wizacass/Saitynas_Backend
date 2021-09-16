using Microsoft.AspNetCore.Http;

namespace Saitynas_API.Services.HeadersValidator
{
    public interface IHeadersValidator
    {
        public bool IsRequestHeaderValid(IHeaderDictionary headers);
    }
}
