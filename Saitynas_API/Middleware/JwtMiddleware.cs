using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Saitynas_API.Services.JwtService;

namespace Saitynas_API.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
     //   private readonly AppSettings _appSettings;

     private const string AuthHeader = "Authorization";
     
        // public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
          //  _appSettings = appSettings.Value;
        }

        // public async Task Invoke(HttpContext context, IUserService userService, IJwtService jwtService)
        public async Task Invoke(HttpContext context, IJwtService jwtService)
        {
            string token = RetrieveToken(context);
            string email = jwtService.ValidateToken(token);
            if (email != null)
            {
                // attach user to context on successful jwt validation
                // context.Items["User"] = userService.GetById(userId.Value);
            }

            await _next(context);
        }

        private string RetrieveToken(HttpContext context)
        {
            return context.Request.Headers[AuthHeader].FirstOrDefault()?.Split(" ").Last();
        }
    }
}
