namespace WebApplication2.Data.Model
{
    public class Room
    {
        public int RoomId { get; set; }
        public int Capacity { get; set; }
        public bool IsTaken { get; set; }
        public double AdultPrice { get; set; }
        public double ChildrenPrice { get; set; }
        public int RoomNumber { get; set; }

        // Foreign key to the RoomType that this room belongs to
        public int RoomTypeId { get; set; }

        public RoomType? RoomType { get; set; }

        // One-to-many relationship: One Room has many RoomImages
        public ICollection<RoomImage>? RoomImages { get; set; }

        // One-to-many relationship: One Room has many Reservations
        public ICollection<Reservation>? Reservations { get; set; }
    }
}