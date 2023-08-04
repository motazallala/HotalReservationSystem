using WebApplication2.Models.ViewModels;

namespace WebApplication2.Models.Reservation
{
    public class ReservationIndexViewModel : PageViewModel
    {
        public IEnumerable<ReservationViewModel> Reservations { get; set; }
    }
}