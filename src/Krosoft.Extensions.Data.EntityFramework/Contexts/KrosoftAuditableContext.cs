using System.Reflection;
using Krosoft.Extensions.Data.Abstractions.Models;
using Krosoft.Extensions.Data.EntityFramework.Extensions;
using Krosoft.Extensions.Data.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;

namespace Krosoft.Extensions.Data.EntityFramework.Contexts;

public abstract class KrosoftAuditableContext : KrosoftContext
{
    private static readonly IList<Type> Types = new List<Type>
    {
        typeof(IAuditable)
    };

    /// <summary>
    /// Find loaded entity types from assemblies that application uses.
    /// </summary>
    private static IList<Type>? _entityTypeCache;

    private static readonly MethodInfo ConfigureAuditableMethod = typeof(KrosoftAuditableContext)
                                                                  .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                                                  .Single(t => t.IsGenericMethod && t.Name == nameof(ConfigureAuditable));

    private readonly IAuditableDbContextProvider _auditableDbContextProvider;

    protected KrosoftAuditableContext(DbContextOptions options,
                                      IAuditableDbContextProvider auditableDbContextProvider) : base(options)
    {
        _auditableDbContextProvider = auditableDbContextProvider;
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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Set BaseEntity rules to all loaded entity types
        foreach (var type in GetEntityTypes())
        {
            //Console.WriteLine(type.FullName); //Debug.

            if (type.GetInterfaces().Contains(typeof(IAuditable)))
            {
                var method = ConfigureAuditableMethod.MakeGenericMethod(type);
                method.Invoke(this, new object[] { builder });
            }
        }
    }

    private void OverrideEntities()
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
    }

    public override int SaveChanges()
    {
        OverrideEntities();

        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OverrideEntities();

        return await base.SaveChangesAsync(true, cancellationToken);
    }
}