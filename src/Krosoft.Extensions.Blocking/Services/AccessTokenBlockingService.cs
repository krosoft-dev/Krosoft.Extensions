using Krosoft.Extensions.Blocking.Abstractions.Interfaces;
using Krosoft.Extensions.Blocking.Abstractions.Models.Enums;
using Krosoft.Extensions.Core.Models.Exceptions;
using Krosoft.Extensions.Core.Tools;
using Krosoft.Extensions.Identity.Abstractions.Interfaces;
using Microsoft.Extensions.Logging;

namespace Krosoft.Extensions.Blocking.Services;

public class AccessTokenBlockingService : BlockingService, IAccessTokenBlockingService
{
    private readonly IAccessTokenProvider _accessTokenProvider;

    public AccessTokenBlockingService(IBlockingStorageProvider blockingStorageProvider,
                                      ILogger<AccessTokenBlockingService> logger,
                                      IAccessTokenProvider accessTokenProvider)
        : base(BlockType.AccessToken, blockingStorageProvider, logger)
    {
        _accessTokenProvider = accessTokenProvider;
    }

    public async Task<bool> IsBlockedAsync(CancellationToken cancellationToken)
    {
        var collectionKey = GetCollectionKey();

        var accessToken = await GetAccessTokenAsync(cancellationToken);
        var isBlocked = await IsBlockedAsync(collectionKey, accessToken, cancellationToken);

        return isBlocked;
    }

    public async Task BlockAsync(string accessToken, CancellationToken cancellationToken)
    {
        var collectionKey = GetCollectionKey();
        await BlockAsync(collectionKey, accessToken, cancellationToken);
    }

    public async Task BlockAsync(ISet<string> accessTokens, CancellationToken cancellationToken)

    {
        Guard.IsNotNull(nameof(accessTokens), accessTokens);

        var collectionKey = GetCollectionKey();

        await BlockAsync(collectionKey, accessTokens, cancellationToken);
    }

    public async Task BlockAsync(CancellationToken cancellationToken)
    {
        var collectionKey = GetCollectionKey();
        var accessToken = await GetAccessTokenAsync(cancellationToken);
        await BlockAsync(collectionKey, accessToken, cancellationToken);
    }

    public async Task<bool> UnblockAsync(string accessToken, CancellationToken cancellationToken)
    {
        var collectionKey = GetCollectionKey();
        return await UnblockAsync(collectionKey, accessToken, cancellationToken);
    }

    public async Task<long> UnblockAsync(ISet<string> accessTokens,
                                         CancellationToken cancellationToken)
    {
        Guard.IsNotNull(nameof(accessTokens), accessTokens);

        var collectionKey = GetCollectionKey();
        return await UnblockAsync(collectionKey, accessTokens, cancellationToken);
    }

    private async Task<string> GetAccessTokenAsync(CancellationToken cancellationToken)
    {
        var accessToken = await _accessTokenProvider.GetAccessTokenAsync(cancellationToken);
        if (string.IsNullOrEmpty(accessToken))
        {
            throw new KrosoftTechniqueException("Impossible d'obtenir le token d'accès !");
        }

        return accessToken;
    }
}