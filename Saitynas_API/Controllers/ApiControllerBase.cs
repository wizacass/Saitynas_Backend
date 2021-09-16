using Microsoft.AspNetCore.Mvc;
using Saitynas_API.Models;

namespace Saitynas_API.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        protected const string ApiContentType = "application/json";
        protected const string RoutePrefix = "api/v1";

        protected ApiContext Context { get; }
        
        public ApiControllerBase(ApiContext context)
        {
            Context = context;
        }
    }
}
