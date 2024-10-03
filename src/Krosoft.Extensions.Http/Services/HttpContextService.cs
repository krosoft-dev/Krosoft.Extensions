using Krosoft.Extensions.Http.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Krosoft.Extensions.Http.Services;

public class HttpContextService : IHttpContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetBaseUrl()
    {
        if (_httpContextAccessor.HttpContext != null)
        {
            var host = _httpContextAccessor.HttpContext.Request.Host.Value;
            var scheme = _httpContextAccessor.HttpContext.Request.Scheme;
            return $"{scheme}://{host}/";
        }

        return string.Empty;
    }

    public IEnumerable<string> GetInformations()
    {
        var informations = new List<string>
        {
            $"HttpContext.Connection.RemoteIpAddress : {_httpContextAccessor.HttpContext.Connection.RemoteIpAddress}",
            $"HttpContext.Connection.RemoteIpPort : {_httpContextAccessor.HttpContext.Connection.RemotePort}",
            $"HttpContext.Request.Scheme : {_httpContextAccessor.HttpContext.Request.Scheme}",
            $"HttpContext.Request.Host : {_httpContextAccessor.HttpContext.Request.Host}"
        };

        var headers = _httpContextAccessor.HttpContext.Request.Headers
                                          .Where(h => h.Key.StartsWith("X", StringComparison.OrdinalIgnoreCase))
                                          .ToList();
        foreach (var header in headers)
        {
            informations.Add($"Request-Header {header.Key}: {header.Value}");
        }

        return informations;
    }
}