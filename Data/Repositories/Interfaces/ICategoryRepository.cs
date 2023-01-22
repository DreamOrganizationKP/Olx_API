using Data.Models;

namespace Data.Repositories.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category, string>
    {
        Task<ICollection<Category>> GetAllAsync();
    }
}
