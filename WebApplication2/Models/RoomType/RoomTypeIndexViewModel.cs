using WebApplication2.Models.ViewModels;

namespace WebApplication2.Models.RoomType
{
    public class RoomTypeIndexViewModel : PageViewModel
    {
        public IEnumerable<RoomTypeViewModel> RoomTypes { get; set; }
    }
}