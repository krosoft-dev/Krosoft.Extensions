using Krosoft.Extensions.Blocking.Abstractions.Interfaces;
using Krosoft.Extensions.Blocking.Abstractions.Models.Enums;
using Microsoft.Extensions.Logging;

namespace Krosoft.Extensions.Blocking.Services;

public abstract class BlockingService
{
    private const string Blocked = "blocked";
    private readonly IBlockingStorageProvider _blockingStorageProvider;
    private readonly BlockType _blockType;
    private readonly ILogger<BlockingService> _logger;

    protected BlockingService(BlockType blockType,
                              IBlockingStorageProvider blockingStorageProvider,
                              ILogger<BlockingService> logger)
    {
        _blockType = blockType;
        _blockingStorageProvider = blockingStorageProvider;
        _logger = logger;
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

        await _blockingStorageProvider.SetRowAsync(collectionKey, entries, cancellationToken);
    }

    protected async Task BlockAsync(string collectionKey,
                                    string key,
                                    CancellationToken cancellationToken)
    {
        _logger.LogDebug($"Blocking {_blockType} : {key}");
        await _blockingStorageProvider.SetRowAsync(collectionKey, key, Blocked, cancellationToken);
    }

    protected string GetCollectionKey() => $"Blocking_{_blockType.ToString()}";

    protected async Task<bool> IsBlockedAsync(string collectionKey,
                                              string key,
                                              CancellationToken cancellationToken)
    {
        var isExist = await _blockingStorageProvider.IsExistRowAsync(collectionKey, key, cancellationToken);
        if (isExist)
        {
            _logger.LogDebug($"{_blockType} is blocked : {key}");
        }

        return isExist;
    }

    protected async Task<long> UnblockAsync(string collectionKey,
                                            ISet<string> keys,
                                            CancellationToken cancellationToken)
    {
        _logger.LogDebug($"Unblocking {_blockType} : {string.Join(",", keys)}");

        var number = await _blockingStorageProvider.DeleteRowsAsync(collectionKey, keys, cancellationToken);
        return number;
    }

    protected async Task<bool> UnblockAsync(string collectionKey,
                                            string key,
                                            CancellationToken cancellationToken)
    {
        _logger.LogDebug($"Unblocking {_blockType} : {key}");

        var isDelete = await _blockingStorageProvider.DeleteRowAsync(collectionKey, key, cancellationToken);
        return isDelete;
    }
}