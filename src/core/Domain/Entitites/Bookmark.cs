namespace Domain.Entitites;
public class Bookmark
{
    public Guid ArtworkId { get; set; }
    public Guid CollectionId { get; set; }

    public virtual Artwork Artwork { get; set; } = default!;
    public virtual Collection Collection { get; set; } = default!;
}
