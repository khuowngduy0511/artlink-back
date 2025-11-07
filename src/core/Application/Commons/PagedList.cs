using Domain.Entities.Commons;

namespace Application.Commons;

public class PagedList<T> : IPagedList<T>
{
    public int CurrentPage { get; private set; } // trang hien tai
    public int TotalPages { get; private set; } // tong so trang
    public int PageSize { get; private set; } // so luong item tren 1 trang
    public int TotalCount { get; private set; } // tong so item
    public bool HasPrevious => CurrentPage > 1; // co trang truoc
    public bool HasNext => CurrentPage < TotalPages; // co trang sau
    public List<T> Items { get; private set; } = default!;

    public PagedList() { }
    public PagedList(List<T> items, int totalCount, int pageNumber, int pageSize)
    {
        TotalCount = totalCount;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        Items = items;
    }
}