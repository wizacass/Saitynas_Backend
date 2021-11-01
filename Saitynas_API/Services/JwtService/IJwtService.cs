using Saitynas_API.Models.Authentication;
using Saitynas_API.Models.UserEntity;

namespace Saitynas_API.Services.JwtService
{
    public interface IJwtService
    {
        public string GenerateSecurityToken(User user);

        public RefreshToken GenerateRefreshToken(User user);
    }
}
