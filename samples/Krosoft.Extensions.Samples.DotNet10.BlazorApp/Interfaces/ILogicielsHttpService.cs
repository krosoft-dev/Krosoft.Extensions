using Krosoft.Extensions.Core.Models;
using Krosoft.Extensions.Samples.DotNet10.BlazorApp.Models;

namespace Krosoft.Extensions.Samples.DotNet10.BlazorApp.Interfaces;

public interface ILogicielsHttpService
{
    Task<Result<IEnumerable<Logiciel>?>> GetLogicielsAsync(string text,
                                                           CancellationToken cancellationToken);

    Task<Result<Guid>> CreateAsync(Logiciel logiciel, CancellationToken cancellationToken);
}