using Krosoft.Extensions.Core.Helpers;
using Krosoft.Extensions.Core.Models.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace Krosoft.Extensions.Core.Tests.Helpers;

[TestClass]
public class StringHelperTests
{
    [TestMethod]
    public void FormatDateTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void GenerateStreamFromStringTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void TrimTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void TryParseToBooleanTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void TryParseToIntTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void TryParseToLongTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void ClearFilePathTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void TrimIfNotNullTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void TrimTest1()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void JoinTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void DateStringToDateTimeTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void RandomStringTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void TruncateTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void KeepDigitsOnlyTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void FormatNumberTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void FormatCurrencyTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void ToBase64Test()
    {
        var base64 = StringHelper.ToBase64("jwtToken");
        Check.That(base64).IsEqualTo("and0VG9rZW4=");
    }

    [TestMethod]
    public void ToBase64EmptyTest()
    {
        var base64 = StringHelper.ToBase64(string.Empty);
        Check.That(base64).IsEqualTo(string.Empty);
    }

    [TestMethod]
    public void ToBase64NullTest()
    {
        Check.ThatCode(() => StringHelper.ToBase64(null))
             .Throws<KrosoftTechniqueException>()
             .WithMessage("La variable payload n'est pas renseignée.");
    }

    [TestMethod]
    public void GetAbbreviationNullTest()
    {
        Check.That(StringHelper.GetAbbreviation(null)).IsEqualTo(string.Empty);
    }

    [TestMethod]
    public void GetAbbreviationEmptyTest()
    {
        Check.That(StringHelper.GetAbbreviation(string.Empty)).IsEqualTo(string.Empty);
    }

    [TestMethod]
    public void GetAbbreviationTest()
    {
        Check.That(StringHelper.GetAbbreviation("T")).IsEqualTo("T");
        Check.That(StringHelper.GetAbbreviation("T ")).IsEqualTo("T");
        Check.That(StringHelper.GetAbbreviation(" T")).IsEqualTo("T");
        Check.That(StringHelper.GetAbbreviation(" T ")).IsEqualTo("T");
        Check.That(StringHelper.GetAbbreviation("au")).IsEqualTo("AU");
        Check.That(StringHelper.GetAbbreviation("AU")).IsEqualTo("AU");
        Check.That(StringHelper.GetAbbreviation("Au")).IsEqualTo("AU");
        Check.That(StringHelper.GetAbbreviation("Thibault de Montaigu")).IsEqualTo("TD");
        Check.That(StringHelper.GetAbbreviation("thibault De montaigu")).IsEqualTo("TD");
        Check.That(StringHelper.GetAbbreviation("Thibault de Montaigu de Lyon en Lausane")).IsEqualTo("TD");
        Check.That(StringHelper.GetAbbreviation("Admin")).IsEqualTo("AD");
        Check.That(StringHelper.GetAbbreviation("Administrator")).IsEqualTo("AD");
        Check.That(StringHelper.GetAbbreviation("administrator")).IsEqualTo("AD");
        Check.That(StringHelper.GetAbbreviation("AdministratorAuService")).IsEqualTo("AA");
        Check.That(StringHelper.GetAbbreviation("Pierre Louis-Calixte de la Comédie-Française")).IsEqualTo("PL");
        Check.That(StringHelper.GetAbbreviation("Claire de La Rüe du Can de la Comédie-Française")).IsEqualTo("CD");
    }
}