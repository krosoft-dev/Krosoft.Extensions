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
    public void Filter_Item_NoItem()
    {
        var data = GetQueryable();
        int? requestId = null;
        var query = data.Filter(requestId, id => c => c.Id == id);

        Check.That(query.Expression.ToString()).IsEqualTo(data.Expression.ToString());
    }

    [TestMethod]
    public void Filter_Item_NoMatch()
    {
        var data = GetQueryable();
        var requestId = 42;

        var query = data.Filter<SampleEntity, int>(requestId, id => c => c.Id <= id);

        Check.That(query).HasSize(4);
        Check.That(query.Select(x => x.Id)).ContainsExactly(1, 2, 3, 4);
        Check.That(query.Select(x => x.Name)).ContainsExactly("Item1", "Item2", "Item3", "Item4");
    }

    [TestMethod]
    public void Filter_Item_Ok()
    {
        var data = GetQueryable();
        var requestId = 2;

        var query = data.Filter<SampleEntity, int>(requestId, id => c => c.Id <= id);

        Check.That(query).HasSize(2);
        Check.That(query.Select(x => x.Id)).ContainsExactly(1, 2);
        Check.That(query.Select(x => x.Name)).ContainsExactly("Item1", "Item2");
    }

    [TestMethod]
    public void Filter_Mandatory()
    {
        var data = GetQueryable();
        var func = GetFunc();
        var query = data.Filter(new List<int> { 2, 3 }, func, true);

        Check.That(query).HasSize(2);
        Check.That(query.Select(x => x.Id)).ContainsExactly(2, 3);
        Check.That(query.Select(x => x.Name)).ContainsExactly("Item2", "Item3");
    }

    [TestMethod]
    public void Filter_Mandatory_ItemsEmpty()
    {
        var data = GetQueryable();
        var func = GetFunc();
        var query = data.Filter(new List<int>(), func, true);

        Check.That(query).HasSize(0);
    }

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
    public void Filter_NoMandatory()
    {
        var data = GetQueryable();
        var func = GetFunc();
        var query = data.Filter(new List<int> { 2, 3 }, func);

        Check.That(query).HasSize(2);
        Check.That(query.Select(x => x.Id)).ContainsExactly(2, 3);
        Check.That(query.Select(x => x.Name)).ContainsExactly("Item2", "Item3");
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