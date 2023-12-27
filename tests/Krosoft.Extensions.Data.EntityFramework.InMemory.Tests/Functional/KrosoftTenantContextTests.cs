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
public class KrosoftTenantContextTests : BaseTest
{
    private IReadRepository<Logiciel> _repository = null!;

    protected override void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepositories();
 
        services.AddScoped<ITenantDbContextProvider, FakeTenantDbContextProvider>();
        services.AddDbContextInMemory<SampleKrosoftTenantContext>(true);
        services.AddSeedService<SampleKrosoftTenantContext, SampleSeedService<SampleKrosoftTenantContext>>();
    }

    [TestMethod]
    public async Task Query_Ok()
    {
        var logiciels = await _repository.Query()
                                         .ToListAsync(CancellationToken.None);

        Check.That(logiciels).IsNotNull();
        Check.That(logiciels).HasSize(5);
        Check.That(logiciels.Select(x => x.Nom)).ContainsExactly("Logiciel1","Logiciel2","Logiciel3","Logiciel4","Logiciel5");
    }

    [TestInitialize]
    public void SetUp()
    {
        var serviceProvider = CreateServiceCollection();
        _repository = serviceProvider.GetRequiredService<IReadRepository<Logiciel>>();
    }
}