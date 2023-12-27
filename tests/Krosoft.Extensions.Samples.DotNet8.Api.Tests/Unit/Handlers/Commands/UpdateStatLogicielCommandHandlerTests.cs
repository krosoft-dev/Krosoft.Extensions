using Krosoft.Extensions.Samples.DotNet8.Api.Tests.Core;
using Krosoft.Extensions.Samples.Library.Models.Commands;
using Krosoft.Extensions.Testing.Cqrs.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Krosoft.Extensions.Samples.DotNet8.Api.Tests.Unit.Handlers.Commands;

[TestClass]
public class UpdateStatLogicielCommandHandlerTests : SampleBaseTest<Startup>
{
    [TestMethod]
    public async Task Handle_Ok()
    {
        var serviceProvider = CreateServiceCollection();

        var command = new UpdateStatLogicielCommand(string.Empty);
        await this.SendCommandAsync(serviceProvider, command);

        //Check.That(result).IsNotNull();
        //Check.That(result).IsEqualTo("Hello DotNet8");
    }
}