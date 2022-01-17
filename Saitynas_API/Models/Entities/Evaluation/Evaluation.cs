using System;
using System.ComponentModel.DataAnnotations;
using Saitynas_API.Models.Entities.Evaluation.DTO;

namespace Saitynas_API.Models.Entities.Evaluation;

public class Evaluation
{
    [Key]
    [Required]
    public int Id { get; init; }

    [Required]
    public int? Value { get; set; }

    [StringLength(255)]
    public string Comment { get; set; }

    public DateTime CreatedAt { get; set; }

    [Required]
    public int SpecialistId { get; set; }

    [Required]
    public Specialist.Specialist Specialist { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public User.User User { get; set; }

    public Evaluation()
    {
        CreatedAt = DateTime.UtcNow;
    }

    public Evaluation(EvaluationDTO dto) : this()
    {
        Value = dto.Value;
        Comment = dto.Comment;
        SpecialistId = dto.SpecialistId;
    }

    public Evaluation(int id, EvaluationDTO dto) : this(dto)
    {
        Id = id;
    }

    public Evaluation(User.User user, EvaluationDTO dto) : this(dto)
    {
        User = user;
    }

    public Evaluation(EditEvaluationDTO dto)
    {
        Comment = dto.Comment;
        Value = dto.Value;
    }

    public void Update(Evaluation e)
    {
        Value = e.Value ?? Value;
        Comment = e.Comment ?? Comment;
    }
}