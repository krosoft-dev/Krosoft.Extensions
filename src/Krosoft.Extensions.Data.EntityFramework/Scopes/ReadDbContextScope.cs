using Krosoft.Extensions.Core.Models.Exceptions;
using Krosoft.Extensions.Core.Tools;
using Krosoft.Extensions.Data.Abstractions.Interfaces;
using Krosoft.Extensions.Data.EntityFramework.Contexts;
using Krosoft.Extensions.Data.EntityFramework.Extensions;
using Krosoft.Extensions.Data.EntityFramework.Repositories;
using Krosoft.Extensions.Data.EntityFramework.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Krosoft.Extensions.Data.EntityFramework.Scopes;

public class ReadDbContextScope<T> : IServiceScope where T : KrosoftContext
{
    private readonly IServiceScope _serviceScope;
    protected readonly T DbContext;

    public ReadDbContextScope(IServiceScope serviceScope,
                              IDbContextSettings<T> dbContextSettings)
    {
        Guard.IsNotNull(nameof(serviceScope), serviceScope);

        _serviceScope = serviceScope;

        var krosoftContext = GetContext(serviceScope, dbContextSettings);
        DbContext = krosoftContext;
    }

    public void Dispose()
    {
        DbContext.Dispose();
        _serviceScope.Dispose();
    }

    public IServiceProvider ServiceProvider => _serviceScope.ServiceProvider;

    private static T GetContext(IServiceScope serviceScope, IDbContextSettings<T> dbContextSettings)
    {
        if (dbContextSettings is IAuditableDbContextSettings<T> auditableDbContextSettings)
        {
            var auditableDbContextProvider = new AuditableDbContextProvider(auditableDbContextSettings.Now,
                                                                            auditableDbContextSettings.UtilisateurId);

            var krosoftContext = (T?)Activator.CreateInstance(typeof(T),
                                                              serviceScope.ServiceProvider.GetRequiredService<DbContextOptions>(),
                                                              auditableDbContextProvider);

            if (krosoftContext == null)
            {
                throw new KrosoftTechniqueException($"Impossible d'instancer le dbcontext de type {typeof(T).Name}");
            }

            return krosoftContext;
        }

        if (dbContextSettings is ITenantDbContextSettings<T> tenantDbContextSettings)
        {
            var tenantDbContextProvider = new TenantDbContextProvider(tenantDbContextSettings.TenantId);

            var krosoftContext = (T?)Activator.CreateInstance(typeof(T),
                                                              serviceScope.ServiceProvider.GetRequiredService<DbContextOptions>(),
                                                              tenantDbContextProvider);

            if (krosoftContext == null)
            {
                throw new KrosoftTechniqueException($"Impossible d'instancer le dbcontext de type {typeof(T).Name}");
            }

            return krosoftContext;
        }

        if (dbContextSettings is ITenantAuditableDbContextSettings<T> tenantAuditableDbContextSettings)
        {
            var tenantDbContextProvider = new TenantDbContextProvider(tenantAuditableDbContextSettings.TenantId);

            var auditableDbContextProvider = new AuditableDbContextProvider(tenantAuditableDbContextSettings.Now,
                                                                            tenantAuditableDbContextSettings.UtilisateurId);

            var krosoftContext = (T?)Activator.CreateInstance(typeof(T),
                                                              serviceScope.ServiceProvider.GetRequiredService<DbContextOptions>(),
                                                              tenantDbContextProvider,
                                                              auditableDbContextProvider);

            if (krosoftContext == null)
            {
                throw new KrosoftTechniqueException($"Impossible d'instancer le dbcontext de type {typeof(T).Name}");
            }

            return krosoftContext;
        }

        if (dbContextSettings is IDbContextSettings<T> a)
        {
            var krosoftContext = (T?)Activator.CreateInstance(typeof(T),
                                                              serviceScope.ServiceProvider.GetRequiredService<DbContextOptions>()
                                                             );

            if (krosoftContext == null)
            {
                throw new KrosoftTechniqueException($"Impossible d'instancer le dbcontext de type {typeof(T).Name}");
            }

            return krosoftContext;
        }

        throw new KrosoftTechniqueException($"Impossible d'instancer le dbcontext de type {typeof(T).Name}");
    }

    public IReadRepository<TEntity> GetReadRepository<TEntity>()
        where TEntity : class
        => new ReadRepository<TEntity>(DbContext);
}