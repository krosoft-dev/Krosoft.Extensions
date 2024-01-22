namespace Krosoft.Extensions.Blocking.Abstractions.Interfaces;

public interface IIdentifierProvider
{
    Task<string> GetIdentifierAsync(CancellationToken cancellationToken);
}