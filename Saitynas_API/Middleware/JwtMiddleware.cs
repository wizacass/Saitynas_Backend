using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Saitynas_API.Models.UserEntity;
using Saitynas_API.Services.JwtService;

namespace Saitynas_API.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private const string AuthHeader = "Authorization";

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, UserManager<User> userManager, IJwtService jwtService)
        {
            string token = RetrieveToken(context);
            string email = jwtService.ValidateToken(token);
            if (email != null) context.Items[nameof(User)] = await userManager.FindByEmailAsync(email);

            await _next(context);
        }

        private static string RetrieveToken(HttpContext context)
        {
            return context.Request.Headers[AuthHeader].FirstOrDefault()?.Split(" ").Last();
        }
    }
}
