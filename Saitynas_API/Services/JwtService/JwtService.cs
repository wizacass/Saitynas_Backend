using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Saitynas_API.Models;
using Saitynas_API.Models.Authentication;
using Saitynas_API.Models.UserEntity;

namespace Saitynas_API.Services.JwtService
{
    public class JwtService : IJwtService
    {
        private SymmetricSecurityKey SecurityKey => new(Encoding.ASCII.GetBytes(_secret));

        private readonly string _secret;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly JwtSettings _settings;

        private const string JwtSecretKey = "JwtSecret";

        public JwtService(IConfiguration config, IOptions<JwtSettings> settings)
        {
            _secret = config[JwtSecretKey] ?? Environment.GetEnvironmentVariable(JwtSecretKey);
            _settings = settings.Value;

            _tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = SecurityKey,
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };
        }

        public string GenerateSecurityToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new ClaimsIdentity(new[]
            {
                new Claim(CustomClaims.TokenID, Guid.NewGuid().ToString()),
                new Claim(CustomClaims.UserID, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.RoleId.ToString())
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.Add(_settings.AccessTokenTTL),
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

        public RefreshToken GenerateRefreshToken(User user)
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                ExpiresAt = DateTime.UtcNow.Add(_settings.RefreshTokenTTL),
                UserId = user.Id
            };

            return refreshToken;
        }
    }
}
