using Krosoft.Extensions.Cqrs.Models.Commands;

namespace Krosoft.Extensions.Samples.Library.Models.Commands;

public record HelloDotNet10Command(string Name) : BaseCommand<string>;