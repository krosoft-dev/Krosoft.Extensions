using Krosoft.Extensions.Blocking.Abstractions.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Positive.Extensions.Identity.Abstractions.Interfaces;
using Positive.Extensions.Identity.Cache.Distributed.Middlewares;
using Positive.Extensions.Identity.Cache.Distributed.Services;
using Positive.Extensions.Identity.Extensions;
using Positive.Extensions.Identity.Services;

namespace Positive.Extensions.Identity.Cache.Distributed.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBlocking(this IServiceCollection services,
                                                 IConfiguration configuration)
    {
  

        services.AddSingleton<IHttpContextService, HttpContextService>();
        services.AddTransient<BlockingMiddleware>();

        return services;
    }
}