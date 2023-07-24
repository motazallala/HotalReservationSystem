using WebApplication2.Data.Model;

namespace WebApplication2.services
{
    public interface IRoomImageService
    {
        Task<RoomImage> AddRoomImageAsync(RoomImage roomImage);
        Task RemoveRoomImageAsync(int roomImageId);
        Task<List<RoomImage>> GetAllRoomImageAsync(RoomImage roomImage);
        Task<RoomImage> UpdateRoomImageAsync(int roomImageId, RoomImage roomImage);
    }
}
