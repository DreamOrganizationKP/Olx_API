using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Data.Repositories.Interfaces
{
    public interface IGenericRepository<TModel, TId>
    {
        public Task<EntityEntry> Create(TModel model);
        public Task<TModel> GetById(TId id);
        public Task<EntityEntry> Update(TModel model);
        public Task<EntityEntry> Delete(TModel model);
    }
}
