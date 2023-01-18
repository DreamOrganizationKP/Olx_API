using Data.Context;
using Data.Models;
using Data.Repositories.Interfaces;

namespace Data.Repositories.Classes
{
    public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(AppDbContext dbContext) : base(dbContext) { }
        
        
    }
}
