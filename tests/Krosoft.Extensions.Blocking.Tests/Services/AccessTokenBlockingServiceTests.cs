using JetBrains.Annotations;
using Krosoft.Extensions.Blocking.Abstractions.Interfaces;
using Krosoft.Extensions.Blocking.Extensions;
using Krosoft.Extensions.Blocking.Memory.Extensions;
using Krosoft.Extensions.Blocking.Services;
using Krosoft.Extensions.Identity.Abstractions.Fakes;
using Krosoft.Extensions.Identity.Abstractions.Interfaces;
using Krosoft.Extensions.Testing;
using Krosoft.Extensions.WebApi.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace Krosoft.Extensions.Blocking.Tests.Services;

[TestClass]
[TestSubject(typeof(AccessTokenBlockingService))]
public class AccessTokenBlockingServiceTests : BaseTest
{
    private IAccessTokenBlockingService _accessTokenBlockingService = null!;

    protected override void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddLoggingExt();
        services.AddBlocking();
        services.AddMemoryBlockingStorage();
        services.AddTransient<IAccessTokenProvider, FakeAccessTokenProvider>();
    }

    [TestMethod]
    public async Task BlockAsync_Ok()
    {
        await _accessTokenBlockingService.BlockAsync(CancellationToken.None);
        var isBlocked = await _accessTokenBlockingService.IsBlockedAsync(CancellationToken.None);

        Check.That(isBlocked).IsTrue();
    }

    [TestMethod]
    public async Task IsBlockedAsync_Ok()
    {
        var id = $"TU_{DateTime.Now.Ticks}";
        var isBlocked1 = await _accessTokenBlockingService.IsBlockedAsync(id, CancellationToken.None);
        Check.That(isBlocked1).IsFalse();

        await _accessTokenBlockingService.BlockAsync(id, CancellationToken.None);

        var isBlocked2 = await _accessTokenBlockingService.IsBlockedAsync(id, CancellationToken.None);
        Check.That(isBlocked2).IsTrue();
    }

    [TestInitialize]
    public void SetUp()
    {
        var serviceProvider = CreateServiceCollection();
        _accessTokenBlockingService = serviceProvider.GetRequiredService<IAccessTokenBlockingService>();
    }

    [TestMethod]
    public async Task UnblockAsync_Ok()
    {
        var id = $"TU_{DateTime.Now.Ticks}";
        var isBlocked1 = await _accessTokenBlockingService.IsBlockedAsync(id, CancellationToken.None);
        Check.That(isBlocked1).IsFalse();

        await _accessTokenBlockingService.BlockAsync(id, CancellationToken.None);

        var isBlocked2 = await _accessTokenBlockingService.IsBlockedAsync(id, CancellationToken.None);
        Check.That(isBlocked2).IsTrue();

        await _accessTokenBlockingService.UnblockAsync(id, CancellationToken.None);

        var isBlocked3 = await _accessTokenBlockingService.IsBlockedAsync(id, CancellationToken.None);
        Check.That(isBlocked3).IsFalse();
    }
}