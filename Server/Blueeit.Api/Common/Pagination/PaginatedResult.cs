namespace Blueeit.Api.Common.Pagination;

public class PaginatedResult<T>
{
    public required IReadOnlyList<T> Items { get; init; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
