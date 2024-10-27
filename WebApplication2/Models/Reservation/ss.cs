using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication2.Models.Reservation
{
    public class ss
    {
        public ReservationInputModel Input { get; set; }
        public List<SelectListItem>? Rooms { get; set; }
    }
}