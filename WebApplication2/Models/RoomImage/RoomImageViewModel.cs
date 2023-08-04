using WebApplication2.Models.Room;

namespace WebApplication2.Models.RoomImage
{
    public class RoomImageViewModel
    {
        public int RoomImageId { get; set; }
        public string ImageUrl { get; set; }

        // Foreign key to the Room that this image belongs to
        public int RoomId { get; set; }

        public RoomViewModel Room { get; set; }
    }
}