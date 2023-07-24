namespace WebApplication2.Data.Model
{
    public class RoomType
    {
        public int RoomTypeId { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        // One-to-many relationship: One RoomType has many Rooms
        public virtual ICollection<Room> Rooms { get; set; }
    }
}