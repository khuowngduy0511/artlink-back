using Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class CategoryArtworkDetailConfiguration : IEntityTypeConfiguration<CategoryArtworkDetail>
{
    public void Configure(EntityTypeBuilder<CategoryArtworkDetail> builder)
    {
        builder.ToTable("CategoryArtworkDetail");
        builder.HasKey(x => new { x.CategoryId, x.ArtworkId });

        // relationship
        builder.HasOne(x => x.Category).WithMany(c => c.CategoryArtworkDetails).HasForeignKey(x => x.CategoryId);
        builder.HasOne(x => x.Artwork).WithMany(a => a.CategoryArtworkDetails).HasForeignKey(x => x.ArtworkId);

        #region init data
        builder.HasData
        (
            // artwork 1
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },

            // artwork 2
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },

            // artwork 3
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },

            // artwork 4
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-00000000000a")
            },

            // artwork 5
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },

            // artwork 6
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },

            // artwork 7
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },

            // artwork 8
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-00000000000b")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },

            // artwork 9
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },

            // artwork 10
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000a"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },

            // artwork 11
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000b"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },

            // artwork 12
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000c"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },

            // artwork 13
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000d"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },

            // artwork 14
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000e"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000e"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000e"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000007")
            },

            // artwork 15
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000f"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000f"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },

            // artwork 16
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },

            // artwork 17
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000011"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },

            // artwork 18
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000012"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-00000000000e")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000012"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000012"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-00000000000b")
            },

            // artwork 19
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000013"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },

            // artwork 20
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000014"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000014"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000008")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000014"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },

            // artwork 21
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000015"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000015"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000008")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000015"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },

            // artwork 22
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000016"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000016"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000008")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000016"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },

            // artwork 23
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000017"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },

            // artwork 24
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000018"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },

            // artwork 25
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000019"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },

            // artwork 26
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001a"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },

            // artwork 27
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001b"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },

            // artwork 28
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001c"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001c"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000004")
            },

            // artwork 29
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001d"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001d"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000004")
            },

            // artwork 30
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001e"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001e"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000004")
            },

            // artwork 31
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001f"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001f"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000004")
            },

            // artwork 32
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000020"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },

            // artwork 33
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000021"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },

            // artwork 34
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000022"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000022"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000006")
            },


            // artwork 35
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000023"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },

            // artwork 36
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000024"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },

            // artwork 37
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000025"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },

            // artwork 38
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000026"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },

            // artwork 39
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000027"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },

            // artwork 40
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000028"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },

            // artwork 41
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000029"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },

            // artwork 42
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000002a"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },

            // artwork 43
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000002b"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },

            // artwork 44
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000002c"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },

            // artwork 45
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000002d"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },

            // artwork 46
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000002e"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },

            // artwork 47
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000002f"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },

            // artwork 48
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000030"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },

            // artwork 49
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000031"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000031"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },

            // artwork 50
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000032"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },

            // artwork 51
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000033"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000003")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000033"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-00000000000b")
            },

            // artwork 52
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000034"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000003")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000034"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-00000000000b")
            },

            // artwork 53
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000035"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000003")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000035"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-00000000000b")
            },

            // artwork 54
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000036"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-00000000000a")
            },

            // artwork 55
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000037"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-00000000000a")
            },

            // artwork 56
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000038"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-00000000000a")
            },

            // artwork 57
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000039"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-00000000000a")
            },

            // artwork 58
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003a"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-00000000000a")
            },

            // artwork 59
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003b"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-00000000000a")
            },

            // artwork 60
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003c"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003c"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000008")
            },

            // artwork 61
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003d"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003d"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000008")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003d"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-00000000000e")
            },

            // artwork 62
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003e"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003e"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000008")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003e"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-00000000000e")
            },

            // artwork 63
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003f"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003f"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000008")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003f"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-00000000000e")
            },

            // artwork 64
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000040"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000040"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000008")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000040"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-00000000000e")
            },

            // artwork 65
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000041"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },

            // artwork 66
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000042"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-00000000000d")
            },

            // artwork 67
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000043"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-00000000000d")
            },

            // artwork 68
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000044"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-00000000000d")
            },

            // artwork 69
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000045"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000045"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000008")
            },

            // artwork 70
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000046"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000046"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000003")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000046"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-00000000000b")
            },

            // artwork 71
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000047"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000047"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-00000000000b")
            },

            // artwork 72
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000048"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000048"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000003")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000048"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000007")
            },

            // artwork 73
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000049"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000049"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-00000000000c")
            },

            // artwork 74
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004a"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004a"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000008")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004a"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },

            // artwork 75
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004b"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004b"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000008")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004b"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },

            // artwork 76
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004c"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004c"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000008")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004c"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },

            // artwork 77
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004d"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004d"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000007")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004d"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-00000000000b")
            },

            // artwork 78
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004e"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },

            // artwork 79
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004f"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000004")
            },
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004f"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-00000000000a")
            },

            // artwork 80
            new CategoryArtworkDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000050"),
                CategoryId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            }

        );
        #endregion

    }
}
