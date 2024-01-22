using Krosoft.Extensions.Blocking.Abstractions.Interfaces;
using Krosoft.Extensions.Cache.Distributed.Redis.Interfaces;

namespace Krosoft.Extensions.Blocking.Redis.Services;

public class RedisBlockingStorageProvider : IBlockingStorageProvider
{
//    private const string Blocked = "blocked";
//    private readonly BlockType _blockType;
    private readonly IDistributedCacheProvider _distributedCacheProvider;

    public RedisBlockingStorageProvider(IDistributedCacheProvider distributedCacheProvider)
    {
        _distributedCacheProvider = distributedCacheProvider;
    }
    //    private readonly ILogger<DistributedBlockingService> _logger;

//    protected string GetCollectionKey() => $"Blocking_{_blockType.ToString()}";

//    protected async Task<bool> IsBlockedAsync(string collectionKey,
//                                              string key,
//                                              CancellationToken cancellationToken)
//    {
//        var isExist = await _distributedCacheProvider.IsExistRowAsync(collectionKey, key, cancellationToken);
//        if (isExist)
//        {
//            _logger.LogDebug($"{_blockType} is blocked : {key}");
//        }

//        return isExist;
//    }

//    protected async Task BlockAsync(string collectionKey,
//                                    ISet<string> keys,
//                                    CancellationToken cancellationToken)
//    {
//        _logger.LogDebug($"Blocking {_blockType} : {string.Join(",", keys)}");

//        var entries = new Dictionary<string, string>();
//        foreach (var key in keys)
//        {
//            entries.Add(key, Blocked);
//        }

//        await _distributedCacheProvider.SetRowAsync(collectionKey, entries, cancellationToken);
//    } 

    public virtual async Task<bool> IsExistRowAsync(string collectionKey,
                                                    string key,
                                                    CancellationToken cancellationToken)
    {
        
        var isExist = await _distributedCacheProvider.IsExistRowAsync(collectionKey, key, cancellationToken);
//      


        return isExist;
    }

    public virtual async Task<long> DeleteRowsAsync(string collectionKey,
                                                    ISet<string> keys,
                                                    CancellationToken cancellationToken)
    { var number = await _distributedCacheProvider.DeleteRowsAsync(collectionKey, keys, cancellationToken);
        return number;
    }

    public virtual async Task SetRowAsync(string collectionKey,
                                          IDictionary<string, string> entries,
                                          CancellationToken cancellationToken)
   {
       await _distributedCacheProvider.SetRowAsync(collectionKey,  entries, cancellationToken);
   }

    public virtual async Task<bool> DeleteRowAsync(string collectionKey,
                                                   string key,
                                                   CancellationToken cancellationToken)
    {
         

        var isDelete = await _distributedCacheProvider.DeleteRowAsync(collectionKey, key, cancellationToken);
        return isDelete;
    }

    public virtual async Task SetRowAsync(string collectionKey,
                                          string entryKey,
                                          string entry,
                                          CancellationToken cancellationToken)
    {
        await _distributedCacheProvider.SetRowAsync(collectionKey, entryKey, entry, cancellationToken);
    }
}