using WebApplication2.Data.Model;

namespace WebApplication2.services
{
    public interface IEscortService
    {
        Task AddEscort(Escort escort);
        Task RemoveEscort(int Id);
        Task<List<Escort>> GetAllEscorts(Escort escort);
        Task UpdateEscort(int Id, Escort escort);
    }
}
