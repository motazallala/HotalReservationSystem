using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models.Room;
using WebApplication2.Models.RoomType;
using WebApplication2.services;
using WebApplication2.services.Common;

namespace WebApplication2.Controllers
{
    public class RoomsController : Controller
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        public async Task<IActionResult> Index(int id = 1, int pageSize = 5, bool availableOnly = false, int minCapacity = 0, int? type = null)
        {
            var searchResults = await _roomService.GetSearchResults<RoomViewModel>(availableOnly, minCapacity, type);
            var resultsCount = searchResults.Count();
            if (pageSize <= 0)
            {
                pageSize = 5;
            }
            var pages = (int)Math.Ceiling((double)resultsCount / pageSize);
            if (id > resultsCount || id < 1)
            {
                id = 1;
            }

            var room = new RoomIndexVirwModel
            {
                PagesCount = pages,
                CurrentPage = id,
                Rooms = searchResults.GetPageItems(id, pageSize),
                Controller = "Rooms",
                Action = nameof(Index),
                AvailableOnly = availableOnly,
                MinCapacity = minCapacity,
                MaxCapacity = await _roomService.GetMaxCapacity(),
                roomType = type,
                roomTypes = await _roomService.GetRoomTypeList<RoomTypeViewModel>()
            };

            return View(room);
        }
    }
}