namespace Krosoft.Extensions.Blocking.Abstractions.Interfaces;

public interface IBlockingStorageProvider
{
    Task<bool> IsExistRowAsync(string collectionKey, string key, CancellationToken cancellationToken);
    Task<long> DeleteRowsAsync(string collectionKey, ISet<string> keys, CancellationToken cancellationToken);
    Task SetRowAsync(string collectionKey, IDictionary<string, string> entries, CancellationToken cancellationToken);
    Task<bool> DeleteRowAsync(string collectionKey, string key, CancellationToken cancellationToken);
    Task SetRowAsync(string collectionKey, string entries, string cancellationToken, CancellationToken cancellationToken1);
}
public interface IAccessTokenProvider
{
    Task<string> GetAccessTokenAsync(CancellationToken cancellationToken);
}
public interface IIdentifierProvider
{
    Task<string> GetIdentifierAsync(CancellationToken cancellationToken);
}