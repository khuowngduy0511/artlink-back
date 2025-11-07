namespace Domain.Entities.Commons;

public interface IPagedList<T>
{
    public int CurrentPage { get; } // trang hien tai
    public int TotalPages { get; } // tong so trang
    public int PageSize { get; } // so luong item tren 1 trang
    public int TotalCount { get; } // tong so item
    public bool HasPrevious { get; } // co trang truoc
    public bool HasNext { get; } // co trang sau
    public List<T> Items { get; }
}
