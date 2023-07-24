using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication2.Data.Model;

namespace WebApplication2.services
{
    public class RoomService : IRoomService
    {
        private readonly ApplicationDBContext _db;

        public RoomService(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task Add(Room room)
        {
            await _db.Rooms.AddAsync(room);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var room = await _db.Rooms
                .Include(x => x.RoomImages)
                .Include(x => x.Reservations)
                .FirstOrDefaultAsync(x => x.RoomId == id);

            if (room != null)
            {
                // Remove the reservations and their room images if there are any
                if (room.Reservations != null)
                {
                    _db.Reservations.RemoveRange(room.Reservations);
                }

                if (room.RoomImages != null)
                {
                    _db.RoomImages.RemoveRange(room.RoomImages);
                }

                // Remove the room
                _db.Rooms.Remove(room);

                // Save all changes using the same _db context instance
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Room>> GetAllRoom()
        {
            return await _db.Rooms.Include(x => x.Reservations).Include(x => x.RoomImages).ToListAsync();
        }

        public async Task<Room> GetId(int id)
        {
            var data = await _db.Rooms.Where(x => x.RoomId == id).FirstOrDefaultAsync();
            if (data != null)
            {
                return data;
            }
            return null;
        }

        public async Task Update(int id, Room room)
        {
            room.RoomId = id;
            _db.Rooms.Add(room);
            await _db.SaveChangesAsync();
        }
    }
}