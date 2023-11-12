using System.Globalization;
using Krosoft.Extensions.Core.Helpers;
using Krosoft.Extensions.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Krosoft.Extensions.Core.Tests.Helpers;

[TestClass]
public class DateTimeHelperTests : BaseTest
{
    [TestMethod]
    public void TimestampToDateTimeTest()
    {
        const long dateInTimestamp = 1372061224000;
        var dateInDateTime = DateTimeHelper.TimestampToDateTime(dateInTimestamp);
        Assert.AreEqual("24/06/2013 10:07:04", dateInDateTime.ToString(CultureInfo.InvariantCulture));
    }
}