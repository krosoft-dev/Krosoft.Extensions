//using Microsoft.Extensions.Logging;
//using Positive.Extensions.Cache.Distributed.Redis.Interfaces;
//using Positive.Extensions.Core.Tools;
//using Positive.Extensions.Identity.Abstractions.Interfaces;
//using Positive.Extensions.Identity.Abstractions.Models;

//namespace Positive.Extensions.Identity.Cache.Distributed.Services;

//public class DistributedIdentifierBlockingService : DistributedBlockingService, IIdentifierBlockingService
//{
//    private readonly IHttpContextService _httpContextService;
//    private readonly IJwtTokenGenerator _jwtTokenGenerator;

//    public DistributedIdentifierBlockingService(IDistributedCacheProvider distributedCacheProvider,
//                                                IHttpContextService httpContextService,
//                                                IJwtTokenGenerator jwtTokenGenerator,
//                                                ILogger<DistributedIdentifierBlockingService> logger)
//        : base(BlockType.Identifier, distributedCacheProvider, logger)
//    {
//        _httpContextService = httpContextService;
//        _jwtTokenGenerator = jwtTokenGenerator;
//    }

//    public async Task<bool> IsBlockedAsync(CancellationToken cancellationToken)
//    {
//        var collectionKey = GetCollectionKey();
//        var accessToken = await _httpContextService.GetAccessTokenAsync();
//        var identifier = _jwtTokenGenerator.GetIdentifierFromToken(accessToken);
//        var isBlocked = await IsBlockedAsync(collectionKey, identifier, cancellationToken);
//        return isBlocked;
//    }

//    public async Task BlockAsync(string identifier, CancellationToken cancellationToken)
//    {
//        var collectionKey = GetCollectionKey();
//        await BlockAsync(collectionKey, identifier, cancellationToken);
//    }

//    public async Task BlockAsync(ISet<string> identifiers, CancellationToken cancellationToken)
//    {
//        Guard.IsNotNull(nameof(identifiers), identifiers);

//        var collectionKey = GetCollectionKey();

//        await BlockAsync(collectionKey, identifiers, cancellationToken);
//    }

//    public async Task<bool> UnblockAsync(string identifier, CancellationToken cancellationToken)
//    {
//        var collectionKey = GetCollectionKey();
//        return await UnblockAsync(collectionKey, identifier, cancellationToken);
//    }

//    public async Task<long> UnblockAsync(ISet<string> identifiers, CancellationToken cancellationToken)
//    {
//        Guard.IsNotNull(nameof(identifiers), identifiers);

//        var collectionKey = GetCollectionKey();

//        return await UnblockAsync(collectionKey, identifiers, cancellationToken);
//    }
//}