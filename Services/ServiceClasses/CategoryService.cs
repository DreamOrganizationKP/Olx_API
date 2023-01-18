using Data.Repositories.Interfaces;

namespace Services.ServiceClasses
{
    public class CategoryService
    {
        private readonly ICategoryRepository _repository;
        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }
    }
}
