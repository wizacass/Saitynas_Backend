using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Saitynas_API.Models.Authentication;
using Saitynas_API.Models.Authentication.DTO;
using Saitynas_API.Models.EvaluationEntity;
using Saitynas_API.Models.RoleEntity;

namespace Saitynas_API.Models.UserEntity
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

        public Role Role { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }
        
        public ICollection<RefreshToken> RefreshTokens { get; set; }

        public ICollection<Evaluation> Evaluations { get; set; }

        public User()
        {
            RegistrationDate = DateTime.UtcNow;
            Evaluations = new List<Evaluation>();
            RefreshTokens = new List<RefreshToken>();
        }

        public User(SignupDTO dto) : this()
        {
            Email = dto.Email;
            RoleId = (RoleId)dto.Role;
        }
    }
}
