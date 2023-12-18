using System.Linq.Expressions;
using Krosoft.Extensions.Data.Abstractions.Extensions;
using Krosoft.Extensions.Samples.Library.Models.Entities;
using NFluent;

namespace Krosoft.Extensions.Data.Abstractions.Tests.Extensions;




[TestClass]
public class QueryableExtensionsTests
{
   


    [TestMethod]
    public void Filter_NoItems_ReturnsOriginalQuery()
    {
        // Arrange
        var data = new List<SampleEntity>
        {
            new SampleEntity { Id = 1, Name = "Item1" },
            new SampleEntity { Id = 2, Name = "Item2" },
                    new SampleEntity { Id = 3, Name = "Item3" },
                   new SampleEntity { Id = 4, Name = "Item4" }
        }.AsQueryable();

       Func<int, Expression<Func<SampleEntity, bool>>> func = id => entity => entity.Id == id;

        // Act
        var result = data.Filter(null, func, false);

        // Assert
        Check.That(result).IsEqualTo(data);
    }

    [TestMethod]
    public void Filter_NoItems_ReturnsOriginalQuery2()
    {
        // Arrange
        var data = new List<SampleEntity>
        {
            new SampleEntity { Id = 1, Name = "Item1" },
            new SampleEntity { Id = 2, Name = "Item2" },
            new SampleEntity { Id = 3, Name = "Item3" },
            new SampleEntity { Id = 4, Name = "Item4" }
        }.AsQueryable();

       Func<int, Expression<Func<SampleEntity, bool>>> func = id => entity => entity.Id == id;

        // Act
        var result = data.Filter(null, func, true);

        // Assert
        Check.That(result).IsEqualTo(data);
    }

    [TestMethod]
    public void Filter_WithItemsAndIsMandatoryFalse_ReturnsOriginalQuery()
    {
        // Arrange
        var data = new List<SampleEntity>
        {
            new SampleEntity { Id = 1, Name = "Item1" },
            new SampleEntity { Id = 2, Name = "Item2" },
            new SampleEntity { Id = 3, Name = "Item3" },
            new SampleEntity { Id = 4, Name = "Item4" }
        }.AsQueryable();

       Func<int, Expression<Func<SampleEntity, bool>>> func = id => entity => entity.Id == id;

        // Act
        var result = data.Filter(new List<int> { 1, 2 }, func, false);

        // Assert
        Check.That(result).IsEqualTo(data);
    }

    [TestMethod]
    public void Filter_WithItemsAndIsMandatoryTrue_ReturnsFilteredQuery()
    {
        // Arrange
        var data = new List<SampleEntity>
        {
            new SampleEntity { Id = 1, Name = "Item1" },
            new SampleEntity { Id = 2, Name = "Item2" },
            new SampleEntity { Id = 3, Name = "Item3" },
            new SampleEntity { Id = 4, Name = "Item4" }
        }.AsQueryable();

       Func<int, Expression<Func<SampleEntity, bool>>> func = id => entity => entity.Id == id;

        // Act
        var result = data.Filter(new List<int> { 1, 2 }, func, true);

        // Assert
        Check.That(result.Count()).IsEqualTo(2);
        Check.That(result.Any(entity => entity.Id == 1)).IsTrue();
        Check.That(result.Any(entity => entity.Id == 2)).IsTrue();
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