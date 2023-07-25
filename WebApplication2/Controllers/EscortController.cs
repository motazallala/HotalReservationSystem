using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication2.Data.Model;
using WebApplication2.services;

namespace WebApplication2.Controllers
{
    public class EscortController : Controller
    {
        private readonly ApplicationDBContext _db;
        private readonly EscortService _escortService;
        public EscortController(ApplicationDBContext db, EscortService escortService)
        {
            _db = db;
            _escortService = escortService;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Escort escort)
        {
            if (ModelState.IsValid)
            {
                _db.Escorts.Add(escort);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Add));
            }

            return View(escort);
        }

        public async Task<IActionResult> Escorts()
        {
            var allEscorts = await _db.Escorts.ToListAsync();
            return View(allEscorts);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Edit));
            }

            var escort = await _db.Escorts.FindAsync(id);

            if (escort == null)
            {
                return NotFound();
            }

            return View("Edit", escort);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, Escort editedEscort)
        {
            if (id == null || id <= 0)
            {
                return RedirectToAction(nameof(Edit));
            }

            if (ModelState.IsValid)
            {
                var existingUser = await _db.Escorts.FindAsync(id);

                if (existingUser == null)
                {
                    return NotFound();
                }

                existingUser.FullName = editedEscort.FullName;
                existingUser.Email = editedEscort.Email;
                existingUser.IsAdult = editedEscort.IsAdult;

                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Edit));
            }

            return View("Edit", editedEscort);
        }


        public IActionResult Info(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Info));
            }

            var escort = _db.Escorts.FirstOrDefault(u => u.EscortId == id);

            if (escort == null)
            {
                return NotFound();
            }

            return View("Info", escort);
        }

    }
}
