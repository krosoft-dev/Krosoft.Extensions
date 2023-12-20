using Krosoft.Extensions.Data.Abstractions.Interfaces;
using Krosoft.Extensions.Data.EntityFramework.Identity.Services;
using Krosoft.Extensions.Data.EntityFramework.Interfaces;
using Krosoft.Extensions.Data.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Krosoft.Extensions.Data.EntityFramework.Identity.Extensions;

public static class ServiceCollectionExtensions
{
    private static readonly object DbLock = new object();

    public static IServiceCollection AddDbContextSettings(this IServiceCollection services)
    {
        services.AddScoped<IDbContextSettingsProvider, HttpDbContextSettingsProvider>();

        return services;
    }
     
}