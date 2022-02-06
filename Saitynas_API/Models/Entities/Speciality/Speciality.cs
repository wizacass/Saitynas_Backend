using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Saitynas_API.Models.Entities.Speciality;

public class Speciality
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public ICollection<Specialist.Specialist> Specialists { get; set; }

    public Speciality()
    {
        Specialists = new List<Specialist.Specialist>();
    }
    
    public Speciality(int id, string name) : this()
    {
        Id = id;
        Name = name;
    }
}
