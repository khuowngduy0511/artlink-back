namespace Domain.Entitites;
public class Like
{
    public Guid AccountId { get; set; }
    public virtual Account Account { get; set; } = default!;
    public Guid ArtworkId { get; set; }
    public virtual Artwork Artwork { get; set; } = default!;
}
