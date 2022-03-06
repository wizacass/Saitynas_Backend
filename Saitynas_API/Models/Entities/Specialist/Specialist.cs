using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Saitynas_API.Models.Entities.Specialist.DTO;

namespace Saitynas_API.Models.Entities.Specialist;

public class Specialist
{
    [Key]
    [Required]
    public int Id { get; init; }

    [Required]
    [StringLength(255)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(255)]
    public string LastName { get; set; }

    [StringLength(255)]
    public string Address { get; set; }
    
    [StringLength(255)]
    public string City { get; set; }

    [Required]
    public int? SpecialityId { get; set; }

    [Required]
    public Speciality.Speciality Speciality { get; set; }

    public int? WorkplaceId { get; set; }

    public Workplace.Workplace Workplace { get; set; }

    public ICollection<Evaluation.Evaluation> Evaluations { get; set; }

    public int UserId { get; set; }
    
    public User.User User { get; set; }

    public Specialist() { }

    public Specialist(CreateSpecialistDTO dto, User.User user = null)
    {
        FirstName = dto.FirstName;
        LastName = dto.LastName;
        City = dto.City;
        Address = "remote";
        SpecialityId = dto.SpecialityId;
        UserId = user?.Id ?? 0;
        User = user;
        
        Evaluations = new List<Evaluation.Evaluation>();
    }

    public Specialist(int id, EditSpecialistDTO dto)
    {
        Id = id;
        FirstName = dto.FirstName;
        LastName = dto.LastName;
        Address = dto.Address;
        SpecialityId = dto.SpecialityId;
        WorkplaceId = dto.WorkplaceId;
    }

    public void Update(Specialist s)
    {
        FirstName = s.FirstName ?? FirstName;
        LastName = s.LastName ?? LastName;
        Address = s.Address ?? Address;
        SpecialityId = s.SpecialityId ?? SpecialityId;
        WorkplaceId = s.WorkplaceId ?? WorkplaceId;
    }
}
