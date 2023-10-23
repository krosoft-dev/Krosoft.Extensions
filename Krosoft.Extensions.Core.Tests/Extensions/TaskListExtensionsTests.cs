using Krosoft.Extensions.Core.Extensions;
using Krosoft.Extensions.Samples.Library.Factories;
using Krosoft.Extensions.Samples.Library.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace Krosoft.Extensions.Core.Tests.Extensions;

[TestClass]
public class TaskListExtensionsTests
{
    [TestMethod]
    public async Task ToDictionaryTest()
    {
        var adresses = AddressFactory.GetAdresses();
        Task<IEnumerable<Address>> task = Task.FromResult(adresses);
        var adressesParX = await task.ToDictionary(x => x.StreetLine1);

        Check.That(adressesParX.Keys).HasSize(6);
        Check.That(adressesParX.Keys).ContainsExactly("street3Line1", "street4Line1", "street5Line1", "street1Line1", "street2Line1", "street6Line1");
    }

    [TestMethod]
    public async Task ToDictionaryElementSelectorTest()
    {
        var adresses = AddressFactory.GetAdresses();
        Task<IEnumerable<Address>> task = Task.FromResult(adresses);
        var adressesParX = await task.ToDictionary(x => x.StreetLine1, x => x.City);

        Check.That(adressesParX.Keys).HasSize(6);
        Check.That(adressesParX.Keys).ContainsExactly("street3Line1", "street4Line1", "street5Line1", "street1Line1", "street2Line1", "street6Line1");
    }

    [TestMethod]
    public async Task ToDictionaryFromListTest()
    {
        var adresses = AddressFactory.GetAdresses();
        Task<List<Address>> task = Task.FromResult(adresses.ToList());
        var adressesParX = await task.ToDictionary(x => x.StreetLine1);

        Check.That(adressesParX.Keys).HasSize(6);
        Check.That(adressesParX.Keys).ContainsExactly("street3Line1", "street4Line1", "street5Line1", "street1Line1", "street2Line1", "street6Line1");
    }

    [TestMethod]
    public async Task ToDictionaryFromListElementSelectorTest()
    {
        var adresses = AddressFactory.GetAdresses();
        Task<List<Address>> task = Task.FromResult(adresses.ToList());
        var adressesParX = await task.ToDictionary(x => x.StreetLine1, x => x.City);

        Check.That(adressesParX.Keys).HasSize(6);
        Check.That(adressesParX.Keys).ContainsExactly("street3Line1", "street4Line1", "street5Line1", "street1Line1", "street2Line1", "street6Line1");
    }

    [TestMethod]
    public async Task ToLookupTest()
    {
        var adresses = AddressFactory.GetAdresses();
        Task<IEnumerable<Address>> task = Task.FromResult(adresses);
        var adressesParX = await task.ToLookup(x => x.StreetLine1);

        Check.That(adressesParX).HasSize(6);
        Check.That(adressesParX.Select(x => x.Key)).ContainsExactly("street3Line1", "street4Line1", "street5Line1", "street1Line1", "street2Line1", "street6Line1");
    }

    [TestMethod]
    public async Task ToLookupFromListTest()
    {
        var adresses = AddressFactory.GetAdresses();
        Task<List<Address>> task = Task.FromResult(adresses.ToList());
        var adressesParX = await task.ToLookup(x => x.StreetLine1);

        Check.That(adressesParX).HasSize(6);
        Check.That(adressesParX.Select(x => x.Key)).ContainsExactly("street3Line1", "street4Line1", "street5Line1", "street1Line1", "street2Line1", "street6Line1");
    }

    [TestMethod]
    public async Task ToDictionaryDistinctTest()
    {
        var adresses = AddressFactory.GetAdresses();
        Task<IEnumerable<Address>> task = Task.FromResult(adresses);
        var adressesParX = await task.ToDictionary(x => x.City, true);

        Check.That(adressesParX).HasSize(5);
        Check.That(adressesParX.Select(x => x.Key)).ContainsExactly("city3", "city4", "city", "city1", "city2");
    }

    [TestMethod]
    public async Task ToDictionaryDistinctFromListTest()
    {
        var adresses = AddressFactory.GetAdresses();
        Task<List<Address>> task = Task.FromResult(adresses.ToList());
        var adressesParX = await task.ToDictionary(x => x.City, true);

        Check.That(adressesParX).HasSize(5);
        Check.That(adressesParX.Select(x => x.Key)).ContainsExactly("city3", "city4", "city", "city1", "city2");
    }

    [TestMethod]
    public async Task DistinctByTest()
    {
        var adresses = AddressFactory.GetAdresses();
        Task<IEnumerable<Address>> task = Task.FromResult(adresses);
        var adressesUnique = await task.DistinctBy(x => x.City);

        Check.That(adressesUnique).HasSize(5);
        Check.That(adressesUnique!.Select(x => x.City)).ContainsExactly("city3", "city4", "city", "city1", "city2");
    }

    [TestMethod]
    public async Task DistinctByFromListTest()
    {
        var adresses = AddressFactory.GetAdresses();
        Task<List<Address>> task = Task.FromResult(adresses.ToList());
        var adressesUnique = await task.DistinctBy(x => x.City);

        Check.That(adressesUnique).HasSize(5);
        Check.That(adressesUnique.Select(x => x.City)).ContainsExactly("city3", "city4", "city", "city1", "city2");
    }

    [TestMethod]
    public async Task FirstOrDefaultTest()
    {
        var adresses = AddressFactory.GetAdresses();
        Task<List<Address>> task = Task.FromResult(adresses.ToList());
        var address = await task.FirstOrDefault(x => x.City == "city3");

        Check.That<Address>(address).IsNotNull();
        Check.That(address!.City).IsEqualTo("city3");
    }

    [TestMethod]
    public async Task FirstOrDefaultDefaultTest()
    {
        var adresses = AddressFactory.GetAdresses();
        Task<List<Address>> task = Task.FromResult(adresses.ToList());
        var address = await task.FirstOrDefault(x => x.City == "test");

        Check.That<Address>(address).IsNull();
    }
}