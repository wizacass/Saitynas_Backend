using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Saitynas_API.Models.SpecialistEntity;

namespace Saitynas_API.Models.SpecialityEntity
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
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public ICollection<Specialist> Specialists { get; set; }

        public Speciality()
        {
            Specialists = new List<Specialist>();
        }

        public Speciality(int id, string name)
        {
            Id = id;
            Name = name;
            Specialists = new List<Specialist>();
        }
    }
}
