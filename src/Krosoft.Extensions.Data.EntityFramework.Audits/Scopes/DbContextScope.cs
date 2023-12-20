using Krosoft.Extensions.Data.Abstractions.Interfaces;
using Krosoft.Extensions.Data.EntityFramework.Audits.Contexts;
using Krosoft.Extensions.Data.EntityFramework.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Krosoft.Extensions.Data.EntityFramework.Audits.Scopes;

public class DbContextScope<T> : ReadDbContextScope<T> where T : KrosoftAuditContext
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