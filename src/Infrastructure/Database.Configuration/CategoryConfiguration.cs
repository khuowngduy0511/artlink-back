using Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable(nameof(Category));

        builder.Property(x => x.Id).HasDefaultValueSql("newid()");

        // relationship
        builder.HasOne(x => x.Parent).WithMany(p => p.Children).HasForeignKey(x => x.ParentCategory);
        builder.HasMany(x => x.CategoryServiceDetails).WithOne(d => d.Category).HasForeignKey(d => d.CategoryId);
        builder.HasMany(x => x.CategoryArtworkDetails).WithOne(d => d.Category).HasForeignKey(d => d.CategoryId);

        #region init data
        builder.HasData
        (
            new Category() { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), CategoryName = "Minh hoạ", ParentCategory = null },
            new Category() { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), CategoryName = "Thiết kế đồ họa", ParentCategory = null },
            new Category() { Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), CategoryName = "UI/UX", ParentCategory = null },
            new Category() { Id = Guid.Parse("00000000-0000-0000-0000-000000000004"), CategoryName = "Kiến trúc", ParentCategory = null },
            new Category() { Id = Guid.Parse("00000000-0000-0000-0000-000000000005"), CategoryName = "Thời trang", ParentCategory = null },
            new Category() { Id = Guid.Parse("00000000-0000-0000-0000-000000000006"), CategoryName = "Đồ họa chuyển động", ParentCategory = null },
            new Category() { Id = Guid.Parse("00000000-0000-0000-0000-000000000007"), CategoryName = "In ấn", ParentCategory = null },
            new Category() { Id = Guid.Parse("00000000-0000-0000-0000-000000000008"), CategoryName = "Đồ họa 3D", ParentCategory = null },
            new Category() { Id = Guid.Parse("00000000-0000-0000-0000-000000000009"), CategoryName = "Nghệ thuật số", ParentCategory = Guid.Parse("00000000-0000-0000-0000-000000000001") },
            new Category() { Id = Guid.Parse("00000000-0000-0000-0000-00000000000a"), CategoryName = "Nhiếp ảnh", ParentCategory = null },
            new Category() { Id = Guid.Parse("00000000-0000-0000-0000-00000000000b"), CategoryName = "Thiết kế sản phẩm", ParentCategory = null },
            new Category() { Id = Guid.Parse("00000000-0000-0000-0000-00000000000c"), CategoryName = "Quảng cáo", ParentCategory = null },
            new Category() { Id = Guid.Parse("00000000-0000-0000-0000-00000000000d"), CategoryName = "Phác họa", ParentCategory = Guid.Parse("00000000-0000-0000-0000-000000000001") },
            new Category() { Id = Guid.Parse("00000000-0000-0000-0000-00000000000e"), CategoryName = "Thiết kế nhân vật", ParentCategory = Guid.Parse("00000000-0000-0000-0000-000000000001") }
        );
        #endregion
    }
}
