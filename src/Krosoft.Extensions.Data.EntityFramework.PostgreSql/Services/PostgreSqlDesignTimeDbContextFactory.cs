using Microsoft.EntityFrameworkCore;
using Krosoft.Extensions.Data.EntityFramework.Services;

namespace Krosoft.Extensions.Data.EntityFramework.PostgreSql.Services;

public abstract class PostgreSqlDesignTimeDbContextFactory<TDbContext> : DesignTimeDbContextFactory<TDbContext> where TDbContext : DbContext
{
    protected override void UseProvider(DbContextOptionsBuilder options, string connectionString)
    {
        options.UseNpgsql(connectionString, sqlOptions => sqlOptions.MigrationsAssembly(typeof(TDbContext).Assembly.FullName));
    }
}