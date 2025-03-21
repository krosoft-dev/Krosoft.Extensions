using Krosoft.Extensions.Cqrs.Models.Queries;

namespace Krosoft.Extensions.Samples.Library.Models.Queries;

public record HelloApiKeyDotNet9Query : AuthBaseQuery<string>
{
    public override bool IsUtilisateurRequired => false;
}