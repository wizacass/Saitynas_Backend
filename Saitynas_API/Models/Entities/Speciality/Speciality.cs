using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Saitynas_API.Models.Entities.Speciality
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
        
        public ICollection<Specialist.Specialist> Specialists { get; set; }

        public Speciality()
        {
            Specialists = new List<Specialist.Specialist>();
        }

        public Speciality(int id, string name)
        {
            Id = id;
            Name = name;
            Specialists = new List<Specialist.Specialist>();
        }
    }
}
