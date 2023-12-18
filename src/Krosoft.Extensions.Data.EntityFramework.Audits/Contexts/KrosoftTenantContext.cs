using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;
using Krosoft.Extensions.Data.Abstractions.Models;
using Krosoft.Extensions.Data.EntityFramework.Extensions;
using Krosoft.Extensions.Data.EntityFramework.Interfaces;

namespace Krosoft.Extensions.Data.EntityFramework.Contexts;

public abstract class KrosoftAuditContext : KrosoftContext
{
    private static readonly IList<Type> Types = new List<Type>
    {
        typeof(ITenantId),
        typeof(IAuditable)
    };

    /// <summary>
    /// Find loaded entity types from assemblies that application uses.
    /// </summary>
    private static IList<Type> _entityTypeCache;

    /// <summary>
    /// Applying BaseEntity rules to all entities that inherit from it.
    /// Define MethodInfo member that is used when model is built.
    /// </summary>
    private static readonly MethodInfo ConfigureTenantMethod = typeof(KrosoftAuditContext)
                                                               .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                                               .Single(t => t.IsGenericMethod && t.Name == nameof(ConfigureTenant));

    private static readonly MethodInfo ConfigureAuditableMethod = typeof(KrosoftAuditContext)
                                                                  .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                                                  .Single(t => t.IsGenericMethod && t.Name == nameof(ConfigureAuditable));

    private readonly IDbContextSettingsProvider _dbContextSettingsProvider;

    protected KrosoftTenantContext(DbContextOptions options,
                                    IDbContextSettingsProvider dbContextSettingsProvider) : base(options)
    {
        _dbContextSettingsProvider = dbContextSettingsProvider;
    }

    public override int SaveChanges()
    {
        OverrideEntities();

        return base.SaveChanges();
    }

    private void OverrideEntities()
    {
        var useAudit = ChangeTracker.Entries<IAuditable>().Any();
        var useTenant = ChangeTracker.Entries<ITenantId>().Any();
        if (useAudit || useTenant)
        {
            ChangeTracker.DetectChanges();

            if (useTenant)
            {
                var tenantId = _dbContextSettingsProvider.GetTenantId();
                ChangeTracker.ProcessCreationTenant(tenantId);
            }

            if (useAudit)
            {
                var now = _dbContextSettingsProvider.GetNow();
                var utilisateurId = _dbContextSettingsProvider.GetUtilisateurId();

                ChangeTracker.ProcessModificationAuditable(now, utilisateurId);
                ChangeTracker.ProcessCreationAuditable(now, utilisateurId);
            }
        }
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OverrideEntities();

        return await base.SaveChangesAsync(true, cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Set BaseEntity rules to all loaded entity types
        foreach (var type in GetEntityTypes())
        {
            //Console.WriteLine(type.FullName); //Debug.

            if (type.GetInterfaces().Contains(typeof(ITenantId)))
            {
                var method = ConfigureTenantMethod.MakeGenericMethod(type);
                method.Invoke(this, new object[] { modelBuilder });
            }

            if (type.GetInterfaces().Contains(typeof(IAuditable)))
            {
                var method = ConfigureAuditableMethod.MakeGenericMethod(type);
                method.Invoke(this, new object[] { modelBuilder });
            }
        }
    }

    private static IEnumerable<Type> GetEntityTypes()
    {
        if (_entityTypeCache != null)
        {
            return _entityTypeCache.ToList();
        }

        _entityTypeCache = (from a in GetReferencingAssemblies()
                            from t in a.DefinedTypes
                            where !t.IsAbstract && t.GetInterfaces().Any(Types.Contains)
                            select t.AsType())
                           .DistinctBy(x => x.FullName)
                           .ToList();

        return _entityTypeCache;
    }

    private static IEnumerable<Assembly> GetReferencingAssemblies()
    {
        var assemblies = new List<Assembly>();
        if (DependencyContext.Default != null)
        {
            var dependencies = DependencyContext.Default.RuntimeLibraries;

            foreach (var library in dependencies)
            {
                try
                {
                    var assembly = Assembly.Load(new AssemblyName(library.Name));
                    assemblies.Add(assembly);
                }
                catch (FileNotFoundException)
                {
                }
            }
        }

        return assemblies;
    }

    /// <summary>
    /// This method is called for every loaded entity type in OnModelCreating method.
    /// Here type is known through generic parameter and we can use EF Core methods.
    /// </summary>
    public void ConfigureTenant<T>(ModelBuilder builder) where T : class, ITenantId
    {
        builder.Entity<T>()
               .HasIndex(p => p.TenantId);

        builder.Entity<T>()
               .Property(t => t.TenantId)
               .IsRequired();

        builder.Entity<T>().HasQueryFilter(e => e.TenantId == _dbContextSettingsProvider.GetTenantId());
    }

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
}