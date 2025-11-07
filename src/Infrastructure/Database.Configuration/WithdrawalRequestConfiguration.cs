using Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class WithdrawalRequestConfiguration : IEntityTypeConfiguration<WithdrawalRequest>
{
    public void Configure(EntityTypeBuilder<WithdrawalRequest> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Wallet)
            .WithMany()
            .HasForeignKey(x => x.WalletId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Amount)
            .IsRequired();

        builder.Property(x => x.BankCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.BankName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.BankAccountNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.BankAccountName)
            .HasMaxLength(200);

        builder.Property(x => x.Status)
            .IsRequired()
            .HasMaxLength(20)
            .HasDefaultValue("Pending");

        builder.Property(x => x.AdminNote)
            .HasMaxLength(500);

        builder.Property(x => x.WalletBalanceSnapshot)
            .IsRequired();

        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.CreatedOn);
    }
}
