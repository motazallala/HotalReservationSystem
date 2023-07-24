using WebApplication2.Data.Model;

namespace WebApplication2.services
{
    public interface IReservationsService
    {
        IEnumerable<RoomType> GetAll();

        RoomType GetId(int id);

        void Add(Reservation reservation);

        RoomType Update(int id, Reservation reservation);

        void Delete(int id);
    }
}