using Krosoft.Extensions.Data.Abstractions.Interfaces;
using Krosoft.Extensions.Data.EntityFramework.Audits.Services;
using Krosoft.Extensions.Data.EntityFramework.Interfaces;
using Krosoft.Extensions.Data.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Krosoft.Extensions.Data.EntityFramework.Audits.Extensions;

public static class ServiceCollectionExtensions
{
    private static readonly object DbLock = new object();

    public static IServiceCollection AddDbContextSettings(this IServiceCollection services)
    {
        services.AddScoped<IDbContextSettingsProvider2, HttpDbContextSettingsProvider2>();

        return services;
    }


     
}