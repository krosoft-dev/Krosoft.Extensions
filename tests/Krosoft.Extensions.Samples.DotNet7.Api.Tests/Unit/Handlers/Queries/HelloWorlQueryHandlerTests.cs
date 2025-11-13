using Krosoft.Extensions.Samples.DotNet7.Api.Tests.Core;
using Krosoft.Extensions.Samples.Library.Models.Queries;
using Krosoft.Extensions.Testing.Cqrs.Extensions;

namespace Krosoft.Extensions.Samples.DotNet7.Api.Tests.Unit.Handlers.Queries;

[TestClass]
public class HelloWorlQueryHandlerTests : SampleBaseTest<Startup>
{
    [TestMethod]
    public async Task HandleEmptyTest()
    {
        var serviceProvider = CreateServiceCollection();

        var result = await this.SendQueryAsync(serviceProvider, new HelloDotNet7Query());
        Check.That(result).IsNotNull();
        Check.That(result).IsEqualTo("Hello DotNet7");
    }
}