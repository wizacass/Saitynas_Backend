using Saitynas_API.Models.Authentication;

namespace Saitynas_API.Services.JwtService
{
    public interface IJwtService
    {
        public string GenerateSecurityToken(JwtUser jwtUser);
    }
}
