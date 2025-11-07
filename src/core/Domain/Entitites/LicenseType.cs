using Domain.Entities.Commons;

namespace Domain.Entitites;

public class LicenseType : BaseEntity
{
    public string LicenseName { get; set; } = default!;
    public string? LicenseDescription { get; set; }
    public virtual ICollection<Artwork> Artworks { get; set; } = new List<Artwork>();
}
