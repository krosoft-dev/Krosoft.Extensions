﻿using Krosoft.Extensions.Data.EntityFramework.Identity.Contexts;
using Krosoft.Extensions.Data.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Krosoft.Extensions.Samples.DotNet8.Api.Data;

public class SampleKrosoftTenantAuditableContext : KrosoftTenantAuditableContext
{
    public SampleKrosoftTenantAuditableContext(DbContextOptions options,
                                               ITenantDbContextProvider tenantDbContextProvider,
                                               IAuditableDbContextProvider auditableDbContextProvider)
        : base(options,
               tenantDbContextProvider,
               auditableDbContextProvider)
    {
    }
}