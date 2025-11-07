namespace Domain.Entitites;

public class CategoryArtworkDetail
{
    public Guid CategoryId { get; set; }
    public Guid ArtworkId { get; set; }

    public virtual Category Category { get; set; } = default!;
    public virtual Artwork Artwork { get; set; } = default!;
}
