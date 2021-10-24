using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Saitynas_API.Models.Authentication;

namespace Saitynas_API.Services.JwtService
{
    public class JwtService : IJwtService
    {
        private SymmetricSecurityKey SecurityKey => new(Encoding.ASCII.GetBytes(_secret));

        private readonly string _secret;
        private readonly int _expirationDays;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public JwtService(IConfiguration config)
        {
            var configSection = config.GetSection("Jwt");

            _secret = config["JwtSecret"] ?? Environment.GetEnvironmentVariable("JwtSecret");
            _expirationDays = int.Parse(configSection.GetSection("ExpirationDays").Value);

            _tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = SecurityKey,
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };
        }

        public string GenerateSecurityToken(JwtUser jwtUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("token_id", Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Email, jwtUser.Email),
                    new Claim(ClaimTypes.Role, jwtUser.RoleId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string ValidateToken(string token)
        {
            if (token == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                string email = jwtToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;

                return email;
            }
            catch
            {
                return null;
            }
        }
    }
}
