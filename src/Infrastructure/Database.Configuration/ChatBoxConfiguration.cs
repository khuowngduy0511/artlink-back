using Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class ChatBoxConfiguration : IEntityTypeConfiguration<ChatBox>
{
    public void Configure(EntityTypeBuilder<ChatBox> builder)
    {
        builder.ToTable(nameof(ChatBox));

        builder.Property(x => x.Id).HasDefaultValueSql("newid()");

        // relationship
        builder.HasOne(x => x.Account_1).WithMany(a => a.ChatBoxes_1)
                                        .HasForeignKey(x => x.AccountId_1)
                                        .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Account_2).WithMany(a => a.ChatBoxes_2)
                                        .HasForeignKey(x => x.AccountId_2)
                                        .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(x => x.Messages).WithOne(p => p.ChatBox).HasForeignKey(p => p.ChatBoxId);

        builder.HasData(
            new ChatBox()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                AccountId_1 = Guid.Parse("00000000-0000-0000-0000-000000000005"), // phuhuynh
                AccountId_2 = Guid.Parse("00000000-0000-0000-0000-000000000002") // lamlam
            }
        );
    }
}
