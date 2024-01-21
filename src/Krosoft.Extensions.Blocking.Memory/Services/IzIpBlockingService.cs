using Krosoft.Extensions.Blocking.Abstractions.Interfaces;
using Krosoft.Extensions.Blocking.Abstractions.Models.Enums;
using Krosoft.Extensions.Cache.Memory.Interfaces;
using Krosoft.Extensions.Core.Tools;
using Microsoft.Extensions.Logging;
using Positive.Extensions.Cache.Memory.Interfaces;
using Positive.Extensions.Core.Tools;
using Positive.Extensions.Identity.Abstractions.Interfaces;
using Positive.Extensions.Identity.Abstractions.Models;

namespace IzRoadbook.Extensions.Services;

public class IzIpBlockingService : CacheBlockingService, IIpBlockingService
{
    public IzIpBlockingService(ICacheProvider distributedCacheProvider,
                               ILogger<IzIpBlockingService> logger)
        : base(BlockType.Ip, distributedCacheProvider, logger)
    {
    }

    public async Task<bool> IsBlockedAsync(string remoteIp, CancellationToken cancellationToken)
    {
        var collectionKey = GetCollectionKey();
        var isBlocked = await IsBlockedAsync(collectionKey, remoteIp, cancellationToken);
        return isBlocked;
    }

    public async Task BlockAsync(string remoteIp,
                                 CancellationToken cancellationToken)
    {
        var collectionKey = GetCollectionKey();
        await BlockAsync(collectionKey, remoteIp, cancellationToken);
    }

    public async Task BlockAsync(ISet<string> remotesIp, CancellationToken cancellationToken)
    {
        Guard.IsNotNull(nameof(remotesIp), remotesIp);

        var collectionKey = GetCollectionKey();
        await BlockAsync(collectionKey, remotesIp, cancellationToken);
    }

    public async Task<bool> UnblockAsync(string remoteIp,
                                         CancellationToken cancellationToken)
    {
        var collectionKey = GetCollectionKey();
        return await UnblockAsync(collectionKey, remoteIp, cancellationToken);
    }

    public async Task<long> UnblockAsync(ISet<string> remotesIp, CancellationToken cancellationToken)
    {
        Guard.IsNotNull(nameof(remotesIp), remotesIp);

        var collectionKey = GetCollectionKey();
        return await UnblockAsync(collectionKey, remotesIp, cancellationToken);
    }
}

public class MemoryBlockingStorageProvider : IBlockingStorageProvider
{

}
