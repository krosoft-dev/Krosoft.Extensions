using Krosoft.Extensions.Data.EntityFramework.Contexts;
using Krosoft.Extensions.Data.EntityFramework.Identity.Contexts;
using Krosoft.Extensions.Data.EntityFramework.Identity.Scopes;
using Microsoft.Extensions.DependencyInjection;

namespace Krosoft.Extensions.Data.EntityFramework.Identity.Extensions;

public static class ServiceProviderExtensions
{
    public static DbContextScope<T> CreateDbContextScope<T>(this IServiceProvider provider,
                                                            IDbContextSettings? dbContextSettings = null)
        where T : KrosoftContext
        => new DbContextScope<T>(provider.CreateScope(),
                                 dbContextSettings);

    public static ReadDbContextScope<T> CreateReadDbContextScope<T>(this IServiceProvider provider,
                                                                    IDbContextSettings? dbContextSettings = null)
        where T : KrosoftContext
        => new ReadDbContextScope<T>(provider.CreateScope(),
                                     dbContextSettings);
}

public interface IDbContextSettings
{
}

public interface ITenantDbContextSettings : IDbContextSettings
{
    string TenantId { get; }
}

public interface IAuditableDbContextSettings : IDbContextSettings
{
    string UtilisateurId { get; }
    DateTime Now { get; }
}

public class DbContextSettings : IDbContextSettings
{
  
}
public class TenantDbContextSettings : ITenantDbContextSettings
{
    public TenantDbContextSettings(string tenantId)
    {
        TenantId = tenantId;
    }

    public string TenantId { get; }
}

public class AuditableDbContextSettings : IAuditableDbContextSettings
{
    public AuditableDbContextSettings(DateTime now, string utilisateurId)
    {
        Now = now;
        UtilisateurId = utilisateurId;
    }

    public DateTime Now { get; }

    public string UtilisateurId { get; }
}

public class TenantAuditableDbContextSettings  :    IAuditableDbContextSettings, ITenantDbContextSettings  
{
    public TenantAuditableDbContextSettings(string tenantId, DateTime now, string utilisateurId)
    {
        Now = now;
        UtilisateurId = utilisateurId;
        TenantId = tenantId;
    }

    public DateTime Now { get; }

    public string UtilisateurId { get; }

    public string TenantId { get; }
}