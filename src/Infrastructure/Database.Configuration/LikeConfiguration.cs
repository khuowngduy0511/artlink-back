using Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;
public class LikeConfiguration : IEntityTypeConfiguration<Like>
{
    public void Configure(EntityTypeBuilder<Like> builder)
    {
        builder.ToTable(nameof(Like));

        //composite key
        builder.HasKey(x => new { x.AccountId, x.ArtworkId });

        //relationship
        builder.HasOne(x => x.Account).WithMany(a => a.Likes).HasForeignKey(x => x.AccountId);
        builder.HasOne(x => x.Artwork).WithMany(a => a.Likes).HasForeignKey(x => x.ArtworkId);

        #region init data
        builder.HasData
        (
            new Like()
            {
                AccountId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            },
            new Like()
            {
                AccountId = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            },
            new Like()
            {
                AccountId = Guid.Parse("00000000-0000-0000-0000-00000000000a"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            },
            new Like()
            {
                AccountId = Guid.Parse("00000000-0000-0000-0000-00000000000b"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            },
            new Like()
            {
                AccountId = Guid.Parse("00000000-0000-0000-0000-00000000000c"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            },
            new Like()
            {
                AccountId = Guid.Parse("00000000-0000-0000-0000-00000000000d"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            },
            new Like()
            {
                AccountId = Guid.Parse("00000000-0000-0000-0000-00000000000e"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            },
            new Like()
            {
                AccountId = Guid.Parse("00000000-0000-0000-0000-00000000000f"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            }
        );
        #endregion
    }
}
