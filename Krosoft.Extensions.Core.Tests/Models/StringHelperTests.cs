﻿using Krosoft.Extensions.Core.Models;
using Krosoft.Extensions.Core.Models.Exceptions;
using Krosoft.Extensions.Samples.Library.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace Krosoft.Extensions.Core.Tests.Models;

[TestClass]
public class ResultTests
{
    [TestMethod]
    public void SuccessTest()
    {
        CheckSuccess();
    }

    [TestMethod]
    public void FaultedTest()
    {
        CheckFaulted();
    }

    [TestMethod]
    public void ValidateTest()
    {
        var result = CheckSuccess();

        var address = result.Validate();

        Check.That(address).IsNotNull();
        Check.That(address.City).IsEqualTo("Paris");
    }

    [TestMethod]
    public void ValidateFailTest()
    {
        var result = CheckFaulted();

        Check.ThatCode(() => result.Validate())
             .Throws<KrosoftMetierException>()
             .WithMessage("Test");
    }

    private static Result<Address> CheckSuccess()
    {
        var result = new Result<Address>(new Address(null!, null!,
                                                     "Paris", null!));
        Check.That(result).IsInstanceOf<Result<Address>>();
        Check.That(result.IsSuccess).IsTrue();
        Check.That(result.IsFaulted).IsFalse();
        Check.That(result.Value).IsNotNull();
        Check.That(result.Value?.City).IsEqualTo("Paris");
        Check.That(result.Exception).IsNull();

        return result;
    }

    private static Result<Address> CheckFaulted()
    {
        var result = new Result<Address>(new Exception("Test"));
        Check.That(result).IsInstanceOf<Result<Address>>();
        Check.That(result.IsSuccess).IsFalse();
        Check.That(result.IsFaulted).IsTrue();
        Check.That(result.Value).IsNull();
        Check.That(result.Exception).IsNotNull();
        Check.That(result.Exception?.Message).IsEqualTo("Test");

        return result;
    }
}