using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Saitynas_API.Models.UserEntity;

namespace Saitynas_API.Models.RoleEntity
{
    public enum RoleId
    {
        None = 1,
        Admin,
        Patient,
        Specialist
    }

    public class Role
    {
        [Key]
        [Required]
        public RoleId Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
