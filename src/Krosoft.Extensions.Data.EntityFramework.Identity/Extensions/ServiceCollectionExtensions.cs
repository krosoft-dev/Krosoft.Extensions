using Krosoft.Extensions.Data.EntityFramework.Identity.Services;
using Krosoft.Extensions.Data.EntityFramework.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Krosoft.Extensions.Data.EntityFramework.Identity.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDbContextSettings(this IServiceCollection services)
    {
        services.AddScoped<IDbContextSettingsProvider, HttpDbContextSettingsProvider>();

        return services;
    }
}