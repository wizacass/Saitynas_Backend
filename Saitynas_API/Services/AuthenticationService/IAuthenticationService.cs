using System.Threading.Tasks;
using Saitynas_API.Models.Authentication.DTO;

namespace Saitynas_API.Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        Task<AuthenticationDTO> Signup(SignupDTO dto);

        Task<AuthenticationDTO> Login();
        
       // AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress);
       //AuthenticateResponse RefreshToken(string token, string ipAddress);
        // void RevokeToken(string token, string ipAddress);
        // User GetById(int id);
    }
}
