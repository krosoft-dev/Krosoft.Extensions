using IzRoadbook.Extensions.Services;
using Krosoft.Extensions.Blocking.Abstractions.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Positive.Extensions.Identity.Cache.Distributed.Services;

namespace Krosoft.Extensions.Blocking.Redis.Extensions;

public static class ServiceCollectionExtensions
{
    
    public static IServiceCollection AddRedisBlockingStorage(this IServiceCollection services
    )
    {
        
        services.AddTransient<IBlockingStorageProvider, RedisBlockingStorageProvider>();
 

        return services;
    }

     
}