﻿namespace Krosoft.Extensions.Blocking.Abstractions.Interfaces;

public interface IIdentifierBlockingService
{
    Task BlockAsync(string identifier, CancellationToken cancellationToken);
    Task BlockAsync(ISet<string> identifiers, CancellationToken cancellationToken);
    Task BlockAsync(CancellationToken cancellationToken);
    Task<bool> IsBlockedAsync(CancellationToken cancellationToken);
    Task<bool> UnblockAsync(string identifier, CancellationToken cancellationToken);
    Task<long> UnblockAsync(ISet<string> identifiers, CancellationToken cancellationToken);
}