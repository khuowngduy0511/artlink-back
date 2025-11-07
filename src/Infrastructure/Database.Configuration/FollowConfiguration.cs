using Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class FollowConfiguration : IEntityTypeConfiguration<Follow>
{
    public void Configure(EntityTypeBuilder<Follow> builder)
    {
        builder.ToTable(nameof(Follow));
        builder.HasKey(x => new { x.FollowingId, x.FollowedId });

        #region relationships
        // relationships
        builder.HasOne(x => x.Followed).WithMany(a => a.Followings).HasForeignKey(x => x.FollowedId);
        builder.HasOne(x => x.Following).WithMany(a => a.Followers).HasForeignKey(x => x.FollowingId)
            .OnDelete(DeleteBehavior.NoAction);
        #endregion

        #region init data
        builder.HasData(
            // hoanganh
            new Follow
            {
                FollowingId = Guid.Parse("00000000-0000-0000-0000-000000000003"), // hoanganh
                FollowedId = Guid.Parse("00000000-0000-0000-0000-000000000002")  // lamlam
            },
            new Follow
            {
                FollowingId = Guid.Parse("00000000-0000-0000-0000-000000000003"), // hoanganh
                FollowedId = Guid.Parse("00000000-0000-0000-0000-000000000005")  // phuhuynh
            },

            // phuhuynh
            new Follow
            {
                FollowingId = Guid.Parse("00000000-0000-0000-0000-000000000005"), // phuhuynh
                FollowedId = Guid.Parse("00000000-0000-0000-0000-000000000002")  // lamlam
            },
            new Follow
            {
                FollowingId = Guid.Parse("00000000-0000-0000-0000-000000000005"), // phuhuynh
                FollowedId = Guid.Parse("00000000-0000-0000-0000-000000000003")  // hoanganh
            },

            // lamlam
            new Follow
            {
                FollowingId = Guid.Parse("00000000-0000-0000-0000-000000000002"), // lamlam
                FollowedId = Guid.Parse("00000000-0000-0000-0000-000000000005")  // phuhuynh
            },

            // thong
            new Follow
            {
                FollowingId = Guid.Parse("00000000-0000-0000-0000-000000000004"), // thong
                FollowedId = Guid.Parse("00000000-0000-0000-0000-000000000002")  // lamlam
            }
        );
        #endregion
    }
}
