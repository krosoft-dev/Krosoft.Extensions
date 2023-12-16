﻿using System.Net;
using Krosoft.Extensions.Core.Converters;
using Krosoft.Extensions.Core.Models.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NFluent;

namespace Krosoft.Extensions.Core.Tests.Converters;

[TestClass]
public class KrosoftMetierExceptionConverterTests
{
    [TestMethod]
    public void ConvertEmptyTest()
    {
        var obj = JsonConvert.DeserializeObject<KrosoftMetierException>(string.Empty, new KrosoftMetierExceptionConverter());
        Check.That(obj).IsNull();
    }

    [TestMethod]
    public void ConvertJsonArrayEmptyTest()
    {
        var json = "[]";
        var obj = JsonConvert.DeserializeObject<IEnumerable<KrosoftMetierException>>(json, new KrosoftMetierExceptionConverter());
        Check.That(obj).IsEmpty();
    }

    [TestMethod]
    public void ConvertJsonEmptyNoErreursTest()
    {
        const string json = @"{
            ""Code"": 400,
            ""Message"": ""BadRequest"",
            ""Erreurs"": []
        }";
        var obj = JsonConvert.DeserializeObject<KrosoftMetierException>(json, new KrosoftMetierExceptionConverter());
        Check.That(obj).IsNotNull();
        Check.That(obj!.Code).IsEqualTo(HttpStatusCode.BadRequest);
        Check.That(obj.Erreurs).IsEmpty();
    }

    [TestMethod]
    public void ConvertJsonEmptyTest()
    {
        var json = "{}";
        var obj = JsonConvert.DeserializeObject<KrosoftMetierException>(json, new KrosoftMetierExceptionConverter());
        Check.That(obj!.GetType()).IsEqualTo(typeof(KrosoftMetierException));
        Check.That(obj).IsNotNull();
    }

    [TestMethod]
    public void ConvertJsonTest()
    {
        const string json = @"{
            ""Code"": 400,
            ""Message"": ""BadRequest"",
            ""Erreurs"": [
                ""An ean13 must contain 13 digits."",
                ""Un message là.""
                ]

             }";
        var obj = JsonConvert.DeserializeObject<KrosoftMetierException>(json, new KrosoftMetierExceptionConverter());
        Check.That(obj).IsNotNull();
        Check.That(obj!.Code).IsEqualTo(HttpStatusCode.BadRequest);
        Check.That(obj.Message).IsEqualTo("An ean13 must contain 13 digits.");
        Check.That(obj.Erreurs).HasSize(2);
        Check.That(obj.Erreurs.First()).IsEqualTo("An ean13 must contain 13 digits.");
        Check.That(obj.Erreurs.Last()).IsEqualTo("Un message là.");
    }
}