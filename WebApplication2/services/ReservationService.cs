using WebApplication1.Data;
using WebApplication2.Data.Model;

namespace WebApplication2.services
{
    public class ReservationService : IReservationsService
    {
        private readonly ApplicationDBContext _db;

        public ReservationService(ApplicationDBContext db)
        {
            _db = db;
        }

        public void Add(Reservation reservation)
        {
            _db.Reservations.Add(reservation);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RoomType> GetAll()
        {
            throw new NotImplementedException();
        }

        public RoomType GetId(int id)
        {
            throw new NotImplementedException();
        }

        public RoomType Update(int id, Reservation reservation)
        {
            throw new NotImplementedException();
        }
    }
}