using Microsoft.EntityFrameworkCore;
using Krosoft.Extensions.Core.Tools;
using Krosoft.Extensions.Data.Abstractions.Interfaces;

namespace Krosoft.Extensions.Data.EntityFramework.Repositories
{
    public class ReadRepository<TEntity> : IReadRepository<TEntity>
        where TEntity : class

    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public ReadRepository(DbContext dbDbContext)
        {
            Guard.IsNotNull(nameof(dbDbContext), dbDbContext);

            _dbContext = dbDbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> Query()
        {
            return _dbSet.AsNoTracking();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}