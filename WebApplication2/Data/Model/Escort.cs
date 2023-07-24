namespace WebApplication2.Data.Model
{
    public class Escort
    {
        public int EscortId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsAdult { get; set; }

        // Foreign key to the Reservation that this escort belongs to
        public int ReservationId { get; set; }

        public Reservation Reservation { get; set; }
    }
}