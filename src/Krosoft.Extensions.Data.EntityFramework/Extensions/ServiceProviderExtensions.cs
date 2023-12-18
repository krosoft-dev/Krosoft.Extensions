using Microsoft.Extensions.DependencyInjection;
using Krosoft.Extensions.Data.EntityFramework.Contexts;
using Krosoft.Extensions.Data.EntityFramework.Scopes;

namespace Krosoft.Extensions.Data.EntityFramework.Extensions;

public static class ServiceProviderExtensions
{
    public static DbContextScope<T> CreateDbContextScope<T>(this IServiceProvider provider,
                                                            string utilisateurId,
                                                            string tenantId)
        where T : KrosoftTenantContext
        => new DbContextScope<T>(provider.CreateScope(),
                                 utilisateurId,
                                 tenantId);

    public static ReadDbContextScope<T> CreateReadDbContextScope<T>(this IServiceProvider provider,
                                                                    string tenantId)
        where T : KrosoftTenantContext
        => new ReadDbContextScope<T>(provider.CreateScope(),
                                     "N/A",
                                     tenantId);
}