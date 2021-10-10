using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Saitynas_API.Models.SpecialistEntity;
using Saitynas_API.Models.WorkplaceEntity.DTO;

namespace Saitynas_API.Models.WorkplaceEntity
{
    public class Workplace
    {
        [Key]
        [Required]
        public int Id { get; init; }

        [Required]
        [StringLength((255))]
        public string Address { get; set; }

        [Required]
        [StringLength(255)]
        public string City { get; set; }
        
        public ICollection<Specialist> Specialists { get; set; }

        public Workplace()
        {
            Specialists = new List<Specialist>();
        }
        
        public Workplace(WorkplaceDTO dto)
        {
            Address = dto.Address;
            City = dto.City;
            Specialists = new List<Specialist>();
        }

        public Workplace(int id, WorkplaceDTO dto) : this(dto)
        {
            Id = id;
        }

        public void Update(Workplace w)
        {
            Address = w.Address ?? Address;
            City = w.City ?? City;
        }
    }
}
