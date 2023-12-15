﻿using System.IO.Compression;
using System.Reflection;
using Krosoft.Extensions.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace Krosoft.Extensions.Core.Tests.Helpers;

[TestClass]
public class FileHelperTests
{
    [TestMethod]
    public async Task WriteTextAsync_Ok()
    {
         await FileHelper.WriteTextAsync("hello.txt", "Hello World", CancellationToken.None);
         var file = await FileHelper.ReadAsStringAsync("hello.txt",  CancellationToken.None);

        Check.That(file).IsNotNull();
        Check.That(file.Length).IsEqualTo(13264);
        Check.That(file).IsEqualTo("");
    }

 
}