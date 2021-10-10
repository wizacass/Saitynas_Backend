using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Saitynas_API.Models.Authentication.DTO;
using Saitynas_API.Models.DTO;
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

        public User() { }

        public User(SignupDTO dto)
        {
            Email = dto.Email;
            RoleId = (RoleId)dto.Role;
            RegistrationDate = DateTime.Now;
        }
    }
}
