using WebApplication2.Data.Model;

namespace WebApplication2.services
{
    public interface IRoomService
    {
        Task Add(Room room);

        Task Update(int id, Room room);

        Task Delete(int id);

        Task<IEnumerable<Room>> GetAllRoom();

        Task<IEnumerable<T>> GetSearchResults<T>(bool availableOnly = false,
                                            int? minCapacity = null,
                                            int? types = null);

        public Task<int> GetMaxCapacity();

        public Task<IEnumerable<T>> GetRoomTypeList<T>();

        Task<Room> GetId(int id);
    }
}