namespace WebApplication2.Data.Model
{
    public class RoomImage
    {
        public int RoomImageId { get; set; }
        public string ImageUrl { get; set; }

        // Foreign key to the Room that this image belongs to
        public int RoomId { get; set; }

        public Room Room { get; set; }
    }
}