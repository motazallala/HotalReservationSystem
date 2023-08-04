using WebApplication2.Models.Escort;

namespace WebApplication2.Models.Reservation
{
    public class ReservationInputModel
    {
        public string FullName { get; set; }
        public string Nationality { get; set; }
        public string NationalityId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public bool IsAdult { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public bool Breakfast { get; set; }
        public bool Lunch { get; set; }
        public bool Dinner { get; set; }
        public bool ExtraBed { get; set; }
        public double Price { get; set; }

        // Foreign key for Room
        public int RoomId { get; set; }

        public ICollection<EscortViewModel> Escorts { get; set; }
    }
}