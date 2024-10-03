using Krosoft.Extensions.Http.Interfaces;
using Krosoft.Extensions.Http.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Krosoft.Extensions.Http.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddHttpContextService(this IServiceCollection services)
    {
        services.AddTransient<IHttpContextService, HttpContextService>();

        return services;
    }
}