namespace Krosoft.Extensions.Blocking.Abstractions.Interfaces;

public interface IAccessTokenProvider
{
    Task<string> GetAccessTokenAsync(CancellationToken cancellationToken);
}