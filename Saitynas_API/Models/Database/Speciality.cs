using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Saitynas_API.Models.SpecialistsEntity;

namespace Saitynas_API.Models.Database
{
    public enum SpecialityId
    {
        Other = 1,
        Allergologist,
        Cardiologist,
        Dermatologist,
        Endocrinologist,
        Gastroenterologist,
        GeneralPractician,
        Surgeon,
        Hematologist,
        Immunologist,
        Nephrologist,
        Neurologist,
        Gynecologist,
        Oncologist,
        Ophthalmologist,
        Otorhinolaryngologist,
        Pediatrician,
        Pathologist,
        Psychiatrist,
        Rheumatologist,
        Stomatologist,
        Urologist,
        Venereologist
    }
    
    public class Speciality
    {
        [Key]
        [Required]
        public SpecialityId Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public ICollection<Specialist> Specialists { get; set; }
    }
}
