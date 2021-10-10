using System.ComponentModel.DataAnnotations;

namespace Saitynas_API.Models.UserEntity
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        
        public string Address { get; set; }

        public string City { get; set; }
    }
}
