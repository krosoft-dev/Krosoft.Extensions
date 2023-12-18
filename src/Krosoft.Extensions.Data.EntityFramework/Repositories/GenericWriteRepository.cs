using Microsoft.EntityFrameworkCore;
using Krosoft.Extensions.Core.Tools;
using Krosoft.Extensions.Data.Abstractions.Interfaces;

namespace Krosoft.Extensions.Data.EntityFramework.Repositories
{
    public class GenericWriteRepository : IGenericWriteRepository
    {
        private readonly DbContext _dbContext;

        public GenericWriteRepository(DbContext dbContext)
        {
            Guard.IsNotNull(nameof(dbContext), dbContext);

            _dbContext = dbContext;
        }

        public void Insert<TEntity>(TEntity entity) where TEntity : class
        {
            Guard.IsNotNull(nameof(entity), entity);
            var dbSet = _dbContext.Set<TEntity>();

            dbSet.Add(entity);
        }

        public ValueTask<TEntity> GetAsync<TEntity>(params object[] key) where TEntity : class
        {
            var dbSet = _dbContext.Set<TEntity>();
            return dbSet.FindAsync(key);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            Guard.IsNotNull(nameof(entity), entity);
            var dbSet = _dbContext.Set<TEntity>();
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }

            dbSet.Remove(entity);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            Guard.IsNotNull(nameof(entity), entity);
            var dbSet = _dbContext.Set<TEntity>();
            dbSet.Update(entity);
        }
    }
}