using Microsoft.EntityFrameworkCore;

namespace Krosoft.Extensions.Data.EntityFramework.Interfaces
{
    public interface ISeedService<TDbContext> where TDbContext : DbContext
    {
        void InitializeDbForTests(TDbContext db);
        bool Initialized { get; set; }
    }
}