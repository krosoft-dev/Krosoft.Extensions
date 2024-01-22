using Krosoft.Extensions.Blocking.Abstractions.Interfaces;
using Krosoft.Extensions.Blocking.Abstractions.Models.Enums;
using Krosoft.Extensions.Core.Tools;
using Microsoft.Extensions.Logging;

namespace Krosoft.Extensions.Blocking.Services;

public class IdentifierBlockingService : BlockingService, IIdentifierBlockingService
{
    private readonly IIdentifierProvider _identifierProvider; 

    public IdentifierBlockingService(IBlockingStorageProvider blockingStorageProvider,
                              
                                     ILogger<IdentifierBlockingService> logger,
                                     IIdentifierProvider identifierProvider)
        : base(BlockType.Identifier, blockingStorageProvider, logger)
    {
        _identifierProvider = identifierProvider;
    }

    public async Task<bool> IsBlockedAsync(CancellationToken cancellationToken)
    {
        var collectionKey = GetCollectionKey(); 
        var identifier = await _identifierProvider.GetIdentifierAsync(cancellationToken);
        var isBlocked = await IsBlockedAsync(collectionKey, identifier, cancellationToken);
        return isBlocked;
    }

    public async Task BlockAsync(string identifier, CancellationToken cancellationToken)
    {
        var collectionKey = GetCollectionKey();
        await BlockAsync(collectionKey, identifier, cancellationToken);
    }

    public async Task BlockAsync(ISet<string> identifiers, CancellationToken cancellationToken)
    {
        Guard.IsNotNull(nameof(identifiers), identifiers);

        var collectionKey = GetCollectionKey();

        await BlockAsync(collectionKey, identifiers, cancellationToken);
    }

    public async Task<bool> UnblockAsync(string identifier, CancellationToken cancellationToken)
    {
        var collectionKey = GetCollectionKey();
        return await UnblockAsync(collectionKey, identifier, cancellationToken);
    }

    public async Task<long> UnblockAsync(ISet<string> identifiers, CancellationToken cancellationToken)
    {
        Guard.IsNotNull(nameof(identifiers), identifiers);

        var collectionKey = GetCollectionKey();

        return await UnblockAsync(collectionKey, identifiers, cancellationToken);
    }
}