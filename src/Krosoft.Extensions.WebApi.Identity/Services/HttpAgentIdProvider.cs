using Krosoft.Extensions.Core.Models.Exceptions;
using Krosoft.Extensions.Identity.Abstractions.Interfaces;
using Krosoft.Extensions.WebApi.Identity.Middlewares;
using Microsoft.AspNetCore.Http;

namespace Krosoft.Extensions.WebApi.Identity.Services;

internal class HttpAgentIdProvider : IAgentIdProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpAgentIdProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<string?> GetAgentIdAsync(CancellationToken cancellationToken)
    {
        //On affecte le token que s'il n'y en pas déjà un.
        if (_httpContextAccessor.HttpContext != null)
        {
            var headers = _httpContextAccessor.HttpContext.Request.Headers;
            if (headers.ContainsKey(AgentIdMiddleware.AgentIdHeaderName))
            {
                string? agentId = headers[AgentIdMiddleware.AgentIdHeaderName];
                return Task.FromResult<string?>(agentId);
            }
        }

        throw new KrosoftTechnicalException("HttpContext non défini.");
    }
}