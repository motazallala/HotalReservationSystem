using WebApplication2.Data.Model;

namespace WebApplication2.services
{
    public interface IRoomTypeService
    {
        public Task Add(RoomType roomType);

        public Task Update(int id, RoomType roomType);

        public Task Delete(int id);

        public int CountAllRoomType();

        public int CountAllRoomType(string searchText);

        public Task<IEnumerable<RoomType>> GetAllRoomTypes();

        public Task<IEnumerable<T>> GetAllRoomTypes<T>(string searchText);

        public Task<T> GetRoomType<T>(int id);
    }
}