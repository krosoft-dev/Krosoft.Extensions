using Microsoft.EntityFrameworkCore;
using Krosoft.Extensions.Data.Abstractions.Interfaces;

namespace Krosoft.Extensions.Data.EntityFramework.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext dbContext)
        {
            _context = dbContext;
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}