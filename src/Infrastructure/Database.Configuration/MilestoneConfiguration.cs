using Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class MilestoneConfiguration : IEntityTypeConfiguration<Milestone>
{
    public void Configure(EntityTypeBuilder<Milestone> builder)
    {
        builder.ToTable(nameof(Milestone));

        builder.Property(x => x.Id).HasDefaultValueSql("newid()");
        builder.Property(x => x.CreatedOn).HasDefaultValueSql("getutcdate()");

        // relationship
        builder.HasOne(x => x.Proposal).WithMany(p => p.Milestones).HasForeignKey(x => x.ProposalId);
        builder.HasOne(x => x.CreatedAccount).WithMany(p => p.Milestones).HasForeignKey(x => x.CreatedBy);

        #region data
        builder.HasData(
            new Milestone()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                ProposalId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                MilestoneName = "Thỏa thuận đã được tạo",
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                CreatedOn = DateTime.Parse("2024-1-2"),
            },
            new Milestone()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                ProposalId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                MilestoneName = "Đặt cọc thành công 20000 xu (25%)",
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                CreatedOn = DateTime.Parse("2024-1-3"),
            },
            new Milestone()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                ProposalId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                MilestoneName = "Thỏa thuận đã được chấp nhận",
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                CreatedOn = DateTime.Parse("2024-1-4"),
            },
            new Milestone()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                ProposalId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                MilestoneName = "Gửi tài nguyên thỏa thuận (phác thảo)",
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                CreatedOn = DateTime.Parse("2024-1-5"),
            },
            new Milestone()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                ProposalId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                MilestoneName = "Gửi tài nguyên thỏa thuận (phác thảo)",
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                CreatedOn = DateTime.Parse("2024-1-10"),
            },
            new Milestone()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                ProposalId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                MilestoneName = "Gửi tài nguyên thỏa thuận (cuối cùng)",
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                CreatedOn = DateTime.Parse("2024-1-25"),
            },
            new Milestone()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                ProposalId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                MilestoneName = "Gửi tài nguyên thỏa thuận (chỉnh sửa)",
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                CreatedOn = DateTime.Parse("2024-1-30"),
            },
            new Milestone()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                ProposalId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                MilestoneName = "Hoàn tất thanh toán 60000 xu (75%)",
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                CreatedOn = DateTime.Parse("2024-2-1"),
            }
        );
        #endregion
    }
}
