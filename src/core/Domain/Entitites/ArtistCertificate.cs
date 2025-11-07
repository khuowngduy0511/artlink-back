using Domain.Entities.Commons;

namespace Domain.Entitites;

public class ArtistCertificate : BaseEntity
{
    public Guid AccountId { get; set; }
    public string Certificatename { get; set; } = default!;
    public string CertificateUrl { get; set; } = default!;

    public virtual Account Account { get; set; } = default!;
}
