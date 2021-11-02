using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Saitynas_API.Models.Authentication;
using Saitynas_API.Models.Authentication.DTO;
using Saitynas_API.Models.Entities.Role;

namespace Saitynas_API.Models.Entities.User
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        [DefaultValue(RoleId.None)]
        public RoleId RoleId { get; set; } = RoleId.None;

        public Role.Role Role { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }
        
        public List<RefreshToken> RefreshTokens { get; set; }

        public ICollection<Evaluation.Evaluation> Evaluations { get; set; }
        
        public int? SpecialistId { get; set; }
        
        public Specialist.Specialist Specialist { get; set; }

        public User()
        {
            RegistrationDate = DateTime.UtcNow;
            Evaluations = new List<Evaluation.Evaluation>();
            RefreshTokens = new List<RefreshToken>();
        }

        public User(SignupDTO dto) : this()
        {
            Email = dto.Email;
            RoleId = (RoleId)dto.Role;
        }

        public void RemoveOldTokens()
        {
            RefreshTokens?.RemoveAll(t => !t.IsActive && t.IsExpired);
        }

        public void RevokeAllTokens()
        {
            foreach (var token in RefreshTokens.Where(token => token.IsActive))
            {
                token.Revoke();
            }
        }
    }
}
