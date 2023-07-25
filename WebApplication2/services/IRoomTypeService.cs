using WebApplication2.Data.Model;

namespace WebApplication2.services
{
    public interface IRoomTypeService
    {
        Task<IEnumerable<RoomType>> GetAllRoomTypes();

        Task<IEnumerable<T>> GetAllRoomTypesPager<T>(string searchText);

        public int CountAllRoomType(string searchText);

        Task<RoomType> GetId(int id);

        Task Add(RoomType roomType);

        Task Update(int id, RoomType roomType);

        Task Delete(int id);
    }
}