using Krosoft.Extensions.Core.Extensions;
using Krosoft.Extensions.Samples.Library.Models.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace Krosoft.Extensions.Core.Tests.Extensions;

[TestClass]
public class EnumExtensionsTests
{
    [DataTestMethod]
    [DataRow(SampleEnum.Value1, "Description for Value1")]
    [DataRow(SampleEnum.Value2, "Description for Value2")]
    [DataRow(SampleEnum.Value3, "Description for Value3")]
    [DataRow(SampleEnum.Value4, "Description for Value4")]
    [DataRow(SampleEnum.Value5, "Value5")]
    public void GetDescription_ShouldReturnCorrectDescription(SampleEnum value, string expectedDescription)
    {
        // Act
        var result = value.GetDescription();

        // Assert
        Check.That(result).IsEqualTo(expectedDescription);
    }

    [DataTestMethod]
    [DataRow(SampleEnum.Value1, "Display Name for Value1")]
    [DataRow(SampleEnum.Value2, "Display Name for Value2")]
    [DataRow(SampleEnum.Value3, "Display Name for Value3")]
    [DataRow(SampleEnum.Value4, "Display Name for Value4")]
    [DataRow(SampleEnum.Value5, "Value5")]
    public void GetDisplayName_ShouldReturnCorrectDisplayName(SampleEnum value, string expectedDisplayName)
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
        var value = SampleEnum.Value1 | SampleEnum.Value3;

        // Act
        var result = value.GetFlags();

        // Assert
        Check.That(result).ContainsExactly(SampleEnum.Value1, SampleEnum.Value3);
    }

    [TestMethod]
    public void GetIndividualFlags_ShouldReturnCorrectIndividualFlags()
    {
        // Arrange
        var value = SampleEnum.Value1 | SampleEnum.Value3;

        // Act
        var result = value.GetIndividualFlags();

        // Assert
        Check.That(result).ContainsExactly(SampleEnum.Value1, SampleEnum.Value3);
    }
}