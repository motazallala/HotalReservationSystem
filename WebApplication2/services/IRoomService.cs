using WebApplication2.Data.Model;

namespace WebApplication2.services
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAllRoom();

        Task<Room> GetId(int id);

        Task Add(Room room);

        Task Update(int id, Room room);

        Task Delete(int id);
    }
}