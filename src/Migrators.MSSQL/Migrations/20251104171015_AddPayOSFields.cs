using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddPayOSFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "TransactionHistory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PaymentOrderCode",
                table: "TransactionHistory",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentTransactionId",
                table: "TransactionHistory",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "TransactionHistory",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "PaymentMethod", "PaymentOrderCode", "PaymentTransactionId" },
                values: new object[] { "Wallet", null, null });

            migrationBuilder.UpdateData(
                table: "TransactionHistory",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "PaymentMethod", "PaymentOrderCode", "PaymentTransactionId" },
                values: new object[] { "Wallet", null, null });

            migrationBuilder.UpdateData(
                table: "TransactionHistory",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "PaymentMethod", "PaymentOrderCode", "PaymentTransactionId" },
                values: new object[] { "Wallet", null, null });

            migrationBuilder.UpdateData(
                table: "TransactionHistory",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "PaymentMethod", "PaymentOrderCode", "PaymentTransactionId" },
                values: new object[] { "Wallet", null, null });

            migrationBuilder.UpdateData(
                table: "TransactionHistory",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "PaymentMethod", "PaymentOrderCode", "PaymentTransactionId" },
                values: new object[] { "Wallet", null, null });

            migrationBuilder.UpdateData(
                table: "TransactionHistory",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "PaymentMethod", "PaymentOrderCode", "PaymentTransactionId" },
                values: new object[] { "Wallet", null, null });

            migrationBuilder.UpdateData(
                table: "TransactionHistory",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "PaymentMethod", "PaymentOrderCode", "PaymentTransactionId" },
                values: new object[] { "Wallet", null, null });

            migrationBuilder.UpdateData(
                table: "TransactionHistory",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "PaymentMethod", "PaymentOrderCode", "PaymentTransactionId" },
                values: new object[] { "Wallet", null, null });

            migrationBuilder.UpdateData(
                table: "TransactionHistory",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "PaymentMethod", "PaymentOrderCode", "PaymentTransactionId" },
                values: new object[] { "Wallet", null, null });

            migrationBuilder.UpdateData(
                table: "TransactionHistory",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "PaymentMethod", "PaymentOrderCode", "PaymentTransactionId" },
                values: new object[] { "Wallet", null, null });

            migrationBuilder.UpdateData(
                table: "TransactionHistory",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"),
                columns: new[] { "PaymentMethod", "PaymentOrderCode", "PaymentTransactionId" },
                values: new object[] { "Wallet", null, null });

            migrationBuilder.UpdateData(
                table: "TransactionHistory",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"),
                columns: new[] { "PaymentMethod", "PaymentOrderCode", "PaymentTransactionId" },
                values: new object[] { "Wallet", null, null });

            migrationBuilder.UpdateData(
                table: "TransactionHistory",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000101"),
                columns: new[] { "PaymentMethod", "PaymentOrderCode", "PaymentTransactionId" },
                values: new object[] { "Wallet", null, null });

            migrationBuilder.UpdateData(
                table: "TransactionHistory",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000102"),
                columns: new[] { "PaymentMethod", "PaymentOrderCode", "PaymentTransactionId" },
                values: new object[] { "Wallet", null, null });

            migrationBuilder.UpdateData(
                table: "TransactionHistory",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000103"),
                columns: new[] { "PaymentMethod", "PaymentOrderCode", "PaymentTransactionId" },
                values: new object[] { "Wallet", null, null });

            migrationBuilder.UpdateData(
                table: "TransactionHistory",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000104"),
                columns: new[] { "PaymentMethod", "PaymentOrderCode", "PaymentTransactionId" },
                values: new object[] { "Wallet", null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "TransactionHistory");

            migrationBuilder.DropColumn(
                name: "PaymentOrderCode",
                table: "TransactionHistory");

            migrationBuilder.DropColumn(
                name: "PaymentTransactionId",
                table: "TransactionHistory");
        }
    }
}
