using WebApplication2.Data.Model;

namespace WebApplication2.services
{
    public interface IRoomImageService
    {
        public Task AddRange(int id, List<IFormFile> roomImages);

        public Task Remove(int roomImageId);

        public Task Update(int roomImageId, RoomImage roomImage);

        public Task<List<RoomImage>> GetAllRoomImage();

        public Task<IEnumerable<T>> GetRoomImage<T>(int id);
    }
}