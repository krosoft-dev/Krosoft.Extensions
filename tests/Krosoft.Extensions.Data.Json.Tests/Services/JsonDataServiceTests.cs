﻿using AutoMapper;
using FluentValidation;
using Krosoft.Extensions.Core.Extensions;
using Krosoft.Extensions.Core.Models.Exceptions;
using Krosoft.Extensions.Data.Json.Extensions;
using Krosoft.Extensions.Data.Json.Interfaces;
using Krosoft.Extensions.Data.Json.Models;
using Krosoft.Extensions.Data.Json.Services;
using Krosoft.Extensions.Samples.Library.Models;
using Krosoft.Extensions.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace Krosoft.Extensions.Data.Json.Tests.Services;

[TestClass]
public class JsonDataServiceTests : BaseTest
{
    private IJsonDataService<Item> _jsonDataService = null!;
    private IJsonDataService<Shortcut> _jsonDataServiceShortcut = null!;

    protected override void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddJsonDataService(configuration);
    }

    [TestInitialize]
    public void SetUp()
    {
        var serviceProvider = CreateServiceCollection();
        _jsonDataService = serviceProvider.GetRequiredService<IJsonDataService<Item>>();
        _jsonDataServiceShortcut = serviceProvider.GetRequiredService<IJsonDataService<Shortcut>>();
    }

    [TestMethod]
    public void Query_Ok()
    {
        var comptes = _jsonDataService.Query().ToList();

        Check.That(comptes).Not.IsNullOrEmpty();
        Check.That(comptes).HasSize(2);
        Check.That(comptes.Select(x => x.Code)).IsOnlyMadeOf("test 1", "test 2");
    }

    [TestMethod]
    public void Init_Ko()
    {
        Check.ThatCode(() => new JsonDataService<Shortcut>(Options.Create(new JsonDataSettings()),
                                                         new List<IValidator<Shortcut>>()).Query())
             .Throws<KrosoftTechniqueException>()
             .WithMessage("DataFileName non renseigné.");
    }

    [TestMethod]
    public async Task InsertAsyncTest()
    {
        var countBefore = _jsonDataServiceShortcut.Query().Count();


        var max = _jsonDataServiceShortcut.Query().MaxOrDefault(x => x.Id);

        var id = max + 1;
        

        var shortcut = new Shortcut { Id =id, Code = "test"};
        await _jsonDataServiceShortcut.InsertAsync(shortcut, CancellationToken.None);

        var countAfter = _jsonDataServiceShortcut.Query().Count();
        Check.That(countAfter).IsEqualTo(countBefore + 1);


        var fromBdd = _jsonDataServiceShortcut.Query().FirstOrDefault(x=>x.Id == id);
        Check.That(fromBdd).IsNotNull();
        Check.That(fromBdd!.Id).IsEqualTo(id);
        Check.That(fromBdd.Code).IsEqualTo("test");






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