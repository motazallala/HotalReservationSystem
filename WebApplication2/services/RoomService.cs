using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication2.Data.Model;

namespace WebApplication2.services
{
    public class RoomService : IRoomService
    {
        private readonly ApplicationDBContext _db;
        private readonly IMapper _mapper;
        private readonly IRoomImageService _roomImageService;

        public RoomService(ApplicationDBContext db, IMapper mapper, IRoomImageService roomImageService)
        {
            _db = db;
            _mapper = mapper;
            _roomImageService = roomImageService;
        }

        public async Task Add(Room room, List<IFormFile> roomImages)
        {
            await _db.Rooms.AddAsync(room);
            await _db.SaveChangesAsync();

            if (roomImages != null && roomImages.Any())
            {
                await _roomImageService.AddRange(room.RoomId, roomImages);
            }
        }

        public async Task Update(int id, Room room)
        {
            room.RoomId = id;
            var roomToChange = await _db.Rooms.AsNoTracking().FirstOrDefaultAsync(x => x.RoomId == id);
            if (roomToChange != null)
            {
                _db.Rooms.Update(room);
                await _db.SaveChangesAsync();
            }
        }

        public async Task Update(int id, Room room, List<IFormFile> roomImages)
        {
            room.RoomId = id;
            var roomToChange = await _db.Rooms.AsNoTracking().FirstOrDefaultAsync(x => x.RoomId == id);
            if (roomToChange != null)
            {
                // Update the existing room information
                _db.Rooms.Update(room);

                // Check if there are new images
                if (roomImages != null && roomImages.Any())
                {
                    await _roomImageService.AddRange(room.RoomId, roomImages);
                }
                await _db.SaveChangesAsync();
            }
        }


        public async Task Delete(int id)
        {
            // Retrieve the room from the database
            var room = await _db.Rooms.FindAsync(id);

            if (room == null)
            {
                throw new ArgumentException($"Room with ID {id} not found.");
            }

            // Remove the room from the database
            _db.Rooms.Remove(room);

            // Save changes to the database
            await _db.SaveChangesAsync();

        }

        public Task<IEnumerable<Room>> GetAllRoom()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetSearchResults<T>(bool availableOnly = false, int? minCapacity = null, int? type = null)
        {
            IQueryable<Room> result = _db.Rooms;
            if (availableOnly)
            {
                result = result.Where(x => !x.Reservations.Any(y => y.CheckIn <= DateTime.Today
                                                                 && y.CheckOut > DateTime.Today));
            }

            if (type != null && type > 0)
            {
                result = result.Where(x => x.RoomTypeId == type);
            }

            if (minCapacity != null && minCapacity > 0)
            {
                result = result.Where(x => x.Capacity >= minCapacity.Value);
            }
            return await result.OrderBy(x => x.RoomNumber).ProjectTo<T>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<int> GetMaxCapacity()
        {
            return await _db.Rooms.AsNoTracking().
                           OrderByDescending(x => x.Capacity).
                           Select(x => x.Capacity).
                           FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetRoomTypeList<T>()
        {
            return await _db.RoomTypes.ProjectTo<T>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<T> GetId<T>(int id)
        {
            return await _db.Rooms.Where(x => x.RoomId == id).ProjectTo<T>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }

        public async Task<bool> IsRoomNumberFree(int number, int? roomId = null)
        {
            return await _db.Rooms.AsNoTracking().Where(x => x.RoomId != roomId).AnyAsync(x => x.RoomNumber == number);
        }
    }
}