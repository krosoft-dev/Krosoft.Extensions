using System.Net;
using Krosoft.Extensions.Samples.DotNet6.Api.Tests.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace Krosoft.Extensions.Samples.DotNet6.Api.Tests.Functional;

[TestClass]
public class LogicielsControllerTests : SampleBaseApiTest<Startup>
{
    [TestMethod]
    public async Task Logiciels_Ok()
    {
        var nom = "Excel";
        var url = $"/Logiciels?Nom={nom}";

        var httpClient = Factory.CreateClient();
        var response = await httpClient.GetAsync(url);

        Check.That(response.StatusCode).IsEqualTo(HttpStatusCode.InternalServerError);
    }

    //[TestMethod]
    //public async Task Logiciels_Empty()
    //{
    //    var beneficiaryEan13 = "1234567890123";
    //    var url = $"/Logiciels?Code={beneficiaryEan13}";

    //    var httpClient = Factory.CreateClient();
    //    var response = await httpClient.GetAsync(url);

    //    Check.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);

    //    var beneficiaries = await response.Content.ReadAsJsonAsync<IEnumerable<LogicielDto>>(CancellationToken.None);
    //    Check.That(beneficiaries).IsEmpty();
    //}
}