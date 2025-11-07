using Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class LicenseTypeConfiguration : IEntityTypeConfiguration<LicenseType>
{
    public void Configure(EntityTypeBuilder<LicenseType> builder)
    {
        builder.ToTable(nameof(LicenseType));
        builder.Property(x => x.Id).HasDefaultValueSql("newid()");

        // relationship
        builder.HasMany(x => x.Artworks).WithOne(a => a.LicenseType).HasForeignKey(a => a.LicenseTypeId);

        #region init data
        builder.HasData
        (
            new LicenseType()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                LicenseName = "All Rights Reserved",
                LicenseDescription = "Tất cả các quyền đều được bảo lưu. Không ai được sao chép, " +
                "phân phối hoặc sử dụng tác phẩm mà không có sự cho phép của tác giả"
            },
            new LicenseType()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                LicenseName = "Attribution (CC BY)",
                LicenseDescription = "Cho phép người dùng chia sẻ, sao chép, phân phối và trình " +
                "bày tác phẩm, cũng như tạo ra các tác phẩm phái sinh từ tác phẩm ban đầu, miễn " +
                "là họ ghi công tác phẩm của bạn một cách đúng cách"
            },
            new LicenseType()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                LicenseName = "Attribution ShareAlike (CC BY-SA)",
                LicenseDescription = "Cho phép người dùng chia sẻ, sao chép, phân phối và trình " +
                "bày tác phẩm, cũng như tạo ra các tác phẩm phái sinh từ tác phẩm ban đầu, miễn " +
                "là họ ghi công tác phẩm của bạn một cách đúng cách. Tất cả các tác phẩm phái " +
                "sinh phải được phân phối dưới cùng một giấy phép như tác phẩm ban đầu"
            },
            new LicenseType()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                LicenseName = "Attribution NoDerivs (CC BY-ND)",
                LicenseDescription = "Cho phép người dùng chia sẻ, sao chép, phân phối và trình " +
                "bày tác phẩm, miễn là họ ghi công tác phẩm của bạn một cách đúng cách. Tác phẩm " +
                "không được thay đổi hoặc tạo ra các tác phẩm phái sinh từ tác phẩm ban đầu"
            },
            new LicenseType()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                LicenseName = "Attribution-NonCommercial (CC BY-NC)",
                LicenseDescription = "Cho phép người dùng chia sẻ, sao chép, phân phối và trình " +
                "bày tác phẩm, cũng như tạo ra các tác phẩm phái sinh từ tác phẩm ban đầu, miễn " +
                "là họ ghi công tác phẩm của bạn một cách đúng cách. Tuy nhiên, họ không được " +
                "sử dụng tác phẩm với mục đích thương mại"
            },
            new LicenseType()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                LicenseName = "Attribution-NonCommercial-ShareAlike (CC BY-NC-SA)",
                LicenseDescription = "Cho phép người dùng chia sẻ, sao chép, phân phối và trình " +
                "bày tác phẩm, cũng như tạo ra các tác phẩm phái sinh từ tác phẩm ban đầu, miễn " +
                "là họ ghi công tác phẩm của bạn một cách đúng cách. Tuy nhiên, họ không được " +
                "sử dụng tác phẩm với mục đích thương mại. Tất cả các tác phẩm phái sinh phải " +
                "được phân phối dưới cùng một giấy phép như tác phẩm ban đầu"
            },
            new LicenseType()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                LicenseName = "Attribution-NonCommercial-NoDerivs (CC BY-NC-ND)",
                LicenseDescription = "Cho phép người dùng chia sẻ, sao chép, phân phối và trình " +
                "bày tác phẩm, miễn là họ ghi công tác phẩm của bạn một cách đúng cách. Tuy nhiên, " +
                "họ không được sử dụng tác phẩm với mục đích thương mại. Tác phẩm không được thay " +
                "đổi hoặc tạo ra các tác phẩm phái sinh từ tác phẩm ban đầu"
            },
            new LicenseType()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                LicenseName = "No Rights Reserved",
                LicenseDescription = "Tác phẩm được công bố dưới dạng CC0 cho phép người khác " +
                "có toàn quyền phân phối, sao chép, chỉnh sửa và xây dựng trên nội dung gốc mà " +
                "không gặp bất kỳ ràng buộc nào. Bao gồm cả việc sử dụng cho mục đích thương mại."
            }
        );
        #endregion
    }
}
