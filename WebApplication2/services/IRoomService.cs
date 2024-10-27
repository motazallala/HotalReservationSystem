using WebApplication2.Data.Model;

namespace WebApplication2.services
{
    public interface IRoomService
    {
        public Task Add(Room room, List<IFormFile> roomImages);

        public Task Update(int id, Room room, List<IFormFile> roomImages);

        public Task Update(int id, Room room);

        public Task Delete(int id);

        public Task<IEnumerable<Room>> GetAllRoom();

        public Task<IEnumerable<T>> GetSearchResults<T>(bool availableOnly = false,
                                            int? minCapacity = null,
                                            int? types = null);

        public Task<int> GetMaxCapacity();

        public Task<IEnumerable<T>> GetRoomTypeList<T>();

        public Task<T> GetId<T>(int id);

        public Task<bool> IsRoomNumberFree(int number, int? roomId = null);

        int GetRoomCapacity(int roomId);
    }
}