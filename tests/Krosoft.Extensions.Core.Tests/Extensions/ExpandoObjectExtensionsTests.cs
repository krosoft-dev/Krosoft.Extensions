﻿using System.Dynamic;
using Krosoft.Extensions.Core.Extensions;
using Krosoft.Extensions.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NFluent;

namespace Krosoft.Extensions.Core.Tests.Extensions;

[TestClass]
public class ExpandoObjectExtensionsTests
{
    [TestMethod]
    public void GetStringTest()
    {
        var json = FileHelper.ReadAsString(typeof(ExpandoObjectExtensionsTests).Assembly, "en.json", EncodingHelper.GetEuropeOccidentale());
        var expandoObject = JsonConvert.DeserializeObject<ExpandoObject>(json, new ExpandoObjectConverter());
        var titre = expandoObject!.GetString("titre");

        Check.That(titre).IsEqualTo("Forgotten password");
    }
}