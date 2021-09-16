using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Saitynas_API.Services.HeadersValidator
{
    public class HeadersValidator : IHeadersValidator
    {
        private const string ApiHeader = "X-Api-Request";

        public bool IsRequestHeaderValid(IHeaderDictionary headers)
        {
            headers.TryGetValue(ApiHeader, out var headerValues);

            return IsRequestValid(headerValues);
        }

        private static bool IsRequestValid(StringValues headers)
        {
            return IsHeaderValid(headers) && headers == "true";
        }

        private static bool IsHeaderValid(StringValues headers)
        {
            return !(headers.Count != 1 || string.IsNullOrWhiteSpace(headers.FirstOrDefault()));
        }
    }
}
