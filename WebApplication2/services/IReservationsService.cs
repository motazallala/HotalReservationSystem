using WebApplication2.Data.Model;

namespace WebApplication2.services
{
    public interface IReservationsService
    {
        public void Add(Reservation reservation);

        public RoomType Update(int id, Reservation reservation);

        public void Delete(int id);

        public Task<IEnumerable<T>> GetSearchResults<T>();

        public IEnumerable<RoomType> GetAll();

        public RoomType GetId(int id);
    }
}