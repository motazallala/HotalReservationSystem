using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models.RoomType
{
    public class RoomTypeInput
    {
        [Required]
        [MaxLength(150, ErrorMessage = "The type is to long")]
        public string Type { get; set; }

        [Required]
        public string Description { get; set; }
    }
}