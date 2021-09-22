using System.ComponentModel.DataAnnotations;

namespace Saitynas_API.Models.Messages
{
    public class Message
    {
        [Key]
        [Required]
        public int Id { get; init; }
        
        [Required]
        public string Text { get; set; }

        public Message() { }

        public Message(string text)
        {
            Text = text;
        }
    }
}
