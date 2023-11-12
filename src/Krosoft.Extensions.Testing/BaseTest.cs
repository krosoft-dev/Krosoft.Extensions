using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Krosoft.Extensions.Testing;

public abstract class BaseTest
{
    private static IConfigurationRoot GetConfiguration() =>
        new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true)
            .Build();

    protected ServiceProvider CreateServiceCollection(Action<IServiceCollection> action = null)
    {
        var services = new ServiceCollection();

        var configuration = GetConfiguration();
        services.AddSingleton<IConfiguration>(configuration);
        AddServices(services, configuration);

        if (action != null)
        {
            action(services);
        }

        return services.BuildServiceProvider();
    }

    protected virtual void AddServices(ServiceCollection services, IConfiguration configuration)
    {
    }
}