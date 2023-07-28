using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication2.Data.Model;
using WebApplication2.services.Mapping;

namespace WebApplication2.services
{
    public class RoomImageService : IRoomImageService
    {
        private readonly ApplicationDBContext _db;

        public RoomImageService(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task Add(RoomImage roomImage)
        {
            await _db.RoomImages.AddAsync(roomImage);
            await _db.SaveChangesAsync();
        }

        public async Task Remove(int roomImageId)
        {
            var removeRoomImage = await _db.RoomImages.FirstOrDefaultAsync(u => u.RoomImageId == roomImageId);

            if (removeRoomImage != null)
            {
                _db.RoomImages.Remove(removeRoomImage);
                await _db.SaveChangesAsync();
            }
        }

        public async Task Update(int Id, RoomImage roomImage)
        {
            roomImage.RoomImageId = Id;
            var updateRoomImage = await _db.RoomImages.FirstOrDefaultAsync(u => u.RoomImageId == Id);

            if (roomImage != null)
            {
                _db.Update(roomImage);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<List<RoomImage>> GetAllRoomImage()
        {
            var allRoomImage = await _db.RoomImages.ToListAsync();
            return allRoomImage;
        }

        public async Task<IEnumerable<T>> GetRoomImage<T>(int id)
        {
            IQueryable<RoomImage> data = _db.RoomImages;
            data = data.Where(x => x.RoomId == id);

            return await data.ProjectTo<T>().ToListAsync();
        }
    }
}