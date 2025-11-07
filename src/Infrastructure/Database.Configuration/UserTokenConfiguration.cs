using Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.ToTable(nameof(UserToken));
        builder.HasIndex(x => x.ATid).IsUnique();
        builder.HasIndex(x => x.RTid).IsUnique();

        builder.Property(x => x.IssuedDate).HasDefaultValueSql("getutcdate()");

        // relationship
        builder.HasOne(x => x.User).WithMany(u => u.UserTokens).HasForeignKey(x => x.UserId);
    }
}
