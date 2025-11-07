using Domain.Entitites;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class RequestConfiguration : IEntityTypeConfiguration<Request>
{
    public void Configure(EntityTypeBuilder<Request> builder)
    {
        builder.ToTable(nameof(Request));

        builder.Property(x => x.Id).HasDefaultValueSql("newid()");
        builder.Property(x => x.CreatedOn).HasDefaultValueSql("getutcdate()");

        // relationship
        builder.HasOne(x => x.Account).WithMany(a => a.Requests).HasForeignKey(x => x.CreatedBy);
        builder.HasOne(x => x.Service).WithMany(s => s.Requests).HasForeignKey(x => x.ServiceId);
        builder.HasOne(x => x.MessageObj).WithOne(m => m.Request).HasForeignKey<Message>(m => m.RequestId);

        builder.HasData
        (
            new Request()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                ServiceId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Message = "Yêu cầu làm website mua bán.",
                Timeline = "2 - 3 tuần",
                Budget = 69000,
                RequestStatus = StateEnum.Declined,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000005"), // phuhuynh
                CreatedOn = DateTime.Parse("2023-09-09")
            },

            new Request()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                ServiceId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Message = "Yêu cầu làm website mua bán thiệp đám cưới.",
                Timeline = "2 - 3 tuần",
                Budget = 69000,
                RequestStatus = StateEnum.Accepted,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000005"), // phuhuynh
                CreatedOn = DateTime.Parse("2023-09-14")
            }
        );
    }
}
