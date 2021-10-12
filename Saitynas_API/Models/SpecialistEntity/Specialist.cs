using System.ComponentModel.DataAnnotations;
using Saitynas_API.Models.SpecialityEntity;
using Saitynas_API.Models.WorkplaceEntity;

namespace Saitynas_API.Models.SpecialistEntity
{
    public class Specialist
    {
        [Key]
        public int Id { get; init; }
        
        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string LastName { get; set; }
        
        public string Address { get; set; }
        
        [Required]
        public int? SpecialityId { get; set; }
        
        [Required]
        public Speciality Speciality { get; set; }

        public int? WorkplaceId { get; set; }
        
        public Workplace Workplace { get; set; }

        public void Update(Specialist s)
        {
            FirstName = s.FirstName ?? FirstName;
            LastName = s.LastName ?? LastName;
            Address = s.Address ?? Address;
            SpecialityId = s.SpecialityId ?? SpecialityId;
            WorkplaceId = s.WorkplaceId ?? WorkplaceId;
        }
    }
}
