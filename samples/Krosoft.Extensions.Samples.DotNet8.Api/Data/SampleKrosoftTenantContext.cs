using Krosoft.Extensions.Data.EntityFramework.Audits.Contexts;
using Krosoft.Extensions.Data.EntityFramework.Contexts;
using Krosoft.Extensions.Data.EntityFramework.Interfaces;
using Krosoft.Extensions.Data.EntityFramework.Tenants.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Krosoft.Extensions.Samples.DotNet8.Api.Data;

public class SampleKrosoftTenantContext : KrosoftTenantContext
{
    public SampleKrosoftTenantContext(DbContextOptions options,
                                      IDbContextSettingsProvider dbContextSettingsProvider)
        : base(options, dbContextSettingsProvider)
    {
    }
}public class SampleKrosoftAuditContext : KrosoftAuditContext
{
    public SampleKrosoftAuditContext(DbContextOptions options,
                                     IDbContextSettingsProvider dbContextSettingsProvider)
        : base(options, dbContextSettingsProvider)
    {
    }
}public class SampleKrosoftContext : KrosoftContext
{
    public SampleKrosoftContext(DbContextOptions options
                                   )
        : base(options)
    {
    }
}