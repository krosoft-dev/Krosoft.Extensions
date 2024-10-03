﻿namespace Krosoft.Extensions.Core.Models;

public interface IPaginationRequest
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? Text { get; set; }
    public IEnumerable<string> SortBy { get; set; }
}