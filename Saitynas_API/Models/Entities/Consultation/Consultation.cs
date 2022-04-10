using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Saitynas_API.Models.Entities.Consultation;

public class Consultation
{
    [Key]
    [Required]
    public int Id { get; init; }

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
    
    [Required]
    public int PatientId { get; set; }

    [Required]
    public Patient.Patient Patient { get; set; }
    
    public int? SpecialistId { get; set; }
    
    public Specialist.Specialist Specialist { get; set; }

    public Consultation()
    {
        RequestedAt = DateTime.UtcNow;
    }
}
