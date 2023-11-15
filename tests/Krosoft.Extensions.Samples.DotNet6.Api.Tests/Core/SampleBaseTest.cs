using Krosoft.Extensions.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

//using Moq;
//using Positive.Extensions.Application.Extensions;
//using Positive.Extensions.Data.EntityFramework.Extensions;
//using Positive.Extensions.Data.EntityFramework.InMemory.Extensions;
//using Positive.Extensions.Identity.Abstractions.Interfaces;
//using Positive.Extensions.Infrastructure.Extensions;
//using Positive.Extensions.Samples.Api.Data;
//using Positive.Extensions.Testing;
//using Positive.Extensions.Testing.Extensions;

namespace Krosoft.Extensions.Samples.DotNet6.Api.Tests.Core;

public abstract class SampleBaseTest<TEntry> : BaseTest
{
    protected override void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        //var executingAssembly = typeof(TEntry).Assembly;
        //services.AddApplication(executingAssembly)
        //        .AddInfrastructure(configuration)
        //    ;

        ////Data.
        //services.AddRepositories();
        //services.AddDbContextInMemory<PositiveExtensionTenantContext>(true);
        //services.AddDbContextSettings();

        ////Mock
        //var mockUserProvider = new Mock<IIdentityService>();
        //mockUserProvider.Setup(userProvider => userProvider.GetId()).Returns(new Guid().ToString());
        //services.SwapTransient(_ => mockUserProvider.Object);

        base.AddServices(services, configuration);
    }
}