namespace WebApplication2.Data.Model
{
    public class Reservation
    {
        public int ReservationId { get; set; }
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

        public Room? Room { get; set; }

        // One-to-many relationship: One Reservation has many Escorts
        public ICollection<Escort>? Escorts { get; set; }
    }
}