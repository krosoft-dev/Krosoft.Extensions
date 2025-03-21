namespace Krosoft.Extensions.Cqrs.Models;

public interface IAuth
{
    string? UtilisateurCourantId { get; set; }
    string? TenantId { get; set; }
    string? ApiKey { get; set; }

    bool IsUtilisateurRequired => true;
    bool IsTenantRequired => true;
    bool IsApiKeyRequired => false;
}