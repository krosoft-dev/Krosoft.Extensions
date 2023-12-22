using Krosoft.Extensions.Data.EntityFramework.Audits.Contexts;
using Krosoft.Extensions.Data.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Krosoft.Extensions.Samples.DotNet8.Api.Data;

public class SampleKrosoftAuditContext : KrosoftAuditContext
{
    public SampleKrosoftAuditContext(DbContextOptions options,
                                     IAuditableDbContextProvider auditableDbContextProvider)
        : base(options, auditableDbContextProvider)
    {
    }
}