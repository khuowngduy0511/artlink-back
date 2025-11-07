using Domain.Entitites;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.ToTable(nameof(Wallet));

        builder.Property(x => x.Id).HasDefaultValueSql("newid()");

        // relationship
        builder.HasOne(x => x.Account).WithOne(a => a.Wallet).HasForeignKey<Wallet>(x => x.AccountId);

        #region init data
        builder.HasData(
            // account 1
            new Wallet // wallet of user
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                AccountId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                WithdrawMethod = WithdrawMethodEnum.Zalopay,
                WithdrawInformation = "0902287461",
                Balance = 10000
            },

            // account 2
            new Wallet // wallet of lamlam
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                AccountId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                WithdrawMethod = WithdrawMethodEnum.Zalopay,
                WithdrawInformation = "0939959417",
                Balance = 185000
            },

            // account 3
            new Wallet // wallet of hoanganh
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                AccountId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                WithdrawMethod = WithdrawMethodEnum.Zalopay,
                WithdrawInformation = "0912695680",
                Balance = 10000
            },

            // account 4
            new Wallet // wallet of thong
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                AccountId = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                WithdrawMethod = WithdrawMethodEnum.Zalopay,
                WithdrawInformation = "0365960823",
                Balance = 21000
            },

            // account 5
            new Wallet // wallet of phuhuynh
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                AccountId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                WithdrawMethod = WithdrawMethodEnum.Zalopay,
                WithdrawInformation = "0398550944",
                Balance = 670000
            },

            // account 9
            new Wallet
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                AccountId = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                WithdrawMethod = WithdrawMethodEnum.Zalopay,
                WithdrawInformation = "0365960823",
                Balance = 0
            },

            // account 10 phamthanh
            new Wallet
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000a"),
                AccountId = Guid.Parse("00000000-0000-0000-0000-00000000000a"),
                WithdrawMethod = WithdrawMethodEnum.Zalopay,
                WithdrawInformation = "0365960823",
                Balance = 142500
            },

            // account 11 melodysheep
            new Wallet
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000b"),
                AccountId = Guid.Parse("00000000-0000-0000-0000-00000000000b"),
                WithdrawMethod = WithdrawMethodEnum.Zalopay,
                WithdrawInformation = "0365960823",
                Balance = 47500
            },

            // account 12
            new Wallet
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000c"),
                AccountId = Guid.Parse("00000000-0000-0000-0000-00000000000c"),
                WithdrawMethod = WithdrawMethodEnum.Zalopay,
                WithdrawInformation = "0365960823",
                Balance = 0
            },

            // account 13
            new Wallet
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000d"),
                AccountId = Guid.Parse("00000000-0000-0000-0000-00000000000d"),
                WithdrawMethod = WithdrawMethodEnum.Zalopay,
                WithdrawInformation = "0365960823",
                Balance = 0
            },

            // account 14
            new Wallet
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000e"),
                AccountId = Guid.Parse("00000000-0000-0000-0000-00000000000e"),
                WithdrawMethod = WithdrawMethodEnum.Zalopay,
                WithdrawInformation = "0365960823",
                Balance = 0
            },

            //// account 15 hoangtuan
            //new Wallet
            //{
            //    Id = Guid.Parse("00000000-0000-0000-0000-00000000000f"),
            //    AccountId = Guid.Parse("00000000-0000-0000-0000-00000000000f"),
            //    WithdrawMethod = WithdrawMethodEnum.Zalopay,
            //    WithdrawInformation = "0365960823",
            //    Balance = 0
            //},

            // account 16
            new Wallet
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                AccountId = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                WithdrawMethod = WithdrawMethodEnum.Zalopay,
                WithdrawInformation = "0365960823",
                Balance = 0
            },

            // account 17
            new Wallet
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000011"),
                AccountId = Guid.Parse("00000000-0000-0000-0000-000000000011"),
                WithdrawMethod = WithdrawMethodEnum.Zalopay,
                WithdrawInformation = "0365960823",
                Balance = 0
            },

            // account 18 doantrang
            new Wallet
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000012"),
                AccountId = Guid.Parse("00000000-0000-0000-0000-000000000012"),
                WithdrawMethod = WithdrawMethodEnum.Zalopay,
                WithdrawInformation = "0365960823",
                Balance = 85500
            },

            // account 19
            new Wallet
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000013"),
                AccountId = Guid.Parse("00000000-0000-0000-0000-000000000013"),
                WithdrawMethod = WithdrawMethodEnum.Zalopay,
                WithdrawInformation = "0365960823",
                Balance = 0
            },

            // account 20
            new Wallet
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000014"),
                AccountId = Guid.Parse("00000000-0000-0000-0000-000000000014"),
                WithdrawMethod = WithdrawMethodEnum.Zalopay,
                WithdrawInformation = "0365960823",
                Balance = 0
            },

            // account 21
            new Wallet
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000015"),
                AccountId = Guid.Parse("00000000-0000-0000-0000-000000000015"),
                WithdrawMethod = WithdrawMethodEnum.Zalopay,
                WithdrawInformation = "0365960823",
                Balance = 0
            },

            // account 22
            new Wallet
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000016"),
                AccountId = Guid.Parse("00000000-0000-0000-0000-000000000016"),
                WithdrawMethod = WithdrawMethodEnum.Zalopay,
                WithdrawInformation = "0365960823",
                Balance = 0
            },

            // account 23
            new Wallet
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000017"),
                AccountId = Guid.Parse("00000000-0000-0000-0000-000000000017"),
                WithdrawMethod = WithdrawMethodEnum.Zalopay,
                WithdrawInformation = "0365960823",
                Balance = 0
            },

            // account 24
            new Wallet
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000018"),
                AccountId = Guid.Parse("00000000-0000-0000-0000-000000000018"),
                WithdrawMethod = WithdrawMethodEnum.Zalopay,
                WithdrawInformation = "0365960823",
                Balance = 0
            },

            // account 25
            new Wallet
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000019"),
                AccountId = Guid.Parse("00000000-0000-0000-0000-000000000019"),
                WithdrawMethod = WithdrawMethodEnum.Zalopay,
                WithdrawInformation = "0365960823",
                Balance = 0
            }
        );
        #endregion

    }
}
