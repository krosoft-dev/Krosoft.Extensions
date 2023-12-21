﻿using JetBrains.Annotations;
using Krosoft.Extensions.Core.Models;
using Krosoft.Extensions.Data.Abstractions.Models;
using Krosoft.Extensions.Samples.Library.Models.Enums;

namespace Krosoft.Extensions.Samples.Library.Models.Entities;

[NoReorder]
public class Categorie : AuditableEntity, IId
{
    public Guid Id { get; set; }
    public string? Libelle { get; set; }
    public StatutCode StatutCode { get; set; }
    public IEnumerable<Logiciel> Logiciels { get; set; } = null!;
}

[NoReorder]
public class Langue : Entity, IId
{
    public Guid Id { get; set; }
    public string? Code { get; set; }
    public string? Libelle { get; set; }
    public bool IsDefaut { get; set; }
    public bool IsActif { get; set; }
}