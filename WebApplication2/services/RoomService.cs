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

        public async Task<IEnumerable<T>> GetSearchResults<T>(bool availableOnly = false, RoomType[] types = null, int? minCapacity = null)
        {
            IQueryable<Room> result = _db.Rooms;
            if (availableOnly)
            {
                result = result.Where(x => !x.Reservations.Any(y => y.CheckIn <= DateTime.Today
                                                                 && y.CheckOut > DateTime.Today));
            }

            if (types != null && (types?.Count() ?? 0) > 0)
            {
                result = result.Where(x => types.Contains(x.RoomType));
            }

            if (minCapacity != null && minCapacity > 0)
            {
                result = result.Where(x => x.Capacity > minCapacity);
            }
            return await result.OrderBy(x => x.RoomNumber).ProjectTo<T>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public Task<Room> GetId(int id)
        {
            throw new NotImplementedException();
        }
    }
}