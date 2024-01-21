using Krosoft.Extensions.Blocking.Abstractions.Interfaces;
using Krosoft.Extensions.Blocking.Abstractions.Models.Enums;
using Krosoft.Extensions.Core.Tools;
using Microsoft.Extensions.Logging;

namespace Krosoft.Extensions.Blocking.Services;

public class IpBlockingService : BlockingService, IIpBlockingService
{
    public IpBlockingService(IBlockingStorageProvider blockingStorageProvider,
                             ILogger<IpBlockingService> logger)
        : base(BlockType.Ip, blockingStorageProvider, logger)
    {
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

    public async Task<bool> IsBlockedAsync(string remoteIp, CancellationToken cancellationToken)
    {
        var collectionKey = GetCollectionKey();
        var isBlocked = await IsBlockedAsync(collectionKey, remoteIp, cancellationToken);
        return isBlocked;
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