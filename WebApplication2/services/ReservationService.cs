using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Data.Model;

namespace WebApplication2.services
{
    public class ReservationService : IReservationsService
    {
        private readonly WebApplication2DBContext _db;
        private readonly IMapper _mapper;

        public ReservationService(WebApplication2DBContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
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

        public async Task<IEnumerable<T>> GetSearchResults<T>()
        {
            IQueryable<Reservation> result = _db.Reservations;
            return await result.OrderBy(x => x.ReservationId).ProjectTo<T>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public RoomType Update(int id, Reservation reservation)
        {
            throw new NotImplementedException();
        }
    }
}