using Krosoft.Extensions.Http.DelegatingHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace Krosoft.Extensions.Http.Extensions;

public static class HttpClientBuilderExtensions
{
    public static IHttpClientBuilder AddHttpMessageHandlers(this IHttpClientBuilder httpClientBuilder,
                                                            IServiceCollection services,
                                                            bool useAuthorization)
    {
        if (useAuthorization)
        {
            services.AddHttpContextAccessor();
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
            httpClientBuilder.AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();
        }

        return httpClientBuilder;
    }
}