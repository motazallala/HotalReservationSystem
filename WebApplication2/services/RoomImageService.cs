using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Composition;
using WebApplication1.Data;
using WebApplication2.Data.Model;

namespace WebApplication2.services
{
    public class RoomImageService : IRoomImageService
    {
        private readonly ApplicationDBContext _db;

        public RoomImageService(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task AddRoomImage(RoomImage roomImage)
        {
            await _db.RoomImages.AddAsync(roomImage);
            await _db.SaveChangesAsync();
        }

        public async Task<List<RoomImage>> GetAllRoomImage(RoomImage roomImage)
        {
            var allRoomImage = await _db.RoomImages.ToListAsync();
            return allRoomImage;
        }

        public async Task RemoveRoomImage(int roomImageId)
        {
            var removeRoomImage = await _db.RoomImages.FirstOrDefaultAsync(u => u.RoomImageId == roomImageId);

            if (removeRoomImage != null)
            {
                _db.RoomImages.Remove(removeRoomImage);
                await _db.SaveChangesAsync();
            }
        }

        public async Task UpdateRoomImage(int Id, RoomImage roomImage)
        {
            roomImage.RoomImageId = Id;
            var updateRoomImage = await _db.RoomImages.FirstOrDefaultAsync(u => u.RoomImageId == Id);

            if (roomImage != null)
            {
                _db.Update(roomImage);
                await _db.SaveChangesAsync();
            }
        }
    }
}
