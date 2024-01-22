using Krosoft.Extensions.WebApi.Blocking.Middlewares;
using Microsoft.Extensions.DependencyInjection;

namespace Krosoft.Extensions.WebApi.Blocking.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBlocking(this IServiceCollection services)
    {
        services.AddTransient<BlockingMiddleware>();

        return services;
    }
}