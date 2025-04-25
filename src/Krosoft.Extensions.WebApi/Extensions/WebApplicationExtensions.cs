using System.Reflection;
using Krosoft.Extensions.WebApi.Interfaces;
using Microsoft.AspNetCore.Builder;

namespace Krosoft.Extensions.WebApi.Extensions;

public static class WebApplicationExtensions
{
#if NET9_0_OR_GREATER
    public static WebApplication AddEndpoints(this WebApplication app)
    {
        // Recherche tous les types qui implémentent IEndpoint dans l'assembly actuel
        var endpointTypes = Assembly.GetExecutingAssembly()
                                    .GetTypes()
                                    .Where(t => !t.IsAbstract && !t.IsInterface && typeof(IEndpoint).IsAssignableFrom(t))
                                    .ToList();

        foreach (var endpointType in endpointTypes)
        {
            // Instanciation de l'endpoint
            if (Activator.CreateInstance(endpointType) is IEndpoint endpoint)
            {
                // Création du groupe de routes
                var group = endpoint.DefineGroup(app);

                // Enregistrement des endpoints dans le groupe
                endpoint.Register(group);
            }
        }

        return app;
    }
#endif
}