using JetBrains.Annotations;
using Krosoft.Extensions.Blocking.Abstractions.Interfaces;
using Krosoft.Extensions.Blocking.Extensions;
using Krosoft.Extensions.Blocking.Services;
using Krosoft.Extensions.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Krosoft.Extensions.Cache.Distributed.Redis.Tests.Services;

[TestClass]
[TestSubject(typeof(AccessTokenBlockingService))]
public class AccessTokenBlockingServiceTests : BaseTest
{
    private IAccessTokenBlockingService _accessTokenBlockingService = null!;

    protected override void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddBlocking();
    }

    [TestMethod]
    public async Task BlockAsync_Ok()
    {
        await _accessTokenBlockingService.BlockAsync(CancellationToken.None);
        var isBlocked = await _accessTokenBlockingService.IsBlockedAsync(CancellationToken.None);

        Check.That(isBlocked).IsTrue();
    }

    [TestInitialize]
    public void SetUp()
    {
        var serviceProvider = CreateServiceCollection();
        _accessTokenBlockingService = serviceProvider.GetRequiredService<IAccessTokenBlockingService>();
    }
}