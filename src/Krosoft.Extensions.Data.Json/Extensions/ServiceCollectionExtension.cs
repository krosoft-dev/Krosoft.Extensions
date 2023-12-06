using Krosoft.Extensions.Core.Legacy.Interfaces;
using Krosoft.Extensions.Core.Legacy.Models;
using Krosoft.Extensions.Core.Legacy.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Krosoft.Extensions.Core.Legacy.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddJsonDataService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions();
        services.Configure<JsonDataSettings>(configuration.GetSection(nameof(JsonDataSettings)));
        services.AddScoped(typeof(IJsonDataService<>), typeof(JsonDataService<>));
        return services;
    }
}