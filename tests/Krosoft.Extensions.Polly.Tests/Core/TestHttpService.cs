using Krosoft.Extensions.Core.Extensions;

namespace Krosoft.Extensions.Polly.Tests.Core;

public class TestHttpService
{
    private const string Uri = "https://jsonplaceholder.typicode.com/todos/1";
    private readonly IHttpClientFactory _httpClientFactory;

    public TestHttpService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<Todo?> GetAsync(CancellationToken cancellationToken)
    {
        using (var httpClient = _httpClientFactory.CreateClient("sitemap"))
        {
            var responseMessage = await httpClient.GetAsync(Uri, cancellationToken);
            responseMessage.EnsureSuccessStatusCode();
            var todo = await responseMessage.Content.ReadAsJsonAsync<Todo>(cancellationToken);

            return todo;
        }
    }
}