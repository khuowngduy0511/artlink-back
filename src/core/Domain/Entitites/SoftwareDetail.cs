namespace Domain.Entitites;

public class SoftwareDetail
{
    public Guid SoftwareUsedId { get; set; }
    public Guid ArtworkId { get; set; }

    public virtual SoftwareUsed SoftwareUsed { get; set; } = default!;
    public virtual Artwork Artwork { get; set; } = default!;
}
