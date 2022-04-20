using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Saitynas_API.Models.Entities.Consultation;

[Index(nameof(PublicId), IsUnique = true)]
public class Consultation
{
    [Key]
    [Required]
    public int Id { get; init; }
    
    [Required]
    public Guid PublicId { get; set; }

    [Required]
    [StringLength(255)]
    public string PatientDeviceToken { get; set; }

    [StringLength(255)]
    public string SpecialistDeviceToken { get; set; }

    [Required]
    public DateTime RequestedAt { get; set; }
    
    public DateTime? StartedAt { get; set; }
    
    public DateTime? FinishedAt { get; set; }

    [DefaultValue(false)]
    public bool IsCancelled { get; set; } = false;
    
    public int? RequestedSpecialityId { get; set; }
    
    public Speciality.Speciality RequestedSpeciality { get; set; }
    
    [Required]
    public int PatientId { get; set; }

    [Required]
    public Patient.Patient Patient { get; set; }
    
    public int? SpecialistId { get; set; }
    
    public Specialist.Specialist Specialist { get; set; }

    public Consultation()
    {
        PublicId = Guid.NewGuid();
        RequestedAt = DateTime.UtcNow;
    }
}
