using System;
using System.ComponentModel.DataAnnotations;
using Saitynas_API.Models.UserEntity;

namespace Saitynas_API.Models.Authentication
{
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
        
        [Required]
        public DateTime? Revoked { get; set; }
        
        [Required]
        public string ReplacedByToken { get; set; }
        
        [Required]
        public int UserId { get; set; }
        
        [Required]
        public User User { get; set; }
        
        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
        
        public bool IsRevoked => Revoked != null;
        
        public bool IsActive => !IsRevoked && !IsExpired;

        public RefreshToken()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}
