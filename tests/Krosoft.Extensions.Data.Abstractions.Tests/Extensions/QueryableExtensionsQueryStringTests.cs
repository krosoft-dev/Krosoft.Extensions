using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NFluent;
using Krosoft.Extensions.Data.EntityFramework.Extensions;
using Krosoft.Extensions.Data.EntityFramework.PostgreSql.Extensions;
using Krosoft.Extensions.Infrastructure.Extensions;
using Krosoft.Extensions.Samples.Api.Data;


namespace Krosoft.Extensions.Data.Abstractions.Tests.Extensions;

[TestClass]
public class QueryableExtensionsQueryStringTests : QueryableExtensionsBaseTest
{
    private const string ITEM_SELECT_COLUMNS = "SELECT i.\"Id\", i.\"Code\", i.\"Date\", i.\"Description\", i.\"IsActif\", i.\"Libelle\"";
    protected override void AddServices(ServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddRepositories();
        services.AddDbContextPostgreSql<PositiveExtensionContext>(configuration);
    }

    [TestMethod]
    public void FilterCollectionEmptyTest()
    {
        var queryString = FilterCollectionEmptyQuery().ToQueryString();

        Check.That(queryString)
             .IsEqualTo(ITEM_SELECT_COLUMNS + Environment.NewLine + "FROM \"Systeme\".\"Items\" AS i");
    }

    [TestMethod]
    public void FilterCollectionNullTest()
    {
        var queryString = FilterCollectionNullQuery().ToQueryString();
        Check.That(queryString)
             .IsEqualTo(ITEM_SELECT_COLUMNS + Environment.NewLine + "FROM \"Systeme\".\"Items\" AS i");
    }

    [TestMethod]
    public void FilterCollectionTest()
    {
        var queryString = FilterCollectionQuery().ToQueryString();
        Check.That(queryString)
             .IsEqualTo("-- @__id_0='00000000-0000-0000-0000-000000000001'" +
                        Environment.NewLine +
                        ITEM_SELECT_COLUMNS +
                        Environment.NewLine +
                        "FROM \"Systeme\".\"Items\" AS i" +
                        Environment.NewLine +
                        "WHERE i.\"Id\" = @__id_0");
    }

    [TestMethod]
    public void SearchTest()
    {
        var queryString = SearchQuery().ToQueryString();
        Check.That(queryString)
             .IsEqualTo(ITEM_SELECT_COLUMNS +
                        Environment.NewLine +
                        "FROM \"Systeme\".\"Items\" AS i" +
                        Environment.NewLine +
                        "WHERE strpos(lower(i.\"Description\"), 'lorem') > 0");
    }

    [TestMethod]
    public void SearchUpperTest()
    {
        var queryString = SearchUpperQuery().ToQueryString();
        Check.That(queryString)
             .IsEqualTo(ITEM_SELECT_COLUMNS +
                        Environment.NewLine +
                        "FROM \"Systeme\".\"Items\" AS i" +
                        Environment.NewLine +
                        "WHERE strpos(lower(i.\"Description\"), 'lorem') > 0");
    }

    [TestMethod]
    public void SearchMultipleSelectorTest()
    {
        var queryString = SearchMultipleSelectorQuery().ToQueryString();
        Check.That(queryString)
             .IsEqualTo(ITEM_SELECT_COLUMNS +
                        Environment.NewLine +
                        "FROM \"Systeme\".\"Items\" AS i" +
                        Environment.NewLine +
                        "WHERE ((strpos(lower(i.\"Code\"), 'consectetur') > 0) OR (strpos(lower(i.\"Libelle\"), 'consectetur') > 0)) OR (strpos(lower(i.\"Description\"), 'consectetur') > 0)");
    }

    [TestMethod]
    public void SearchAllTest()
    {
        var queryString = SearchAllQuery().ToQueryString();
        Check.That(queryString)
             .IsEqualTo(ITEM_SELECT_COLUMNS +
                        Environment.NewLine +
                        "FROM \"Systeme\".\"Items\" AS i" +
                        Environment.NewLine +
                        "WHERE (strpos(lower(i.\"Description\"), 'lorem') > 0) AND (strpos(lower(i.\"Description\"), 'omnis') > 0)");
    }

    [TestMethod]
    public void SearchNullTest()
    {
        var queryString = SearchNullQuery().ToQueryString();
        Check.That(queryString)
             .IsEqualTo(ITEM_SELECT_COLUMNS + Environment.NewLine + "FROM \"Systeme\".\"Items\" AS i");
    }

    [TestMethod]
    public void SearchEmptyTest()
    {
        var queryString = SearchEmptyQuery().ToQueryString();
        Check.That(queryString)
             .IsEqualTo(ITEM_SELECT_COLUMNS + Environment.NewLine + "FROM \"Systeme\".\"Items\" AS i");
    }

    [TestMethod]
    public void FilterNullableTest()
    {
        var queryString = FilterNullableQuery().ToQueryString();
        Check.That(queryString)
             .IsEqualTo("-- @__date_0='2002-01-01T00:00:00.0000000' (Nullable = true) (DbType = DateTime)" +
                        Environment.NewLine +
                        ITEM_SELECT_COLUMNS +
                        Environment.NewLine +
                        "FROM \"Systeme\".\"Items\" AS i" +
                        Environment.NewLine +
                        "WHERE i.\"Date\" >= @__date_0");
    }

    [TestMethod]
    public void FilterNullableNullTest()
    {
        var queryString = FilterNullableNullQuery().ToQueryString();
        Check.That(queryString)
             .IsEqualTo(ITEM_SELECT_COLUMNS + Environment.NewLine + "FROM \"Systeme\".\"Items\" AS i");
    }
}