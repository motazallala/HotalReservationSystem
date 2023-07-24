using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication2.Data.Model;

namespace WebApplication2.services
{
    public class RoomImageService : IRoomImageService
    {
        private readonly ApplicationDBContext _dbRoomImage;

        public RoomImageService(ApplicationDBContext roomImage)
        {
            _dbRoomImage = roomImage;
        }

        public async Task<RoomImage> AddRoomImageAsync(RoomImage roomImage)
        {
            await _dbRoomImage.RoomImages.AddAsync(roomImage);
            await _dbRoomImage.SaveChangesAsync();
            return roomImage;
        }

        public async Task<List<RoomImage>> GetAllRoomImageAsync(RoomImage roomImage)
        {
            var allRoomImage = await _dbRoomImage.RoomImages.ToListAsync();
            return allRoomImage;
        }

        public async Task RemoveRoomImageAsync(int roomImageId)
        {
            var removeRoomImage = await _dbRoomImage.RoomImages.FirstOrDefaultAsync(u => u.RoomImageId == roomImageId);

            if (removeRoomImage != null)
            {
                _dbRoomImage.RoomImages.Remove(removeRoomImage);
                await _dbRoomImage.SaveChangesAsync();
            }
        }

        public async Task<RoomImage> UpdateRoomImageAsync(int roomImageId, RoomImage roomImage)
        {
            var updateRoomImage = await _dbRoomImage.RoomImages.FirstOrDefaultAsync(u => u.RoomImageId == roomImageId);

            if (roomImage != null)
            {

                roomImage.ImageUrl = updateRoomImage.ImageUrl;

                await _dbRoomImage.SaveChangesAsync();
            }
            else
            {
                return null;
            }

            return roomImage;
        }
    }
}
