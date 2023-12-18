using Microsoft.Extensions.DependencyInjection;
using Krosoft.Extensions.Data.Abstractions.Interfaces;
using Krosoft.Extensions.Data.EntityFramework.Contexts;
using Krosoft.Extensions.Data.EntityFramework.Repositories;

namespace Krosoft.Extensions.Data.EntityFramework.Scopes;

public class DbContextScope<T> : ReadDbContextScope<T> where T : KrosoftTenantContext
{
    public DbContextScope(IServiceScope serviceScope,
                          string utilisateurId,
                          string tenantId) : base(serviceScope, utilisateurId, tenantId)
    {
    }

    public IWriteRepository<TEntity> GetWriteRepository<TEntity>()
        where TEntity : class
        => new WriteRepository<TEntity>(DbContext);

    public IUnitOfWork GetUnitOfWork() => new UnitOfWork(DbContext);
}