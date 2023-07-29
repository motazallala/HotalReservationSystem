using WebApplication2.Data.Model;

namespace WebApplication2.services
{
    public interface IEscortService
    {
        public Task AddEscort(Escort escort);

        public Task RemoveEscort(int Id);

        public Task<List<Escort>> GetAllEscorts(Escort escort);

        public Task UpdateEscort(int Id, Escort escort);
    }
}