using Data.Context;
using Data.Models;
using Data.Repositories.Interfaces;

namespace Data.Repositories.Classes
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext dbContext) : base(dbContext) { }


    }
}
