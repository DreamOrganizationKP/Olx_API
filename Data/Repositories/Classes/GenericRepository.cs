using Data.Context;
using Data.Models;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Data.Repositories.Classes
{
    public class GenericRepository<TModel> : IGenericRepository<TModel, string> where TModel : class, IModel<string>
    {
        protected readonly AppDbContext _dbContext;
        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext= dbContext;
        }
        public async Task<EntityEntry> Create(TModel model)
        {
            var result = await _dbContext.Set<TModel>().AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<EntityEntry> Delete(TModel model)
        {
            var result = _dbContext.Set<TModel>().Remove(model);
            await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> Delete(string id)
        {
            var result = await _dbContext.Set<TModel>().Where(x => x.Id == id).ExecuteDeleteAsync();
            await _dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<ICollection<TModel>> GetAll()
        {
            var result = await _dbContext.Set<TModel>().ToListAsync();
            return result;
        }

        public async Task<TModel> GetById(string id)
        {
            var result =  await _dbContext.Set<TModel>().FirstOrDefaultAsync(r => r.Id == id);
            return result;
        }

       
        public async Task<EntityEntry> Update(TModel model)
        {
            var result = _dbContext.Set<TModel>().Update(model);
            await _dbContext.SaveChangesAsync();
            return result;
        }
    }
}
