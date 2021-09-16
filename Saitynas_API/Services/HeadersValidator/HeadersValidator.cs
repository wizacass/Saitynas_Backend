using Microsoft.AspNetCore.Http;

namespace Saitynas_API.Services.HeadersValidator
{
    public class HeadersValidator : IHeadersValidator
    {
        public bool IsRequestHeaderValid(IHeaderDictionary headers)
        {
            return false;
        }
    }
}
