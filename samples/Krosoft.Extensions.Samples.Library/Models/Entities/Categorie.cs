using Krosoft.Extensions.Core.Models;
using Krosoft.Extensions.Data.Abstractions.Models;
using Krosoft.Extensions.Samples.Library.Models.Enums;

namespace Krosoft.Extensions.Samples.Library.Models.Entities;

public class Categorie : TenantAuditableEntity, IId
{
    public string? Libelle { get; set; }
    public StatutCode StatutCode { get; set; }
    public IEnumerable<Logiciel> Logiciels { get; set; } = null!;
    public Guid Id { get; set; }
}