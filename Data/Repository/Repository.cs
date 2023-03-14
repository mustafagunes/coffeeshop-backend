using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Interface;
using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        // Definitions
        protected readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(CoffeeShopDbContext context)
        {
            // _context ile veri tabanına erişirim
            // _dbSet ile de tablolara erişmiş olurum, TEntitiy ne gelirse o Entity üzerinde işlem yapmış olacağım.
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.SingleOrDefaultAsync(predicate);
        }

        public async Task<bool> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            var result = await SaveAsync();
            return result;
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public bool Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
            var result = Save();
            return result;
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            Save();
        }

        public TEntity Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            Save();
            return entity;
        }

        private async Task<bool> SaveAsync()
        {
            try
            {
                return (await _context.SaveChangesAsync() > 0);
            }
            catch (Exception e)
            {
                return false;
            }
        }
        
        private bool Save()
        {
            try
            {
                return (_context.SaveChanges() > 0);
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}