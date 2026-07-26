using System.Net;
using Krosoft.Extensions.Core.Extensions;
using Krosoft.Extensions.Samples.DotNet10.Api.Tests.Core;
using Krosoft.Extensions.Samples.Library.Models.Dto;

namespace Krosoft.Extensions.Samples.DotNet10.Api.Tests.Functional;

[TestClass]
public class SettingsControllerTests : SampleBaseApiTest<Program>
{
    [TestMethod]
    public async Task CorsPolicy_Ok()
    {
        var url = "/Settings/CorsPolicy";

        var httpClient = Factory.CreateClient();
        var response = await httpClient.GetAsync(url);

        Check.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);
        var corsPolicyDto = await response.Content.ReadAsNewtonsoftJsonAsync<CorsPolicyDto>(CancellationToken.None);
        Check.That(corsPolicyDto).IsNotNull();
        Check.That(corsPolicyDto!.Origins).IsNotNull();
    }
}