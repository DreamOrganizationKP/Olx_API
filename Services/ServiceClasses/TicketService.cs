using AutoMapper;
using Data.Models;
using Data.Repositories.Interfaces;
using Data.ViewModels;

namespace Services.ServiceClasses
{
    public class TicketService
    {
        private readonly ITicketRepository _repository;
        private readonly IMapper _mapper;
        public TicketService(ITicketRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SimpleResponseVM> GetAsync(string id)
        {
            try
            {
                var result = await _repository.GetById(id);

                return new SimpleResponseVM()
                {
                    IsSuccess = true,
                    Payload = result
                };
            }
            catch (Exception)
            {

                return new SimpleResponseVM()
                {
                    IsSuccess = false
                };
            }
        }

        public async Task<SimpleResponseVM> GetAllAsync(string categoryId)
        {
            try
            {
                var result = await _repository.GetAllAsync(categoryId);

                return new SimpleResponseVM()
                {
                    IsSuccess = true,
                    Payload = result
                };
            }
            catch (Exception)
            {
                return new SimpleResponseVM()
                {
                    IsSuccess = false
                };
            }
        }

        public async Task<SimpleResponseVM> CreateAsync(CreateTicketRequestVM model)
        {
            try
            {
                var ticket = _mapper.Map<Ticket>(model);
                var result = await _repository.Create(ticket);

                return new SimpleResponseVM()
                {
                    IsSuccess = true
                };
            }
            catch (Exception)
            {
                return new SimpleResponseVM()
                {
                    IsSuccess = false
                };
            }
        }
    }
}
