using Krosoft.Extensions.Identity.Abstractions.Interfaces;
using Krosoft.Extensions.Samples.Library.Models.Queries;
using MediatR;

namespace Krosoft.Extensions.Samples.DotNet9.Api.Handlers.Queries;

public class HelloApiKeyDotNet9QueryHandler : IRequestHandler<HelloApiKeyDotNet9Query, string>
{
    private readonly IApiKeyStorageProvider _apiKeyStorageProvider;
    private readonly ILogger<HelloApiKeyDotNet9QueryHandler> _logger;

    public HelloApiKeyDotNet9QueryHandler(ILogger<HelloApiKeyDotNet9QueryHandler> logger, IApiKeyStorageProvider apiKeyStorageProvider)
    {
        _logger = logger;
        _apiKeyStorageProvider = apiKeyStorageProvider;
    }

    public async Task<string> Handle(HelloApiKeyDotNet9Query request,
                                     CancellationToken cancellationToken)
    {
        _logger.LogInformation("Hello DotNet9...");

        var identifiant = await _apiKeyStorageProvider.GetIdentifiantAsync(request.ApiKey, cancellationToken);

        _logger.LogInformation($"Identifiant : {identifiant}");

        return "Hello DotNet9";
    }
}