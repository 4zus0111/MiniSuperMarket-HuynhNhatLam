namespace MiniSupermarket.API.DTOs;

public class PagedResult<T>
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public List<T> Items { get; set; } = new();

    public static PagedResult<T> Create(List<T> items, int totalItems, int currentPage, int pageSize) => new()
    {
        CurrentPage = currentPage,
        PageSize = pageSize,
        TotalItems = totalItems,
        TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
        Items = items
    };
}
