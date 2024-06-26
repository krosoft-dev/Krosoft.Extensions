using System.Net;
using Krosoft.Extensions.Samples.DotNet6.Api.Tests.Core;

namespace Krosoft.Extensions.Samples.DotNet6.Api.Tests.Functional;

[TestClass]
public class LogicielsControllerTests : SampleBaseApiTest<Startup>
{
    [TestMethod]
    public async Task Csv_Ok()
    {
        var httpClient = Factory.CreateClient();
        var response = await httpClient.GetAsync("/Logiciels/Export/Csv");

        Check.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
    }

    [TestMethod]
    public async Task Logiciels_Ok()
    {
        var httpClient = Factory.CreateClient();
        var response = await httpClient.GetAsync("/Logiciels");

        Check.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
    }

    [TestMethod]
    public async Task Logiciels_Query_Ok()
    {
        const string nom = "Excel";

        var httpClient = Factory.CreateClient();
        var response = await httpClient.GetAsync($"/Logiciels?Nom={nom}");

        Check.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
    }

    [TestMethod]
    public async Task Pdf_Ok()
    {
        var httpClient = Factory.CreateClient();
        var response = await httpClient.GetAsync("/Logiciels/Export/Pdf");

        Check.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
    }

    [TestMethod]
    public async Task Zip_Ok()
    {
        var httpClient = Factory.CreateClient();
        var response = await httpClient.GetAsync("/Logiciels/Export/Zip");

        Check.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
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