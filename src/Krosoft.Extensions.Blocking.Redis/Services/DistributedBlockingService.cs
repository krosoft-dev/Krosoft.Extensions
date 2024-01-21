using Krosoft.Extensions.Cache.Distributed.Redis.Interfaces;
using Microsoft.Extensions.Logging;
using Positive.Extensions.Cache.Distributed.Redis.Interfaces;
using Positive.Extensions.Identity.Abstractions.Models;

namespace Positive.Extensions.Identity.Cache.Distributed.Services;

public abstract class DistributedBlockingService
{
    private const string Blocked = "blocked";
    private readonly BlockType _blockType;
    private readonly IDistributedCacheProvider _distributedCacheProvider;
    private readonly ILogger<DistributedBlockingService> _logger;

    protected DistributedBlockingService(BlockType blockType,
                                         IDistributedCacheProvider distributedCacheProvider,
                                         ILogger<DistributedBlockingService> logger)
    {
        _blockType = blockType;
        _distributedCacheProvider = distributedCacheProvider;
        _logger = logger;
    }

    protected string GetCollectionKey() => $"Blocking_{_blockType.ToString()}";

    protected async Task<bool> IsBlockedAsync(string collectionKey,
                                              string key,
                                              CancellationToken cancellationToken)
    {
        var isExist = await _distributedCacheProvider.IsExistRowAsync(collectionKey, key, cancellationToken);
        if (isExist)
        {
            _logger.LogDebug($"{_blockType} is blocked : {key}");
        }

        return isExist;
    }

    protected async Task BlockAsync(string collectionKey,
                                    ISet<string> keys,
                                    CancellationToken cancellationToken)
    {
        _logger.LogDebug($"Blocking {_blockType} : {string.Join(",", keys)}");

        var entries = new Dictionary<string, string>();
        foreach (var key in keys)
        {
            entries.Add(key, Blocked);
        }

        await _distributedCacheProvider.SetRowAsync(collectionKey, entries, cancellationToken);
    }

    protected async Task<long> UnblockAsync(string collectionKey,
                                            ISet<string> keys,
                                            CancellationToken cancellationToken)
    {
        _logger.LogDebug($"Unblocking {_blockType} : {string.Join(",", keys)}");

        var number = await _distributedCacheProvider.DeleteRowsAsync(collectionKey, keys, cancellationToken);
        return number;
    }

    protected async Task BlockAsync(string collectionKey,
                                    string key,
                                    CancellationToken cancellationToken)
    {
        _logger.LogDebug($"Blocking {_blockType} : {key}");
        await _distributedCacheProvider.SetRowAsync(collectionKey, key, Blocked, cancellationToken);
    }

    protected async Task<bool> UnblockAsync(string collectionKey,
                                            string key,
                                            CancellationToken cancellationToken)
    {
        _logger.LogDebug($"Unblocking {_blockType} : {key}");

        var isDelete = await _distributedCacheProvider.DeleteRowAsync(collectionKey, key, cancellationToken);
        return isDelete;
    }
}