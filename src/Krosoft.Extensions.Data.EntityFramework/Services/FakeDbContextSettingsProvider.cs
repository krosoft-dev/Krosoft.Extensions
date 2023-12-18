using Krosoft.Extensions.Data.EntityFramework.Interfaces;

namespace Krosoft.Extensions.Data.EntityFramework.Services;

public class FakeDbContextSettingsProvider : IDbContextSettingsProvider
{
    public string GetTenantId() => throw new NotImplementedException();

    public DateTime GetNow() => throw new NotImplementedException();

    public string GetUtilisateurId() => throw new NotImplementedException();
}