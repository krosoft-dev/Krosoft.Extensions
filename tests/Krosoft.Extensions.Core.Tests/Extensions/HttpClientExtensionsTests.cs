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
                             .EnsureAsync<IEnumerable<TodoHttp>>((code, json) => throw new HttpException(HttpStatusCode.InternalServerError, json), cancellationToken))
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
        var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);

        var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        httpMessageHandlerMock.Protected()
                              .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), cancellationToken)
                              .ReturnsAsync(httpResponseMessage);

        var httpClient = new HttpClient(httpMessageHandlerMock.Object);
        var eee = httpClient.GetAsync("https://example.com", cancellationToken);

        Check.ThatCode(() => eee.EnsureStreamAsync((code, json) => throw new HttpException(code, json),
                                                   cancellationToken))
             .Throws<Exception>();
    }

    [TestMethod]
    public async Task EnsureStreamAsync_Ok()
    {
        var cancellationToken = CancellationToken.None;
        var expectedContent = "Test content";
        var expectedContentType = "application/json";
        var expectedFileName = "test.json";

        var content = new StringContent(expectedContent);
        content.Headers.ContentType = new MediaTypeHeaderValue(expectedContentType);
        content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
        {
            FileName = expectedFileName
        };

        var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = content
        };

        var httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        httpMessageHandlerMock.Protected()
                              .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), cancellationToken)
                              .ReturnsAsync(httpResponseMessage);

        var httpClient = new HttpClient(httpMessageHandlerMock.Object);

        var result = await httpClient.GetAsync("https://example.com", cancellationToken)
                                     .EnsureStreamAsync((code, json) => throw new HttpException(code, json), cancellationToken);

        Check.That(result).IsNotNull();
        Check.That(result!.ContentType).IsEqualTo(expectedContentType);
        Check.That(result.FileName).IsEqualTo(expectedFileName);

        using var reader = new StreamReader(result.Stream);
        var actualContent = await reader.ReadToEndAsync(cancellationToken);
        Check.That(actualContent).IsEqualTo(expectedContent);
    }
}