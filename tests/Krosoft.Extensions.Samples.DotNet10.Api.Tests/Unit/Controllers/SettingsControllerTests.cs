using Krosoft.Extensions.Samples.DotNet10.Api.Controllers;
using Krosoft.Extensions.Samples.Library.Models.Dto;
using Krosoft.Extensions.Samples.Library.Models.Queries;
using Krosoft.Extensions.Testing.WebApi;

namespace Krosoft.Extensions.Samples.DotNet10.Api.Tests.Unit.Controllers;

[TestClass]
public class SettingsControllerTests : ControllerBaseTest<SettingsController>
{
    [TestMethod]
    public async Task CorsPolicyAsync_Ok()
    {
        var expectedOutput = new CorsPolicyDto
        {
            Origins = ["http://localhost"]
        };

        Mock.Setup(x => x.Send(It.IsAny<CorsPolicyQuery>(), CancellationToken.None))
            .ReturnsAsync(() => expectedOutput);

        var result = await Controller.CorsPolicyAsync(CancellationToken.None);

        Mock.Verify(x => x.Send(It.IsAny<CorsPolicyQuery>(), CancellationToken.None), Times.Once());

        Check.That(result).IsNotNull();
        Check.That(result.Origins).ContainsExactly("http://localhost");
    }
}