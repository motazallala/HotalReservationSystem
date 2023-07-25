using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication2.services;

namespace WebApplication2.Controllers
{
    public class RoomImageController : Controller
    {
        private readonly ApplicationDBContext _db;
        private readonly IRoomImageService _roomImageService;

        public RoomImageController(ApplicationDBContext db, IRoomImageService roomImageService)
        {
            _db = db;
            _roomImageService = roomImageService;
        }

        public async Task<IActionResult> RoomImages()
        {
            var roomImgaes = await _db.RoomImages.ToListAsync();
            return View(roomImgaes);
        }


        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction(nameof(Delete));
            }

            var deleteImage = _db.RoomImages.FirstOrDefault(u => u.RoomImageId == Id);
            if (deleteImage == null)
            {
                return NotFound();
            }

            _db.RoomImages.Remove(deleteImage);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Delete));

        }

    }
}
