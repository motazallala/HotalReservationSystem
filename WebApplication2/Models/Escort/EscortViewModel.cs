using WebApplication2.Data.Model;

namespace WebApplication2.Models.Escort
{
    public class EscortViewModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsAdult { get; set; }
        public Reservation Reservation { get; set; }
    }
}
