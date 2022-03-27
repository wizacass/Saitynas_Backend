using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Saitynas_API.Models.Entities.Specialist;

public enum SpecialistStatusId
{
    Offline = 1,
    Available,
    Busy
}

public class SpecialistStatus
{
    [Key]
    [Required]
    public SpecialistStatusId Id { get; set; }

    [Required]
    public string Name { get; set; }

    public ICollection<Specialist> Specialists { get; set; }

    public override string ToString()
    {
        return Name;
    }
}
