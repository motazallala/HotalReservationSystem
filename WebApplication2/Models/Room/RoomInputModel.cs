using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication2.Data.Model;

namespace WebApplication2.Models.Room
{
    public class RoomInputModel
    {
        public int Capacity { get; set; }
        public bool IsTaken { get; set; }
        public double AdultPrice { get; set; }
        public double ChildrenPrice { get; set; }
        public int RoomNumber { get; set; }

        public List<IFormFile> RoomImages { get; set; }
        public ICollection<RoomImage>? RoomImagesUrl { get; set; }
        public List<SelectListItem>? RoomTypes { get; set; }

        // Foreign key to the RoomType that this room belongs to
        public int RoomTypeId { get; set; }
    }
}