using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Saitynas_API.Models.Common;
using Saitynas_API.Models.DTO.Common;
using Saitynas_API.Services.HeadersValidator;

namespace Saitynas_API.Middleware
{
    public class RequestMiddleware
    {
        private readonly RequestDelegate _next;

        private static string SerializedError
        {
            get
            {
                var error = new ErrorDTO
                {
                    Type = 400,
                    Title = ApiErrorSlug.InvalidHeaders,
                    Details = null
                };

                return JsonConvert.SerializeObject(error, Formatting.Indented);
            }
        }

        public RequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IHeadersValidator headersValidator)
        {
            if (IsApiRequestInvalid(httpContext, headersValidator))
            {
                httpContext.Response.StatusCode = 400;
                httpContext.Response.ContentType = "application/json";

                await httpContext.Response.WriteAsync(SerializedError);
                return;
            }

            await _next(httpContext);
        }

        private static bool IsApiRequestInvalid(HttpContext httpContext, IHeadersValidator headersValidator)
        {
            return IsApiRequest(httpContext) &&
                   !headersValidator.IsRequestHeaderValid(httpContext.Request.Headers);
        }

        private static bool IsApiRequest(HttpContext httpContext)
        {
            return httpContext.Request.Path.StartsWithSegments("/api");
        }
    }
}
