using System;

namespace Saitynas_API.Models.Authentication;

public class JwtSettings
{
    public TimeSpan AccessTokenTTL { get; set; }

    public TimeSpan RefreshTokenTTL { get; set; }
}