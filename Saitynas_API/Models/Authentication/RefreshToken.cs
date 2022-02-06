using System;
using System.ComponentModel.DataAnnotations;
using Saitynas_API.Models.Entities.User;

namespace Saitynas_API.Models.Authentication;

public class RefreshToken
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string Token { get; set; }

    [Required]
    public DateTime ExpiresAt { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    public DateTime? RevokedAt { get; set; }

    public string ReplacedByToken { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public User User { get; set; }

    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;

    public bool IsRevoked => RevokedAt != null;

    public bool IsActive => !IsRevoked && !IsExpired;

    public RefreshToken()
    {
        CreatedAt = DateTime.UtcNow;
    }

    public void Revoke(string replacedByToken = null)
    {
        RevokedAt = DateTime.UtcNow;
        ReplacedByToken = replacedByToken;
    }
}
