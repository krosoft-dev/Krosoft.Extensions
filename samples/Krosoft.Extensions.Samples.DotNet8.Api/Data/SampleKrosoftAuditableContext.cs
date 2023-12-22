﻿using Krosoft.Extensions.Data.EntityFramework.Audits.Contexts;
using Krosoft.Extensions.Data.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Krosoft.Extensions.Samples.DotNet8.Api.Data;

public class SampleKrosoftAuditableContext : KrosoftAuditableContext
{
    public SampleKrosoftAuditableContext(DbContextOptions options,
                                     IAuditableDbContextProvider auditableDbContextProvider)
        : base(options, auditableDbContextProvider)
    {
    }
}