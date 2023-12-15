using Krosoft.Extensions.Core.Extensions;
using Krosoft.Extensions.Samples.Library.Models.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace Krosoft.Extensions.Core.Tests.Extensions;

[TestClass]
public class EnumExtensionsTests
{
    [DataTestMethod]
    [DataRow(SampleCode.Value1, "Description for Value1")]
    [DataRow(SampleCode.Value2, "Description for Value2")]
    [DataRow(SampleCode.Value3, "Description for Value3")]
    [DataRow(SampleCode.Value4, "Description for Value4")]
    [DataRow(SampleCode.Value5, "Value5")]
    public void GetDescription_ShouldReturnCorrectDescription(SampleCode value, string expectedDescription)
    {
        // Act
        var result = value.GetDescription();

        // Assert
        Check.That(result).IsEqualTo(expectedDescription);
    }

    [DataTestMethod]
    [DataRow(SampleCode.Value1, "Display Name for Value1")]
    [DataRow(SampleCode.Value2, "Display Name for Value2")]
    [DataRow(SampleCode.Value3, "Display Name for Value3")]
    [DataRow(SampleCode.Value4, "Display Name for Value4")]
    [DataRow(SampleCode.Value5, "Value5")]
    public void GetDisplayName_ShouldReturnCorrectDisplayName(SampleCode value, string expectedDisplayName)
    {
        // Act
        var result = value.GetDisplayName();

        // Assert
        Check.That(result).IsEqualTo(expectedDisplayName);
    }

    [TestMethod]
    public void GetFlags_ShouldReturnCorrectFlags()
    {
        // Arrange
        var value = SampleCode.Value1 | SampleCode.Value3;

        // Act
        var result = value.GetFlags();

        // Assert
        Check.That(result).ContainsExactly(SampleCode.Value1, SampleCode.Value3);
    }

    [TestMethod]
    public void GetIndividualFlags_ShouldReturnCorrectIndividualFlags()
    {
        // Arrange
        var value = SampleCode.Value1 | SampleCode.Value3;

        // Act
        var result = value.GetIndividualFlags();

        // Assert
        Check.That(result).ContainsExactly(SampleCode.Value1, SampleCode.Value3);
    }
}