using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Data;
using WebApplication2.Data.Model;
using WebApplication2.Models.RoomType;
using WebApplication2.services;
using WebApplication2.services.Common;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class RoomTypesController : Controller
    {
        private readonly WebApplication2DBContext _context;
        private readonly IRoomTypeService _roomTypeService;

        public RoomTypesController(WebApplication2DBContext context, IRoomTypeService roomTypeService)
        {
            _context = context;
            _roomTypeService = roomTypeService;
        }

        // GET: RoomTypes
        public async Task<IActionResult> Index(int id = 1, int pageSize = 5, string search = "")
        {
            if (pageSize <= 0)
            {
                pageSize = 5;
            }
            int pageCount = (int)Math.Ceiling((double)_roomTypeService.CountAllRoomType(search) / pageSize);
            if (id > pageCount || id < 1)
            {
                id = 1;
            }

            var indexdRoomType = new RoomTypeIndexViewModel
            {
                CurrentPage = id,
                PagesCount = pageCount,
                RoomTypes = await _roomTypeService.GetAllRoomTypes<RoomTypeViewModel>(search).GetPageItems(id, pageSize),
                Action = nameof(Index),
                Controller = "RoomTypes",
                searchString = search,
            };
            return View(indexdRoomType);
        }

        // GET: RoomTypes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomType = await _roomTypeService.GetRoomType<RoomTypeViewModel>(id);
            if (roomType == null)
            {
                return NotFound();
            }

            return View(roomType);
        }

        // GET: RoomTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RoomTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomTypeId,Type,Description")] RoomTypeInput roomTypeIn)
        {
            if (ModelState.IsValid)
            {
                var data = new RoomType
                {
                    Type = roomTypeIn.Type,
                    Description = roomTypeIn.Description,
                };
                await _roomTypeService.Add(data);
                return RedirectToAction(nameof(Index));
            }
            return View(roomTypeIn);
        }

        // GET: RoomTypes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var roomType = await _roomTypeService.GetRoomType<RoomTypeInput>(id);
            if (roomType == null)
            {
                return NotFound();
            }
            return View(roomType);
        }

        // POST: RoomTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RoomTypeInput input)
        {
            var uRoomType = await _roomTypeService.GetRoomType<RoomTypeInput>(id);
            if (uRoomType == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            var roomType = new RoomType
            {
                Type = input.Type,
                Description = input.Description,
            };
            await _roomTypeService.Update(id, roomType);
            return RedirectToAction(nameof(Index));
        }

        // GET: RoomTypes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomType = await _roomTypeService.GetRoomType<RoomTypeViewModel>(id);
            if (roomType == null)
            {
                return NotFound();
            }

            return View(roomType);
        }

        // POST: RoomTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _roomTypeService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool RoomTypeExists(int id)
        {
            return (_context.RoomTypes?.Any(e => e.RoomTypeId == id)).GetValueOrDefault();
        }
    }
}