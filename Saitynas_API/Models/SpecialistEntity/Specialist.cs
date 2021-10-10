using System.ComponentModel.DataAnnotations;
using Saitynas_API.Models.SpecialityEntity;
using Saitynas_API.Models.WorkplaceEntity;

namespace Saitynas_API.Models.SpecialistEntity
{
    public class Specialist
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string LastName { get; set; }
        
        [Required]
        public int SpecialityId { get; set; }
        
        [Required]
        public Speciality Speciality { get; set; }

        public int? WorkplaceId { get; set; }
        
        public Workplace Workplace { get; set; }
    }
}
