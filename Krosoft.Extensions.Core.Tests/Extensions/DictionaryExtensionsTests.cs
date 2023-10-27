using Krosoft.Extensions.Core.Extensions;
using Krosoft.Extensions.Samples.Library.Factories;
using Krosoft.Extensions.Samples.Library.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace Krosoft.Extensions.Core.Tests.Extensions;

[TestClass]
public class DictionaryExtensionsTests
{
    [TestMethod]
    public void GetValueOrDefaultDictionaryTest()
    {
        Dictionary<string, Address> adressesParStreetLine1 = AddressFactory.GetAdresses().ToDictionary(x => x.StreetLine1);

        var address = adressesParStreetLine1.GetValueOrDefault("street1Line1");
        Check.That(address).IsNotNull();
        Check.That(address!.StreetLine2).IsEqualTo("street1Line2");
    }

    [TestMethod]
    public void GetValueOrDefaultIDictionaryTest()
    {
        IDictionary<string, Address> adressesParStreetLine1 = AddressFactory.GetAdresses().ToDictionary(x => x.StreetLine1);

        var address = adressesParStreetLine1.GetValueOrDefault("street1Line1");
        Check.That(address).IsNotNull();
        Check.That(address!.StreetLine2).IsEqualTo("street1Line2");
    }

    [TestMethod]
    public void GetValueOrDefaultDictionaryDefaultValueTest()
    {
        Dictionary<string, Address> adressesParStreetLine1 = AddressFactory.GetAdresses().ToDictionary(x => x.StreetLine1);

        var address = adressesParStreetLine1.GetValueOrDefault("test");
        Check.That(address).IsNull();
    }

    [TestMethod]
    public void GetValueOrDefaultIDictionaryDefaultValueTest()
    {
        IDictionary<string, Address> adressesParStreetLine1 = AddressFactory.GetAdresses().ToDictionary(x => x.StreetLine1);

        var address = adressesParStreetLine1.GetValueOrDefault("test");
        Check.That(address).IsNull();
    }
}