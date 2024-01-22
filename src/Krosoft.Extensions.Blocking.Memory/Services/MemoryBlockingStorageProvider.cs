using Krosoft.Extensions.Blocking.Abstractions.Interfaces;
using Krosoft.Extensions.Cache.Memory.Interfaces;

namespace Krosoft.Extensions.Blocking.Memory.Services;

public class MemoryBlockingStorageProvider : IBlockingStorageProvider
{
    private readonly ICacheProvider _cacheProvider;

    public MemoryBlockingStorageProvider(ICacheProvider cacheProvider)
    {
        _cacheProvider = cacheProvider;
    }

    public Task<bool> IsExistRowAsync(string collectionKey,
                                      string key,
                                      CancellationToken cancellationToken)
    {
        var isExist = _cacheProvider.IsSet(GetFullKey(collectionKey, key));
        return Task.FromResult(isExist);
    }

    public async Task<long> DeleteRowsAsync(string collectionKey,
                                            ISet<string> keys,
                                            CancellationToken cancellationToken)
    {
        long number = 0;
        foreach (var key in keys)
        {
            await DeleteRowAsync(collectionKey, key, cancellationToken);
            number++;
        }

        return number;
    }

    public async Task SetRowAsync(string collectionKey,
                                  IDictionary<string, string> entries,
                                  CancellationToken cancellationToken)
    {
        foreach (var key in entries)
        {
            await SetRowAsync(collectionKey, key.Key, key.Value, cancellationToken);
        }
    }

    public Task<bool> DeleteRowAsync(string collectionKey,
                                     string key,
                                     CancellationToken cancellationToken)
    {
        _cacheProvider.Remove(GetFullKey(collectionKey, key));

        return Task.FromResult(true);
    }

    public Task SetRowAsync(string collectionKey, string key, string entry, CancellationToken cancellationToken)
    {
        _cacheProvider.Set(GetFullKey(collectionKey, key), entry);

        return Task.CompletedTask;
    }

    private static string GetFullKey(string collectionKey, string key) => $"{collectionKey}_{key}";
}