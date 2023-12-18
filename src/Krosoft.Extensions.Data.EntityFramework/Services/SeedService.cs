using Microsoft.EntityFrameworkCore;
using Krosoft.Extensions.Core.Helpers;
using Krosoft.Extensions.Data.EntityFramework.Interfaces;

namespace Krosoft.Extensions.Data.EntityFramework.Services
{
    public abstract class SeedService<TDbContext> : ISeedService<TDbContext> where TDbContext : DbContext
    {
        public bool Initialized { get; set; }

        public void InitializeDbForTests(TDbContext db)
        {
            Initialized = true;

            BeforeSave(db);

            db.SaveChanges();
        }

        protected abstract void BeforeSave(TDbContext db);

        protected void Import<T>(DbContext db) where T : class
        {
            var entities = JsonHelper.Get<T>(typeof(T).Assembly);
            db.Set<T>().AddRange(entities);
        }
    }
}