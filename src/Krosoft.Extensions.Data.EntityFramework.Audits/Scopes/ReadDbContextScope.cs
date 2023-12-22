//using Krosoft.Extensions.Core.Interfaces;
//using Krosoft.Extensions.Core.Tools;
//using Krosoft.Extensions.Data.Abstractions.Interfaces;
//using Krosoft.Extensions.Data.EntityFramework.Audits.Contexts;
//using Krosoft.Extensions.Data.EntityFramework.Contexts;
//using Krosoft.Extensions.Data.EntityFramework.Repositories;
//using Krosoft.Extensions.Data.EntityFramework.Services;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;

//namespace Krosoft.Extensions.Data.EntityFramework.Audits.Scopes;

//public class ReadDbContextScope<T> : IServiceScope where T : KrosoftContext
//{
//    private readonly IServiceScope _serviceScope;
//    protected readonly T DbContext = null!;

//    public ReadDbContextScope(IServiceScope serviceScope,
//                              string utilisateurId,
//                              string tenantId)
//    {
//        Guard.IsNotNull(nameof(serviceScope), serviceScope);
//        Guard.IsNotNullOrWhiteSpace(nameof(utilisateurId), utilisateurId);
//        Guard.IsNotNullOrWhiteSpace(nameof(tenantId), tenantId);

//        _serviceScope = serviceScope;

//        if (T is KrosoftAuditContext)
//        {

//        }

//        var dateTimeService = serviceScope.ServiceProvider.GetRequiredService<IDateTimeService>();
//        var tenantDbContextProvider = new TenantDbContextProvider(tenantId);
//        var auditableDbContextProvider = new AuditableDbContextProvider(dateTimeService.Now, utilisateurId );

//        DbContext = (T)Activator.CreateInstance(typeof(T), serviceScope.ServiceProvider.GetRequiredService<DbContextOptions>(),
//                                                dbContextSettingsProvider)!;
//    }

//    public void Dispose()
//    {
//        DbContext.Dispose();
//        _serviceScope.Dispose();
//    }

//    public IServiceProvider ServiceProvider => _serviceScope.ServiceProvider;

//    public IReadRepository<TEntity> GetReadRepository<TEntity>()
//        where TEntity : class
//        => new ReadRepository<TEntity>(DbContext);
//}