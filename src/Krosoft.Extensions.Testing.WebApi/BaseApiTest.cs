﻿using System.Net;
using Krosoft.Extensions.Core.Models;
using Krosoft.Extensions.Testing.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;

//using Positive.Extensions.Cache.Distributed.Redis.Interfaces;
//using Positive.Extensions.Core.Models.Business;
//using Positive.Extensions.Data.EntityFramework.Contexts;
//using Positive.Extensions.Testing.Extensions;

namespace Krosoft.Extensions.Testing.WebApi;

public abstract class BaseApiTest<TStartup, TPositiveContext> : BaseTest
    where TStartup : class
    //where TPositiveContext : PositiveContext
{
    protected CustomWebApplicationFactory<TStartup, TPositiveContext> Factory = null!;
    protected virtual bool UseFakeAuth => true;

    [TestInitialize]
    public void TestInitialize()
    {
        Factory = new CustomWebApplicationFactory<TStartup, TPositiveContext>(ConfigureServices, ConfigureClaims, UseFakeAuth);
    }

    protected abstract void ConfigureServices(IServiceCollection obj);

    protected abstract void ConfigureClaims(KrosoftToken krosoftToken);

    [TestCleanup]
    public void TestCleanup()
    {
        if (Factory != null)
        {
            Factory.Dispose();
        }
    }

    protected CustomWebApplicationFactory<TStartup, TPositiveContext> GetFactoryRedis(bool fakeRedis)
    {
        void Action(IServiceCollection services)
        {
            ConfigureServices(services);

            //if (fakeRedis)
            //{
            //    //Mock pour Redis.
            //    var mockDistributedCacheProvider = new Mock<IDistributedCacheProvider>();
            //    mockDistributedCacheProvider.Setup(x => x.PingAsync(CancellationToken.None))
            //                                .ReturnsAsync(() => throw new NotImplementedException());

            //    services.SwapTransient(_ => mockDistributedCacheProvider.Object);
            //}

            //Mock pour HttpClient.
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                                  .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                                  .ReturnsAsync(new HttpResponseMessage
                                  {
                                      StatusCode = HttpStatusCode.OK
                                  });

            var client = new HttpClient(mockHttpMessageHandler.Object);

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            mockHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(client);
            services.SwapTransient(_ => mockHttpClientFactory.Object);
        }

        return new CustomWebApplicationFactory<TStartup, TPositiveContext>(Action, null, UseFakeAuth);
    }
}