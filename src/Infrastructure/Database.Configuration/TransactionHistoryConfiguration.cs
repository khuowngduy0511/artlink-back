using Domain.Entitites;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class TransactionHistoryConfiguration : IEntityTypeConfiguration<TransactionHistory>
{
    public void Configure(EntityTypeBuilder<TransactionHistory> builder)
    {
        builder.ToTable(nameof(TransactionHistory));

        builder.Property(x => x.Id).HasDefaultValueSql("newid()");
        builder.Property(x => x.CreatedOn).HasDefaultValueSql("getutcdate()");

        // relationship
        builder.HasOne(x => x.Account).WithMany(a => a.TransactionHistories).HasForeignKey(x => x.CreatedBy);
        builder.HasOne(x => x.ToAccount).WithMany(a => a.TransactionHistoriesReceivedCoins).HasForeignKey(x => x.ToAccountId);
        builder.HasOne(x => x.Asset).WithMany(a => a.TransactionHistories).HasForeignKey(x => x.AssetId);
        builder.HasOne(x => x.Proposal).WithMany(p => p.TransactionHistories).HasForeignKey(x => x.ProposalId);

        #region Init data
        builder.HasData
        (
        #region asset
            // transaction 1 phu -> phamthanh
            new TransactionHistory
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                AssetId = new Guid("00000000-0000-0000-0000-000000000083"),
                CreatedBy = new Guid("00000000-0000-0000-0000-000000000005"), // phu
                Detail = "Mở khóa tài nguyên \"Pixel Art GUI / UI Kit + 151 icons!\"",
                Price = -150000,
                TransactionStatus = TransactionStatusEnum.Success,
                CreatedOn = DateTime.Parse("2023-12-29T15:30:03.678Z"),
                ToAccountId = Guid.Parse("00000000-0000-0000-0000-00000000000a"), // phamthanh
                Fee = 0,
                WalletBalance = 850000
            },

            // transaction 2
            new TransactionHistory
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                AssetId = new Guid("00000000-0000-0000-0000-000000000083"),
                CreatedBy = new Guid("00000000-0000-0000-0000-00000000000a"), // phamthanh
                Detail = "Mở khóa tài nguyên \"Pixel Art GUI / UI Kit + 151 icons!\"",
                Price = 142500,
                TransactionStatus = TransactionStatusEnum.Success,
                CreatedOn = DateTime.Parse("2023-12-29T15:30:03.678Z"),
                ToAccountId = Guid.Parse("00000000-0000-0000-0000-000000000005"), // phu
                WalletBalance = 142500,
                Fee = 7500
            },

            // transaction 2 thong -> lamlam
            new TransactionHistory
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                AssetId = new Guid("00000000-0000-0000-0000-000000000001"),
                CreatedBy = new Guid("00000000-0000-0000-0000-000000000004"), // thong
                Detail = "Mở khóa tài nguyên \"Dấu vết của quá khứ\"",
                Price = -10000,
                TransactionStatus = TransactionStatusEnum.Success,
                CreatedOn = DateTime.Parse("2024-01-16T15:30:03.678Z"),
                ToAccountId = Guid.Parse("00000000-0000-0000-0000-000000000002"),  //lamlam
                WalletBalance = 71000,
                Fee = 0
            },            
            new TransactionHistory
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                AssetId = new Guid("00000000-0000-0000-0000-000000000001"),
                CreatedBy = new Guid("00000000-0000-0000-0000-000000000002"), // lamlam
                Detail = "Mở khóa tài nguyên \"Dấu vết của quá khứ\"",
                Price = 9500,
                TransactionStatus = TransactionStatusEnum.Success,
                CreatedOn = DateTime.Parse("2024-01-16T15:30:03.678Z"),
                ToAccountId = Guid.Parse("00000000-0000-0000-0000-000000000004"),  //thong
                WalletBalance = 128000,
                Fee = 500
            },
            // transaction 3 lamlam -> thong
            new TransactionHistory
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                AssetId = new Guid("00000000-0000-0000-0000-000000000010"), 
                CreatedBy = new Guid("00000000-0000-0000-0000-000000000002"), // lamlam
                Detail = "Mở khóa tài nguyên \"File PTS tuyển tập minh hoạ sách tâm lý\"",
                Price = -20000,
                TransactionStatus = TransactionStatusEnum.Success,
                CreatedOn = DateTime.Parse("2024-01-15T02:59:59.000Z"),
                ToAccountId = Guid.Parse("00000000-0000-0000-0000-000000000004"), // thong
                WalletBalance = 118500
            },
            new TransactionHistory
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                AssetId = new Guid("00000000-0000-0000-0000-000000000010"),
                CreatedBy = new Guid("00000000-0000-0000-0000-000000000004"), // thong
                Detail = "Mở khóa tài nguyên \"File PTS tuyển tập minh hoạ sách tâm lý\"",
                Price = 19000,
                TransactionStatus = TransactionStatusEnum.Success,
                CreatedOn = DateTime.Parse("2024-01-15T02:59:59.000Z"),
                ToAccountId = Guid.Parse("00000000-0000-0000-0000-000000000002"), // lamlam
                WalletBalance = 81000,
                Fee = 1000
            },
            // transaction 4 thong -> melodysheep
            new TransactionHistory
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                AssetId = new Guid("00000000-0000-0000-0000-000000000050"), 
                CreatedBy = new Guid("00000000-0000-0000-0000-000000000004"), // thong
                Detail = "Mở khóa tài nguyên \"Không gian vũ trụ\"",
                Price = -50000,
                TransactionStatus = TransactionStatusEnum.Success,
                CreatedOn = DateTime.Parse("2024-01-16T15:30:03.678Z"),
                ToAccountId = Guid.Parse("00000000-0000-0000-0000-00000000000b"), //melodysheep
                WalletBalance = 21000,
                Fee = 0
            },
            new TransactionHistory
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                AssetId = new Guid("00000000-0000-0000-0000-000000000050"),
                CreatedBy = new Guid("00000000-0000-0000-0000-00000000000b"), // melodysheep
                Detail = "Mở khóa tài nguyên \"Không gian vũ trụ\"",
                Price = 47500,
                TransactionStatus = TransactionStatusEnum.Success,
                CreatedOn = DateTime.Parse("2024-01-16T15:30:03.678Z"),
                ToAccountId = Guid.Parse("00000000-0000-0000-0000-000000000004"), //thong
                WalletBalance = 47500,
                Fee = 2500
            },
            // transaction 5 phu -> lamlam
            new TransactionHistory
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                AssetId = new Guid("00000000-0000-0000-0000-000000000001"), 
                CreatedBy = new Guid("00000000-0000-0000-0000-000000000005"), // phu
                Detail = "Mở khóa tài nguyên \"Dấu vết của quá khứ\"",
                Price = -10000,
                TransactionStatus = TransactionStatusEnum.Success,
                CreatedOn = DateTime.Parse("2024-01-01T15:30:03.678Z"),
                ToAccountId = Guid.Parse("00000000-0000-0000-0000-000000000002"), //lamlam
                WalletBalance = 840000,
                Fee = 0
            },

            new TransactionHistory
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                AssetId = new Guid("00000000-0000-0000-0000-000000000001"),
                CreatedBy = new Guid("00000000-0000-0000-0000-000000000002"), // lamlam
                Detail = "Mở khóa tài nguyên \"Dấu vết của quá khứ\"",
                Price = 9500,
                TransactionStatus = TransactionStatusEnum.Success,
                CreatedOn = DateTime.Parse("2024-01-01T15:30:03.678Z"),
                ToAccountId = Guid.Parse("00000000-0000-0000-0000-000000000005"), // phu
                WalletBalance = 119500,
                Fee = 500
            },

            // transaction 6 phu -> lamlam
            new TransactionHistory
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000011"),
                AssetId = new Guid("00000000-0000-0000-0000-000000000080"),
                CreatedBy = new Guid("00000000-0000-0000-0000-000000000005"), // phu
                Detail = "Mở khóa tài nguyên \"Figma Full\"",
                Price = -90000,
                TransactionStatus = TransactionStatusEnum.Success,
                CreatedOn = DateTime.Parse("2024-01-02T15:30:03.678Z"),
                ToAccountId = Guid.Parse("00000000-0000-0000-0000-000000000012"), //doantrang
                WalletBalance = 750000,
                Fee = 0
            },
            new TransactionHistory
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000012"),
                AssetId = new Guid("00000000-0000-0000-0000-000000000080"),
                CreatedBy = new Guid("00000000-0000-0000-0000-000000000012"), // doantrang
                Detail = "Mở khóa tài nguyên \"Figma Full\"",
                Price = 85500,
                TransactionStatus = TransactionStatusEnum.Success,
                CreatedOn = DateTime.Parse("2024-01-02T15:30:03.678Z"),
                ToAccountId = Guid.Parse("00000000-0000-0000-0000-000000000005"), // phu
                WalletBalance = 85500,
                Fee = 4500
            },



        #endregion


        #region proposal
            // init transaction
            new TransactionHistory
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000101"),
                ProposalId = new Guid("00000000-0000-0000-0000-000000000001"),
                CreatedBy = new Guid("00000000-0000-0000-0000-000000000005"), // phuhuynh
                Detail = "Đặt cọc thỏa thuận \"Làm web thiệp cưới\" (25%)",
                Price = -20000,
                TransactionStatus = TransactionStatusEnum.Success,
                CreatedOn = DateTime.Parse("2024-01-03T02:59:59.000Z"),
                ToAccountId = Guid.Parse("00000000-0000-0000-0000-000000000002"),  // lamlam
                WalletBalance = 730000,
                Fee = 0
            },
            new TransactionHistory
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000102"),
                ProposalId = new Guid("00000000-0000-0000-0000-000000000001"),
                CreatedBy = new Guid("00000000-0000-0000-0000-000000000002"), // lamlam
                Detail = "Đặt cọc thỏa thuận \"Làm web thiệp cưới\" (25%)",
                Price = 19000,
                TransactionStatus = TransactionStatusEnum.Success,
                CreatedOn = DateTime.Parse("2024-01-03T02:59:59.000Z"),
                ToAccountId = Guid.Parse("00000000-0000-0000-0000-000000000005"),  // phuhuynh
                WalletBalance = 138500,
                Fee = 1000
            },
            // complete transaction
            new TransactionHistory
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000103"),
                ProposalId = new Guid("00000000-0000-0000-0000-000000000001"),
                CreatedBy = new Guid("00000000-0000-0000-0000-000000000005"), // phuhuynh
                Detail = "Hoàn tất thanh toán thỏa thuận \"Làm web thiệp cưới\" (75%)",
                Price = -60000,
                TransactionStatus = TransactionStatusEnum.Success,
                CreatedOn = DateTime.Parse("2024-02-01T02:59:59.000Z"),
                ToAccountId = Guid.Parse("00000000-0000-0000-0000-000000000002"),  // lamlam
                WalletBalance = 670000,
                Fee = 0
            },
             new TransactionHistory
             {
                 Id = Guid.Parse("00000000-0000-0000-0000-000000000104"),
                 ProposalId = new Guid("00000000-0000-0000-0000-000000000001"),
                 CreatedBy = new Guid("00000000-0000-0000-0000-000000000002"), // lamlam
                 Detail = "Hoàn tất thanh toán thỏa thuận \"Làm web thiệp cưới\" (75%)",
                 Price = 57000,
                 TransactionStatus = TransactionStatusEnum.Success,
                 CreatedOn = DateTime.Parse("2024-02-01T02:59:59.000Z"),
                 ToAccountId = Guid.Parse("00000000-0000-0000-0000-000000000005"),  // phuhuynh
                 WalletBalance = 185000,
                 Fee = 3000
             }
             #endregion
        );
        #endregion
    }
}
