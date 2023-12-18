//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using NFluent;
//using Krosoft.Extensions.Samples.Tests.Extensions;

//namespace Krosoft.Extensions.Data.Abstractions.Tests.Extensions;

//[TestClass]
//public class QueryableExtensionsInMemoryTests : QueryableExtensionsBaseTest
//{
//    protected override void AddServices(ServiceCollection services, IConfiguration configuration)
//    {
//        services.AddSampleInfrastructure(configuration);
//    }

//    [TestMethod]
//    public void FilterCollectionEmptyTest()
//    {
//        var items = FilterCollectionEmptyQuery().ToList();

//        Check.That(items).HasSize(3);
//        Check.That(items.Select(l => l.Code)).ContainsExactly("001", "002", "003");
//    }

//    [TestMethod]
//    public void FilterCollectionNullTest()
//    {
//        var items = FilterCollectionNullQuery().ToList();

//        Check.That(items).HasSize(3);
//        Check.That(items.Select(l => l.Code)).ContainsExactly("001", "002", "003");
//    }

//    [TestMethod]
//    public void FilterCollectionTest()
//    {
//        var items = FilterCollectionQuery().ToList();

//        Check.That(items).HasSize(1);
//        Check.That(items.Select(l => l.Code)).ContainsExactly("001");
//    }

//    [TestMethod]
//    public void SearchTest()
//    {
//        var items = SearchQuery().ToList();

//        Check.That(items).HasSize(2);
//        Check.That(items.Select(l => l.Code)).ContainsExactly("001", "003");
//    }

//    [TestMethod]
//    public void SearchUpperTest()
//    {
//        var items = SearchUpperQuery().ToList();

//        Check.That(items).HasSize(2);
//        Check.That(items.Select(l => l.Code)).ContainsExactly("001", "003");
//    }

//    [TestMethod]
//    public void SearchMultipleSelectorTest()
//    {
//        var items = SearchMultipleSelectorQuery().ToList();

//        Check.That(items).HasSize(2);
//        Check.That(items.Select(l => l.Code)).ContainsExactly("001", "002");
//    }

//    [TestMethod]
//    public void SearchAllTest()
//    {
//        var items = SearchAllQuery().ToList();

//        Check.That(items).HasSize(1);
//        Check.That(items.Select(l => l.Code)).ContainsExactly("003");
//    }

//    [TestMethod]
//    public void SearchNullTest()
//    {
//        var items = SearchNullQuery().ToList();

//        Check.That(items).HasSize(3);
//        Check.That(items.Select(l => l.Code)).ContainsExactly("001", "002", "003");
//    }

//    [TestMethod]
//    public void SearchEmptyTest()
//    {
//        var items = SearchEmptyQuery().ToList();

//        Check.That(items).HasSize(3);
//        Check.That(items.Select(l => l.Code)).ContainsExactly("001", "002", "003");
//    }

//    [TestMethod]
//    public void FilterNullableTest()
//    {
//        var items = FilterNullableQuery().ToList();

//        Check.That(items).HasSize(2);
//        Check.That(items.Select(l => l.Code)).ContainsExactly("002", "003");
//    }

//    [TestMethod]
//    public void FilterNullableNullTest()
//    {
//        var items = FilterNullableNullQuery().ToList();

//        Check.That(items).HasSize(3);
//        Check.That(items.Select(l => l.Code)).ContainsExactly("001", "002", "003");
//    }
//}