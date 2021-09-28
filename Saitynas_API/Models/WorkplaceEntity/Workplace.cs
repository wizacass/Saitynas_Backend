using System.ComponentModel.DataAnnotations;

namespace Saitynas_API.Models.WorkplaceEntity
{
    public class Workplace
    {
        [Key]
        [Required]
        public int Id { get; init; }

        [Required]
        [StringLength((255))]
        public string Address { get; set; }

        [Required]
        [StringLength(255)]
        public string City { get; set; }
    }
}
