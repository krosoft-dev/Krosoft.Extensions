using Krosoft.Extensions.Data.EntityFramework.Contexts;
using Krosoft.Extensions.Data.EntityFramework.Identity.Extensions;
using Krosoft.Extensions.Data.EntityFramework.Identity.Scopes;
using Krosoft.Extensions.Samples.DotNet8.Api.Data;
using Krosoft.Extensions.Samples.Library.Models.Entities;
using Krosoft.Extensions.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;

namespace Krosoft.Extensions.Data.EntityFramework.Tests.Scopes;

[TestClass]
public class ReadDbContextScopeTests : BaseTest
{
    private static async Task Check<T>(ReadDbContextScope<T> contextScope) where T : KrosoftContext
    {
        var repository = contextScope.GetReadRepository<Logiciel>();

        var logiciels = await repository.Query()
                                        .ToListAsync(CancellationToken.None);

        NFluent.Check.That(logiciels).IsNotNull();
        NFluent.Check.That(logiciels).HasSize(5);
        NFluent.Check.That(logiciels.Select(x => x.Nom)).ContainsExactly("Logiciel1", "Logiciel2", "Logiciel3", "Logiciel4", "Logiciel5");
    }

    [TestMethod]
    public async Task ReadDbContextScope_NoTenantAudit_Ok()
    {
        using (var scope = CreateServiceCollection())
        {
            var dbContextSettings = new AuditableDbContextSettings(DateTime.Now,
                                                                   "UtilisateurId");
            using (var contextScope = new ReadDbContextScope<SampleKrosoftTenantContext>(scope.CreateScope(), dbContextSettings))
            {
                await Check(contextScope);
            }
        }
    }

    [TestMethod]
    public async Task ReadDbContextScope_NoTenantNoAudit_Ok()
    {
        using (var scope = CreateServiceCollection())
        {
            using (var contextScope = new ReadDbContextScope<SampleKrosoftTenantContext>(scope.CreateScope()))
            {
                await Check(contextScope);
            }
        }
    }

    [TestMethod]
    public async Task ReadDbContextScope_TenantAudit_Ok()
    {
        using (var scope = CreateServiceCollection())
        {
            var dbContextSettings = new TenantAuditableDbContextSettings("TenantId",
                                                                         DateTime.Now,
                                                                         "UtilisateurId");
            using (var contextScope = new ReadDbContextScope<SampleKrosoftTenantContext>(scope.CreateScope(), dbContextSettings))
            {
                await Check(contextScope);
            }
        }
    }

    [TestMethod]
    public async Task ReadDbContextScope_TenantNoAudit_Ok()
    {
        using (var scope = CreateServiceCollection())
        {
            var dbContextSettings = new TenantDbContextSettings("TenantId");
            using (var contextScope = new ReadDbContextScope<SampleKrosoftTenantContext>(scope.CreateScope(), dbContextSettings))
            {
                await Check(contextScope);
            }
        }
    }
}