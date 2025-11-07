using Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;
public class ServiceDetailConfiguration : IEntityTypeConfiguration<ServiceDetail>
{
    public void Configure(EntityTypeBuilder<ServiceDetail> builder)
    {
        builder.ToTable(nameof(ServiceDetail));

        //composite key
        builder.HasKey(x => new { x.ServiceId, x.ArtworkId });

        //relationship
        builder.HasOne(x => x.Service).WithMany(a => a.ServiceDetails).HasForeignKey(x => x.ServiceId);
        builder.HasOne(x => x.Artwork).WithMany(a => a.ServiceDetails).HasForeignKey(x => x.ArtworkId);


        #region init data
        builder.HasData
        (

            // service 1
            new ServiceDetail()
            {
                ServiceId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000e"),
            },

            // service 2
            new ServiceDetail()
            {
                ServiceId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
            },

            // service 3
            new ServiceDetail()
            {
                ServiceId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000012"),
            },

            // service 4
            new ServiceDetail()
            {
                ServiceId = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000009"),
            },

            // service 5
            new ServiceDetail()
            {
                ServiceId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000020"),
            },

            // service 7
            new ServiceDetail()
            {
                ServiceId = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000034"),
            },
            new ServiceDetail()
            {
                ServiceId = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000035"),
            },
            new ServiceDetail()
            {
                ServiceId = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000033"),
            },

            // service 8
            new ServiceDetail()
            {
                ServiceId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003d"),
            },
            new ServiceDetail()
            {
                ServiceId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003e"),
            },
            new ServiceDetail()
            {
                ServiceId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003f"),
            }

        );
        #endregion
    }
}
