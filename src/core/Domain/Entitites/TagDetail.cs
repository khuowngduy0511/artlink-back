namespace Domain.Entitites;

public class TagDetail
{
    public Guid TagId { get; set; }
    public Guid ArtworkId { get; set; }

    public virtual Tag Tag { get; set; } = default!;
    public virtual Artwork Artwork { get; set; } = default!;
}
