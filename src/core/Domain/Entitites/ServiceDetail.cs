namespace Domain.Entitites;
public class ServiceDetail
{
    public Guid ServiceId { get; set; }
    public virtual Service Service { get; set; } = default!;
    public Guid ArtworkId { get; set; }
    public virtual Artwork Artwork { get; set; } = default!;
}
