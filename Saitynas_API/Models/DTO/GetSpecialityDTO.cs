using System.Collections.Generic;
using Saitynas_API.Models.SpecialistsEntity.DTO;

namespace Saitynas_API.Models.DTO
{
    public class GetSpecialityDTO
    {
        public string Name { get; set; }
        
        public ICollection<SpecialistDTO> Specialists { get; set; }
    }
}
