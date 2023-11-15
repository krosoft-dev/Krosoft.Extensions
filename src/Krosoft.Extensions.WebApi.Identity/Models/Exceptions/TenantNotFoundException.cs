using Krosoft.Extensions.Core.Models.Exceptions;

namespace Krosoft.Extensions.AspNetCore.Models.Exceptions
{
    public class TenantNotFoundException : KrosoftTechniqueException
    {
        public TenantNotFoundException() : base("Tenant introuvable.")
        {
        }
    }
}