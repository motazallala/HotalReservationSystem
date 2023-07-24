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
            var room = await _db.Rooms.Include(x => x.Reservations).ThenInclude(x => x.).FirstOrDefaultAsync(x => x.RoomId == id);
            if (room != null)
            {
                if (room.Reservations != null)
                {
                    _db.Reservations.RemoveRange(room.Reservations);
                    await _db.SaveChangesAsync();
                }
                _db.Rooms.Remove(room);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Room>> GetAllRoom()
        {
            return await _db.Rooms.Include(x => x.Reservations).ToListAsync();
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

        public Task Update(int id, Room room)
        {
            room.RoomId = id;
        }
    }
}