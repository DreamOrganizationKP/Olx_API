using Data.Models;

namespace Data.Repositories.Interfaces
{
    public interface ITicketRepository : IGenericRepository<Ticket, string>
    {
        Task<ICollection<Ticket>> GetAllAsync(string categoryId);
        Task<Ticket> GetById(string id);
    }
}
