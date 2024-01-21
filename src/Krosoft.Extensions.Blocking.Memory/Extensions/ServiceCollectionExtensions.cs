using IzRoadbook.Extensions.Services;
using Krosoft.Extensions.Blocking.Abstractions.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Krosoft.Extensions.Blocking.Memory.Extensions;

public static class ServiceCollectionExtensions
{
    
    public static IServiceCollection AddMemoryBlockingStorage(this IServiceCollection services
                                                        )
    {
        
        services.AddTransient<IBlockingStorageProvider, MemoryBlockingStorageProvider>();
 

        return services;
    }

     
}