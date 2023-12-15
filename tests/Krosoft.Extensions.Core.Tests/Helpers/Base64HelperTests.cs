using Microsoft.VisualStudio.TestTools.UnitTesting;
using Krosoft.Extensions.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFluent;

namespace Krosoft.Extensions.Core.Helpers.Tests
{
    [TestClass()]
    public class Base64HelperTests
    {
        [DataTestMethod]
        [DataRow("SGVsbG8sIFdvcmxkIQ==", "Hello, World!")]
        [DataRow("", "")]
        [DataRow(null, null)]
        public void Base64ToString_ShouldConvertBase64ToString(string base64EncodedData, string expectedPlainText)
        {
            // Act
            string result = Base64Helper.Base64ToString(base64EncodedData);

            // Assert
            Check.That(result).IsEqualTo(expectedPlainText);
        }
    }
}