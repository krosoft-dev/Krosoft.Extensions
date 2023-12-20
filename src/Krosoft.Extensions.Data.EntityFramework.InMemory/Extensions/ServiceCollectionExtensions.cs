using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Krosoft.Extensions.Data.EntityFramework.Extensions;
using Krosoft.Extensions.Data.EntityFramework.Helpers;

namespace Krosoft.Extensions.Data.EntityFramework.InMemory.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDbContextInMemory<TDbContext>(this IServiceCollection services,
                                                                          bool isUnitTest) where TDbContext : DbContext
        {
            string databaseName;
            if (isUnitTest)
            {
                databaseName = Guid.NewGuid().ToString();
            }
            else
            {
                databaseName = nameof(TDbContext);
            }

            services.AddScoped<DbContext, TDbContext>();
            services.AddDbContext<TDbContext>(options => options.UseLoggerFactory(LoggerFactoryHelper.MyLoggerFactory)
                                                                .UseInMemoryDatabase(databaseName)
                                                                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            //services.AddDbContextSettings();
            return services;
        }
    }
}