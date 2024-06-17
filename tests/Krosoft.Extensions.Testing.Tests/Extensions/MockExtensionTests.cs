using System.Data;
using Krosoft.Extensions.Samples.Library.Models;
using Krosoft.Extensions.Testing.Extensions;
using Krosoft.Extensions.Testing.Tests.Models;

namespace Krosoft.Extensions.Testing.Tests.Extensions;

[TestClass]
public class MockExtensionTests
{
    private static IReadOnlyDictionary<string, object?> GetDataRow(IDbCommand cmd)
    {
        var reader = cmd.ExecuteReader();

        reader.Read();

        var dataRow = new Dictionary<string, object?>(reader.FieldCount);

        for (var i = 0; i < reader.FieldCount; i++)
        {
            dataRow[reader.GetName(i)] = reader.GetValue(i);
        }

        return dataRow;
    }

    /// <summary>
    /// Représente un service métier faisant appel à la BDD.
    /// </summary>
    private static IEnumerable<User> GetResultats(IDbCommand cmd)
    {
        var movies = new List<User>();

        var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            var movie = new User
            {
                Id = Convert.ToInt32(reader["Id"]),
                FirstName = (string)reader["FirstName"],
                LastName = (string)reader["LastName"]
            };

            movies.Add(movie);
        }

        return movies;
    }

    [TestMethod]
    public void SetupWithData_One_Line_With_MultiValues()
    {
        var data = new List<List<KeyValuePair<string, object?>>>
        {
            new List<KeyValuePair<string, object?>>
            {
                new KeyValuePair<string, object?>("col1", 1),
                new KeyValuePair<string, object?>("col2", 1.0F),
                new KeyValuePair<string, object?>("col3", 1.1D),
                new KeyValuePair<string, object?>("col4", 1.2M),
                new KeyValuePair<string, object?>("col5", 1L),
                new KeyValuePair<string, object?>("col6", "1.3"),
                new KeyValuePair<string, object?>("col7", new DateTime(2024, 5, 15)),
                new KeyValuePair<string, object?>("col8", new DateTime(2024, 5, 15, 9, 11, 3)),
                new KeyValuePair<string, object?>("col9", true),
                new KeyValuePair<string, object?>("col10", 'a')
            }
        };

        var mock = new Mock<IDbCommand>();
        mock.SetupWithData(data);

        var dataRow = GetDataRow(mock.Object);
        Check.That(dataRow).HasSize(10);
        CollectionAssert.AreEqual(data[0].ToDictionary(ct => ct.Key, ct => ct.Value), dataRow.ToDictionary());
    }

    [DataTestMethod]
    public void SetupWithData_One_Line_With_No_Value()
    {
        var data = new List<List<KeyValuePair<string, object?>>>
        {
            new List<KeyValuePair<string, object?>>
            {
                new KeyValuePair<string, object?>("col1", null),
                new KeyValuePair<string, object?>("col2", null),
                new KeyValuePair<string, object?>("col3", null),
                new KeyValuePair<string, object?>("col4", null),
                new KeyValuePair<string, object?>("col5", null),
                new KeyValuePair<string, object?>("col6", null),
                new KeyValuePair<string, object?>("col7", null),
                new KeyValuePair<string, object?>("col8", null),
                new KeyValuePair<string, object?>("col9", null)
            }
        };
        var mock = new Mock<IDbCommand>();
        mock.SetupWithData(data);

        var dataRow = GetDataRow(mock.Object);
        Check.That(dataRow).HasSize(9);
        CollectionAssert.AreEqual(data[0].ToDictionary(ct => ct.Key, ct => ct.Value), dataRow.ToDictionary());
    }

    [TestMethod]
    public void SetupWithData_KeyValuePair()
    {
        var data = new List<List<KeyValuePair<string, object?>>>
        {
            new List<KeyValuePair<string, object?>>
            {
                new KeyValuePair<string, object?>("Id", 1),
                new KeyValuePair<string, object?>("FirstName", "Joe"),
                new KeyValuePair<string, object?>("LastName", "Doe")
            },
            new List<KeyValuePair<string, object?>>
            {
                new KeyValuePair<string, object?>("Id", 2),
                new KeyValuePair<string, object?>("FirstName", "Steve"),
                new KeyValuePair<string, object?>("LastName", "Smith")
            }
        };

        var mock = new Mock<IDbCommand>(MockBehavior.Strict);
        mock.SetupWithData(data);

        var resultats = GetResultats(mock.Object).ToList();

        Check.That(resultats).HasSize(2);
        Check.That(resultats.Select(x => x.Id)).ContainsExactly(1, 2);
        Check.That(resultats.Select(x => x.FirstName)).ContainsExactly("Joe", "Steve");
        Check.That(resultats.Select(x => x.LastName)).ContainsExactly("Doe", "Smith");
    }

    [TestMethod]
    public void SetupWithData_Object()
    {
        var data = new List<User>
        {
            new User
            {
                Id = 1,
                FirstName = "Joe",
                LastName = "Doe"
            },
            new User
            {
                Id = 2,
                FirstName = "Steve",
                LastName = "Smith"
            }
        };

        var mock = new Mock<IDbCommand>(MockBehavior.Strict);
        mock.SetupWithData(data);

        var resultats = GetResultats(mock.Object).ToList();

        Check.That(resultats).HasSize(2);
        Check.That(resultats.Select(x => x.Id)).ContainsExactly(1, 2);
        Check.That(resultats.Select(x => x.FirstName)).ContainsExactly("Joe", "Steve");
        Check.That(resultats.Select(x => x.LastName)).ContainsExactly("Doe", "Smith");
    }

    [TestMethod]
    public void VerifyNeverCallTest()
    {
        var foo = new Mock<IFoo>(MockBehavior.Loose);

        Check.That(foo).Verify(m => m.Call1(), Times.Never);
        Check.That(foo).Verify(m => m.Call2(), Times.Never);
    }

    [TestMethod]
    public void VerifyTest()
    {
        var foo = new Mock<IFoo>(MockBehavior.Loose);

        foo.Object.Call1();

        Check.That(foo).Verify(m => m.Call1(), Times.Once);
        Check.That(foo).Verify(m => m.Call2(), Times.Never);
    }
}