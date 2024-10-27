using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication2.Models.Room
{
    public class RoomWithRoomTypeInputModel
    {
        public RoomInputModel Input { get; set; }
        public List<SelectListItem>? RoomTypes { get; set; }
    }
}