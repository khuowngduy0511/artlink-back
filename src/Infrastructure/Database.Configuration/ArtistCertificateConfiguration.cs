using Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class ArtistCertificateConfiguration : IEntityTypeConfiguration<ArtistCertificate>
{
    public void Configure(EntityTypeBuilder<ArtistCertificate> builder)
    {
        builder.ToTable(nameof(ArtistCertificate));

        builder.Property(x => x.Id).HasDefaultValueSql("newid()");

        // relationship
        builder.HasOne(x => x.Account).WithMany(a => a.ArtistCertificates).HasForeignKey(a => a.AccountId);
    }
}
