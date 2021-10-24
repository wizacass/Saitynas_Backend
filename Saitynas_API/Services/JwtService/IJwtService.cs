using Saitynas_API.Models.Authentication;
using Saitynas_API.Models.UserEntity;

namespace Saitynas_API.Services.JwtService
{
    public interface IJwtService
    {
        public string GenerateSecurityToken(JwtUser jwtUser);

        public string ValidateToken(string token);

        public RefreshToken GenerateRefreshToken(User user);
    }
}
