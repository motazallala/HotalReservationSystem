using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication2.Models.RoomType;
using WebApplication2.Models.ViewModels;

namespace WebApplication2.Models.Room
{
    public class RoomIndexVirwModel : PageViewModel
    {
        public IEnumerable<RoomViewModel> Rooms { get; set; }
        public bool AvailableOnly { get; set; }
        public int MinCapacity { get; set; }
        public int MaxCapacity { get; set; }
        public int? roomType { get; set; }

        public IEnumerable<RoomTypeViewModel> roomTypes { get; set; }

        public List<SelectListItem> GetCapacitySelectList()
        {
            return Enumerable.Range(1, MaxCapacity).Select(x =>
            new SelectListItem
            {
                Value = x.ToString(),
                Text = x.ToString(),
                Selected = x == MinCapacity,
            }).ToList();
        }

        public List<SelectListItem> GetRoomTypeSelectList()
        {
            return roomTypes
                .Select(rt => new SelectListItem
                {
                    Value = rt.RoomTypeId.ToString(),
                    Text = rt.Type,
                    Selected = rt.RoomTypeId == roomType,
                })
                .ToList();
        }
    }
}