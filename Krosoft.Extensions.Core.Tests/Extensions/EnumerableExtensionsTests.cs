﻿using Krosoft.Extensions.Core.Extensions;
using Krosoft.Extensions.Samples.Library.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace Krosoft.Extensions.Core.Tests.Extensions;

[TestClass]
public class EnumerableExtensionsTests
{
    [TestMethod]
    public void ChunkByTest()
    {
        var adresses = AddressFactory.GetAdresses().ToList();
        Check.That(adresses.Extracting(a => a.ZipCode)).ContainsExactly("zipcode3", "zipcode4", "zipcode5", "zipcode1", "zipcode2", "zipcode6");

        var chunks = adresses.ChunkBy(2);
        Check.That(chunks).HasSize(3);
        Check.That(chunks.Select(a => a.Count)).ContainsExactly(2, 2, 2);
    }

    [TestMethod]
    public void ToDictionaryDistinctEnumerableTest()
    {
        var adresses = AddressFactory.GetAdresses().ToList();
        var adressesCity = adresses.Where(x => x.City == "city");
        Check.That(adressesCity).HasSize(2);

        var adressesParCity = adresses.AsEnumerable().ToDictionary(x => x.City, true);
        Check.That(adressesParCity).HasSize(5);
    }

    [TestMethod]
    public void ToDictionaryDistinctListTest()
    {
        var adresses = AddressFactory.GetAdresses().ToList();
        var adressesCity = adresses.Where(x => x.City == "city");
        Check.That(adressesCity).HasSize(2);

        var adressesParCity = adresses.ToList().ToDictionary(x => x.City, true);
        Check.That(adressesParCity).HasSize(5);
    }

    [TestMethod]
    public void ToDictionaryDistinctModificatorEnumerableTest()
    {
        var adresses = AddressFactory.GetAdresses().ToList();
        var adressesCity = adresses.Where(x => x.City == "city");
        Check.That(adressesCity).HasSize(2);

        var adressesParCity = adresses.AsEnumerable().ToDictionary(x => x.City, x => x.ToUpper(), true);
        Check.That(adressesParCity).HasSize(5);
    }

    [TestMethod]
    public void ToDictionaryDistinctModificatorListTest()
    {
        var adresses = AddressFactory.GetAdresses().ToList();
        var adressesCity = adresses.Where(x => x.City == "city");
        Check.That(adressesCity).HasSize(2);

        var adressesParCity = adresses.ToList().ToDictionary(x => x.City, x => x.ToUpper(), true);
        Check.That(adressesParCity).HasSize(5);
    }

    [TestMethod]
    public async Task ApplyWithAsyncTest()
    {
        var adresses = await AddressFactory.GetAdresses()
                                           .Select(c => c.City)
                                           .ApplyWithAsync(CompteFactory.ToCompteAsync);

        Check.That(adresses).HasSize(5);
        Check.That(adresses.Select(x => x.Name)).ContainsExactly("city3", "city4", "city", "city1", "city2");
    }
}