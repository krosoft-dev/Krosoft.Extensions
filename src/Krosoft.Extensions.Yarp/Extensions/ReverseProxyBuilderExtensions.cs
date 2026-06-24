using Krosoft.Extensions.Core.Models.Exceptions;
using Krosoft.Extensions.Yarp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.Transforms;

namespace Krosoft.Extensions.Yarp.Extensions;

public static class ReverseProxyBuilderExtensions
{
    public static IReverseProxyBuilder LoadFromCustomConfig(this IReverseProxyBuilder builder,
                                                            IConfiguration configuration)
    {
        var customReverseProxySettings = new CustomReverseProxySettings();
        configuration.GetSection(nameof(CustomReverseProxySettings)).Bind(customReverseProxySettings);

        var routes = new List<RouteConfig>();
        var clusters = new List<ClusterConfig>();
        foreach (var service in customReverseProxySettings.Services)
        {
            var clusterId = service.Key;
            if (string.IsNullOrWhiteSpace(clusterId))
            {
                throw new KrosoftTechnicalException("Clé de service non renseigné !");
            }

            var destination = service.Value.Destination;
            if (string.IsNullOrWhiteSpace(destination))
            {
                throw new KrosoftTechnicalException($"Destination du service '{clusterId}' non renseigné !");
            }

            var route = new RouteConfig
            {
                RouteId = clusterId,
                ClusterId = clusterId,
                Match = new RouteMatch
                {
                    Path = $"/{clusterId}/{{**catch-all}}"
                }
            }.WithTransform(transform => { transform["PathPattern"] = "{**catch-all}"; });

            routes.Add(route);

            var cluster = new ClusterConfig
            {
                ClusterId = clusterId,
                Destinations = new Dictionary<string, DestinationConfig>
                {
                    { "destination", new DestinationConfig { Address = destination } }
                }
            };
            clusters.Add(cluster);
        }

        builder.LoadFromMemory(routes, clusters);

        // Chaque service est exposé sous le préfixe /{clusterId}, retiré du chemin avant
        // d'être forwardé. On réexpose ce préfixe en PathBase : le transform X-Forwarded
        // natif de YARP émet alors X-Forwarded-Prefix vers le service, ce qui permet à ce
        // dernier (Swagger, génération de liens, ...) de reconstruire ses URLs publiques.
        builder.AddTransforms(context =>
        {
            var routeId = context.Route.RouteId;
            if (!string.IsNullOrWhiteSpace(routeId))
            {
                context.AddRequestTransform(transformContext =>
                {
                    transformContext.HttpContext.Request.PathBase = "/" + routeId;
                    return ValueTask.CompletedTask;
                });
            }
        });

        return builder;
    }
}