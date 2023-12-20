using Krosoft.Extensions.Core.Models;
using Krosoft.Extensions.Data.Abstractions.Models;
using Krosoft.Extensions.Samples.Library.Models.Enums;

namespace Krosoft.Extensions.Samples.Library.Models.Entities;

public class Logiciel : Entity
{
    public Guid Id { get; set; }
    public string? Nom { get; set; }
    public string? Description { get; set; }
    public Guid CategorieId { get; set; }

    required public Categorie Categorie { get; set; }
    public StatutCode StatutCode { get; set; }
    public DateTime DateCreation { get; set; }
}

public class Categorie : TenantAuditableEntity, IId
{
    public string? Libelle { get; set; }
    public StatutCode StatutCode { get; set; }
    required public IEnumerable<Logiciel> Logiciels { get; set; }
    public Guid Id { get; set; }
}