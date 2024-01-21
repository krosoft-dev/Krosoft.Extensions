using Microsoft.AspNetCore.Builder;
using Positive.Extensions.Identity.Cache.Distributed.Middlewares;

namespace Krosoft.Extensions.WebApi.Blocking.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseBlocking(this IApplicationBuilder app)
        => app.UseMiddleware<BlockingMiddleware>();
}