using Domain.Entitites;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class ProposalConfiguration : IEntityTypeConfiguration<Proposal>
{
    public void Configure(EntityTypeBuilder<Proposal> builder)
    {
        builder.ToTable(nameof(Proposal));

        builder.Property(x => x.Id).HasDefaultValueSql("newid()");
        builder.Property(x => x.CreatedOn).HasDefaultValueSql("getutcdate()");

        // relationship
        builder.HasOne(x => x.Account).WithMany(a => a.Proposals).HasForeignKey(x => x.CreatedBy);
        builder.HasOne(x => x.Orderer).WithMany(a => a.OrderProposals).HasForeignKey(x => x.OrdererId);
        builder.HasOne(x => x.Service).WithMany(s => s.Proposals).HasForeignKey(x => x.ServiceId);
        builder.HasMany(x => x.ProposalAssets).WithOne(a => a.Proposal).HasForeignKey(a => a.ProposalId);
        builder.HasOne(x => x.Review).WithOne(r => r.Proposal).HasForeignKey<Review>(x => x.ProposalId);
        builder.HasMany(x => x.TransactionHistories).WithOne(t => t.Proposal).HasForeignKey(t => t.ProposalId);
        builder.HasMany(x => x.Milestones).WithOne(m => m.Proposal).HasForeignKey(m => m.ProposalId);
        builder.HasOne(x => x.MessageObj).WithOne(m => m.Proposal).HasForeignKey<Message>(m => m.ProposalId);

        #region data
        builder.HasData(
            // service 1
            new Proposal()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                ServiceId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                ProjectTitle = "Yêu cầu làm website mua bán thiệp đám cưới.",
                Category = "Website",
                Description = @"Yêu cầu làm website mua bán thiệp đám cưới. 
                                Giao diện trực quan, dễ sử dụng. 
                                Tìm kiếm và bộ lọc sản phẩm.
                                Hệ thống thanh toán an toàn.",
                TargetDelivery = DateTime.Parse("2024-01-30"),
                ActualDelivery = DateTime.Parse("2024-02-01"),
                NumberOfConcept = 2,
                NumberOfRevision = 3,
                InitialPrice = 0.25,
                Total = 80000,
                ProposalStatus = ProposalStateEnum.CompletePayment,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000002"), //lamlam
                CreatedOn = DateTime.Parse("2023-12-31"),
                OrdererId = Guid.Parse("00000000-0000-0000-0000-000000000005"), //phuhuynh
            },

            // service 6
            new Proposal()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000a"),
                ServiceId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                ProjectTitle = "Thiết kế Hình Ảnh Khoa Học - Dự Án Tranh Tường.",
                Category = "Thiết kế",
                Description = "Yêu cầu thiết kế một bức tranh tường với chủ đề khoa học viễn tưởng cho không gian phòng khách.",
                TargetDelivery = DateTime.Parse("2024-02-15"),
                ActualDelivery = DateTime.Parse("2024-02-01"),
                NumberOfConcept = 2,
                NumberOfRevision = 3,
                InitialPrice = 0.25,
                Total = 50000,
                ProposalStatus = ProposalStateEnum.CompletePayment,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-00000000000b"), // melodysheep
                CreatedOn = DateTime.Parse("2024-1-1"),
                OrdererId = Guid.Parse("00000000-0000-0000-0000-00000000000a"), //phamthanh
            },
            new Proposal()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000b"),
                ServiceId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                ProjectTitle = "Thiết kế Hình Ảnh Khoa Học - Dự Án Trang Trí Phòng Làm Việc",
                Category = "Thiết kế",
                Description = " Yêu cầu thiết kế các tác phẩm nghệ thuật nhỏ về khoa học viễn tưởng để trang trí không gian phòng làm việc.",
                TargetDelivery = DateTime.Parse("2024-02-25"),
                ActualDelivery = DateTime.Parse("2024-02-25"),
                NumberOfConcept = 2,
                NumberOfRevision = 3,
                InitialPrice = 0.25,
                Total = 60000,
                ProposalStatus = ProposalStateEnum.CompletePayment,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-00000000000b"), // melodysheep
                CreatedOn = DateTime.Parse("2024-01-15"),
                OrdererId = Guid.Parse("00000000-0000-0000-0000-00000000000c"),
            },
            new Proposal()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000c"),
                ServiceId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                ProjectTitle = "Thiết kế Hình Ảnh Khoa Học - Dự Án Poster Khoa Học",
                Category = "Thiết kế",
                Description = "Yêu cầu thiết kế một loạt các poster khoa học viễn tưởng để trưng bày " +
                "trong sự kiện khoa học của trường địa phương.",
                TargetDelivery = DateTime.Parse("2024-03-01"),
                ActualDelivery = DateTime.Parse("2024-03-01"),
                NumberOfConcept = 2,
                NumberOfRevision = 3,
                InitialPrice = 0.25,
                Total = 80000,
                ProposalStatus = ProposalStateEnum.CompletePayment,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-00000000000b"), // melodysheep
                CreatedOn = DateTime.Parse("2024-02-01"),
                OrdererId = Guid.Parse("00000000-0000-0000-0000-00000000000d"),
            },
            new Proposal()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000d"),
                ServiceId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                ProjectTitle = "Thiết kế Hình Ảnh Khoa Học - Dự Án Sách Truyện Khoa Học",
                Category = "Thiết kế",
                Description = "Yêu cầu thiết kế bìa và các hình minh họa cho cuốn sách truyện khoa học viễn tưởng mới.",
                TargetDelivery = DateTime.Parse("2024-03-15"),
                ActualDelivery = DateTime.Parse("2024-03-01"),
                NumberOfConcept = 2,
                NumberOfRevision = 3,
                InitialPrice = 0.25,
                Total = 50000,
                ProposalStatus = ProposalStateEnum.CompletePayment,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-00000000000b"), // melodysheep
                CreatedOn = DateTime.Parse("2024-03-01"),
                OrdererId = Guid.Parse("00000000-0000-0000-0000-00000000000e"),
            },
            new Proposal()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000e"),
                ServiceId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                ProjectTitle = "Hợp đồng Thiết kế Hình Ảnh Khoa Học - Dự Án Phim Ngắn",
                Category = "Thiết kế",
                Description = "Yêu cầu thiết kế các cảnh và hiệu ứng đặc biệt cho một bộ phim ngắn với chủ đề khoa học.",
                TargetDelivery = DateTime.Parse("2024-03-29"),
                ActualDelivery = DateTime.Parse("2024-03-29"),
                NumberOfConcept = 2,
                NumberOfRevision = 3,
                InitialPrice = 0.25,
                Total = 100000,
                ProposalStatus = ProposalStateEnum.CompletePayment,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-00000000000b"), // melodysheep
                CreatedOn = DateTime.Parse("2024-02-20"),
                OrdererId = Guid.Parse("00000000-0000-0000-0000-00000000000f"),
            }
        );
        #endregion
    }
}
