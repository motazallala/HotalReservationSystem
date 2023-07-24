using WebApplication2.Data.Model;

namespace WebApplication2.services
{
    public interface IRoomImageService
    {
        Task AddRoomImage(RoomImage roomImage);
        Task RemoveRoomImage(int roomImageId);
        Task<List<RoomImage>> GetAllRoomImage(RoomImage roomImage);
        Task UpdateRoomImage(int roomImageId, RoomImage roomImage);
    }
}
