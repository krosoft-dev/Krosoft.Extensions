using Krosoft.Extensions.Data.Abstractions.Interfaces;
using Krosoft.Extensions.Data.EntityFramework.Extensions;
using Krosoft.Extensions.Data.EntityFramework.InMemory.Extensions;
using Krosoft.Extensions.Samples.DotNet8.Api.Data;
using Krosoft.Extensions.Samples.Library.Models;
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
    private IReadRepository<Item> _repository = null!;

    protected override void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepositories();
        services.AddDbContextInMemory<SampleKrosoftContext>(true);
    }

    [TestMethod]
    public async Task ExtractZip_Ok()
    {
        var items = await _repository.Query()
                                     .ToListAsync(CancellationToken.None)
            ;

        Check.That(items).IsNotNull();
    }

    [TestInitialize]
    public void SetUp()
    {
        var serviceProvider = CreateServiceCollection();
        _repository = serviceProvider.GetRequiredService<IReadRepository<Item>>();
    }
}