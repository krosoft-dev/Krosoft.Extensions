using Krosoft.Extensions.Data.EntityFramework.Interfaces;

namespace Krosoft.Extensions.Samples.DotNet8.Api.Data;

public class SampleKrosoftSeedService : ISeedService<SampleKrosoftContext>
{
    public void InitializeDbForTests(SampleKrosoftContext db)
    { 
    }

    public bool Initialized { get; set; }
}