using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Data.Model;
using WebApplication2.Models.Reservation;
using WebApplication2.services;
using WebApplication2.services.Common;

namespace WebApplication2.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly WebApplication2DBContext _context;
        private readonly IReservationsService _reservationsService;
        private readonly IRoomService _roomService;

        public ReservationsController(WebApplication2DBContext context, IReservationsService reservationsService, IRoomService roomService)
        {
            _context = context;
            _reservationsService = reservationsService;
            _roomService = roomService;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var webApplication2DBContext = _context.Reservations.Include(r => r.Room);
            return View(await webApplication2DBContext.ToListAsync());
        }

        public async Task<IActionResult> Index2(int id = 1, int pageSize = 5)
        {
            var searchResults = await _reservationsService.GetSearchResults<ReservationViewModel>();
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
            var reservation = new ReservationIndexViewModel
            {
                PagesCount = pages,
                CurrentPage = id,
                Controller = "Reservations",
                Action = nameof(Index2),
                Reservations = searchResults.GetPageItems(id, pageSize)
            };
            return View(reservation);
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reservations == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.ReservationId == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public async Task<IActionResult> Create()
        {
            var allRoomAvailable = await _roomService.GetAllRoom();
            var model = new ss
            {
                Rooms = allRoomAvailable.Select(rt => new SelectListItem
                {
                    Value = rt.RoomId.ToString(),
                    Text = rt.RoomNumber.ToString()
                }).ToList()
            };
            return View(model);
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomId", reservation.RoomId);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reservations == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomId", reservation.RoomId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReservationId,FullName,Nationality,NationalityId,PhoneNumber,Email,IsAdult,CheckIn,CheckOut,Breakfast,Lunch,Dinner,ExtraBed,Price,RoomId")] Reservation reservation)
        {
            if (id != reservation.ReservationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.ReservationId))
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
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomId", reservation.RoomId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reservations == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.ReservationId == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reservations == null)
            {
                return Problem("Entity set 'WebApplication2DBContext.Reservations'  is null.");
            }
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return (_context.Reservations?.Any(e => e.ReservationId == id)).GetValueOrDefault();
        }

        public IActionResult GetRoomCapacity(int roomId)
        {
            var roomCapacity = _roomService.GetRoomCapacity(roomId);  // Implement this method
            return Json(roomCapacity);
        }
    }
}