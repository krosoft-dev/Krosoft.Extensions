﻿using Krosoft.Extensions.Core.Helpers;
using Krosoft.Extensions.Core.Models.Exceptions;
using Krosoft.Extensions.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace Krosoft.Extensions.Core.Tests.Helpers;

[TestClass]
public class StringHelperTests : BaseTest
{
    [DataTestMethod]
    [DataRow("", null)]
    [DataRow(" ", null)]
    [DataRow("20211215", "2021-12-15")] // Format YYYYMMDD
    [DataRow("20210230", null)] // Invalid date (February 30)
    [DataRow("20211301", null)] // Invalid month (13)
    [DataRow("20210532", null)] // Invalid day (32)
    [DataRow("99991231", null)] // Extreme value (9999-12-31)
    [DataRow("00000101", null)] // Extreme value (0001-01-01)]
    public void DateStringToDateTime_ShouldConvertStringToDateTime(string input, string? expectedDateString)
    {
        // Act
        var result = StringHelper.DateStringToDateTime(input);

        // Assert
        if (expectedDateString == null)
        {
            Check.That(result).IsNull();
        }
        else
        {
            var expectedDate = DateTime.Parse(expectedDateString);
            Check.That(result).IsEqualTo(expectedDate);
        }
    }

    [DataTestMethod]
    [DataRow("test", "01/01/0001")]
    [DataRow(null!, "01/01/0001")]
    [DataRow("", "01/01/0001")]
    [DataRow("12/08/1988", "12/08/1988")]
    [DataRow("2012-04-23T18:25:43.511Z", "23/04/2012")]
    public void FormatDateStringIncorrectTest(string input, string expected)
    {
        var formatDate = StringHelper.FormatDate(input);
        Check.That(formatDate).IsEqualTo(expected);
    }

    [DataTestMethod]
    [DataRow("Hello, World!", "Hello, World!")]
    [DataRow("", "")]
    [DataRow(null, null)]
    public void GenerateStreamFromString_ShouldGenerateCorrectStream(string? input, string? expectedContent)
    {
        // Act
        var resultStream = StringHelper.GenerateStreamFromString(input);

        // Assert
        if (expectedContent == null)
        {
            Check.That(resultStream.Length).IsEqualTo(0);
        }
        else
        {
            using var reader = new StreamReader(resultStream);
            var resultContent = reader.ReadToEnd();
            Check.That(resultContent).IsEqualTo(expectedContent);
        }
    }

    [TestMethod]
    public void GetAbbreviationEmptyTest()
    {
        Check.That(StringHelper.GetAbbreviation(string.Empty)).IsEqualTo(string.Empty);
    }

    [TestMethod]
    public void GetAbbreviationNullTest()
    {
        Check.That(StringHelper.GetAbbreviation(null)).IsEqualTo(string.Empty);
    }

    [DataTestMethod]
    [DataRow("T", "T")]
    [DataRow("T ", "T")]
    [DataRow(" T", "T")]
    [DataRow(" T ", "T")]
    [DataRow("au", "AU")]
    [DataRow("AU", "AU")]
    [DataRow("Au", "AU")]
    [DataRow("Thibault de Montaigu", "TD")]
    [DataRow("thibault De montaigu", "TD")]
    [DataRow("Thibault de Montaigu de Lyon en Lausane", "TD")]
    [DataRow("Admin", "AD")]
    [DataRow("Administrator", "AD")]
    [DataRow("administrator", "AD")]
    [DataRow("AdministratorAuService", "AA")]
    [DataRow("Pierre Louis-Calixte de la Comédie-Française", "PL")]
    [DataRow("Claire de La Rüe du Can de la Comédie-Française", "CD")]
    public void GetAbbreviationTest(string input, string expected)
    {
        var formatDate = StringHelper.GetAbbreviation(input);
        Check.That(formatDate).IsEqualTo(expected);
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
             .WithMessage("La variable 'payload' n'est pas renseignée.");
    }

    [TestMethod]
    public void ToBase64Test()
    {
        var base64 = StringHelper.ToBase64("jwtToken");
        Check.That(base64).IsEqualTo("and0VG9rZW4=");
    }

    [DataTestMethod]
    [DataRow(null, "")]
    [DataRow("", "")]
    [DataRow("   test   ", "test")]
    [DataRow("test   ", "test")]
    [DataRow("     test   ", "test")]
    public void TrimTest(string input, string expected)
    {
        var formatDate = StringHelper.Trim(input);
        Check.That(formatDate).IsEqualTo(expected);
    }

    [DataTestMethod]
    [DataRow(null, 0)]
    [DataRow("", 0)]
    [DataRow("160519", 160519)]
    [DataRow("9432.0", 0)]
    [DataRow("16,667", 0)]
    [DataRow("42.42", 0)]
    [DataRow("   -322   ", -322)]
    [DataRow("+4302", 4302)]
    [DataRow("(100);", 0)]
    [DataRow("01FA", 0)]
    public void TryParseToIntTest(string input, int expected)
    {
        var formatDate = StringHelper.TryParseToInt(input);
        Check.That(formatDate).IsEqualTo(expected);
    }
}