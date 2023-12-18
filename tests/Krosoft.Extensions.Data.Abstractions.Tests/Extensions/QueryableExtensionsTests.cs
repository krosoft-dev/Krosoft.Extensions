using System.Linq.Expressions;
using Krosoft.Extensions.Core.Models.Exceptions;
using Krosoft.Extensions.Data.Abstractions.Extensions;
using Krosoft.Extensions.Samples.Library.Models.Entities;
using NFluent;

namespace Krosoft.Extensions.Data.Abstractions.Tests.Extensions;

[TestClass]
public class QueryableExtensionsTests
{
    [TestMethod]
    public void Filter_NoFunc()
    {
        var data = GetQueryable();

        Check.ThatCode(() => data.Filter<SampleEntity, int>(null, null, true))
             .Throws<KrosoftTechniqueException>()
             .WithMessage("La variable 'items' n'est pas renseignée.");
    }

    [TestMethod]
    public void Filter_NoItems()
    {
        var data = GetQueryable();

        Check.ThatCode(() => data.Filter(new List<int>(), null, true))
             .Throws<KrosoftTechniqueException>()
             .WithMessage("La variable 'func' n'est pas renseignée.");
    }

    [TestMethod]
    public void Filter_WithItemsAndIsMandatoryFalse_ReturnsOriginalQuery()
    {
        var data = GetQueryable();
        var func = GetFunc();
        var query = data.Filter(new List<int> { 2, 3 }, func, false);

        Check.That(query).HasSize(2);
        Check.That(query.Select(x => x.Id)).ContainsExactly(2, 3);
        Check.That(query.Select(x => x.Name)).ContainsExactly("Item2", "Item3");
    }

    [TestMethod]
    public void Filter_WithItemsAndIsMandatoryTrue_ReturnsFilteredQuery()
    {
        var data = GetQueryable();
        var func = GetFunc();
        var query = data.Filter(new List<int> { 2, 3 }, func, true);

        Check.That(query).HasSize(2);
        Check.That(query.Select(x => x.Id)).ContainsExactly(2, 3);
        Check.That(query.Select(x => x.Name)).ContainsExactly("Item2", "Item3");
    }

    [TestMethod]
    public void Filter_WithItemsAndIsMandatoryTrue_ReturnsFilteredQuery2()
    {
        var data = GetQueryable();
        var func = GetFunc();
        var query = data.Filter(new List<int>(), func, true);

        Check.That(query).HasSize(0);
    }

    private static Func<int, Expression<Func<SampleEntity, bool>>> GetFunc() => id => entity => entity.Id == id;

    private static IQueryable<SampleEntity> GetQueryable()
    {
        var data = new List<SampleEntity>
        {
            new SampleEntity { Id = 1, Name = "Item1" },
            new SampleEntity { Id = 2, Name = "Item2" },
            new SampleEntity { Id = 3, Name = "Item3" },
            new SampleEntity { Id = 4, Name = "Item4" }
        }.AsQueryable();
        return data;
    }
}

//[TestMethod]
//[DataRow(new[] { 1, 2, 3 }, true, 3)]
//[DataRow(new[] { 1, 2, 3 }, false, 3)]
//[DataRow(_d, true, 3)]
//[DataRow(_d , false, 3)]
//[DataRow(null, true, 4)]
//[DataRow(null, false, 4)]
//public void Filter_Tests(IEnumerable<int>? items, bool isMandatory, int expectedCount)
//{
//    // Arrange
//    var data = new List<SampleEntity>
//    {
//        new SampleEntity { Id = 1, Name = "Item1" },
//        new SampleEntity { Id = 2, Name = "Item2" },
//        new SampleEntity { Id = 3, Name = "Item3" },
//        new SampleEntity { Id = 4, Name = "Item4" }
//    }.AsQueryable();

//    Func<int, Expression<Func<SampleEntity, bool>>> func = id => entity => entity.Id == id;

//    var result = data.Filter(items, func, isMandatory).ToList();

//    Check.That(result.Count).IsEqualTo(expectedCount);
//}

//}