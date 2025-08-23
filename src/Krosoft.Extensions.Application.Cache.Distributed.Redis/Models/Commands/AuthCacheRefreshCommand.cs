using Krosoft.Extensions.Cqrs.Models.Commands;
using MediatR;

namespace Krosoft.Extensions.Application.Cache.Distributed.Redis.Models.Commands;

public record AuthCacheRefreshCommand : AuthBaseCommand<Unit>
{
    public AuthCacheRefreshCommand(bool isUserIdRequired, bool isTenantRequired)
    {
        IsUserIdRequired = isUserIdRequired;
        IsTenantIdRequired = isTenantRequired;
    }

    public AuthCacheRefreshCommand(bool isUserIdRequired)
        : this(isUserIdRequired, false)
    {
    }

    public override bool IsUserIdRequired { get; }
    public override bool IsTenantIdRequired { get; }
}