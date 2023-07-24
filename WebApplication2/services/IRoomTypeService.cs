using WebApplication2.Data.Model;

namespace WebApplication2.services
{
    public interface IRoomTypeService
    {
        IEnumerable<RoomType> GetAllRoomTypes();

        RoomType? GetId(int id);

        void Add(RoomType roomType);

        void Update(int id, RoomType roomType);

        void Delete(int id);
    }
}