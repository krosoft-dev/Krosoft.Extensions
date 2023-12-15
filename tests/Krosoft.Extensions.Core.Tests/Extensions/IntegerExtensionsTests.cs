﻿using Krosoft.Extensions.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace Krosoft.Extensions.Core.Tests.Extensions;

[TestClass]
public class IntegerExtensionsTests
{
    [DataTestMethod]
    [DataRow(5, 1, 10, true, true)]
    [DataRow(1, 1, 10, true, true)]
    [DataRow(10, 1, 10, true, true)]
    [DataRow(0, 1, 10, false, false)]
    [DataRow(11, 1, 10, false, false)]
    public void IsBetween_ShouldReturnCorrectResult(int num, int lower, int upper, bool inclusive, bool expectedResult)
    {
        // Act
        var result = num.IsBetween(lower, upper, inclusive);

        // Assert
        Check.That(result).IsEqualTo(expectedResult);
    }

    [DataTestMethod]
    [DataRow(5, "00000000-0000-0000-0000-000000000005")]
    [DataRow(123456789, "00000000-0000-0000-0000-000123456789")]
    [DataRow(999999999999, "00000000-0000-0000-0000-999999999999")]
    public void ToGuid_ShouldReturnCorrectGuid(long num, string expectedGuid)
    {
        // Act
        var result = num.ToGuid();

        // Assert
        Check.That(result).IsEqualTo(new Guid(expectedGuid));
    }
}