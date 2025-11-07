using Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable(nameof(Tag));

        builder.Property(x => x.Id).HasDefaultValueSql("newid()");

        builder.HasIndex(x => x.TagName).IsUnique();

        // relationship
        builder.HasMany(x => x.TagDetails).WithOne(d => d.Tag).HasForeignKey(d => d.TagId);


        #region init data
        builder.HasData
        (
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), TagName = "Màu sắc" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), TagName = "Trừu tượng" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), TagName = "Phong cảnh" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-000000000004"), TagName = "Thiên nhiên" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-000000000005"), TagName = "Hình học" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-000000000006"), TagName = "Anime" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-000000000007"), TagName = "Chân dung" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-000000000008"), TagName = "Sáng tạo" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-000000000009"), TagName = "Kỹ thuật số" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-00000000000a"), TagName = "Nhật Bản" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-00000000000b"), TagName = "Dự án cá nhân" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-00000000000c"), TagName = "Nghệ thuật số hóa" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-00000000000d"), TagName = "Thể thao" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-00000000000e"), TagName = "Xã hội" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-00000000000f"), TagName = "Vintage" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-000000000010"), TagName = "Ảo" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-000000000011"), TagName = "Tối giản" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-000000000012"), TagName = "Figma" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-000000000013"), TagName = "Mèo" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-000000000014"), TagName = "Động vật" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-000000000015"), TagName = "Sơn dầu" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-000000000016"), TagName = "Nghệ thuật đương đại" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-000000000017"), TagName = "Khoa học" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-000000000018"), TagName = "Kiến trúc" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-000000000019"), TagName = "AI Tạo Sinh" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-00000000001a"), TagName = "Lịch sử" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-00000000001b"), TagName = "Chính trị" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-00000000001c"), TagName = "Việt Nam" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-00000000001d"), TagName = "Nhiếp ảnh" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-00000000001e"), TagName = "Ảnh bìa" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-00000000001f"), TagName = "Tâm lý" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-000000000020"), TagName = "Vũ trụ" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-000000000021"), TagName = "Tương lai" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-000000000022"), TagName = "Nhân vật" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-000000000023"), TagName = "Trò chơi" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-000000000024"), TagName = "Khoa học viễn tưởng" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-000000000025"), TagName = "Kinh điển" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-000000000026"), TagName = "Dễ thương" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-000000000027"), TagName = "Phát họa" },
            new Tag() { Id = Guid.Parse("00000000-0000-0000-0000-000000000028"), TagName = "Logo" }
        );
        #endregion
    }
}
