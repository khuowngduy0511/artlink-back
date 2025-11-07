using Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class TagDetailConfiguration : IEntityTypeConfiguration<TagDetail>
{
    public void Configure(EntityTypeBuilder<TagDetail> builder)
    {
        builder.ToTable(nameof(TagDetail));
        builder.HasKey(x => new { x.TagId, x.ArtworkId });

        // relationship
        builder.HasOne(x => x.Tag).WithMany(t => t.TagDetails).HasForeignKey(x => x.TagId);
        builder.HasOne(x => x.Artwork).WithMany(t => t.TagDetails).HasForeignKey(x => x.ArtworkId);


        #region init data
        builder.HasData
        (
        #region tag artwork 1 - 10
            // artwork 1
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000003")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000019")
            },

            // artwork 2
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000c")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000019")
            },

            // artwork 3
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000015")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001a")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001b")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001c")
            },

            // artwork 4
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001a")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001b")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001c")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001d")
            },

            // artwork 5
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000c")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000019")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000008")
            },


            // artwork 6
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000013")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000019")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001f")
            },

            // artwork 7
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000019")
            },

            // artwork 8
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000008")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000e")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001e")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001f")
            },

            // artwork 9
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000c")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000017")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000019")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000021")
            },


            // artwork 10
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000a"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000a"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000019")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000a"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001f")
            },
        #endregion

        #region tag artwork 11 - 20
            // artwork 11
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000b"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000b"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000017")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000b"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000019")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000b"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000020")
            },

            // artwork 12
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000c"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000c"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000003")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000c"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000004")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000c"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000014")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000c"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000019")
            },

            // artwork 13
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000d"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000d"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000003")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000d"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000004")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000d"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000006")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000d"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000019")
            },

            // artwork 14
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000e"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000e"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000005")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000e"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000008")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000e"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000e"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000b")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000e"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000017")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000e"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000021")
            },

            // artwork 15
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000f"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000f"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000016")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000f"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000019")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000f"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000026")
            },

            // artwork 16
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000019")
            },

            // artwork 17
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000011"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000003")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000011"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000018")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000011"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001d")
            },

            // artwork 18
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000012"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000006")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000012"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000008")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000012"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000012"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000b")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000012"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000022")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000012"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000023")
            },

            // artwork 19
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000013"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000007")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000013"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000015")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000013"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000016")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000013"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001a")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000013"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001b")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000013"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001c")
            },

            // artwork 20
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000014"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000003")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000014"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000014"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000b")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000014"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000c")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000014"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000017")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000014"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000020")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000014"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000021")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000014"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000024")
            },
        #endregion

        #region tag artwork 21 - 30
            // artwork 21
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000015"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000015"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000008")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000015"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000015"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000021")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000015"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000024")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000015"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000b")
            },

            // artwork 22
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000016"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000008")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000016"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000016"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000017")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000016"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000020")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000016"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000021")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000016"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000024")
            },

            // artwork 23
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000017"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000006")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000017"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000b")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000017"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000017")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000017"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000022")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000017"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000017"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000a")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000017"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000026")
            },

            // artwork 24
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000018"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000007")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000018"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000f")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000018"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000015")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000018"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000025")
            },

            // artwork 25
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000019"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000019"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000010")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000019"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000015")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000019"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000025")
            },

            // artwork 26
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001a"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000007")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001a"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000f")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001a"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000e")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001a"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000c")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001a"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000025")
            },

            // artwork 27
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001b"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001b"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000003")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001b"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000004")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001b"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000006")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001b"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000019")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001b"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000a")
            },

            // artwork 28
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001c"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000003")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001c"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000018")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001c"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001a")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001c"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000025")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001c"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000015")
            },

            // artwork 29
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001d"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000003")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001d"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000018")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001d"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001a")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001d"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000025")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001d"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000015")
            },

            // artwork 30
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001e"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000003")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001e"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000018")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001e"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001a")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001e"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000025")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001e"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000015")
            },
        #endregion

        #region tag artwork 31 - 40
            // artwork 31
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001f"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000003")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001f"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000018")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001f"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001a")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001f"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000025")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001f"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000015")
            },

            // artwork 32
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000020"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000007")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000020"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000020"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000b")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000020"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000f")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000020"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001a")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000020"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001c")
            },

            // artwork 33
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000021"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000006")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000021"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000022")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000021"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000026")
            },

            // artwork 34
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000022"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000006")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000022"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000022")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000022"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000a")
            },

            // artwork 35

            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000023"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000006")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000023"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000022")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000023"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000a")
            },

            // artwork 36
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000024"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000006")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000024"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000022")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000024"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000a")
            },

            // artwork 37
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000025"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000006")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000025"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000022")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000025"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000a")
            },

            // artwork 38
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000026"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000006")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000026"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000022")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000026"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000a")
            },

            // artwork 39
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000027"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000003")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000027"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000004")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000027"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000006")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000027"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000016")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000027"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000a")
            },

            // artwork 40
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000028"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000006")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000028"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000022")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000028"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000003")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000028"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000a")
            },
        #endregion

        #region tag artwork 41 - 50
            // artwork 41
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000029"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000006")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000029"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000022")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000029"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000003")
            },

            // artwork 42
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000002a"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000006")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000002a"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000022")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000002a"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000003")
            },

            // artwork 43
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000002b"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000006")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000002b"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000022")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000002b"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000003")
            },

            // artwork 44
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000002c"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000017")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000002c"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000024")
            },

            // artwork 45
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000002d"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000005")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000002d"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000023")
            },

            // artwork 46
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000002e"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000016")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000002e"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000003")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000002e"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000019")
            },

            // artwork 47
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000002f"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000026")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000002f"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000013")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000002f"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000014")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000002f"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000019")
            },

            // artwork 48
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000030"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000007")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000030"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000e")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000030"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001a")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000030"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001b")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000030"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001c")
            },

            // artwork 49
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000031"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000007")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000031"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000e")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000031"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001a")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000031"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001b")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000031"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001c")
            },

            // artwork 50
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000032"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000e")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000032"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001a")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000032"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001b")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000032"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001c")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000032"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001e")
            },
        #endregion

        #region tag artwork 51 - 60
            // artwork 51
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000033"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000008")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000033"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000033"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000b")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000033"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000012")
            },

            // artwork 52
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000034"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000008")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000034"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000034"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000b")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000034"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000012")
            },

            // artwork 53
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000035"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000008")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000035"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000035"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000b")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000035"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000012")
            },

            // artwork 54
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000036"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000036"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000036"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000f")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000036"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001d")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000036"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000a")
            },

            // artwork 55
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000037"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000003")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000037"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000004")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000037"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001c")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000037"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001d")
            },

            // artwork 56
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000038"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000003")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000038"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000004")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000038"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001c")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000038"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001d")
            },

            // artwork 57
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000039"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000003")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000039"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000004")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000039"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001c")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000039"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001d")
            },

            // artwork 58
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003a"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000e")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003a"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000018")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003a"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001c")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003a"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001d")
            },

            // artwork 59
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003b"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000003")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003b"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000004")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003b"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001c")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003b"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001d")
            },

            // artwork 60
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003c"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000010")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003c"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000017")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003c"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000020")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003c"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000024")
            },
        #endregion

        #region tag artwork 61 - 70
            // artwork 61
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003d"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000008")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003d"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003d"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000a")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003d"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000b")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003d"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000022")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003d"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000023")
            },

            // artwork 62
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003e"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000008")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003e"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003e"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000a")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003e"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000b")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003e"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000022")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003e"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000023")
            },

            // artwork 63
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003f"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000008")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003f"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003f"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000a")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003f"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000b")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003f"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000022")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003f"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000023")
            },

            // artwork 64
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000040"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000008")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000040"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000040"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000a")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000040"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000b")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000040"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000022")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000040"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000023")
            },

            // artwork 65
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000041"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000003")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000041"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000004")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000041"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000016")
            },

            // artwork 66
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000042"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000007")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000042"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000b")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000042"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000027")
            },

            // artwork 67
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000043"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000007")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000043"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000b")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000043"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000027")
            },

            // artwork 68
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000044"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000007")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000044"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000b")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000044"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000027")
            },

            // artwork 69
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000045"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000008")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000045"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000045"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001e")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000045"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000022")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000045"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000023")
            },

            // artwork 70
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000046"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000008")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000046"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000b")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000046"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000022")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000046"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000023")
            },
        #endregion

        #region tag artwork 71 - 80
            // artwork 71
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000047"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000008")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000047"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000047"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000b")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000047"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001e")
            },

            // artwork 72
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000048"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000048"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000005")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000048"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000008")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000048"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000048"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000b")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000048"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000012")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000048"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001e")
            },

            // artwork 73
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000049"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000006")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000049"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000a")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000049"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000b")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000049"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000022")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000049"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000026")
            },

            // artwork 74
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004a"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000008")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004a"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004a"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000017")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004a"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000019")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004a"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000021")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004a"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000023")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004a"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000024")
            },

            // artwork 75
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004b"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000008")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004b"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004b"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000017")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004b"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000019")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004b"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000021")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004b"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000023")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004b"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000024")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004b"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000013")
            },

            // artwork 76
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004c"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000008")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004c"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004c"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000017")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004c"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000019")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004c"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000021")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004c"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000023")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004c"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000024")
            },

            // artwork 77
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004d"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004d"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000008")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004d"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000b")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004d"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000028")
            },

            // artwork 78
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004e"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000007")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004e"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000009")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004e"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000b")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004e"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000000f")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004e"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001a")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004e"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001c")
            },

            // artwork 79
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004f"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000003")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004f"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000018")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004f"),
                TagId = Guid.Parse("00000000-0000-0000-0000-00000000001d")
            },

            // artwork 80
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000050"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000026")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000050"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000013")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000050"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000014")
            },
            new TagDetail()
            {
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000050"),
                TagId = Guid.Parse("00000000-0000-0000-0000-000000000019")
            }
            #endregion

        );
        #endregion
    }
}
