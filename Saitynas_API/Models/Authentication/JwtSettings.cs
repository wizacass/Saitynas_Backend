namespace Saitynas_API.Models.Authentication
{
    public class JwtSettings
    {
        public int AccessTokenTTL { get; set; }
        
        public int RefreshTokenTTL { get; set; }
    }
}
