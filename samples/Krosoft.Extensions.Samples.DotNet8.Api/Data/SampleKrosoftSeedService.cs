using Krosoft.Extensions.Data.EntityFramework.Extensions;
using Krosoft.Extensions.Data.EntityFramework.Services;
using Krosoft.Extensions.Samples.Library.Models.Entities;

namespace Krosoft.Extensions.Samples.DotNet8.Api.Data;

public class SampleKrosoftSeedService : SeedService<SampleKrosoftContext>
{
    protected override void BeforeSave(SampleKrosoftContext db)
    {
        db.Import<Langue>();
        db.Import<Pays>();
    }
}