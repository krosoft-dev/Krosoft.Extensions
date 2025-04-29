using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Krosoft.Extensions.Options.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOptionsValidator<TSettings, TValidateOptions>(this IServiceCollection services,
                                                                                      IConfiguration configuration)
        where TSettings : class
        where TValidateOptions : class, IValidateOptions<TSettings>
    {
        var sectionName = typeof(TSettings).Name;
        var options = services.AddOptions<TSettings>()
                              .Bind(configuration.GetSection(sectionName))
                              .ValidateDataAnnotations();

#if NET9_0_OR_GREATER
        options.ValidateOnStart();
#endif

        return services
            .AddSingleton<IValidateOptions<TSettings>, TValidateOptions>();
    }
}