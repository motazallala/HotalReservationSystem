using WebApplication2.Data.Model;
using WebApplication2.Models.Reservation;
using WebApplication2.Models.RoomType;

namespace WebApplication2.Models.Room
{
    public class RoomViewModel
    {
        public int RoomId { get; set; }
        public int Capacity { get; set; }
        public bool IsTaken { get; set; }
        public double AdultPrice { get; set; }
        public double ChildrenPrice { get; set; }
        public int RoomNumber { get; set; }

        // Foreign key to the RoomType that this room belongs to
        public int RoomTypeId { get; set; }

        public RoomTypeViewModel RoomType { get; set; }

        // One-to-many relationship: One Room has many RoomImages
        // Edit from <RoomViewModel> to <RoomImage>
        public ICollection<RoomImage> RoomImages { get; set; }

        // One-to-many relationship: One Room has many Reservations
        public ICollection<ReservationViewModel> Reservations { get; set; }
    }
}