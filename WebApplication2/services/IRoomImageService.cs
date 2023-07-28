using WebApplication2.Data.Model;

namespace WebApplication2.services
{
    public interface IRoomImageService
    {
        Task Add(RoomImage roomImage);

        Task Remove(int roomImageId);

        Task Update(int roomImageId, RoomImage roomImage);

        Task<List<RoomImage>> GetAllRoomImage();

        Task<IEnumerable<T>> GetRoomImage<T>(int id);
    }
}