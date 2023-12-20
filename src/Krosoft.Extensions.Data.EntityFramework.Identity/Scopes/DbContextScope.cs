using Krosoft.Extensions.Data.Abstractions.Interfaces;
using Krosoft.Extensions.Data.EntityFramework.Repositories;
using Krosoft.Extensions.Data.EntityFramework.Tenants.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace Krosoft.Extensions.Data.EntityFramework.Tenants.Scopes;

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