using WebApplication1.Data;
using WebApplication2.Data.Model;

namespace WebApplication2.services
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly ApplicationDBContext _db;

        public RoomTypeService(ApplicationDBContext db)
        {
            _db = db;
        }

        public void Add(RoomType roomType)
        {
            _db.RoomTypes.Add(roomType);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var data = _db.RoomTypes.FirstOrDefault(x => x.RoomTypeId == id);
            if (data != null)
            {
                _db.RoomTypes.Remove(data);
                _db.SaveChanges();
            }
        }

        public IEnumerable<RoomType> GetAllRoomTypes()
        {
            var result = _db.RoomTypes.ToList();
            return result;
        }

        public RoomType? GetId(int id)
        {
            return _db.RoomTypes.FirstOrDefault(x => x.RoomTypeId == id);
        }

        public void Update(int id, RoomType roomType)
        {
            roomType.RoomTypeId = id;
            _db.SaveChanges();
            return;
        }
    }
}