using AutoMapper;
using Data.Models;
using Data.Repositories.Interfaces;
using Data.ViewModels;

namespace Services.ServiceClasses
{
    public class CategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly ToolsService _toolsService;
        public CategoryService(ICategoryRepository repository, IMapper mapper, ToolsService toolsService)
        {
            _repository = repository;
            _mapper = mapper;
            _toolsService = toolsService;
        }

        public async Task<SimpleResponseVM> GetAllAsync()
        {
            try
            {
                var result = await _repository.GetAllAsync();

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

        public async Task<SimpleResponseVM> CreateAsync(CreateCategoryRequestVM model)
        {
            try
            {
                string photo = null;
                if(model.PhotoBase64 != null)
                {
                    photo = await _toolsService.SaveImageOnDiskAsync(model.PhotoBase64);
                }
                var category = _mapper.Map<Category>(model);
                category.Photo = photo;
                var result = await _repository.Create(category);

                if(category.Id!= null)
                {
                    return new SimpleResponseVM()
                    {
                        IsSuccess = true,
                        Payload = category.Id
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

        public async Task<SimpleResponseVM> DeleteAsync(string id)
        {
            try
            {
                var result = await _repository.Delete(id);

                if(result > 0)
                {
                    return new SimpleResponseVM()
                    {
                        IsSuccess = true,
                    };
                }

                return new SimpleResponseVM()
                {
                    IsSuccess = false,
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
