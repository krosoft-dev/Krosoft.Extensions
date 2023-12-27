using Krosoft.Extensions.Data.Abstractions.Interfaces;
using Krosoft.Extensions.Data.EntityFramework.Contexts;
using Krosoft.Extensions.Data.EntityFramework.Extensions;
using Krosoft.Extensions.Data.EntityFramework.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Krosoft.Extensions.Data.EntityFramework.Scopes;

public class DbContextScope<T> : ReadDbContextScope<T> where T : KrosoftContext
{
    public DbContextScope(IServiceScope serviceScope,
                          IDbContextSettings<T> dbContextSettings ) : base(serviceScope, dbContextSettings)
    {
    }

    public IUnitOfWork GetUnitOfWork() => new UnitOfWork(DbContext);

    public IWriteRepository<TEntity> GetWriteRepository<TEntity>()
        where TEntity : class
        => new WriteRepository<TEntity>(DbContext);
}