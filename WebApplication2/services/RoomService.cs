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

        public RoomService(ApplicationDBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public Task Add(Room room)
        {
            throw new NotImplementedException();
        }

        public Task Update(int id, Room room)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
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

        public Task<Room> GetId(int id)
        {
            throw new NotImplementedException();
        }
    }
}