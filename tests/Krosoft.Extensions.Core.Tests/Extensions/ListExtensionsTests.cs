using System.Collections.ObjectModel;
using Krosoft.Extensions.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace Krosoft.Extensions.Core.Tests.Extensions;

[TestClass]
public class ListExtensionsTests
{
    [DataTestMethod]
    [DataRow(new[] { 1, 2, 3 }, new[] { 3, 4, 5 }, true, new[] { 1, 2, 3, 4, 5 })]
    [DataRow(new[] { 1, 2, 3 }, new[] { 3, 4, 5 }, false, new[] { 1, 2, 3, 3, 4, 5 })]
    [DataRow(new[] { "apple", "orange" }, new[] { "orange", "banana" }, true, new[] { "apple", "orange", "banana" })]
    public void AddRange_ShouldAddCorrectly<T>(IEnumerable<T> initialList, IEnumerable<T> collection, bool checkContains, IEnumerable<T> expectedList)
    {
        // Arrange
        var list = new List<T>(initialList);

        // Act
        list.AddRange(collection, checkContains);

        // Assert
        Check.That(list).ContainsExactly(expectedList);
    }

    [TestMethod]
    public void ToDictionary_ShouldReturnCorrectDictionary()
    {
        // Act
        var result = new[] { "apple", "orange", "banana" }.ToDictionary(x => x.Length, true);

        // Assert
        Check.That(result).IsEqualTo(new Dictionary<int, string> { { 5, "apple" }, { 6, "orange" } });
    }

    [TestMethod]
    public void ToReadOnlyDictionary_ShouldReturnCorrectReadOnlyDictionary()
    {
        Check.ThatCode(() => new[] { "apple", "orange", "banana" }.ToReadOnlyDictionary(x => x.Length, false))
             .Throws<ArgumentException>()
             .WithMessage("An item with the same key has already been added. Key: 6");
    }

    [TestMethod]
    public void ToReadOnlyDictionary_ShouldReturnCorrectReadOnlyDictionary_Distinct()
    {
        // Act
        var result = new[] { "apple", "orange", "banana" }.ToReadOnlyDictionary(x => x.Length, true);

        // Assert
        Check.That(result).IsEqualTo(new ReadOnlyDictionary<int, string>(new Dictionary<int, string> { { 5, "apple" }, { 6, "orange" } }));
    }

    [TestMethod]
    public void ToDictionaryWithModifier_ShouldReturnCorrectDictionary_Distinct()
    {
        // Act
        var result = new[] { "apple", "orange", "apple", "orange", "banana" }.ToDictionary(x => x.Length.ToString(), x => x.ToUpper(), true);

        // Assert
        Check.That(result).ContainsExactly(new Dictionary<string, string> { { "5", "apple" }, { "6", "orange" } });
    }

    [TestMethod]
    public void ToDictionaryWithModifier_ShouldReturnCorrectDictionary()
    {
        Check.ThatCode(() => new[] { "apple", "orange", "apple", "orange", "banana" }.ToDictionary(x => x.Length.ToString(), x => x.ToUpper(), false))
             .Throws<ArgumentException>()
             .WithMessage("An item with the same key has already been added. Key: 5");
    }
}