using Krosoft.Extensions.Data.EntityFramework.Extensions;
using Krosoft.Extensions.Data.EntityFramework.InMemory.Extensions;
using Krosoft.Extensions.Data.EntityFramework.Interfaces;
using Krosoft.Extensions.Data.EntityFramework.Services;
using Krosoft.Extensions.Samples.DotNet8.Api.Data;
using Krosoft.Extensions.Samples.DotNet8.Api.Handlers.Commands;
using Krosoft.Extensions.Samples.DotNet8.Api.Tests.Core;
using Krosoft.Extensions.Samples.Library.Models.Commands;
using Krosoft.Extensions.Samples.Library.Models.Messages;
using Krosoft.Extensions.Testing.Cqrs.Extensions;
using Krosoft.Extensions.Testing.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;

namespace Krosoft.Extensions.Samples.DotNet8.Api.Tests.Unit.Handlers.Commands;

[TestClass]
public class UpdateStatLogicielCommandHandlerTests : SampleBaseTest<Startup>
{
    private Mock<ILogger<UpdateStatLogicielCommandHandler>>? _mockLogger;

    protected override void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepositories();
        services.AddScoped<ITenantDbContextProvider, FakeTenantDbContextProvider>();
        services.AddScoped<IAuditableDbContextProvider, FakeAuditableDbContextProvider>();
        services.AddDbContextInMemory<SampleKrosoftTenantAuditableContext>(true);
        services.AddSeedService<SampleKrosoftTenantAuditableContext, SampleSeedService<SampleKrosoftTenantAuditableContext>>();
        base.AddServices(services, configuration);
    }

    private void GetServices(IServiceCollection services)
    {
        _mockLogger = new Mock<ILogger<UpdateStatLogicielCommandHandler>>();
        services.SwapTransient(_ => _mockLogger.Object);
    }

    [TestMethod]
    public async Task Handle_Empty()
    {
        var serviceProvider = CreateServiceCollection(GetServices);

        var command = new UpdateStatLogicielCommand(string.Empty);
        await this.SendCommandAsync(serviceProvider, command);

        _mockLogger!.VerifyWasCalled(LogLevel.Information, "Mise à jour des statistiques...", Times.Once());
        _mockLogger!.VerifyWasCalled(LogLevel.Information, "Mise à jour des statistiques pour le tenant", Times.Never());
        _mockLogger!.VerifyWasCalled(LogLevel.Error, "Impossible de mettre à jour les statitisques à partir du payload : ", Times.Once());
    }

    [TestMethod]
    public async Task Handle_Null()
    {
        var serviceProvider = CreateServiceCollection(GetServices);

        var command = new UpdateStatLogicielCommand(null!);
        await this.SendCommandAsync(serviceProvider, command);

        _mockLogger!.VerifyWasCalled(LogLevel.Information, "Mise à jour des statistiques...", Times.Once());
        _mockLogger!.VerifyWasCalled(LogLevel.Information, "Mise à jour des statistiques pour le tenant", Times.Never());
        _mockLogger!.VerifyWasCalled(LogLevel.Error, "Impossible de mettre à jour les statitisques à partir du payload : ", Times.Once());
    }

    [TestMethod]
    public async Task Handle_Ok()
    {
        var serviceProvider = CreateServiceCollection(GetServices);

        var message = new UpdateStatLogicielMessage
        {
            TenantId = "my_tenant",
            UtilisateurId = "utilisateur_1"
        };

        var payload = JsonConvert.SerializeObject(message);
        var command = new UpdateStatLogicielCommand(payload);
        await this.SendCommandAsync(serviceProvider, command);

        _mockLogger!.VerifyWasCalled(LogLevel.Information, "Mise à jour des statistiques...", Times.Once());
        _mockLogger!.VerifyWasCalled(LogLevel.Information, $"Mise à jour des statistiques pour le tenant {message.TenantId}", Times.Once());
        _mockLogger!.VerifyWasCalled(LogLevel.Error, "Impossible de mettre à jour les statitisques à partir du payload : ", Times.Never());
    }
}