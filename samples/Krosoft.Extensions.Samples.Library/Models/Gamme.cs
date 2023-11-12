﻿using System;

namespace Krosoft.Extensions.Samples.Library.Models;

public class Item
{
    public Guid Id { get; set; }
    public string? Code { get; set; }
    public string? Libelle { get; set; }
    public string? Description { get; set; }
    public bool IsActif { get; set; }
    public DateTime Date { get; set; }
}