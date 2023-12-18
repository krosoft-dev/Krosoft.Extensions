using Krosoft.Extensions.Data.EntityFramework.Interfaces;

namespace Krosoft.Extensions.Data.EntityFramework.Services;

public class DbContextSettingsProvider : IDbContextSettingsProvider
{
    private readonly DateTime _now;
    private readonly string _tenantId;
    private readonly string _utilisateurId;

    public DbContextSettingsProvider(DateTime now, string utilisateurId, string tenantId)
    {
        _now = now;
        _utilisateurId = utilisateurId;
        _tenantId = tenantId;
    }

    public string GetTenantId() => _tenantId;

    public DateTime GetNow() => _now;

    public string GetUtilisateurId() => _utilisateurId;
}