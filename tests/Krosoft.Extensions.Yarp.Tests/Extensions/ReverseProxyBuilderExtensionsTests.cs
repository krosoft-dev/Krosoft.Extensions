using Krosoft.Extensions.Core.Models.Exceptions;
using Krosoft.Extensions.Yarp.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yarp.ReverseProxy.Configuration;

namespace Krosoft.Extensions.Yarp.Tests.Extensions;

[TestClass]
public class ReverseProxyBuilderExtensionsTests
{
    private static IProxyConfig BuildProxyConfig(Dictionary<string, string?> settings)
    {
        var configuration = new ConfigurationBuilder()
                            .AddInMemoryCollection(settings)
                            .Build();

        var services = new ServiceCollection();
        services.AddReverseProxy()
                .LoadFromCustomConfig(configuration);

        var serviceProvider = services.BuildServiceProvider();
        var configProvider = serviceProvider.GetRequiredService<IProxyConfigProvider>();

        return configProvider.GetConfig();
    }

    [TestMethod]
    public void LoadFromCustomConfig_UnService_GenereRouteEtCluster()
    {
        var config = BuildProxyConfig(new Dictionary<string, string?>
        {
            ["CustomReverseProxySettings:Services:Domotique:Destination"] = "http://eva.domotique.api:5000"
        });

        Check.That(config.Routes).HasSize(1);

        var route = config.Routes.Single();
        Check.That(route.RouteId).IsEqualTo("Domotique");
        Check.That(route.ClusterId).IsEqualTo("Domotique");
        Check.That(route.Match.Path).IsEqualTo("/Domotique/{**catch-all}");

        Check.That(config.Clusters).HasSize(1);

        var cluster = config.Clusters.Single();
        Check.That(cluster.ClusterId).IsEqualTo("Domotique");
        Check.That(cluster.Destinations!.Single().Value.Address).IsEqualTo("http://eva.domotique.api:5000");
    }

    [TestMethod]
    public void LoadFromCustomConfig_UnService_AjouteLeTransformDeSuppressionDePrefixe()
    {
        var config = BuildProxyConfig(new Dictionary<string, string?>
        {
            ["CustomReverseProxySettings:Services:Domotique:Destination"] = "http://eva.domotique.api:5000"
        });

        var route = config.Routes.Single();
        Check.That(route.Transforms).IsNotNull();
        Check.That(route.Transforms!.Any(t => t.TryGetValue("PathPattern", out var value) && value == "{**catch-all}")).IsTrue();
    }

    [TestMethod]
    public void LoadFromCustomConfig_PlusieursServices_GenereUneRouteParService()
    {
        var config = BuildProxyConfig(new Dictionary<string, string?>
        {
            ["CustomReverseProxySettings:Services:Domotique:Destination"] = "http://domotique:5000",
            ["CustomReverseProxySettings:Services:Sonos:Destination"] = "http://sonos:5000"
        });

        Check.That(config.Routes).HasSize(2);
        Check.That(config.Routes.Select(r => r.RouteId)).Contains("Domotique", "Sonos");
        Check.That(config.Routes.Select(r => r.Match.Path)).Contains("/Domotique/{**catch-all}", "/Sonos/{**catch-all}");
        Check.That(config.Clusters.Select(c => c.ClusterId)).Contains("Domotique", "Sonos");
    }

    [TestMethod]
    public void LoadFromCustomConfig_DestinationVide_LeveUneException()
    {
        Check.ThatCode(() => BuildProxyConfig(new Dictionary<string, string?>
              {
                  ["CustomReverseProxySettings:Services:Domotique:Destination"] = ""
              }))
             .Throws<KrosoftTechnicalException>()
             .WithMessage("Destination du service 'Domotique' non renseigné !");
    }

    [TestMethod]
    public void LoadFromCustomConfig_AucunService_AucuneRoute()
    {
        var config = BuildProxyConfig(new Dictionary<string, string?>());

        Check.That(config.Routes).IsEmpty();
        Check.That(config.Clusters).IsEmpty();
    }
}
