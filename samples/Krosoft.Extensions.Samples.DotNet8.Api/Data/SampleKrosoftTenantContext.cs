using Krosoft.Extensions.Data.EntityFramework.Identity.Contexts;
using Krosoft.Extensions.Data.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Krosoft.Extensions.Samples.DotNet8.Api.Data;

public class SampleKrosoftTenantContext : KrosoftTenantContext
{
    public SampleKrosoftTenantContext(DbContextOptions options,
                                      IDbContextSettingsProvider dbContextSettingsProvider)
        : base(options, dbContextSettingsProvider)
    {
    }
}