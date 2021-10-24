using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
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
        //TODO: Fix expiration date for tokens
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
            var claims = new ClaimsIdentity(new[]
            {
                new Claim("token_id", Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, jwtUser.Email),
                new Claim(ClaimTypes.Role, jwtUser.RoleId.ToString())
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
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
        
        public RefreshToken GenerateRefreshToken()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                ExpiresAt = DateTime.UtcNow.AddDays(7),
            };

            return refreshToken;
        }
    }
}
