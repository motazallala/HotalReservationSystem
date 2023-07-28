using WebApplication2.Data.Model;

namespace WebApplication2.services
{
    public interface IRoomService
    {
        Task Add(Room room, List<IFormFile> roomImages);

        Task Update(Room room, List<IFormFile> roomImages);

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