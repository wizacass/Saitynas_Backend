using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Saitynas_API.Exceptions;
using Saitynas_API.Models.Authentication;
using Saitynas_API.Models.Authentication.DTO;
using Saitynas_API.Models.UserEntity;
using Saitynas_API.Services.JwtService;

namespace Saitynas_API.Services.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtService _jwt;

        public AuthenticationService(UserManager<User> userManager, IJwtService jwt)
        {
            _userManager = userManager;
            _jwt = jwt;
        }

        public async Task<AuthenticationDTO> Signup(SignupDTO dto)
        {
            var user = new User(dto);
            var result = await _userManager.CreateAsync(user, dto.Password);
            
            if (!result.Succeeded)
            {
                string error = result.Errors.First().Description;
                throw new AuthenticationException(error);
            }
            
            string accessToken = GenerateAccessToken(user);
            var refreshToken = _jwt.GenerateRefreshToken(user);

            user.RefreshTokens.Add(refreshToken);
            await _userManager.UpdateAsync(user);
            
            return new AuthenticationDTO(accessToken, refreshToken.Token);
        }

        public Task<AuthenticationDTO> Login()
        {
            throw new System.NotImplementedException();
        }
        
        private string GenerateAccessToken(User user)
        {
            return _jwt.GenerateSecurityToken(new JwtUser(user.Email, user.RoleId));
        }
    }
}
