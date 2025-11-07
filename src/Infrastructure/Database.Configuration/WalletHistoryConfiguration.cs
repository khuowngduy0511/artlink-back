using Domain.Entitites;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class WalletHistoryConfiguration : IEntityTypeConfiguration<WalletHistory>
{
    public void Configure(EntityTypeBuilder<WalletHistory> builder)
    {
        builder.ToTable(nameof(WalletHistory));

        builder.Property(x => x.Id).HasDefaultValueSql("newid()");
        builder.Property(x => x.CreatedOn).HasDefaultValueSql("getutcdate()");
        builder.HasIndex(x => x.AppTransId).IsUnique();

        // relationship
        builder.HasOne(x => x.Account).WithMany(w => w.WalletHistories).HasForeignKey(x => x.CreatedBy);

        #region init data
        builder.HasData
        (
            // lamlam histories
            new WalletHistory
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000002"), // lamlam
                PaymentMethod = PaymentMethodEnum.ZaloPay,
                Type = WalletHistoryTypeEnum.Deposit,
                Amount = 100000,
                TransactionStatus = TransactionStatusEnum.Success,
                CreatedOn = DateTime.Parse("2023-11-07T08:30:03.678Z"),
                AppTransId = "240128_7635981",
                WalletBalance = 100000
            },
            new WalletHistory
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000002"), // lamlam
                PaymentMethod = PaymentMethodEnum.ZaloPay,
                Type = WalletHistoryTypeEnum.Withdraw,
                Amount = -50000,
                TransactionStatus = TransactionStatusEnum.Success,
                CreatedOn = DateTime.Parse("2023-11-10T14:20:10.234Z"),
                AppTransId = "180623_2054176",
                WalletBalance = 50000
            },
            new WalletHistory
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000002"), // lamlam
                PaymentMethod = PaymentMethodEnum.ZaloPay,
                Type = WalletHistoryTypeEnum.Deposit,
                Amount = 60000,
                TransactionStatus = TransactionStatusEnum.Success,
                CreatedOn = DateTime.Parse("2023-11-13T19:59:59.000Z"),
                AppTransId = "210430_6849203",
                WalletBalance = 110000
            },

            // hoanganh histories
            new WalletHistory
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000003"), // hoanganh
                PaymentMethod = PaymentMethodEnum.ZaloPay,
                Type = WalletHistoryTypeEnum.Deposit,
                Amount = 10000,
                TransactionStatus = TransactionStatusEnum.Success,
                CreatedOn = DateTime.Parse("2023-11-16T02:37:42.345Z"),
                AppTransId = "220506_1478963",
                WalletBalance = 10000
            },

            // thong histories
            new WalletHistory
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000004"), // thong
                PaymentMethod = PaymentMethodEnum.ZaloPay,
                Type = WalletHistoryTypeEnum.Deposit,
                Amount = 100000,
                TransactionStatus = TransactionStatusEnum.Success,
                CreatedOn = DateTime.Parse("2024-01-01T05:40:28.901Z"),
                AppTransId = "171212_4357692",
                WalletBalance = 100000
            },
            new WalletHistory
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000004"), // thong
                PaymentMethod = PaymentMethodEnum.ZaloPay,
                Type = WalletHistoryTypeEnum.Withdraw,
                Amount = -80000,
                TransactionStatus = TransactionStatusEnum.Success,
                CreatedOn = DateTime.Parse("2024-01-03T18:30:15.567Z"),
                AppTransId = "160509_9270134",
                WalletBalance = 20000
            },
            new WalletHistory
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000a"),
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000004"), // thong
                PaymentMethod = PaymentMethodEnum.ZaloPay,
                Type = WalletHistoryTypeEnum.Deposit,
                Amount = 42000,
                TransactionStatus = TransactionStatusEnum.Success,
                CreatedOn = DateTime.Parse("2024-01-04T05:40:28.901Z"),
                AppTransId = "250321_4685027",
                WalletBalance = 62000
            },

            // phuhuynh histories
            new WalletHistory
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000b"),
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000005"), // phuhuynh
                PaymentMethod = PaymentMethodEnum.ZaloPay,
                Type = WalletHistoryTypeEnum.Deposit,
                Amount = 1000000,
                TransactionStatus = TransactionStatusEnum.Success,
                CreatedOn = DateTime.Parse("2023-12-20T05:37:42.345Z"),
                AppTransId = "231205_7890123",
                WalletBalance = 1000000
            },           

            // user histories
            new WalletHistory
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000e"),
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000001"), //user
                PaymentMethod = PaymentMethodEnum.ZaloPay,
                Type = WalletHistoryTypeEnum.Deposit,
                Amount = 10000,
                TransactionStatus = TransactionStatusEnum.Success,
                CreatedOn = DateTime.Parse("2023-11-03T10:45:20.123Z"),
                AppTransId = "210817_8901234",
                WalletBalance = 10000
            }          
        );
        #endregion

    }
}
