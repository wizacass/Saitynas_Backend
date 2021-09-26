using System;
using System.ComponentModel.DataAnnotations;
using Saitynas_API.Models.EvaluationEntity.DTO;
using Saitynas_API.Models.SpecialistEntity;
using Saitynas_API.Models.UserEntity;

namespace Saitynas_API.Models.EvaluationEntity
{
    public class Evaluation : IEquatable<Evaluation>
    {
        [Key]
        [Required]
        public int Id { get; init; }
        
        [Required]
        public int Value { get; set; }
        
        public string Comment { get; set; }
        
        [Required]
        public int SpecialistId { get; set; }

        [Required]
        public Specialist Specialist { get; set; }
        
        [Required]
        public int PatientId { get; set; }
        
        [Required]
        public Patient Patient { get; set; }

        public Evaluation() { }

        public Evaluation(EvaluationDTO dto)
        {
            Value = dto.Value;
            Comment = dto.Comment;
        }
        
        public Evaluation(int id, EvaluationDTO dto)
        {
            Id = id;
            Value = dto.Value;
            Comment = dto.Comment;
        }

        public bool Equals(Evaluation other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            
            return obj.GetType() == this.GetType() && Equals((Evaluation)obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
