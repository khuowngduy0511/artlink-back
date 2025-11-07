namespace Application.Models;

public class CollectionDetailVM
{
    public Guid Id { get; set; }
    public string CollectionName { get; set; } = default!;
    public string Privacy { get; set; } = default!;
    public AccountDisplayModel CreatedBy { get; set; } = default!;
    public DateTime CreatedOn { get; set; }
    public ICollection<BookmarkVM> Artworks { get; set; } = new List<BookmarkVM>();
}

public class BookmarkVM
{
    public ArtworkDisplayModel Artwork { get; set; } = default!;
}

public class CollectionVM
{
    public Guid Id { get; set; }
    public string CollectionName { get; set; } = default!;
    public string Privacy { get; set; } = default!;
    public AccountDisplayModel CreatedBy { get; set; } = default!;
    public DateTime CreatedOn { get; set; }
    public int Items { get; set; }
    public string Thumbnail { get; set; } = default!;
}
