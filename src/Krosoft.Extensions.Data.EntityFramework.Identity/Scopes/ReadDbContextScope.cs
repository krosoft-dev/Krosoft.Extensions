using Krosoft.Extensions.Core.Tools;
using Krosoft.Extensions.Data.Abstractions.Interfaces;
using Krosoft.Extensions.Data.EntityFramework.Audits.Contexts;
using Krosoft.Extensions.Data.EntityFramework.Contexts;
using Krosoft.Extensions.Data.EntityFramework.Identity.Contexts;
using Krosoft.Extensions.Data.EntityFramework.Identity.Extensions;
using Krosoft.Extensions.Data.EntityFramework.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Krosoft.Extensions.Data.EntityFramework.Identity.Scopes;

public class ReadDbContextScope<T> : IServiceScope where T : KrosoftContext
{
    private readonly IServiceScope _serviceScope;
    protected readonly T DbContext;

    public ReadDbContextScope(IServiceScope serviceScope,
                              IDbContextSettings? dbContextSettings = null)
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

    private static T GetContext(IServiceScope serviceScope, IDbContextSettings? dbContextSettings)
    {

        if (dbContextSettings is T auditContext)
        {
            // Code à exécuter lorsque dbContextSettings est de type T, c'est-à-dire KrosoftAuditContext
        }

        if (dbContextSettings is IAuditableDbContextSettings auditableDbContextSettings)
        {
            // Code à exécuter lorsque dbContextSettings est de type IAuditableDbContextSettings
            // Vous pouvez maintenant utiliser auditableDbContextSettings comme une instance de IAuditableDbContextSettings
        }
        
       
       if (dbContextSettings is ITenantDbContextSettings tenantDbContextSettings)
        {
            // Code à exécuter lorsque dbContextSettings est de type IAuditableDbContextSettings
            // Vous pouvez maintenant utiliser auditableDbContextSettings comme une instance de IAuditableDbContextSettings
        }
        
       

        

        switch (typeof(T).BaseType?.Name)
        {
            case nameof(KrosoftAuditContext):
                // Code à exécuter pour KrosoftAuditContext
                break;

            case nameof(KrosoftTenantContext):
                // Code à exécuter pour KrosoftTenantContext
                break;

            case nameof(KrosoftTenantAuditableContext):
                // Code à exécuter pour KrosoftTenantAuditableContext
                break;

            case nameof(KrosoftContext):
                // Code à exécuter pour KrosoftContext
                break;

            default:
                // Code à exécuter si le type T ne correspond à aucun cas
                break;
        }

        if (typeof(T).BaseType == typeof(KrosoftAuditContext))
        {
        }

        if (typeof(T).BaseType == typeof(KrosoftTenantContext))
        {
        }

        if (typeof(T).BaseType == typeof(KrosoftTenantAuditableContext))
        {
        }

        if (typeof(T).BaseType == typeof(KrosoftContext))
        {
        }

        //if (T is KrosoftAuditContext)
        //{
        //    //IAuditableDbContextSettings, ITenantDbContextSettings
        //}

        //var dateTimeService = serviceScope.ServiceProvider.GetRequiredService<IDateTimeService>();
        //var tenantDbContextProvider = new TenantDbContextProvider(tenantId);
        //var auditableDbContextProvider = new AuditableDbContextProvider(dateTimeService.Now, utilisateurId );

        //var krosoftContext = (T)Activator.CreateInstance(typeof(T), serviceScope.ServiceProvider.GetRequiredService<DbContextOptions>(),
        //                                                 dbContextSettingsProvider)!;
        //return krosoftContext;

        return null!;
    }

    public IReadRepository<TEntity> GetReadRepository<TEntity>()
        where TEntity : class
        => new ReadRepository<TEntity>(DbContext);
}