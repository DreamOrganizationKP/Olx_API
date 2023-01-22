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
        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
                var category = _mapper.Map<Category>(model);
                var result = await _repository.Create(category);

                return new SimpleResponseVM()
                {
                    IsSuccess = true,
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
