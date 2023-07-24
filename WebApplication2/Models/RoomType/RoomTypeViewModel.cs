using System.ComponentModel;

namespace WebApplication2.Models.RoomType
{
    public class RoomTypeViewModel
    {
        [DisplayName("Room Type ID")]
        public int RoomTypeId { get; set; }

        public string Type { get; set; }
        public string Description { get; set; }
    }
}