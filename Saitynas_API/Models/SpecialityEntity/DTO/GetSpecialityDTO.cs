using System.Collections.Generic;
using Saitynas_API.Models.SpecialistEntity.DTO;

namespace Saitynas_API.Models.SpecialityEntity.DTO
{
    public class GetSpecialityDTO
    {
        public string Name { get; set; }
        
        public ICollection<SpecialistDTO> Specialists { get; set; }
    }
}
