using Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Enums;

namespace Infrastructure.Database.Configuration;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable("Notification");

        builder.Property(x => x.Id).HasDefaultValueSql("newid()");
        builder.Property(x => x.CreatedOn).HasDefaultValueSql("getutcdate()");

        // relationship
        builder.HasOne(x => x.Account).WithMany(a => a.Notifications).HasForeignKey(x => x.SentToAccount);

        #region init data
        builder.HasData(
            new Notification
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                SentToAccount = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Content = "Chào mừng đến với Artworkia",
                NotifyType = NotifyTypeEnum.System,
                IsSeen = true,
                CreatedOn = DateTime.Parse("2024-01-01")
            },
            new Notification
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                SentToAccount = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                Content = "Chào mừng đến với Artworkia",
                NotifyType = NotifyTypeEnum.System,
                IsSeen = true,
                CreatedOn = DateTime.Parse("2024-01-01")
            },
            new Notification
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                SentToAccount = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                Content = "Chào mừng đến với Artworkia",
                NotifyType = NotifyTypeEnum.System,
                IsSeen = true,
                CreatedOn = DateTime.Parse("2024-01-01")
            },
            new Notification
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                SentToAccount = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                Content = "Chào mừng đến với Artworkia",
                NotifyType = NotifyTypeEnum.System,
                IsSeen = true,
                CreatedOn = DateTime.Parse("2024-01-01")
            },
            new Notification
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                SentToAccount = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                Content = "Chào mừng đến với Artworkia",
                NotifyType = NotifyTypeEnum.System,
                IsSeen = true,
                CreatedOn = DateTime.Parse("2024-01-01")
            },
            new Notification
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                SentToAccount = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                Content = "Chào mừng đến với Artworkia",
                NotifyType = NotifyTypeEnum.System,
                IsSeen = true,
                CreatedOn = DateTime.Parse("2024-01-01")
            },
            new Notification
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                SentToAccount = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                Content = "Chào mừng đến với Artworkia",
                NotifyType = NotifyTypeEnum.System,
                IsSeen = true,
                CreatedOn = DateTime.Parse("2024-01-01")
            },
            new Notification
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                SentToAccount = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                Content = "Chào mừng đến với Artworkia",
                NotifyType = NotifyTypeEnum.System,
                IsSeen = true,
                CreatedOn = DateTime.Parse("2024-01-01")
            },
            new Notification
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                SentToAccount = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                Content = "Chào mừng đến với Artworkia",
                NotifyType = NotifyTypeEnum.System,
                IsSeen = true,
                CreatedOn = DateTime.Parse("2024-01-01")
            },
            new Notification
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000a"),
                SentToAccount = Guid.Parse("00000000-0000-0000-0000-00000000000a"),
                Content = "Chào mừng đến với Artworkia",
                NotifyType = NotifyTypeEnum.System,
                IsSeen = true,
                CreatedOn = DateTime.Parse("2024-01-01")
            },
            new Notification
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000b"),
                SentToAccount = Guid.Parse("00000000-0000-0000-0000-00000000000b"),
                Content = "Chào mừng đến với Artworkia",
                NotifyType = NotifyTypeEnum.System,
                IsSeen = true,
                CreatedOn = DateTime.Parse("2024-01-01")
            },
            new Notification
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000c"),
                SentToAccount = Guid.Parse("00000000-0000-0000-0000-00000000000c"),
                Content = "Chào mừng đến với Artworkia",
                NotifyType = NotifyTypeEnum.System,
                IsSeen = true,
                CreatedOn = DateTime.Parse("2024-01-01")
            },
            new Notification
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000d"),
                SentToAccount = Guid.Parse("00000000-0000-0000-0000-00000000000d"),
                Content = "Chào mừng đến với Artworkia",
                NotifyType = NotifyTypeEnum.System,
                IsSeen = true,
                CreatedOn = DateTime.Parse("2024-01-01")
            },
            new Notification
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000e"),
                SentToAccount = Guid.Parse("00000000-0000-0000-0000-00000000000e"),
                Content = "Chào mừng đến với Artworkia",
                NotifyType = NotifyTypeEnum.System,
                IsSeen = true,
                CreatedOn = DateTime.Parse("2024-01-01")
            },
            new Notification
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000f"),
                SentToAccount = Guid.Parse("00000000-0000-0000-0000-00000000000f"),
                Content = "Chào mừng đến với Artworkia",
                NotifyType = NotifyTypeEnum.System,
                IsSeen = true,
                CreatedOn = DateTime.Parse("2024-01-01")
            },
            new Notification
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                SentToAccount = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                Content = "Chào mừng đến với Artworkia",
                NotifyType = NotifyTypeEnum.System,
                IsSeen = true,
                CreatedOn = DateTime.Parse("2024-01-01")
            },
            new Notification
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000011"),
                SentToAccount = Guid.Parse("00000000-0000-0000-0000-000000000011"),
                Content = "Chào mừng đến với Artworkia",
                NotifyType = NotifyTypeEnum.System,
                IsSeen = true,
                CreatedOn = DateTime.Parse("2024-01-01")
            },
            new Notification
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000012"),
                SentToAccount = Guid.Parse("00000000-0000-0000-0000-000000000012"),
                Content = "Chào mừng đến với Artworkia",
                NotifyType = NotifyTypeEnum.System,
                IsSeen = true,
                CreatedOn = DateTime.Parse("2024-01-01")
            },
            new Notification
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000013"),
                SentToAccount = Guid.Parse("00000000-0000-0000-0000-000000000013"),
                Content = "Chào mừng đến với Artworkia",
                NotifyType = NotifyTypeEnum.System,
                IsSeen = true,
                CreatedOn = DateTime.Parse("2024-01-01")
            },
            new Notification
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000014"),
                SentToAccount = Guid.Parse("00000000-0000-0000-0000-000000000014"),
                Content = "Chào mừng đến với Artworkia",
                NotifyType = NotifyTypeEnum.System,
                IsSeen = true,
                CreatedOn = DateTime.Parse("2024-01-01")
            },
            new Notification
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000015"),
                SentToAccount = Guid.Parse("00000000-0000-0000-0000-000000000015"),
                Content = "Chào mừng đến với Artworkia",
                NotifyType = NotifyTypeEnum.System,
                IsSeen = true,
                CreatedOn = DateTime.Parse("2024-01-01")
            },
            new Notification
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000016"),
                SentToAccount = Guid.Parse("00000000-0000-0000-0000-000000000016"),
                Content = "Chào mừng đến với Artworkia",
                NotifyType = NotifyTypeEnum.System,
                IsSeen = true,
                CreatedOn = DateTime.Parse("2024-01-01")
            },
            new Notification
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000017"),
                SentToAccount = Guid.Parse("00000000-0000-0000-0000-000000000017"),
                Content = "Chào mừng đến với Artworkia",
                NotifyType = NotifyTypeEnum.System,
                IsSeen = true,
                CreatedOn = DateTime.Parse("2024-01-01")
            },
            new Notification
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000018"),
                SentToAccount = Guid.Parse("00000000-0000-0000-0000-000000000018"),
                Content = "Chào mừng đến với Artworkia",
                NotifyType = NotifyTypeEnum.System,
                IsSeen = true,
                CreatedOn = DateTime.Parse("2024-01-01")
            },
            new Notification
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000019"),
                SentToAccount = Guid.Parse("00000000-0000-0000-0000-000000000019"),
                Content = "Chào mừng đến với Artworkia",
                NotifyType = NotifyTypeEnum.System,
                IsSeen = true,
                CreatedOn = DateTime.Parse("2024-01-01")
            },
            new Notification
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000001a"),
                SentToAccount = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                Content = "Cảnh báo bảo mật",
                NotifyType = NotifyTypeEnum.Warning,
                IsSeen = true,
                CreatedOn = DateTime.Parse("2024-01-02")
            },
            new Notification
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000001b"),
                SentToAccount = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                Content = "Người dùng [hoanganh] vừa theo dõi bạn",
                NotifyType = NotifyTypeEnum.Information,
                IsSeen = true,
                CreatedOn = DateTime.Parse("2024-01-02")
            },
            new Notification
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000001c"),
                SentToAccount = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                Content = "Người dùng [lamlam] vừa theo dõi bạn",
                NotifyType = NotifyTypeEnum.Information,
                IsSeen = true,
                CreatedOn = DateTime.Parse("2024-01-02")
            }

        );
    }
    #endregion
}
