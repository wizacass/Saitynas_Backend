using System.Threading.Tasks;
using Saitynas_API.Models.Authentication.DTO;

namespace Saitynas_API.Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        Task<AuthenticationDTO> Signup(SignupDTO dto);

        Task<AuthenticationDTO> Login(LoginDTO dto);
        
        Task<AuthenticationDTO> RefreshToken(string token);
        
        // void RevokeTokens();
    }
}
