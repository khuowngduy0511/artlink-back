using Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable(nameof(Review));

        builder.Property(x => x.Id).HasDefaultValueSql("newid()");
        builder.Property(x => x.CreatedOn).HasDefaultValueSql("getutcdate()");

        // relationship
        builder.HasOne(x => x.Account).WithMany(a => a.Reviews).HasForeignKey(x => x.CreatedBy);
        builder.HasOne(x => x.Proposal).WithOne(p => p.Review).HasForeignKey<Review>(x => x.ProposalId);

        #region data
        builder.HasData(
            new Review()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                ProposalId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Vote = 5,
                Detail = "Dịch vụ xuất sắc",
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                CreatedOn = DateTime.Parse("2024-2-2"),
            }
        );
        #endregion
    }
}
