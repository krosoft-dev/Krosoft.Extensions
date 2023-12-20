namespace Krosoft.Extensions.Data.EntityFramework.Interfaces;

public interface IDbContextSettingsProvider : IDbContextSettingsProvider2
{
    
    string GetTenantId();
    string GetUtilisateurId();
}

public interface IDbContextSettingsProvider2
{
    DateTime GetNow();
}