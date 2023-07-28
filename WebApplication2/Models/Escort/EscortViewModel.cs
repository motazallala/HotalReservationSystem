using WebApplication2.Models.Reservation;

namespace WebApplication2.Models.Escort
{
    public class EscortViewModel
    {
        public string FullName { get; set; }
        public bool IsAdult { get; set; }
        public ReservationViewModel Reservation { get; set; }
    }
}