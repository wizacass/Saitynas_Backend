using System;
using System.ComponentModel.DataAnnotations;

namespace Saitynas_API.Models.Entities.Patient;

public class Patient
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(255)]
    public string LastName { get; set; }
    
    [Required]
    public DateOnly BirthDate { get; set; }
    
    [Required]
    [StringLength(255)]
    public string City { get; set; }

    public int UserId { get; set; }

    public User.User User { get; set; }
    
    public Patient() { }
}
