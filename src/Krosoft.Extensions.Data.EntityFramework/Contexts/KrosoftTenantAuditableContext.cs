using Krosoft.Extensions.Data.EntityFramework.Contexts;
using Krosoft.Extensions.Data.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Krosoft.Extensions.Data.EntityFramework.Identity.Contexts;

public abstract class KrosoftTenantAuditableContext : KrosoftContext
{
    protected KrosoftTenantAuditableContext(DbContextOptions options,
                                            ITenantDbContextProvider tenantDbContextProvider,
                                            IAuditableDbContextProvider auditableDbContextProvider) : base(options)
    {
    }
}