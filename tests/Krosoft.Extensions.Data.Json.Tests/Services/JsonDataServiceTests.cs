using Krosoft.Extensions.Data.Json.Extensions;
using Krosoft.Extensions.Data.Json.Interfaces;
using Krosoft.Extensions.Samples.Library.Models;
using Krosoft.Extensions.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace Krosoft.Extensions.Data.Json.Tests.Services;

[TestClass]
public class JsonDataServiceTests : BaseTest
{
    private IJsonDataService<Compte> _jsonDataService = null!;

    protected override void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddJsonDataService(configuration);
    }

    [TestInitialize]
    public void SetUp()
    {
        var serviceProvider = CreateServiceCollection();
        _jsonDataService = serviceProvider.GetRequiredService<IJsonDataService<Compte>>();
    }

    [TestMethod]
    public void Query_Ok()
    {
        var comptes = _jsonDataService.Query().ToList();

        Check.That(comptes).Not.IsNullOrEmpty();
        Check.That(comptes).HasSize(2);
        Check.That(comptes.Select(x => x.Name)).IsOnlyMadeOf("test 1", "test 2");
    }

    [TestMethod]
    public void InsertAsyncTest()
    {
        Assert.Inconclusive();
    }

    [TestMethod]
    public void DeleteAsyncTest()
    {
        Assert.Inconclusive();
    }

    [TestMethod]
    public void UpdateAsyncTest()
    {
        Assert.Inconclusive();
    }
}