using Data.Repositories.Interfaces;

namespace Services.ServiceClasses
{
    public class TicketService
    {
        private readonly ITicketRepository _repository;
        public TicketService(ITicketRepository repository)
        {
            _repository = repository;
        }
    }
}
