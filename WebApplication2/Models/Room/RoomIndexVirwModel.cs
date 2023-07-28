using WebApplication2.Models.ViewModels;

namespace WebApplication2.Models.Room
{
    public class RoomIndexVirwModel : PageViewModel
    {
        public IEnumerable<RoomViewModel> Rooms { get; set; }
        public bool AvailableOnly { get; set; }
    }
}