using System.Collections.Generic;
using Saitynas_API.Models.Entities.Specialist.DTO;

namespace Saitynas_API.Models.Entities.Speciality.DTO;

public class GetSpecialityDTO
{
    public string Name { get; set; }

    public ICollection<SpecialistDTO> Specialists { get; set; }
}