using System.Net;
using System.Net.Http.Headers;
using Krosoft.Extensions.Core.Extensions;
using Krosoft.Extensions.Core.Models.Exceptions;
using Krosoft.Extensions.Core.Models.Exceptions.Http;
using Krosoft.Extensions.Samples.Library.Models;
using Krosoft.Extensions.Testing;
using Moq.Protected;

namespace Krosoft.Extensions.Core.Tests.Extensions;

[TestClass]
public class HttpClientExtensionsTests : BaseTest
{
    [TestMethod]
    public async Task EnsureAsync()
    {
        var cancellationToken = CancellationToken.None;

        var result = await new HttpClient()
                           .GetAsync("https://jsonplaceholder.typicode.com/todos", cancellationToken)
                           .EnsureAsync<IEnumerable<TodoHttp>>((_, json) => throw new KrosoftTechnicalException(json), cancellationToken)
                           .ToList();

        Check.That(result).IsNotNull();
        Check.That(result).HasSize(200);
    }

    [TestMethod]
    public void EnsureAsync_Exception_Custom()
    {
        var cancellationToken = CancellationToken.None;

        Check.ThatCode(() => new HttpClient()
                             .PutAsync("https://jsonplaceholder.typicode.com/posts", null, cancellationToken)
                             .EnsureAsync<IEnumerable<TodoHttp>>((_, json) => throw new HttpException(HttpStatusCode.InternalServerError, json), cancellationToken))
             .Throws<HttpException>()
             .WithMessage("{}")
             .And.WhichMember(x => x.Code)
             .IsEqualTo(HttpStatusCode.InternalServerError);
    }

    [TestMethod]
    public void EnsureAsync_Exception_Default()
    {
        var cancellationToken = CancellationToken.None;

        Check.ThatCode(() => new HttpClient()
                             .PutAsync("https://jsonplaceholder.typicode.com/posts", null, cancellationToken)
                             .EnsureAsync<IEnumerable<TodoHttp>>(cancellationToken))
             .Throws<HttpException>()
             .WithMessage("Not Found")
             .And.WhichMember(x => x.Code)
             .IsEqualTo(HttpStatusCode.NotFound);
    }

    [TestMethod]
    public void EnsureStreamAsync_Exception()
    {
        var cancellationToken = CancellationToken.None;

        var mockHttp = GetMock(HttpStatusCode.NotFound, null, null, null);

        Check.ThatCode(() => mockHttp
                             .GetAsync("https://example.com", cancellationToken)
                             .EnsureStreamAsync((code, json) => throw new HttpException(code, json), cancellationToken))
             .Throws<HttpException>()
             .WithMessage("Not Found")
             .And.WhichMember(x => x.Code)
             .IsEqualTo(HttpStatusCode.NotFound);
    }

    [TestMethod]
    public async Task EnsureStreamAsync_Ok()
    {
        var content = "Test content";
        var contentType = "application/json";
        var fileName = "test.json";

        var cancellationToken = CancellationToken.None;

        var mockHttp = GetMock(HttpStatusCode.OK, content, fileName, contentType);

        var result = await mockHttp
                           .GetAsync("https://example.com", cancellationToken)
                           .EnsureStreamAsync((code, json) => throw new HttpException(code, json), cancellationToken);

        Check.That(result).IsNotNull();
        Check.That(result!.ContentType).IsEqualTo(contentType);
        Check.That(result.FileName).IsEqualTo(fileName);

        using var reader = new StreamReader(result.Stream);
        var actualContent = await reader.ReadToEndAsync(cancellationToken);
        Check.That(actualContent).IsEqualTo(content);
    }

    private static HttpClient GetMock(HttpStatusCode statusCode,
                                      string? content,
                                      string? fileName,
                                      string? contentType)
    {
        var httpResponseMessage = new HttpResponseMessage(statusCode);
        if (content != null && fileName != null && contentType != null)
        {
            httpResponseMessage.Content = new StringContent(content);
            httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = fileName
            };
        }

        var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        httpMessageHandlerMock.Protected()
                              .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                              .ReturnsAsync(httpResponseMessage);

        return new HttpClient(httpMessageHandlerMock.Object);
    }
}