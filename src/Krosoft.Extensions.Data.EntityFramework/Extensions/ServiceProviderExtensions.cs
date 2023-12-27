using Krosoft.Extensions.Data.EntityFramework.Contexts;
using Krosoft.Extensions.Data.EntityFramework.Scopes;
using Microsoft.Extensions.DependencyInjection;

namespace Krosoft.Extensions.Data.EntityFramework.Extensions;

public static class ServiceProviderExtensions
{
    public static DbContextScope<T> CreateDbContextScope<T>(this IServiceProvider provider,
                                                            IDbContextSettings<T> dbContextSettings )
        where T : KrosoftContext
        => new DbContextScope<T>(provider.CreateScope(),
                                 dbContextSettings);

    public static ReadDbContextScope<T> CreateReadDbContextScope<T>(this IServiceProvider provider,
                                                                    IDbContextSettings<T> dbContextSettings )
        where T : KrosoftContext
        => new ReadDbContextScope<T>(provider.CreateScope(),
                                     dbContextSettings);
}

public interface IDbContextSettings<T> where T : KrosoftContext
{
}

public interface ITenantDbContextSettings<T> : IDbContextSettings<T> where T : KrosoftContext
{
    string TenantId { get; }
}

public interface IAuditableDbContextSettings<T> : IDbContextSettings<T> where T : KrosoftContext
{
    string UtilisateurId { get; }
    DateTime Now { get; }
}

public interface ITenantAuditableDbContextSettings<T> : IDbContextSettings<T> where T : KrosoftContext
{
    string TenantId { get; }
    string UtilisateurId { get; }
    DateTime Now { get; }
}

public class DbContextSettings<T> : IDbContextSettings<T> where T : KrosoftContext
{
}

public class TenantDbContextSettings<T> : ITenantDbContextSettings<T> where T : KrosoftTenantContext
{
    public TenantDbContextSettings(string tenantId)
    {
        TenantId = tenantId;
    }

    public string TenantId { get; }
}

public class AuditableDbContextSettings<T> : IAuditableDbContextSettings<T> where T : KrosoftAuditableContext
{
    public AuditableDbContextSettings(DateTime now, string utilisateurId)
    {
        Now = now;
        UtilisateurId = utilisateurId;
    }

    public DateTime Now { get; }

    public string UtilisateurId { get; }
}

public class TenantAuditableDbContextSettings<T> : ITenantAuditableDbContextSettings<T>
    where T : KrosoftTenantAuditableContext
{
    public TenantAuditableDbContextSettings(string tenantId, DateTime now, string utilisateurId)
    {
        Now = now;
        UtilisateurId = utilisateurId;
        TenantId = tenantId;
    }

    public DateTime Now { get; }

    public string TenantId { get; }

    public string UtilisateurId { get; }
}