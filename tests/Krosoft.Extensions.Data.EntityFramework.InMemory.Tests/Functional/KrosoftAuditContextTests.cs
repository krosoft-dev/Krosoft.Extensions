using Krosoft.Extensions.Data.Abstractions.Interfaces;
using Krosoft.Extensions.Data.EntityFramework.Extensions;
using Krosoft.Extensions.Data.EntityFramework.InMemory.Extensions;
using Krosoft.Extensions.Data.EntityFramework.Interfaces;
using Krosoft.Extensions.Data.EntityFramework.Services;
using Krosoft.Extensions.Samples.DotNet8.Api.Data;
using Krosoft.Extensions.Samples.Library.Models.Entities;
using Krosoft.Extensions.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace Krosoft.Extensions.Data.EntityFramework.InMemory.Tests.Functional;

[TestClass]
public class KrosoftAuditContextTests : BaseTest
{
    private IReadRepository<Pays> _repository = null!;

    protected override void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepositories();
        services.AddScoped<IDbContextSettingsProvider, FakeDbContextSettingsProvider>();
        services.AddDbContextInMemory<SampleKrosoftAuditContext>(true);
        services.AddSeedService<SampleKrosoftAuditContext, SampleSeedService<SampleKrosoftAuditContext>>();
    }

    [TestMethod]
    public async Task Query_Ok()
    {
        var pays = await _repository.Query()
                                    .ToListAsync(CancellationToken.None);

        Check.That(pays).IsNotNull();
        Check.That(pays).HasSize(5);
        Check.That(pays.Select(x => x.Code)).ContainsExactly("fr", "de", "it", "es", "gb");
    }

    [TestInitialize]
    public void SetUp()
    {
        var serviceProvider = CreateServiceCollection();
        _repository = serviceProvider.GetRequiredService<IReadRepository<Pays>>();
    }
}