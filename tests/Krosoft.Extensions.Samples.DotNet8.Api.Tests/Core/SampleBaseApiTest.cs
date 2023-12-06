﻿using Krosoft.Extensions.Core.Models;
using Krosoft.Extensions.Testing.WebApi;
using Microsoft.Extensions.DependencyInjection;

namespace Krosoft.Extensions.Samples.DotNet8.Api.Tests.Core;

//public abstract class SampleBaseApiTest<TEntry> : BaseApiTest<TEntry, PositiveExtensionTenantContext> where TEntry : class
public abstract class SampleBaseApiTest<TEntry> : BaseApiTest<TEntry, TEntry> where TEntry : class
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        //// Remove DbContext registration.
        //services.RemoveService(d => d.ServiceType == typeof(DbContextOptions<PositiveExtensionTenantContext>));
        //services.AddDbContextInMemory<PositiveExtensionTenantContext>(true);
        //services.AddSeedService<PositiveExtensionTenantContext, SampleSeedService>();

        //// Remove Redis registration.
        //services.AddTransient<IDistributedCacheProvider, DictionaryCacheProvider>();

        //// Remove IHostedService registration.
        //services.RemoveService(d => d.ImplementationType == typeof(SampleBackgroundService));

        //// Mock Pub-Sub services.
        //var mock = new Mock<IPublisherService>();
        //mock.Setup(x => x.PublishAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
        //    .Returns(() => Task.CompletedTask);
        //services.SwapTransient(_ => mock.Object);
    }

    protected override void ConfigureClaims(KrosoftToken krosoftToken)
    {
    }
}