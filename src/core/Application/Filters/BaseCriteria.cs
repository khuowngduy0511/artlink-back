using System.ComponentModel.DataAnnotations;

namespace Application.Filters;

public class PagedCriteria
{
    const int maxPageSize = 50;
    [Range(1, double.PositiveInfinity, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public int PageNumber { get; set; } = 1;
    private int _pageSize = 10;
    [Range(1, 50, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > maxPageSize ? maxPageSize : value;
    }
}
public class BaseCriteria : PagedCriteria
{
    public string? Keyword { set; get; }
    public string? SortColumn { set; get; }
    public string? SortOrder { set; get; }
}