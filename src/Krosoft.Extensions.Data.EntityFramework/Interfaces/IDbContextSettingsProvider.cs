namespace Krosoft.Extensions.Data.EntityFramework.Interfaces;

public interface IDbContextSettingsProvider
{
    string GetTenantId();
    DateTime GetNow();
    string GetUtilisateurId();
}