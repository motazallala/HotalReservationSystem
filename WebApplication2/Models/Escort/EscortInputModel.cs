using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models.Escort
{
    public class EscortInputModel
    {

        [Required]
        [MaxLength(100,ErrorMessage = "The name is to long")]
        [Display(Name ="FullName")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "IsAdult")]
        public bool IsAdult { get; set; }

    }
}
