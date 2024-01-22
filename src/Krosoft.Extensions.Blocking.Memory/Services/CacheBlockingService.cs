//using Krosoft.Extensions.Cache.Memory.Interfaces;
//using Krosoft.Extensions.Identity.Abstractions.Models;
//using Microsoft.Extensions.Logging;
//using Positive.Extensions.Cache.Memory.Interfaces;
//using Positive.Extensions.Identity.Abstractions.Models;

//namespace IzRoadbook.Extensions.Services;

//public abstract class CacheBlockingService
//{
//    private const string Blocked = "blocked";
//    private readonly BlockType _blockType;
//    private readonly ICacheProvider _distributedCacheProvider;
//    private readonly ILogger<CacheBlockingService> _logger;

//    protected CacheBlockingService(BlockType blockType,
//                                ICacheProvider distributedCacheProvider,
//                                ILogger<CacheBlockingService> logger)
//    {
//        _blockType = blockType;
//        _distributedCacheProvider = distributedCacheProvider;
//        _logger = logger;
//    }

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
//        _distributedCacheProvider.Set(GetFullKey(collectionKey, key), Blocked);

//        await Task.CompletedTask;
//    }

//    protected string GetCollectionKey() => $"Blocking_{_blockType.ToString()}";



//    protected Task<bool> IsBlockedAsync(string collectionKey,
//                                        string key,
//                                        CancellationToken cancellationToken)
//    {
//        var isExist = _distributedCacheProvider.IsSet(GetFullKey(collectionKey, key));
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

//        _distributedCacheProvider.Remove(GetFullKey(collectionKey, key));
//        return Task.FromResult(true);
//    }
//}