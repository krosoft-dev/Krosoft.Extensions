using Krosoft.Extensions.Core.Interfaces;
using Krosoft.Extensions.Core.Tools;
using Krosoft.Extensions.Data.Abstractions.Interfaces;
using Krosoft.Extensions.Data.EntityFramework.Identity.Contexts;
using Krosoft.Extensions.Data.EntityFramework.Repositories;
using Krosoft.Extensions.Data.EntityFramework.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Krosoft.Extensions.Data.EntityFramework.Identity.Scopes;

public class ReadDbContextScope<T> : IServiceScope where T : KrosoftTenantContext
{
    private readonly IServiceScope _serviceScope;
    protected readonly T DbContext;

    public ReadDbContextScope(IServiceScope serviceScope,
                              string utilisateurId,
                              string tenantId)
    {
        Guard.IsNotNull(nameof(serviceScope), serviceScope);
        Guard.IsNotNullOrWhiteSpace(nameof(utilisateurId), utilisateurId);
        Guard.IsNotNullOrWhiteSpace(nameof(tenantId), tenantId);

        _serviceScope = serviceScope;
        var dateTimeService = serviceScope.ServiceProvider.GetRequiredService<IDateTimeService>();
        var dbContextSettingsProvider = new DbContextSettingsProvider(dateTimeService.Now, utilisateurId, tenantId);

        DbContext = (T)Activator.CreateInstance(typeof(T), serviceScope.ServiceProvider.GetRequiredService<DbContextOptions>(),
                                                dbContextSettingsProvider)!;
    }

    public void Dispose()
    {
        DbContext.Dispose();
        _serviceScope.Dispose();
    }

    public IServiceProvider ServiceProvider => _serviceScope.ServiceProvider;

    public IReadRepository<TEntity> GetReadRepository<TEntity>()
        where TEntity : class
        => new ReadRepository<TEntity>(DbContext);
}