using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Saitynas_API.Models.Authentication;

namespace Saitynas_API.Services.JwtService
{
    public class JwtService : IJwtService
    {
        private readonly string _secret;
        private readonly int _expirationDays;

        public JwtService(IConfiguration config)
        {
            var configSection = config.GetSection("Jwt");

            _secret = config["JwtSecret"] ?? Environment.GetEnvironmentVariable("JwtSecret");
            _expirationDays = int.Parse(configSection.GetSection("ExpirationDays").Value);
        }

        public string GenerateSecurityToken(JwtUser jwtUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, jwtUser.Email),
                    new Claim(ClaimTypes.Role, jwtUser.RoleId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(_expirationDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
