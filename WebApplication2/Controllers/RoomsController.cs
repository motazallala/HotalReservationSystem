using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models.Room;
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

        public async Task<IActionResult> Index(int id = 1, int pageSize = 5, bool availableOnly = false)
        {
            var searchResults = await _roomService.GetSearchResults<RoomViewModel>(availableOnly);
            var resultsCount = searchResults.Count();
            if (resultsCount <= 0)
            {
                pageSize = 5;
            }
            var pages = (int)Math.Ceiling((double)pageSize / pageSize);
            if (id <= 0 || id > pages)
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
                AvailableOnly = availableOnly
            };

            return View(room);
        }
    }
}