using Krosoft.Extensions.Data.EntityFramework.Interfaces;

namespace Krosoft.Extensions.Data.EntityFramework.Services;

public class FakeDbContextSettingsProvider : IDbContextSettingsProvider
{
    public string GetTenantId() => "Fake_Tenant_Id";

    public string GetUtilisateurId() => "Fake_Utilisateur_Id";

    public DateTime GetNow() => new DateTime(2012, 9, 28);
}