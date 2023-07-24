using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication2.Data.Model;
using WebApplication2.Models.RoomType;
using WebApplication2.services;

namespace WebApplication2.Controllers
{
    public class RoomTypesController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IRoomTypeService _roomTypeService;

        public RoomTypesController(ApplicationDBContext context, IRoomTypeService roomTypeService)
        {
            _context = context;
            _roomTypeService = roomTypeService;
        }

        // GET: RoomTypes
        public IActionResult Index()
        {
            var data = _roomTypeService.GetAllRoomTypes();
            return View(data);
        }

        // GET: RoomTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RoomTypes == null)
            {
                return NotFound();
            }

            var roomType = await _context.RoomTypes
                .FirstOrDefaultAsync(m => m.RoomTypeId == id);
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
        public IActionResult Create([Bind("RoomTypeId,Type,Description")] RoomTypeInput roomTypeIn)
        {
            if (ModelState.IsValid)
            {
                var data = new RoomType
                {
                    Type = roomTypeIn.Type,
                    Description = roomTypeIn.Description,
                };
                _roomTypeService.Add(data);
                return RedirectToAction(nameof(Index));
            }
            return View(roomTypeIn);
        }

        // GET: RoomTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RoomTypes == null)
            {
                return NotFound();
            }

            var roomType = await _context.RoomTypes.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("RoomTypeId,Type,Description")] RoomType roomType)
        {
            if (id != roomType.RoomTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roomType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomTypeExists(roomType.RoomTypeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(roomType);
        }

        // GET: RoomTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RoomTypes == null)
            {
                return NotFound();
            }

            var roomType = await _context.RoomTypes
                .FirstOrDefaultAsync(m => m.RoomTypeId == id);
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
            if (_context.RoomTypes == null)
            {
                return Problem("Entity set 'ApplicationDBContext.RoomTypes'  is null.");
            }
            var roomType = await _context.RoomTypes.FindAsync(id);
            if (roomType != null)
            {
                _context.RoomTypes.Remove(roomType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomTypeExists(int id)
        {
            return (_context.RoomTypes?.Any(e => e.RoomTypeId == id)).GetValueOrDefault();
        }
    }
}