﻿using System.Reflection;
using Krosoft.Extensions.Data.Abstractions.Models;
using Krosoft.Extensions.Data.EntityFramework.Extensions;
using Krosoft.Extensions.Data.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Krosoft.Extensions.Data.EntityFramework.Contexts;

public abstract class KrosoftTenantAuditableContext : KrosoftContext
{
    /// <summary>
    /// Applying BaseEntity rules to all entities that inherit from it.
    /// Define MethodInfo member that is used when model is built.
    /// </summary>
    private static readonly MethodInfo ConfigureAuditableMethod = typeof(KrosoftTenantAuditableContext)
                                                                  .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                                                  .Single(t => t.IsGenericMethod && t.Name == nameof(ConfigureAuditable));

    /// <summary>
    /// Applying BaseEntity rules to all entities that inherit from it.
    /// Define MethodInfo member that is used when model is built.
    /// </summary>
    private static readonly MethodInfo ConfigureTenantMethod = typeof(KrosoftTenantAuditableContext)
                                                               .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                                               .Single(t => t.IsGenericMethod && t.Name == nameof(ConfigureTenant));

    private readonly IAuditableDbContextProvider _auditableDbContextProvider;

    private readonly ITenantDbContextProvider _tenantDbContextProvider;

    protected KrosoftTenantAuditableContext(DbContextOptions options,
                                            ITenantDbContextProvider tenantDbContextProvider,
                                            IAuditableDbContextProvider auditableDbContextProvider) : base(options)
    {
        _auditableDbContextProvider = auditableDbContextProvider;
        _tenantDbContextProvider = tenantDbContextProvider;
    }

    /// <summary>
    /// This method is called for every loaded entity type in OnModelCreating method.
    /// Here type is known through generic parameter and we can use EF Core methods.
    /// </summary>
    public void ConfigureAuditable<T>(ModelBuilder builder) where T : class, IAuditable
    {
        builder.Entity<T>()
               .Property(t => t.ModificateurId)
               .IsRequired();
        builder.Entity<T>()
               .Property(t => t.ModificateurDate)
               .IsRequired();
        builder.Entity<T>()
               .Property(t => t.CreateurId)
               .IsRequired();
        builder.Entity<T>()
               .Property(t => t.CreateurDate)
               .IsRequired();
    }

    /// <summary>
    /// This method is called for every loaded entity type in OnModelCreating method.
    /// Here type is known through generic parameter and we can use EF Core methods.
    /// </summary>
    public void ConfigureTenant<T>(ModelBuilder builder) where T : class, ITenant
    {
        builder.Entity<T>()
               .HasIndex(p => p.TenantId);

        builder.Entity<T>()
               .Property(t => t.TenantId)
               .IsRequired();

        builder.Entity<T>().HasQueryFilter(e => e.TenantId == _tenantDbContextProvider.GetTenantId());
    }

    protected override IEnumerable<Type> GetTypes() => [typeof(ITenant), typeof(IAuditable)];

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Set BaseEntity rules to all loaded entity types
        foreach (var type in GetEntityTypes())
        {
            //Console.WriteLine(type.FullName); //Debug.

            if (type.GetInterfaces().Contains(typeof(IAuditable)))
            {
                var method = ConfigureAuditableMethod.MakeGenericMethod(type);
                method.Invoke(this, new object[] { modelBuilder });
            }

            if (type.GetInterfaces().Contains(typeof(ITenant)))
            {
                var method = ConfigureTenantMethod.MakeGenericMethod(type);
                method.Invoke(this, new object[] { modelBuilder });
            }
        }
    }

    protected override void OverrideEntities()
    {
        var useAudit = ChangeTracker.Entries<IAuditable>().Any();
        if (useAudit)
        {
            ChangeTracker.DetectChanges();

            var now = _auditableDbContextProvider.GetNow();
            var utilisateurId = _auditableDbContextProvider.GetUtilisateurId();

            ChangeTracker.ProcessModificationAuditable(now, utilisateurId);
            ChangeTracker.ProcessCreationAuditable(now, utilisateurId);
        }

        var useTenant = ChangeTracker.Entries<ITenant>().Any();
        if (useTenant)
        {
            ChangeTracker.DetectChanges();

            var tenantId = _tenantDbContextProvider.GetTenantId();
            ChangeTracker.ProcessCreationTenant(tenantId);
        }
    }
}