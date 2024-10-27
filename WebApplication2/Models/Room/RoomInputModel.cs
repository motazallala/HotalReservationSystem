using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication2.Models.RoomImage;

namespace WebApplication2.Models.Room
{
    public class RoomInputModel
    {
        public int Capacity { get; set; }

        public bool IsTaken { get; set; }

        public double AdultPrice { get; set; }

        public double ChildrenPrice { get; set; }

        public int RoomNumber { get; set; }

        public List<IFormFile>? RoomImagesFile { get; set; }

        public ICollection<RoomImageViewModel>? RoomImages { get; set; }
        public List<SelectListItem>? RoomTypes { get; set; }

        // Foreign key to the RoomType that this room belongs to
        public int RoomTypeId { get; set; }
    }
}