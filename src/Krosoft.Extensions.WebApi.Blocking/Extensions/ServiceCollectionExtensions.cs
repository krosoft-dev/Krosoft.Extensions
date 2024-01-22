using Krosoft.Extensions.Blocking.Abstractions.Interfaces;
using Krosoft.Extensions.Blocking.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Positive.Extensions.Identity.Cache.Distributed.Middlewares;

namespace Krosoft.Extensions.WebApi.Blocking.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBlocking(this IServiceCollection services)
    {
   
        services.AddTransient<BlockingMiddleware>();

        return services;
    } public static IServiceCollection AddAccessTokenProvider(this IServiceCollection services)
    {
  

        services.AddTransient<IAccessTokenProvider, HttpAccessTokenProvider>();
         
        return services;
    }
}