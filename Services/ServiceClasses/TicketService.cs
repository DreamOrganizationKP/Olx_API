using AutoMapper;
using Data.Models;
using Data.Repositories.Interfaces;
using Data.ViewModels;

namespace Services.ServiceClasses
{
    public class TicketService
    {
        private readonly ITicketRepository _repository;
        private readonly ToolsService _toolsService;
        private readonly IMapper _mapper;
        public TicketService(ITicketRepository repository, IMapper mapper, ToolsService toolsService)
        {
            _repository = repository;
            _mapper = mapper;
            _toolsService = toolsService;
        }

        public async Task<SimpleResponseVM> GetAsync(string id)
        {
            try
            {
                var result = await _repository.GetById(id);
                if (result == null)
                {
                    return new SimpleResponseVM()
                    {
                        IsSuccess = false
                    };
                }

                var ticket = _mapper.Map<FrontTicketVM>(result);

                return new SimpleResponseVM()
                {
                    IsSuccess = true,
                    Payload = ticket
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
                
                var mappedTickets = _mapper.Map<ICollection<Ticket>, IEnumerable<FrontTicketVM>>(result);

                return new SimpleResponseVM()
                {
                    IsSuccess = true,
                    Payload = mappedTickets
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
                List<TicketPhoto> photos = null;
                if(model.Photos != null && model.Photos.Count > 0)
                {
                    photos = new List<TicketPhoto>();
                    foreach(var photo in model.Photos)
                    {
                        var path = await _toolsService.SaveImageOnDiskAsync(photo.Image);
                        var newPhoto = new TicketPhoto()
                        {
                            Path = path,
                            Index = photo.Index,
                        };
                        photos.Add(newPhoto);
                    }
                }
                model.Photos = null;
                var ticket = _mapper.Map<Ticket>(model);
                ticket.Photos = photos;
                var result = await _repository.Create(ticket);

                if(ticket.Id != null)
                {
                    return new SimpleResponseVM()
                    {
                        IsSuccess = true,
                        Payload = ticket
                    };
                }

                return new SimpleResponseVM()
                {
                    IsSuccess= false
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

        public async Task<SimpleResponseVM> DeleteAsync(string id)
        {
            try
            {
                var result = await _repository.Delete(id);

                if(result > 0)
                {
                    return new SimpleResponseVM()
                    {
                        IsSuccess = true
                    };
                }

                return new SimpleResponseVM()
                {
                    IsSuccess = false
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
