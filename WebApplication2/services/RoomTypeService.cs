using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Data.Model;

namespace WebApplication2.services
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly WebApplication2DBContext _db;
        private readonly IMapper _mapper;

        public RoomTypeService(WebApplication2DBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task Add(RoomType roomType)
        {
            await _db.RoomTypes.AddAsync(roomType);
            await _db.SaveChangesAsync();
        }

        public async Task Update(int id, RoomType roomType)
        {
            roomType.RoomTypeId = id;
            var roomTypeToChange = await _db.RoomTypes.AsNoTracking().FirstOrDefaultAsync(e => e.RoomTypeId == id);
            if (roomTypeToChange != null)
            {
                _db.RoomTypes.Update(roomType);
                _db.SaveChanges();
            }
        }

        public async Task Delete(int id)
        {
            var data = await _db.RoomTypes.FirstOrDefaultAsync(x => x.RoomTypeId == id);
            if (data != null)
            {
                _db.RoomTypes.Remove(data);
                await _db.SaveChangesAsync();
            }
        }

        public int CountAllRoomType()
        {
            return _db.RoomTypes.Count();
        }

        public int CountAllRoomType(string searchText)
        {
            return _db.RoomTypes.Where(x => x.Type.Contains(searchText)).Count();
        }

        public async Task<IEnumerable<RoomType>> GetAllRoomTypes()
        {
            return await _db.RoomTypes.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllRoomTypes<T>(string searchText)
        {
            IQueryable<RoomType> data = _db.RoomTypes;
            data = data.Where(x => x.Type.Contains(searchText));

            return await data.ProjectTo<T>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<T> GetRoomType<T>(int id)
        {
            return await this._db.RoomTypes.Where(x => x.RoomTypeId == id).ProjectTo<T>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }
    }
}