using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Data.Model;

namespace WebApplication2.services
{
    public class EscortService : IEscortService
    {
        private readonly WebApplication2DBContext _db;

        public EscortService(WebApplication2DBContext db)
        {
            _db = db;
        }

        public async Task AddEscort(Escort escort)
        {
            await _db.Escorts.AddAsync(escort);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Escort>> GetAllEscorts()
        {
            var allEscorts = await _db.Escorts.ToListAsync();
            return allEscorts;
        }

        public Task<List<Escort>> GetAllEscorts(Escort escort)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveEscort(int Id)
        {
            var removeEscort = await _db.Escorts.FirstOrDefaultAsync(u => u.EscortId == Id);

            if (removeEscort != null)
            {
                _db.Escorts.Remove(removeEscort);
                await _db.SaveChangesAsync();
            }
        }

        public async Task UpdateEscort(int Id, Escort escort)
        {
            escort.EscortId = Id;
            var updateEscort = await _db.Escorts.FirstOrDefaultAsync(u => u.EscortId == Id);

            if (updateEscort != null)
            {
                _db.Entry(updateEscort).CurrentValues.SetValues(escort);
                await _db.SaveChangesAsync();
            }
        }
    }
}