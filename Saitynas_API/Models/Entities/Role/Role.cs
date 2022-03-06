using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Saitynas_API.Models.Entities.Role;

public class Role
{
    [Key]
    [Required]
    public RoleId Id { get; set; }

    [Required]
    public string Name { get; set; }

    public ICollection<User.User> Users { get; set; }

    public override string ToString()
    {
        return Name;
    }
}
