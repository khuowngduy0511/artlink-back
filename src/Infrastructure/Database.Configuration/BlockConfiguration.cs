using Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class BlockConfiguration : IEntityTypeConfiguration<Block>
{
    public void Configure(EntityTypeBuilder<Block> builder)
    {
        builder.ToTable(nameof(Block));

        //composite key
        builder.HasKey(x => new { x.BlockingId, x.BlockedId });

        #region relationships
        //relationship
        builder.HasOne(x => x.Blocking).WithMany(a => a.Blocking).HasForeignKey(x => x.BlockingId);
        builder.HasOne(x => x.Blocked).WithMany(a => a.Blocked)
            .HasForeignKey(x => x.BlockedId)
            .OnDelete(DeleteBehavior.NoAction);
        #endregion

        #region init data
        builder.HasData(
            new Block
            {
                BlockingId = Guid.Parse("00000000-0000-0000-0000-000000000001"), // user
                BlockedId = Guid.Parse("00000000-0000-0000-0000-000000000002")  // lamlam
            },
            new Block
            {
                BlockingId = Guid.Parse("00000000-0000-0000-0000-000000000001"), //user
                BlockedId = Guid.Parse("00000000-0000-0000-0000-000000000003")  //hoanganh
            },

            new Block
            {
                BlockingId = Guid.Parse("00000000-0000-0000-0000-000000000005"), //phuhuynh,
                BlockedId = Guid.Parse("00000000-0000-0000-0000-000000000017")  //vudang (hitler)

            }
        );
        #endregion
    }
}
