using Krosoft.Extensions.Samples.DotNet7.Api.Controllers;
using Krosoft.Extensions.Samples.Library.Models.Queries;
using Krosoft.Extensions.Testing.WebApi;

namespace Krosoft.Extensions.Samples.DotNet7.Api.Tests.Unit.Controllers;

[TestClass]
public class HelloControllerTests : ControllerBaseTest<HelloController>
{
    [TestMethod]
    public async Task HelloWorldAsync_Ok()
    {
        var expectedOutput = "Hello-World";

        Mock.Setup(x => x.Send(It.IsAny<HelloDotNet7Query>(), CancellationToken.None))
            .ReturnsAsync(() => expectedOutput);

        var result = await Controller.HelloWorldAsync(CancellationToken.None);

        Mock.Verify(x => x.Send(It.IsAny<HelloDotNet7Query>(), CancellationToken.None), Times.Once());

        Check.That(result).IsNotNull();
        Check.That(result).IsEqualTo(expectedOutput);
    }
}