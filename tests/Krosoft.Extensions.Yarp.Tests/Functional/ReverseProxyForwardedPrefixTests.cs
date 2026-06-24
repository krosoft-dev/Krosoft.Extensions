using Krosoft.Extensions.Yarp.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Krosoft.Extensions.Yarp.Tests.Functional;

[TestClass]
public class ReverseProxyForwardedPrefixTests
{
    /// <summary>
    /// Service factice qui renvoie, dans des en-têtes de réponse, le préfixe transmis
    /// (X-Forwarded-Prefix) et le chemin reçu, afin de vérifier ce que la gateway forwarde.
    /// </summary>
    private static async Task<WebApplication> StartBackendAsync()
    {
        var builder = WebApplication.CreateBuilder();
        builder.WebHost.UseUrls("http://127.0.0.1:0");
        builder.Logging.ClearProviders();

        var app = builder.Build();
        app.Run(async context =>
        {
            context.Response.Headers["X-Echo-Prefix"] = context.Request.Headers["X-Forwarded-Prefix"].ToString();
            context.Response.Headers["X-Echo-Path"] = context.Request.Path.ToString();
            await context.Response.WriteAsync("ok");
        });

        await app.StartAsync();
        return app;
    }

    private static async Task<WebApplication> StartGatewayAsync(string destination)
    {
        var configuration = new ConfigurationBuilder()
                           .AddInMemoryCollection(new Dictionary<string, string?>
                           {
                               ["CustomReverseProxySettings:Services:Domotique:Destination"] = destination
                           })
                           .Build();

        var builder = WebApplication.CreateBuilder();
        builder.WebHost.UseUrls("http://127.0.0.1:0");
        builder.Logging.ClearProviders();
        builder.Services.AddReverseProxy()
               .LoadFromCustomConfig(configuration);

        var app = builder.Build();
        app.MapReverseProxy();

        await app.StartAsync();
        return app;
    }

    [TestMethod]
    public async Task Gateway_PropageLePrefixeEnXForwardedPrefix_EtSupprimeLePrefixeDuChemin()
    {
        await using var backend = await StartBackendAsync();
        await using var gateway = await StartGatewayAsync(backend.Urls.First());

        using var client = new HttpClient();
        var response = await client.GetAsync($"{gateway.Urls.First()}/Domotique/Bridges");

        Check.That(response.IsSuccessStatusCode).IsTrue();
        // La gateway expose le service sous /Domotique : ce préfixe est transmis au service...
        Check.That(response.Headers.GetValues("X-Echo-Prefix").Single()).IsEqualTo("/Domotique");
        // ... mais retiré du chemin réellement forwardé.
        Check.That(response.Headers.GetValues("X-Echo-Path").Single()).IsEqualTo("/Bridges");
    }
}
