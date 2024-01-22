using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Positive.Extensions.Identity.Cache.Distributed.Middlewares;

namespace Krosoft.Extensions.WebApi.Blocking.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBlocking(this IServiceCollection services)
    {
  

        //services.AddSingleton<IHttpContextService, HttpContextService>();
        services.AddTransient<BlockingMiddleware>();

        return services;
    }
}