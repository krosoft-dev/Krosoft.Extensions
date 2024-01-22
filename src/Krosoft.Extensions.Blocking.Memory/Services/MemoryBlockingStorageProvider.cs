using Krosoft.Extensions.Blocking.Abstractions.Interfaces;
using Krosoft.Extensions.Cache.Memory.Interfaces;

namespace IzRoadbook.Extensions.Services;

public class MemoryBlockingStorageProvider : IBlockingStorageProvider
{
    private readonly ICacheProvider _cacheProvider;

    public MemoryBlockingStorageProvider(ICacheProvider cacheProvider)
    {
        _cacheProvider = cacheProvider;
    }

    //using Krosoft.Extensions.Cache.Memory.Interfaces;
//using Krosoft.Extensions.Identity.Abstractions.Models;
//using Microsoft.Extensions.Logging;
//using Positive.Extensions.Cache.Memory.Interfaces;
//using Positive.Extensions.Identity.Abstractions.Models;

//   
//    private readonly ILogger<CacheBlockingService> _logger;

 

//    protected async Task BlockAsync(string collectionKey,
//                                    ISet<string> keys,
//                                    CancellationToken cancellationToken)
//    {
//        _logger.LogDebug($"Blocking {_blockType} : {string.Join(",", keys)}");

//        foreach (var key in keys)
//        {
//            await BlockAsync(collectionKey, key, cancellationToken);
//        }
//    }

//    protected async Task BlockAsync(string collectionKey,
//                                    string key,
//                                    CancellationToken cancellationToken)
//    {
//        _logger.LogDebug($"Blocking {_blockType} : {key}");
//        _cacheProvider.Set(GetFullKey(collectionKey, key), Blocked);

//        await Task.CompletedTask;
//    }

//    protected string GetCollectionKey() => $"Blocking_{_blockType.ToString()}";

//    private static string GetFullKey(string collectionKey, string key) => $"{collectionKey}_{key}";

//    protected Task<bool> IsBlockedAsync(string collectionKey,
//                                        string key,
//                                        CancellationToken cancellationToken)
//    {
//        var isExist = _cacheProvider.IsSet(GetFullKey(collectionKey, key));
//        if (isExist)
//        {
//            _logger.LogDebug($"{_blockType} is blocked : {key}");
//        }

//        return Task.FromResult(isExist);
//    }

//    protected async Task<long> UnblockAsync(string collectionKey,
//                                            ISet<string> keys,
//                                            CancellationToken cancellationToken)
//    {
//        _logger.LogDebug($"Unblocking {_blockType} : {string.Join(",", keys)}");

//        long number = 0;
//        foreach (var key in keys)
//        {
//            await UnblockAsync(collectionKey, key, cancellationToken);
//            number++;
//        }

//        return number;
//    }

 

    private static string GetFullKey(string collectionKey, string key) => $"{collectionKey}_{key}";

    public   Task<bool> IsExistRowAsync(string collectionKey,
                                            string key,
                                            CancellationToken cancellationToken)
    {
            var isExist = _cacheProvider.IsSet(GetFullKey(collectionKey, key));
            return     Task.FromResult(isExist);
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
            await SetRowAsync(collectionKey, key.Key, key.Value,  cancellationToken);
        }
    }
      





    public   Task<bool> DeleteRowAsync(string collectionKey,
                                           string key,
                                           CancellationToken cancellationToken)
    { _cacheProvider.Remove(GetFullKey(collectionKey, key));

        return Task.FromResult(true);
    }

    public   Task SetRowAsync(string collectionKey, string key, string entry, CancellationToken cancellationToken)
    {


      
        _cacheProvider.Set(GetFullKey(collectionKey, key), entry);

        return          Task.CompletedTask;
    }
}
 

 

//    protected async Task BlockAsync(string collectionKey,
//                                    ISet<string> keys,
//                                    CancellationToken cancellationToken)
//    {
//        _logger.LogDebug($"Blocking {_blockType} : {string.Join(",", keys)}");

//        foreach (var key in keys)
//        {
//            await BlockAsync(collectionKey, key, cancellationToken);
//        }
//    }

//    protected async Task BlockAsync(string collectionKey,
//                                    string key,
//                                    CancellationToken cancellationToken)
//    {
//        _logger.LogDebug($"Blocking {_blockType} : {key}");
//        _cacheProvider.Set(GetFullKey(collectionKey, key), Blocked);

//        await Task.CompletedTask;
//    }

//    protected string GetCollectionKey() => $"Blocking_{_blockType.ToString()}";

//    private static string GetFullKey(string collectionKey, string key) => $"{collectionKey}_{key}";

//    protected Task<bool> IsBlockedAsync(string collectionKey,
//                                        string key,
//                                        CancellationToken cancellationToken)
//    {
//        var isExist = _cacheProvider.IsSet(GetFullKey(collectionKey, key));
//        if (isExist)
//        {
//            _logger.LogDebug($"{_blockType} is blocked : {key}");
//        }

//        return Task.FromResult(isExist);
//    }

//    protected async Task<long> UnblockAsync(string collectionKey,
//                                            ISet<string> keys,
//                                            CancellationToken cancellationToken)
//    {
//        _logger.LogDebug($"Unblocking {_blockType} : {string.Join(",", keys)}");

//        long number = 0;
//        foreach (var key in keys)
//        {
//            await UnblockAsync(collectionKey, key, cancellationToken);
//            number++;
//        }

//        return number;
//    }

//    protected Task<bool> UnblockAsync(string collectionKey,
//                                      string key,
//                                      CancellationToken cancellationToken)
//    {
//        _logger.LogDebug($"Unblocking {_blockType} : {key}");

//        _cacheProvider.Remove(GetFullKey(collectionKey, key));
//        return Task.FromResult(true);
//    }
//}