using System.ComponentModel.DataAnnotations;
using Saitynas_API.Models.EvaluationEntity.DTO;
using Saitynas_API.Models.SpecialistEntity;
using Saitynas_API.Models.UserEntity;

namespace Saitynas_API.Models.EvaluationEntity
{
    public class Evaluation
    {
        [Key]
        [Required]
        public int Id { get; init; }

        [Required]
        public int Value { get; set; }

        [StringLength(255)]
        public string Comment { get; set; }

        [Required]
        public int SpecialistId { get; set; }

        [Required]
        public Specialist Specialist { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public User User { get; set; }

        public Evaluation() { }

        public Evaluation(EvaluationDTO dto)
        {
            Value = dto.Value;
            Comment = dto.Comment;
        }

        public Evaluation(int id, EvaluationDTO dto) : this(dto)
        {
            Id = id;
        }

        public Evaluation(User user, EvaluationDTO dto) : this(dto)
        {
            User = user;
        }
    }
}
