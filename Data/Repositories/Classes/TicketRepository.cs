using Data.Context;
using Data.Models;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Classes
{
    public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<ICollection<Ticket>> GetAllAsync(string categoryId)
        {
            var result = _dbContext.Tickets.Where(t => t.CategoryId == categoryId).Include(t => t.Photos).ToList();
            return result;
        }

        public async Task<Ticket> GetById(string id)
        {
            var result = _dbContext.Tickets.Where(t => t.Id == id).Include(t => t.Photos).Include(t => t.User).FirstOrDefault();
            return result;
        }
    }
}
