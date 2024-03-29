using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Interface
{
    // TEntity generic olarak class tipini içine alır.
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);

        Task<IEnumerable<TEntity>> GetAllAsync();
        
        // find(x => x.id = 23)
        Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate);
        
        // category.SingleOrDefaultAsync(x => x.name = "kalem")
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<bool> AddAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        bool Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        TEntity Update(TEntity entity);
    }
}