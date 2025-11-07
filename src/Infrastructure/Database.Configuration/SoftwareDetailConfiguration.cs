using Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class SoftwareDetailConfiguration : IEntityTypeConfiguration<SoftwareDetail>
{
    public void Configure(EntityTypeBuilder<SoftwareDetail> builder)
    {
        builder.ToTable(nameof(SoftwareDetail));

        builder.HasKey(x => new { x.SoftwareUsedId, x.ArtworkId });

        builder.HasOne(x => x.SoftwareUsed)
            .WithMany(x => x.SoftwareDetails)
            .HasForeignKey(x => x.SoftwareUsedId);

        builder.HasOne(x => x.Artwork)
            .WithMany(x => x.SoftwareDetails)
            .HasForeignKey(x => x.ArtworkId);

        #region init data
        builder.HasData
        (
            // artwork 1
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-00000000003a"),
            },

            // artwork 2
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000039"),
            },

            // artwork 3
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
            },

            // artwork 5
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-00000000003a"),
            },

            // artwork 6
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000039"),
            },

            // artwork 7
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000039"),
            },

            // artwork 8
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            },

            // artwork 9
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000039"),
            },

            // artwork 10
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000a"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000039"),
            },

            // artwork 11
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000b"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000039"),
            },

            // artwork 12
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000c"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000039"),
            },

            // artwork 13
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000c"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-00000000003a"),
            },

            // artwork 14
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000e"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000039"),
            },

            // artwork 15
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000f"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000039"),
            },

            // artwork 16
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000039"),
            },

            // artwork 18
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000012"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            },

            // artwork 20
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000014"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            },
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000014"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
            },

            // artwork 21
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000015"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            },

            // artwork 22
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000016"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            },

            // artwork 23
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000017"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            },

            // artwork 27
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001b"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-00000000003a"),
            },

            // artwork 32
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000020"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            },

            // artwork 33
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000021"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            },

            // artwork 34
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000022"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            },

            // artwork 35
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000023"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            },

            // artwork 36
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000024"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            },

            // artwork 37
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000025"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            },

            // artwork 38
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000026"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            },

            // artwork 39
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000027"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            },

            // artwork 40
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000028"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            },

            // artwork 41
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000029"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-00000000003a"),
            },

            // artwork 42
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000002a"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-00000000003a"),
            },

            // artwork 43
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000002b"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-00000000003a"),
            },

            // artwork 46
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000002e"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-00000000003a"),
            },

            // artwork 47
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000002f"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-00000000003a"),
            },

            // artwork 48
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000030"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            },

            // artwork 49
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000031"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            },

            // artwork 50
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000032"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            },

            // artwork 51
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000033"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000007"),
            },

            // artwork 52
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000034"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000007"),
            },

            // artwork 53
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000035"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000007"),
            },

            // artwork 60
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003c"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-00000000000c"),
            },

            // artwork 61
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003d"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-00000000000b"),
            },

            // artwork 62
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003e"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            },

            // artwork 63
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003f"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000039"),
            },

            // artwork 64
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000040"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000039"),
            },

            // artwork 65
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000041"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-00000000003a"),
            },

            // artwork 69
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000045"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            },

            // artwork 70
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000046"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000035"),
            },

            // artwork 71
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000047"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            },

            // artwork 72
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000048"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000007"),
            },

            // artwork 73
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000049"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            },

            // artwork 74
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004a"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000039"),
            },

            // artwork 75
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004b"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000039"),
            },

            // artwork 76
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004c"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000039"),
            },

            // artwork 77
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004d"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
            },

            // artwork 78
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004e"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            },

            // artwork 80
            new SoftwareDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000050"),
                SoftwareUsedId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            }

        );
        #endregion
    }
}
