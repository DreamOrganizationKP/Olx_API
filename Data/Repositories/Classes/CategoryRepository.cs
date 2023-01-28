using Data.Context;
using Data.Models;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Classes
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext dbContext) : base(dbContext) { }

        async Task<ICollection<Category>> ICategoryRepository.GetAllAsync()
        {
            var result = _dbContext.Categories.Where(c => c.ParentId == null).Include(c => c.SubCategories).ToList();
            return result;
        }

        public async Task<Category> GetById(string id)
        {
            var result = await _dbContext.Categories.Where(c => c.Id == id).Include(c => c.SubCategories).FirstOrDefaultAsync();
            return result;
        }
    }
}
