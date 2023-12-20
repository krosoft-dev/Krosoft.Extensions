namespace Krosoft.Extensions.Data.EntityFramework.Interfaces;

public interface IDbContextSettingsProvider
{
    string GetTenantId();
    DateTime GetNow();
    string GetUtilisateurId();
}public interface IDbContextSettingsProvider2
{
  
    DateTime GetNow();
   
}