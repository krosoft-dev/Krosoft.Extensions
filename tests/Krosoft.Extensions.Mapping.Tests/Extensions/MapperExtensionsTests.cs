using AutoMapper;
using Krosoft.Extensions.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;
using Positive.Extensions.Application.Extensions;
using Positive.Extensions.Core.Extensions;
using Positive.Extensions.Core.Models.Exceptions;
using Positive.Extensions.Mapping.Extensions;
using Positive.Extensions.Samples.Api;
using Positive.Extensions.Samples.Api.Models;
using Positive.Extensions.Testing;

namespace Positive.Extensions.Mapping.Tests.Extensions;

[TestClass]
public class MapperExtensionsTests : BaseTest
{
    private IMapper _mapper;

    protected override void AddServices(ServiceCollection services, IConfiguration configuration)
    {
        services.AddApplication(typeof(Startup).Assembly);
    }

    [TestInitialize]
    public void SetUp()
    {
        var serviceProvider = CreateServiceCollection();
        _mapper = serviceProvider.GetRequiredService<IMapper>();
    }

    [TestMethod]
    public void MapIfExistDictionaryTest()
    {
        var comptesParId = GetComptes().ToDictionary(x => x.Id);

        var compteDto = new CompteDto();
        _mapper.MapIfExist(comptesParId, "K", compteDto);

        Check.That(compteDto.Name).IsEqualTo("Compte K");
    }

    [TestMethod]
    public void MapIfExistReadOnlyDictionaryTest()
    {
        var comptesParId = GetComptes().ToReadOnlyDictionary(x => x.Id);

        var compteDto = new CompteDto();
        _mapper.MapIfExist((IDictionary<string, Compte>)comptesParId, "K", compteDto);

        Check.That(compteDto.Name).IsEqualTo("Compte K");
    }

    [TestMethod]
    public void MapIfExistConcurrentDictionaryTest()
    {
        var comptesParId = GetComptes().ToConcurrentDictionary(x => x.Id);

        var compteDto = new CompteDto();
        _mapper.MapIfExist(comptesParId, "K", compteDto);

        Check.That(compteDto.Name).IsEqualTo("Compte K");
    }

    [TestMethod]
    public void MapIfExistTest()
    {
        var source = new Compte { Name = "Test" };
        var destination = new CompteDto();

        _mapper.MapIfExist(source, destination);

        Check.That(destination.Name).IsEqualTo("Test");
    }

    [TestMethod]
    public void MapIfExistNullTest()
    {
        Compte source = null;
        var destination = new CompteDto();

        _mapper.MapIfExist(source, destination);

        Check.That(destination.Name).IsNull();
    }

    [TestMethod]
    public void MapIfExistActionTest()
    {
        var source = new Compte { Name = "Test" };
        var destination = new CompteDto();

        _mapper.MapIfExist(source, destination, () => throw new PositiveTechniqueException("Test"));

        Check.That(destination.Name).IsEqualTo("Test");
    }

    [TestMethod]
    public void MapIfExistActionNullTest()
    {
        Compte source = null;
        var destination = new CompteDto();

        Check.ThatCode(() => _mapper.MapIfExist(source, destination, () => throw new PositiveTechniqueException("Test")))
             .Throws<PositiveTechniqueException>()
             .WithMessage("Test");
    }

    private static IEnumerable<Compte> GetComptes()
    {
        for (var c = 'A'; c <= 'Z'; c++)
        {
            yield return new Compte
            {
                Id = $"{c}",
                Name = $"Compte {c}"
            };
        }
    }

    [TestMethod]
    public void MapIfExistToTypeTest()
    {
        var source = new Compte { Name = "Test" };

        var destination = _mapper.MapIfExist<CompteDto>(source);

        Check.That(destination.Name).IsEqualTo("Test");
    }

    [TestMethod]
    public void MapIfExistToTypeNullTest()
    {
        Compte source = null;

        var destination = _mapper.MapIfExist<CompteDto>(source);

        Check.That(destination).IsNull();
    }

    [TestMethod]
    public void MapIfExistToTypeActionTest()
    {
        var source = new Compte { Name = "Test" };

        var destination = _mapper.MapIfExist<CompteDto>(source, () => throw new PositiveTechniqueException("Test"));

        Check.That(destination.Name).IsEqualTo("Test");
    }

    [TestMethod]
    public void MapIfExistToTypeActionNullTest()
    {
        Compte source = null;

        Check.ThatCode(() => _mapper.MapIfExist<CompteDto>(source, () => throw new PositiveTechniqueException("Test")))
             .Throws<PositiveTechniqueException>()
             .WithMessage("Test");
    }
}