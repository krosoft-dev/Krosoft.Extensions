using Krosoft.Extensions.Data.Abstractions.Interfaces;
using Krosoft.Extensions.Data.EntityFramework.Extensions;
using Krosoft.Extensions.Data.EntityFramework.Identity.Extensions;
using Krosoft.Extensions.Data.EntityFramework.Identity.Services;
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

namespace Krosoft.Extensions.Data.EntityFramework.InMemory.Tests;

[TestClass]
public class ZipServiceTests : BaseTest
{
    private IReadRepository<Langue> _repository = null!;

    protected override void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepositories();
        services.AddDbContextInMemory<SampleKrosoftContext>(true);
        services.AddSeedService<SampleKrosoftContext, SampleKrosoftSeedService>();

        
        //services.AddScoped<IDbContextSettingsProvider, FakeDbContextSettingsProvider>();
        //services.AddDbContextInMemory<SampleKrosoftTenantContext>(true);
    }

    [TestMethod]
    public async Task ExtractZip_Ok()
    {
        var items = await _repository.Query()
                                     .ToListAsync(CancellationToken.None)
            ;

        Check.That(items).IsNotNull();
        Check.That(items).HasSize(2);
    }

    [TestInitialize]
    public void SetUp()
    {
        var serviceProvider = CreateServiceCollection();
        _repository = serviceProvider.GetRequiredService<IReadRepository<Langue>>();
    }
}