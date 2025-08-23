using Krosoft.Extensions.Cqrs.Models.Commands;
using MediatR;

namespace Krosoft.Extensions.Application.Cache.Distributed.Redis.Models.Commands;

public record TenantCacheRefreshCommand : AuthBaseCommand<Unit>
{
    public TenantCacheRefreshCommand(bool isUserIdRequired, bool isTenantRequired)
    {
        IsUserIdRequired = isUserIdRequired;
        IsTenantIdRequired = isTenantRequired;
    }

    public TenantCacheRefreshCommand(bool isUserIdRequired)
        : this(isUserIdRequired, false)
    {
    }

    public override bool IsUserIdRequired { get; }
    public override bool IsTenantIdRequired { get; }
}