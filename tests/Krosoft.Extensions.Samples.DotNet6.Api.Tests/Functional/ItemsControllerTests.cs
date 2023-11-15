//using System.Net;
//using Positive.Extensions.Core.Extensions;
//using Positive.Extensions.Samples.Api.Models.Dto;
//using Positive.Extensions.Samples.Api.Tests.Core;

//namespace Positive.Extensions.Samples.Api.Tests.Functional;

//[TestClass]
//public class ItemsControllerTests : SampleBaseApiTest<Startup>
//{
//    [TestMethod]
//    public async Task Items_InvalidEan13()
//    {
//        var beneficiaryEan13 = "123";
//        var url = $"/Items?Code={beneficiaryEan13}";

//        var httpClient = Factory.CreateClient();
//        var response = await httpClient.GetAsync(url);

//        Check.That(response.StatusCode).IsEqualTo(HttpStatusCode.BadRequest);
//    }

//    [TestMethod]
//    public async Task Items_Empty()
//    {
//        var beneficiaryEan13 = "1234567890123";
//        var url = $"/Items?Code={beneficiaryEan13}";

//        var httpClient = Factory.CreateClient();
//        var response = await httpClient.GetAsync(url);

//        Check.That(response.StatusCode).IsEqualTo(HttpStatusCode.OK);

//        var beneficiaries = await response.Content.ReadAsJsonAsync<IEnumerable<ItemDto>>(CancellationToken.None);
//        Check.That(beneficiaries).IsEmpty();
//    }
//}