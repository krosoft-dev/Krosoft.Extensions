using Krosoft.Extensions.Core.Models;

namespace Krosoft.Extensions.Cqrs.Models.Queries;

public abstract class AuthPaginationBaseQuery<T> : AuthBaseQuery<PaginationResult<T>>, IPaginationRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Text { get; set; }
    public IEnumerable<string> SortBy { get; set; } = new List<string>();
}