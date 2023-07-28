using WebApplication2.Data.Model;

namespace WebApplication2.services
{
    public interface IRoomTypeService
    {
        Task Add(RoomType roomType);

        Task Update(int id, RoomType roomType);

        Task Delete(int id);

        public int CountAllRoomType();

        public int CountAllRoomType(string searchText);

        Task<IEnumerable<RoomType>> GetAllRoomTypes();

        Task<IEnumerable<T>> GetAllRoomTypes<T>(string searchText);

        Task<T> GetRoomType<T>(int id);
    }
}