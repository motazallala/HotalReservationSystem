using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication2.Data.Model;

namespace WebApplication2.services
{
    public class EscortService
    {
        private readonly ApplicationDBContext _db;

        public EscortService(ApplicationDBContext db)
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
