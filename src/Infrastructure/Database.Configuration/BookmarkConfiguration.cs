using Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration
{
    public class BookmarkConfiguration : IEntityTypeConfiguration<Bookmark>
    {
        public void Configure(EntityTypeBuilder<Bookmark> builder)
        {
            builder.ToTable(nameof(Bookmark));

            //composite key
            builder.HasKey(x => new { x.ArtworkId, x.CollectionId });

            // relationship
            builder.HasOne(x => x.Artwork).WithMany(a => a.Bookmarks).HasForeignKey(x => x.ArtworkId);
            builder.HasOne(x => x.Collection).WithMany(c => c.Bookmarks).HasForeignKey(x => x.CollectionId);

            #region init data
            builder.HasData(
                // collection 1
                new Bookmark()
                {
                    CollectionId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000045")
                },
                new Bookmark()
                {
                    CollectionId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003f")
                },
                new Bookmark()
                {
                    CollectionId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003e")
                },

                // collection 2
                new Bookmark()
                {
                    CollectionId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000024")
                },
                new Bookmark()
                {
                    CollectionId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000001")
                },
                new Bookmark()
                {
                    CollectionId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000041")
                }
            );
            #endregion
        }
    }
}
