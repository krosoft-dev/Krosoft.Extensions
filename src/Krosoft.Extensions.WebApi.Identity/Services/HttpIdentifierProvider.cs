using Krosoft.Extensions.Core.Models.Exceptions;
using Krosoft.Extensions.Identity.Abstractions.Interfaces;

namespace Krosoft.Extensions.WebApi.Identity.Services;

public class HttpIdentifierProvider : IIdentifierProvider
{
    private readonly IAccessTokenProvider _accessTokenProvider;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public HttpIdentifierProvider(IAccessTokenProvider accessTokenProvider, IJwtTokenGenerator jwtTokenGenerator)
    {
        _accessTokenProvider = accessTokenProvider;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<string?> GetIdentifierAsync(CancellationToken cancellationToken)
    {
        var accessToken = await _accessTokenProvider.GetAccessTokenAsync(cancellationToken);
        if (string.IsNullOrEmpty(accessToken))
        {
            throw new KrosoftTechniqueException("Impossible d'obtenir le token d'accès !");
        }

        var identifier = _jwtTokenGenerator.GetIdentifierFromToken(accessToken);
        return identifier;
    }
}