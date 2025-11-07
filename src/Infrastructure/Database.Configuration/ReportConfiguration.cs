using Domain.Entitites;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable(nameof(Report));

            builder.Property(x => x.Id).HasDefaultValueSql("newid()");

            // relationship
            builder.HasOne(x => x.AccountReport).WithMany(p => p.Reports).HasForeignKey(x => x.CreatedBy);


            #region init data
            builder.HasData
            (
                new Report()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    ReportType = ReportTypeEnum.InappropriateContent,
                    Reason = "Ảnh AI",
                    ReportEntity = ReportEntityEnum.Artwork,
                    State = StateEnum.Accepted,
                    TargetId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000002"), 
                    CreatedOn = DateTime.Parse("2023-11-27T08:40:28.901Z"),
                },
                new Report()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    ReportType = ReportTypeEnum.InappropriateContent,
                    Reason = "Nội dung không phù hợp",
                    ReportEntity = ReportEntityEnum.Artwork,
                    State = StateEnum.Accepted,
                    Note = "Đã cảnh cáo",
                    TargetId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000002"), 
                    CreatedOn = DateTime.Parse("2023-12-07T08:40:28.901Z"),
                },
                new Report()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    ReportType = ReportTypeEnum.Impersonation,
                    Reason = "Không thuộc về tác giả",
                    ReportEntity = ReportEntityEnum.Artwork,
                    State = StateEnum.Declined,
                    Note = "Đã kiểm tra",
                    TargetId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000002"), 
                    CreatedOn = DateTime.Parse("2023-12-27T08:40:28.901Z"),
                },
                new Report()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                    ReportType = ReportTypeEnum.Other,
                    Reason = "Nội dung không cho phép",
                    ReportEntity = ReportEntityEnum.Artwork,
                    State = StateEnum.Accepted,
                    TargetId = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                    CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000002"), 
                    CreatedOn = DateTime.Parse("2023-12-28T08:40:28.901Z"),
                },
                new Report()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                    ReportType = ReportTypeEnum.InappropriateContent,
                    Reason = "Ảnh AI",
                    ReportEntity = ReportEntityEnum.Artwork,
                    State = StateEnum.Declined,
                    TargetId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                    CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000002"), 
                    CreatedOn = DateTime.Parse("2023-12-29T08:40:28.901Z"),
                },
                new Report()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                    ReportType = ReportTypeEnum.InappropriateContent,
                    Reason = "Không có nghệ thuật",
                    ReportEntity = ReportEntityEnum.Artwork,
                    State = StateEnum.Declined,
                    TargetId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                    CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000002"), 
                    CreatedOn = DateTime.Parse("2023-11-27T08:40:28.901Z"),
                },
                new Report()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                    ReportType = ReportTypeEnum.Spam,
                    Reason = "Đây là spam",
                    ReportEntity = ReportEntityEnum.Artwork,
                    State = StateEnum.Accepted,
                    TargetId = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                    CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000002"), 
                    CreatedOn = DateTime.Parse("2023-11-28T08:40:28.901Z"),
                },

                // Account
                new Report()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000100"),
                    ReportType = ReportTypeEnum.Spam,
                    Reason = "Tài khoản spam",
                    ReportEntity = ReportEntityEnum.Account,
                    State = StateEnum.Declined,
                    TargetId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000005"), 
                    CreatedOn = DateTime.Parse("2024-01-01T08:40:28.901Z"),
                },

                new Report()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000101"),
                    ReportType = ReportTypeEnum.InappropriateContent,
                    Reason = "Tài khoản đăng tác phẩm trùng lặp",
                    ReportEntity = ReportEntityEnum.Account,
                    State = StateEnum.Accepted,
                    TargetId = Guid.Parse("00000000-0000-0000-0000-000000000017"),
                    CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000005"), 
                    CreatedOn = DateTime.Parse("2024-01-02T08:40:28.901Z"),
                }

            );
            #endregion
        }
    }
}
