using Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable(nameof(Message));

        builder.Property(x => x.Id).HasDefaultValueSql("newid()");
        builder.Property(x => x.CreatedOn).HasDefaultValueSql("getutcdate()");

        // relationship
        builder.HasOne(x => x.Account).WithMany(a => a.Messages).HasForeignKey(x => x.CreatedBy);
        builder.HasOne(x => x.ChatBox).WithMany(a => a.Messages).HasForeignKey(x => x.ChatBoxId);
        builder.HasOne(x => x.Request).WithOne(r => r.MessageObj).HasForeignKey<Message>(m => m.RequestId);
        builder.HasOne(x => x.Proposal).WithOne(r => r.MessageObj).HasForeignKey<Message>(m => m.ProposalId);


        #region init data
        builder.HasData(
            new Message()  // request
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000a"),
                ChatBoxId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000005"), // phuhuynh
                CreatedOn = DateTime.Parse("2023-09-09"),
                RequestId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },
            new Message()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                ChatBoxId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                CreatedOn = DateTime.Parse("2023-09-10"),
                Text = "Chào bạn"
            },
            new Message()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                ChatBoxId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                CreatedOn = DateTime.Parse("2023-09-11"),
                Text = "Mình muốn bạn làm website mua bán"
            },
            new Message()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                ChatBoxId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                CreatedOn = DateTime.Parse("2023-09-12"),
                Text = "Nếu được hãy chấp nhận nhé"
            },
            new Message()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                ChatBoxId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                CreatedOn = DateTime.Parse("2023-09-13"),
                Text = "Yêu cầu khác đi"
            },
            new Message() // request
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000b"),
                ChatBoxId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000005"), // phuhuynh
                CreatedOn = DateTime.Parse("2023-09-14"),
                RequestId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },
            new Message()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                ChatBoxId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                CreatedOn = DateTime.Parse("2023-09-15"),
                Text = "Vầy được chưa?"
            },
            new Message()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                ChatBoxId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                CreatedOn = DateTime.Parse("2023-09-16"),
                Text = "OK, mình muốn 100k, giá vậy được ko?"
            },
            new Message()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                ChatBoxId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                CreatedOn = DateTime.Parse("2023-09-17"),
                Text = "80k được ko?"
            },
            new Message()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                ChatBoxId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                CreatedOn = DateTime.Parse("2023-09-18"),
                Text = "Thôi cũng được, bạn muốn khi nào xong?"
            },
            new Message()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                ChatBoxId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                CreatedOn = DateTime.Parse("2023-09-19"),
                Text = "Cuối tháng 1"
            },
            new Message()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                ChatBoxId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                CreatedOn = DateTime.Parse("2023-09-20"),
                Text = "Để mình tạo thỏa thuận"
            },
            new Message() // proposal
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000011"),
                ChatBoxId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000002"), //lamlam
                CreatedOn = DateTime.Parse("2023-12-31"),
                ProposalId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },
            new Message()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000012"),
                ChatBoxId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                CreatedOn = DateTime.Parse("2024-01-01"),
                Text = "ok mình làm đây"
            }
        ); ;
        #endregion
    }
}
