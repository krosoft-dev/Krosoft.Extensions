//using Krosoft.Extensions.Blocking.Abstractions.Interfaces;
//using Krosoft.Extensions.Blocking.Abstractions.Models.Enums;
//using Krosoft.Extensions.Cache.Memory.Interfaces;
//using Krosoft.Extensions.Core.Tools;
//using Microsoft.Extensions.Logging;
//using Positive.Extensions.Cache.Memory.Interfaces;
//using Positive.Extensions.Core.Tools;
//using Positive.Extensions.Identity.Abstractions.Interfaces;
//using Positive.Extensions.Identity.Abstractions.Models;

//namespace IzRoadbook.Extensions.Services;

//public class IzAccessTokenBlockingService : CacheBlockingService, IAccessTokenBlockingService
//{
//    private readonly IHttpContextService _httpContextService;

//    public IzAccessTokenBlockingService(ICacheProvider distributedCacheProvider,
//                                        IHttpContextService httpContextService,
//                                        ILogger<IzAccessTokenBlockingService> logger)
//        : base(BlockType.AccessToken, distributedCacheProvider, logger)
//    {
//        _httpContextService = httpContextService;
//    }

//    public async Task BlockAsync(string accessToken, CancellationToken cancellationToken)
//    {
//        var collectionKey = GetCollectionKey();
//        await BlockAsync(collectionKey, accessToken, cancellationToken);
//    }

//    public async Task BlockAsync(ISet<string> accessTokens, CancellationToken cancellationToken)

//    {
//        Guard.IsNotNull(nameof(accessTokens), accessTokens);

//        var collectionKey = GetCollectionKey();

//        await BlockAsync(collectionKey, accessTokens, cancellationToken);
//    }

//    public async Task BlockAsync(CancellationToken cancellationToken)
//    {
//        var collectionKey = GetCollectionKey();
//        var accessToken = await _httpContextService.GetAccessTokenAsync();
//        await BlockAsync(collectionKey, accessToken, cancellationToken);
//    }

//    public async Task<bool> IsBlockedAsync(CancellationToken cancellationToken)
//    {
//        var collectionKey = GetCollectionKey();
//        var accessToken = await _httpContextService.GetAccessTokenAsync();
//        var isBlocked = await IsBlockedAsync(collectionKey, accessToken, cancellationToken);

//        return isBlocked;
//    }

//    public async Task<bool> UnblockAsync(string accessToken, CancellationToken cancellationToken)
//    {
//        var collectionKey = GetCollectionKey();
//        return await UnblockAsync(collectionKey, accessToken, cancellationToken);
//    }

//    public async Task<long> UnblockAsync(ISet<string> accessTokens,
//                                         CancellationToken cancellationToken)
//    {
//        Guard.IsNotNull(nameof(accessTokens), accessTokens);

//        var collectionKey = GetCollectionKey();
//        return await UnblockAsync(collectionKey, accessTokens, cancellationToken);
//    }
//}