namespace Krosoft.Extensions.Http.Interfaces;

public interface IHttpContextService
{
    string GetBaseUrl();
    IEnumerable<string> GetInformations();
}