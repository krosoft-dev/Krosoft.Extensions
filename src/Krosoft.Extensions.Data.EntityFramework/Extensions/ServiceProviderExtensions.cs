using Krosoft.Extensions.Data.EntityFramework.Contexts;
using Krosoft.Extensions.Data.EntityFramework.Models;
using Krosoft.Extensions.Data.EntityFramework.Scopes;
using Microsoft.Extensions.DependencyInjection;

namespace Krosoft.Extensions.Data.EntityFramework.Extensions;

public static class ServiceProviderExtensions
{
    public static DbContextScope<T> CreateDbContextScope<T>(this IServiceProvider provider, string tenantId, DateTime now, string utilisateurId)
        where T : KrosoftTenantAuditableContext
        => CreateDbContextScope(provider,
                                new TenantAuditableDbContextSettings<T>(tenantId,
                                                                        now,
                                                                        utilisateurId));

    public static DbContextScope<T> CreateDbContextScope<T>(this IServiceProvider provider,
                                                            IDbContextSettings<T> dbContextSettings)
        where T : KrosoftContext
        => new DbContextScope<T>(provider.CreateScope(),
                                 dbContextSettings);

    public static ReadDbContextScope<T> CreateReadDbContextScope<T>(this IServiceProvider provider,
                                                                    IDbContextSettings<T> dbContextSettings)
        where T : KrosoftContext
        => new ReadDbContextScope<T>(provider.CreateScope(),
                                     dbContextSettings);
}