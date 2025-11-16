using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Migrators.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Username = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Fullname = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Bio = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Avatar = table.Column<string>(type: "text", nullable: true),
                    Birthdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    ArtisticStyle = table.Column<string>(type: "text", nullable: true),
                    VerifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    LastModificatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    CategoryName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ParentCategory = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Category_ParentCategory",
                        column: x => x.ParentCategory,
                        principalTable: "Category",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LicenseType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    LicenseName = table.Column<string>(type: "text", nullable: false),
                    LicenseDescription = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SoftwareUsed",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    SoftwareName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftwareUsed", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    TagName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArtistCertificate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    Certificatename = table.Column<string>(type: "text", nullable: false),
                    CertificateUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistCertificate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArtistCertificate_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Block",
                columns: table => new
                {
                    BlockingId = table.Column<Guid>(type: "uuid", nullable: false),
                    BlockedId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Block", x => new { x.BlockingId, x.BlockedId });
                    table.ForeignKey(
                        name: "FK_Block_Account_BlockedId",
                        column: x => x.BlockedId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Block_Account_BlockingId",
                        column: x => x.BlockingId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatBox",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    AccountId_1 = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountId_2 = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatBox", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatBox_Account_AccountId_1",
                        column: x => x.AccountId_1,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChatBox_Account_AccountId_2",
                        column: x => x.AccountId_2,
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Collection",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    CollectionName = table.Column<string>(type: "text", nullable: false),
                    Privacy = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Collection_Account_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Follow",
                columns: table => new
                {
                    FollowingId = table.Column<Guid>(type: "uuid", nullable: false),
                    FollowedId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Follow", x => new { x.FollowingId, x.FollowedId });
                    table.ForeignKey(
                        name: "FK_Follow_Account_FollowedId",
                        column: x => x.FollowedId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Follow_Account_FollowingId",
                        column: x => x.FollowingId,
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    SentToAccount = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    NotifyType = table.Column<int>(type: "integer", nullable: false),
                    IsSeen = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    ReferencedArtworkId = table.Column<Guid>(type: "uuid", nullable: true),
                    ReferencedAccountId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_Account_SentToAccount",
                        column: x => x.SentToAccount,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ReportType = table.Column<int>(type: "integer", nullable: false),
                    Reason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ReportEntity = table.Column<int>(type: "integer", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    TargetId = table.Column<Guid>(type: "uuid", nullable: false),
                    Note = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Report_Account_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ServiceName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    DeliveryTime = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    NumberOfConcept = table.Column<int>(type: "integer", nullable: false),
                    NumberOfRevision = table.Column<int>(type: "integer", nullable: false),
                    StartingPrice = table.Column<double>(type: "double precision", nullable: false),
                    Thumbnail = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    LastModificatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Service_Account_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ATid = table.Column<Guid>(type: "uuid", nullable: false),
                    AccessToken = table.Column<string>(type: "text", nullable: false),
                    RTid = table.Column<Guid>(type: "uuid", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: false),
                    IsUsed = table.Column<bool>(type: "boolean", nullable: false),
                    IsRevoked = table.Column<bool>(type: "boolean", nullable: false),
                    IssuedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    ExpiredDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToken_Account_UserId",
                        column: x => x.UserId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wallet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    Balance = table.Column<double>(type: "double precision", nullable: false),
                    WithdrawMethod = table.Column<int>(type: "integer", nullable: false),
                    WithdrawInformation = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    BankCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    BankName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallet_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WalletHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    WalletBalance = table.Column<double>(type: "double precision", nullable: false),
                    Fee = table.Column<double>(type: "double precision", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    PaymentMethod = table.Column<int>(type: "integer", nullable: false),
                    TransactionStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    AppTransId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalletHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WalletHistory_Account_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Artwork",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Title = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    Thumbnail = table.Column<string>(type: "text", nullable: false),
                    ThumbnailName = table.Column<string>(type: "text", nullable: false),
                    ViewCount = table.Column<int>(type: "integer", nullable: false),
                    LikeCount = table.Column<int>(type: "integer", nullable: false),
                    CommentCount = table.Column<int>(type: "integer", nullable: false),
                    Privacy = table.Column<int>(type: "integer", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    IsAIGenerated = table.Column<bool>(type: "boolean", nullable: false),
                    Note = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    LastModificatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LicenseTypeId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artwork", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artwork_Account_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Artwork_LicenseType_LicenseTypeId",
                        column: x => x.LicenseTypeId,
                        principalTable: "LicenseType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CategoryServiceDetail",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryServiceDetail", x => new { x.CategoryId, x.ServiceId });
                    table.ForeignKey(
                        name: "FK_CategoryServiceDetail_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryServiceDetail_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proposal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ServiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectTitle = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Category = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    TargetDelivery = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ActualDelivery = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    NumberOfConcept = table.Column<int>(type: "integer", nullable: false),
                    NumberOfRevision = table.Column<int>(type: "integer", nullable: false),
                    InitialPrice = table.Column<double>(type: "double precision", nullable: false),
                    Total = table.Column<double>(type: "double precision", nullable: false),
                    ProposalStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    OrdererId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proposal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proposal_Account_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Proposal_Account_OrdererId",
                        column: x => x.OrdererId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Proposal_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ServiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Message = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Timeline = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Budget = table.Column<double>(type: "double precision", nullable: false),
                    RequestStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Request_Account_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Request_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WithdrawalRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WalletId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    BankCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BankName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    BankAccountNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    BankAccountName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValue: "Pending"),
                    AdminNote = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ProcessedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ProcessedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    WalletBalanceSnapshot = table.Column<double>(type: "double precision", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WithdrawalRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WithdrawalRequests_Wallet_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Asset",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ArtworkId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssetTitle = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    AssetName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    ContentType = table.Column<string>(type: "text", nullable: false),
                    Size = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    LastModificatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asset", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Asset_Artwork_ArtworkId",
                        column: x => x.ArtworkId,
                        principalTable: "Artwork",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookmark",
                columns: table => new
                {
                    ArtworkId = table.Column<Guid>(type: "uuid", nullable: false),
                    CollectionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookmark", x => new { x.ArtworkId, x.CollectionId });
                    table.ForeignKey(
                        name: "FK_Bookmark_Artwork_ArtworkId",
                        column: x => x.ArtworkId,
                        principalTable: "Artwork",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookmark_Collection_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryArtworkDetail",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    ArtworkId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryArtworkDetail", x => new { x.CategoryId, x.ArtworkId });
                    table.ForeignKey(
                        name: "FK_CategoryArtworkDetail_Artwork_ArtworkId",
                        column: x => x.ArtworkId,
                        principalTable: "Artwork",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryArtworkDetail_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ArtworkId = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    ReplyId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    LastModificatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_Account_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comment_Artwork_ArtworkId",
                        column: x => x.ArtworkId,
                        principalTable: "Artwork",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_Comment_ReplyId",
                        column: x => x.ReplyId,
                        principalTable: "Comment",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ArtworkId = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    ImageHash = table.Column<decimal>(type: "numeric(20,0)", nullable: true),
                    LastModificatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_Artwork_ArtworkId",
                        column: x => x.ArtworkId,
                        principalTable: "Artwork",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Like",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    ArtworkId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Like", x => new { x.AccountId, x.ArtworkId });
                    table.ForeignKey(
                        name: "FK_Like_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Like_Artwork_ArtworkId",
                        column: x => x.ArtworkId,
                        principalTable: "Artwork",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceDetail",
                columns: table => new
                {
                    ServiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ArtworkId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceDetail", x => new { x.ServiceId, x.ArtworkId });
                    table.ForeignKey(
                        name: "FK_ServiceDetail_Artwork_ArtworkId",
                        column: x => x.ArtworkId,
                        principalTable: "Artwork",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceDetail_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoftwareDetail",
                columns: table => new
                {
                    SoftwareUsedId = table.Column<Guid>(type: "uuid", nullable: false),
                    ArtworkId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftwareDetail", x => new { x.SoftwareUsedId, x.ArtworkId });
                    table.ForeignKey(
                        name: "FK_SoftwareDetail_Artwork_ArtworkId",
                        column: x => x.ArtworkId,
                        principalTable: "Artwork",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SoftwareDetail_SoftwareUsed_SoftwareUsedId",
                        column: x => x.SoftwareUsedId,
                        principalTable: "SoftwareUsed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagDetail",
                columns: table => new
                {
                    TagId = table.Column<Guid>(type: "uuid", nullable: false),
                    ArtworkId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagDetail", x => new { x.TagId, x.ArtworkId });
                    table.ForeignKey(
                        name: "FK_TagDetail_Artwork_ArtworkId",
                        column: x => x.ArtworkId,
                        principalTable: "Artwork",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagDetail_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Milestone",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ProposalId = table.Column<Guid>(type: "uuid", nullable: false),
                    MilestoneName = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Milestone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Milestone_Account_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Milestone_Proposal_ProposalId",
                        column: x => x.ProposalId,
                        principalTable: "Proposal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProposalAsset",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ProposalId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    ProposalAssetName = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    ContentType = table.Column<string>(type: "text", nullable: false),
                    Size = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProposalAsset", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProposalAsset_Proposal_ProposalId",
                        column: x => x.ProposalId,
                        principalTable: "Proposal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ProposalId = table.Column<Guid>(type: "uuid", nullable: false),
                    Vote = table.Column<double>(type: "double precision", nullable: false),
                    Detail = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Review_Account_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Review_Proposal_ProposalId",
                        column: x => x.ProposalId,
                        principalTable: "Proposal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ChatBoxId = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    FileLocation = table.Column<string>(type: "text", nullable: true),
                    FileName = table.Column<string>(type: "text", nullable: true),
                    RequestId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProposalId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_Account_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Message_ChatBox_ChatBoxId",
                        column: x => x.ChatBoxId,
                        principalTable: "ChatBox",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Message_Proposal_ProposalId",
                        column: x => x.ProposalId,
                        principalTable: "Proposal",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Message_Request_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Request",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TransactionHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ToAccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    AssetId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProposalId = table.Column<Guid>(type: "uuid", nullable: true),
                    Detail = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    WalletBalance = table.Column<double>(type: "double precision", nullable: false),
                    Fee = table.Column<double>(type: "double precision", nullable: false),
                    TransactionStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    PaymentMethod = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    PaymentOrderCode = table.Column<long>(type: "bigint", nullable: true),
                    PaymentTransactionId = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionHistory_Account_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransactionHistory_Account_ToAccountId",
                        column: x => x.ToAccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransactionHistory_Asset_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Asset",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransactionHistory_Proposal_ProposalId",
                        column: x => x.ProposalId,
                        principalTable: "Proposal",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "Address", "ArtisticStyle", "Avatar", "Bio", "Birthdate", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "Email", "Fullname", "LastModificatedBy", "Password", "Role", "Username", "VerifiedOn" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), null, null, "https://i.pinimg.com/564x/ed/de/aa/eddeaaf250c19489e25bd0a2dd3e7756.jpg", "Tôi là người dùng, không có gì đặc biệt", new DateTime(2000, 10, 14, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2023, 9, 14, 12, 37, 42, 345, DateTimeKind.Utc), null, null, "user@example.com", "Nguyễn Văn A", null, "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", 2, "user", null },
                    { new Guid("00000000-0000-0000-0000-000000000002"), null, null, "https://i.pinimg.com/564x/be/85/2f/be852fd4ad1cb76b83ce962f618895bd.jpg", "Tôi là Trúc Lam Võ, tôi là một nghệ sĩ đầy tài năng", new DateTime(2002, 10, 4, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2023, 9, 15, 17, 15, 47, 890, DateTimeKind.Utc), null, null, "lamlam@example.com", "Trúc Lam Võ", null, "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", 2, "lamlam", new DateTime(2023, 10, 15, 17, 15, 47, 890, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0000-000000000003"), null, null, "https://i.pinimg.com/564x/db/02/67/db02679d039a230d9a37caec679d1b3b.jpg", "Tôi là Đặng Hoàng Anh, tôi là một nghệ sĩ đầy tài năng", new DateTime(2002, 10, 4, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2023, 9, 21, 19, 20, 47, 890, DateTimeKind.Utc), null, null, "anhdhse160846@fpt.edu.vn", "Đặng Hoàng Anh", null, "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", 2, "hoanganh", null },
                    { new Guid("00000000-0000-0000-0000-000000000004"), null, null, "https://i.pinimg.com/564x/6c/a3/4b/6ca34beddfbd279418c915d2258d698b.jpg", "Tôi là Nguyễn Trung Thông, tôi là một nghệ sĩ đầy tài năng", new DateTime(2002, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2023, 9, 27, 19, 23, 47, 890, DateTimeKind.Utc), null, null, "thong@example.com", "Nguyễn Trung Thông", null, "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", 2, "thong", null },
                    { new Guid("00000000-0000-0000-0000-000000000005"), null, null, "https://i.pinimg.com/736x/81/3c/57/813c57fcb969d58fac1672594da05532.jpg", "Tôi là Huỳnh Vạn Phú, tôi là một nghệ sĩ đầy tài năng", new DateTime(2002, 5, 6, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2023, 9, 30, 10, 21, 47, 890, DateTimeKind.Utc), null, null, "phuhuynh923@gmail.com", "Huỳnh Vạn Phú", null, "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", 2, "phuhuynh", new DateTime(2023, 10, 16, 17, 15, 47, 890, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0000-0000-000000000006"), null, null, "https://i.pinimg.com/564x/7d/cd/61/7dcd61988b0add83b5ba9a656512593e.jpg", "Tôi là kiểm soát viên hệ thống", new DateTime(2001, 10, 4, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2023, 9, 14, 12, 37, 42, 345, DateTimeKind.Utc), null, null, "mod@example.com", "Kiểm soát viên", null, "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", 1, "mod", null },
                    { new Guid("00000000-0000-0000-0000-000000000007"), null, null, "https://i.pinimg.com/564x/0e/4b/7a/0e4b7aef4834bfc646775d8fd3705825.jpg", "Tôi là quản trị viên hệ thống", new DateTime(2000, 10, 4, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2023, 9, 2, 10, 21, 47, 890, DateTimeKind.Utc), null, null, "admin@example.com", "Quản trị viên hệ thống", null, "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", 0, "admin", null },
                    { new Guid("00000000-0000-0000-0000-000000000008"), null, null, "https://i.pinimg.com/564x/79/ba/4f/79ba4f6c73168efb975a2d43cc4272a3.jpg", "Tôi là một thiết kế UI/UX tài năng, đã có nhiều dự án thành công với các công ty lớn, cũng là người sáng lập một công ty thiết kế đồ họa.", new DateTime(2002, 10, 12, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2023, 9, 2, 10, 21, 47, 890, DateTimeKind.Utc), null, null, "trieuhan282@gmail.com", "Nguyễn Hoàng", null, "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", 2, "nguyenhoang", null },
                    { new Guid("00000000-0000-0000-0000-000000000009"), null, null, "https://i.pinimg.com/originals/4b/59/75/4b5975b01115a778a6d8016bf4d0ddc7.jpg", "Tôi là một nhà thiết kế web có kinh nghiệm, đã tham gia vào nhiều dự án phức tạp và mang lại sự sáng tạo đặc biệt.", new DateTime(2002, 10, 24, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2023, 9, 2, 10, 21, 47, 890, DateTimeKind.Utc), null, null, "tranminh@example.com", "Trần Minh", null, "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", 2, "tranminh", null },
                    { new Guid("00000000-0000-0000-0000-00000000000a"), null, null, "https://i.pinimg.com/564x/62/4a/2f/624a2fda3e0da8e55b4ea60b0949affa.jpg", "Tôi là một thiết kế 2D và 3D, đã tạo ra nhiều tác phẩm ấn tượng trong lĩnh vực phim hoạt hình và trò chơi điện tử.", new DateTime(2000, 10, 5, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2023, 9, 2, 10, 21, 47, 890, DateTimeKind.Utc), null, null, "phamthanh@example.com", "Phạm Thanh", null, "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", 2, "phamthanh", null },
                    { new Guid("00000000-0000-0000-0000-00000000000b"), null, null, "https://yt3.googleusercontent.com/ytc/AIdro_klHVaP6_ZcnT8VyPFedRHgJOPOym_tLSxoFCL0KJSZL1k=s900-c-k-c0x00ffffff-no-rj", "Tôi là một họa sĩ chuyên về tranh kỹ thuật số, đã có nhiều triển lãm cá nhân và tham gia vào dự án nghệ thuật trên toàn thế giới.", new DateTime(2001, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2023, 9, 2, 10, 21, 47, 890, DateTimeKind.Utc), null, null, "ngothanhtu@example.com", "Ngô Thanh Tú", null, "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", 2, "melodysheep", null },
                    { new Guid("00000000-0000-0000-0000-00000000000c"), null, null, "https://i.pinimg.com/564x/ad/c2/95/adc2953d7533371d1cdb95303d70babe.jpg", "Tôi là một thiết kế đồ họa sáng tạo, đã tham gia vào nhiều dự án quảng cáo và branding cho các thương hiệu lớn.", new DateTime(2002, 7, 2, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2023, 9, 2, 10, 21, 47, 890, DateTimeKind.Utc), null, null, "truongthu@example.com", "Trương Thu", null, "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", 2, "truongthu", null },
                    { new Guid("00000000-0000-0000-0000-00000000000d"), null, null, "https://i.pinimg.com/564x/9c/28/19/9c2819e41426236d748392299cd20246.jpg", "Một nhiếp ảnh gia tự xưng, thích du lịch, chụp nhiều bức ảnh độc đáo về văn hóa và cảnh đẹp Việt Nam và thế giới.", new DateTime(2002, 8, 4, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2023, 9, 2, 10, 21, 47, 890, DateTimeKind.Utc), null, null, "levan@example.com", "Lê Văn Tân", null, "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", 2, "levantan", null },
                    { new Guid("00000000-0000-0000-0000-00000000000e"), null, null, "https://i.pinimg.com/564x/ae/ca/78/aeca78f2453767acdbd8398c4f310025.jpg", "Tôi là một nhà thiết kế đồ họa sáng tạo, đã tham gia vào nhiều dự án quảng cáo, in ấn và branding.", new DateTime(2002, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2023, 9, 2, 10, 21, 47, 890, DateTimeKind.Utc), null, null, "nguyenminh@example.com", "Nguyễn Minh", null, "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", 2, "nguyenminh", null },
                    { new Guid("00000000-0000-0000-0000-00000000000f"), null, null, "https://i.pinimg.com/564x/2a/1c/40/2a1c400fa2d814b78ed36fd21a5316f5.jpg", "Tôi là một họa sĩ có gu thẩm mỹ độc đáo, tạo ra những tác phẩm nghệ thuật đa dạng và phong phú.", new DateTime(2002, 12, 24, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2023, 9, 2, 10, 21, 47, 890, DateTimeKind.Utc), null, null, "hoangtuan@example.com", "Hoàng Tuấn", null, "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", 2, "hoangtuan", null },
                    { new Guid("00000000-0000-0000-0000-000000000010"), null, null, "https://i.pinimg.com/736x/58/29/bd/5829bdfa438410a86cf9b180c077939c.jpg", "Sinh viên đại học mỹ thuật HCM, thích vẽ tranh phong cách Nhật Bản, nếu thích hãy theo dõi mình nhé.", new DateTime(1999, 10, 14, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2023, 10, 2, 10, 21, 47, 890, DateTimeKind.Utc), null, null, "buiduong@example.com", "Bùi Dương", null, "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", 2, "buiduong", null },
                    { new Guid("00000000-0000-0000-0000-000000000011"), null, null, "https://i.pinimg.com/564x/17/f4/97/17f497af6f6b67bd9dbcb93c04dced89.jpg", "Tôi là một họa sĩ chuyên về tranh nghệ thuật, tạo ra những tác phẩm tươi sáng và lôi cuốn.", new DateTime(2003, 10, 4, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2023, 10, 2, 10, 21, 47, 890, DateTimeKind.Utc), null, null, "phamha@example.com", "Phạm Hà", null, "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", 2, "phamha", null },
                    { new Guid("00000000-0000-0000-0000-000000000012"), null, null, "https://i.pinimg.com/564x/ba/74/40/ba744092fe6e7222d44a5e89cf483d6d.jpg", "Tôi là một thiết kế UI/UX đam mê và sáng tạo, đã tham gia vào nhiều dự án thành công trong lĩnh vực công nghệ.", new DateTime(2002, 10, 7, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2023, 10, 2, 10, 21, 47, 890, DateTimeKind.Utc), null, null, "doantrang@example.com", "Đoàn Trang", null, "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", 2, "doantrang", null },
                    { new Guid("00000000-0000-0000-0000-000000000013"), null, null, "https://i.pinimg.com/564x/de/09/b1/de09b1839700e9988e605df833a5450a.jpg", "Tôi là một nghệ sĩ 3D tài năng, đã tham gia vào việc tạo ra các mô hình 3D ấn tượng cho phim và trò chơi.", new DateTime(2002, 10, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2023, 10, 2, 10, 21, 47, 890, DateTimeKind.Utc), null, null, "tranduc@example.com", "Trần Đức", null, "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", 2, "tranduc", null },
                    { new Guid("00000000-0000-0000-0000-000000000014"), null, null, "https://i.pinimg.com/564x/1e/a0/59/1ea05967bf1e5e2054aaecd109a3c662.jpg", "Tôi là một nhà thiết kế đồ họa có tầm nhìn sáng tạo, đã đạt được nhiều giải thưởng trong ngành.", new DateTime(2003, 12, 4, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2023, 10, 2, 10, 21, 47, 890, DateTimeKind.Utc), null, null, "nguyenhieu@example.com", "Nguyễn Hiếu", null, "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", 2, "nguyenhieu", null },
                    { new Guid("00000000-0000-0000-0000-000000000015"), null, null, "https://i.pinimg.com/564x/7b/78/42/7b784268d117a6d57a8d9a83c7eaa977.jpg", "Tôi là một họa sĩ trẻ có sức sáng tạo và tinh thần nghệ thuật cao, đã tham gia vào nhiều dự án nghệ thuật và thiết kế.", new DateTime(2002, 6, 20, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2023, 10, 2, 10, 21, 47, 890, DateTimeKind.Utc), null, null, "vuthao@example.com", "Vũ Thảo", null, "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", 2, "vuthao", null },
                    { new Guid("00000000-0000-0000-0000-000000000016"), null, null, "https://i.pinimg.com/originals/30/33/0b/30330b5e8e0f772f0edaa310294703a2.jpg", "Tôi là một nhà thiết kế đồ họa có kinh nghiệm, đã tham gia vào việc phát triển các ứng dụng di động và giao diện người dùng.", new DateTime(2004, 11, 4, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2023, 10, 2, 10, 21, 47, 890, DateTimeKind.Utc), null, null, "nguyentien@example.com", "Nguyễn Tiến", null, "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", 2, "nguyentien", null },
                    { new Guid("00000000-0000-0000-0000-000000000017"), null, null, "https://i.pinimg.com/564x/f9/7f/c4/f97fc4762b0ca1c3ba76c3b2e6c5041c.jpg", "Tôi là một họa sĩ người Áo có ước mơ vào trường Mỹ Thuật", new DateTime(2002, 4, 4, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2023, 10, 2, 10, 21, 47, 890, DateTimeKind.Utc), null, null, "vudang@example.com", "Vũ Đăng", null, "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", 2, "vudang", null }
                });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "Id", "Address", "ArtisticStyle", "Avatar", "Bio", "Birthdate", "CreatedBy", "DeletedBy", "DeletedOn", "Email", "Fullname", "LastModificatedBy", "Password", "Role", "Username", "VerifiedOn" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000018"), null, null, "https://i.pinimg.com/564x/af/65/88/af6588a1cb6be3602190e4c223b79318.jpg", "Living and working in Japan / big fan of Key (Kagikko - 鍵っ子). A guy of social, cultural, and natural.", null, null, null, null, "minhhuy@example.com", "Trần Nguyễn Minh Huy", null, "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", 2, "minhhuy", null },
                    { new Guid("00000000-0000-0000-0000-000000000019"), null, null, "https://i.pinimg.com/564x/d9/03/0a/d9030a5696d2507a1dfb38a686ac93c2.jpg", "Nole của công ty NaiNovel - công ty game đầu hàng Việt Nam", null, null, null, null, "manhkbrady@example.com", "Nguyễn Đức Mạnh", null, "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", 2, "manhkbrady", null }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CategoryName", "ParentCategory" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "Minh hoạ", null },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "Thiết kế đồ họa", null },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "UI/UX", null },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "Kiến trúc", null },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "Thời trang", null },
                    { new Guid("00000000-0000-0000-0000-000000000006"), "Đồ họa chuyển động", null },
                    { new Guid("00000000-0000-0000-0000-000000000007"), "In ấn", null },
                    { new Guid("00000000-0000-0000-0000-000000000008"), "Đồ họa 3D", null },
                    { new Guid("00000000-0000-0000-0000-00000000000a"), "Nhiếp ảnh", null },
                    { new Guid("00000000-0000-0000-0000-00000000000b"), "Thiết kế sản phẩm", null },
                    { new Guid("00000000-0000-0000-0000-00000000000c"), "Quảng cáo", null }
                });

            migrationBuilder.InsertData(
                table: "LicenseType",
                columns: new[] { "Id", "LicenseDescription", "LicenseName" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "Tất cả các quyền đều được bảo lưu. Không ai được sao chép, phân phối hoặc sử dụng tác phẩm mà không có sự cho phép của tác giả", "All Rights Reserved" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "Cho phép người dùng chia sẻ, sao chép, phân phối và trình bày tác phẩm, cũng như tạo ra các tác phẩm phái sinh từ tác phẩm ban đầu, miễn là họ ghi công tác phẩm của bạn một cách đúng cách", "Attribution (CC BY)" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "Cho phép người dùng chia sẻ, sao chép, phân phối và trình bày tác phẩm, cũng như tạo ra các tác phẩm phái sinh từ tác phẩm ban đầu, miễn là họ ghi công tác phẩm của bạn một cách đúng cách. Tất cả các tác phẩm phái sinh phải được phân phối dưới cùng một giấy phép như tác phẩm ban đầu", "Attribution ShareAlike (CC BY-SA)" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "Cho phép người dùng chia sẻ, sao chép, phân phối và trình bày tác phẩm, miễn là họ ghi công tác phẩm của bạn một cách đúng cách. Tác phẩm không được thay đổi hoặc tạo ra các tác phẩm phái sinh từ tác phẩm ban đầu", "Attribution NoDerivs (CC BY-ND)" },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "Cho phép người dùng chia sẻ, sao chép, phân phối và trình bày tác phẩm, cũng như tạo ra các tác phẩm phái sinh từ tác phẩm ban đầu, miễn là họ ghi công tác phẩm của bạn một cách đúng cách. Tuy nhiên, họ không được sử dụng tác phẩm với mục đích thương mại", "Attribution-NonCommercial (CC BY-NC)" },
                    { new Guid("00000000-0000-0000-0000-000000000006"), "Cho phép người dùng chia sẻ, sao chép, phân phối và trình bày tác phẩm, cũng như tạo ra các tác phẩm phái sinh từ tác phẩm ban đầu, miễn là họ ghi công tác phẩm của bạn một cách đúng cách. Tuy nhiên, họ không được sử dụng tác phẩm với mục đích thương mại. Tất cả các tác phẩm phái sinh phải được phân phối dưới cùng một giấy phép như tác phẩm ban đầu", "Attribution-NonCommercial-ShareAlike (CC BY-NC-SA)" },
                    { new Guid("00000000-0000-0000-0000-000000000007"), "Cho phép người dùng chia sẻ, sao chép, phân phối và trình bày tác phẩm, miễn là họ ghi công tác phẩm của bạn một cách đúng cách. Tuy nhiên, họ không được sử dụng tác phẩm với mục đích thương mại. Tác phẩm không được thay đổi hoặc tạo ra các tác phẩm phái sinh từ tác phẩm ban đầu", "Attribution-NonCommercial-NoDerivs (CC BY-NC-ND)" },
                    { new Guid("00000000-0000-0000-0000-000000000008"), "Tác phẩm được công bố dưới dạng CC0 cho phép người khác có toàn quyền phân phối, sao chép, chỉnh sửa và xây dựng trên nội dung gốc mà không gặp bất kỳ ràng buộc nào. Bao gồm cả việc sử dụng cho mục đích thương mại.", "No Rights Reserved" }
                });

            migrationBuilder.InsertData(
                table: "SoftwareUsed",
                columns: new[] { "Id", "SoftwareName" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "Adobe Photoshop" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "Adobe Illustrator" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "Adobe After Effect" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "Adobe Premiere" },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "Adobe Lightroom" },
                    { new Guid("00000000-0000-0000-0000-000000000006"), "Adobe XD" },
                    { new Guid("00000000-0000-0000-0000-000000000007"), "Figma" },
                    { new Guid("00000000-0000-0000-0000-000000000008"), "Sketch" },
                    { new Guid("00000000-0000-0000-0000-000000000009"), "CorelDRAW" },
                    { new Guid("00000000-0000-0000-0000-00000000000a"), "Inkscape" },
                    { new Guid("00000000-0000-0000-0000-00000000000b"), "Blender" },
                    { new Guid("00000000-0000-0000-0000-00000000000c"), "Cinema 4D" },
                    { new Guid("00000000-0000-0000-0000-00000000000d"), "Maya" },
                    { new Guid("00000000-0000-0000-0000-00000000000e"), "ZBrush" },
                    { new Guid("00000000-0000-0000-0000-00000000000f"), "Substance Painter" },
                    { new Guid("00000000-0000-0000-0000-000000000010"), "Substance Designer" },
                    { new Guid("00000000-0000-0000-0000-000000000011"), "Substance Alchemist" },
                    { new Guid("00000000-0000-0000-0000-000000000012"), "Marvelous Designer" },
                    { new Guid("00000000-0000-0000-0000-000000000013"), "KeyShot" },
                    { new Guid("00000000-0000-0000-0000-000000000014"), "Lumion" },
                    { new Guid("00000000-0000-0000-0000-000000000015"), "AutoCAD" },
                    { new Guid("00000000-0000-0000-0000-000000000016"), "Revit" },
                    { new Guid("00000000-0000-0000-0000-000000000017"), "3ds Max" },
                    { new Guid("00000000-0000-0000-0000-000000000018"), "Rhino" },
                    { new Guid("00000000-0000-0000-0000-000000000019"), "Grasshopper" },
                    { new Guid("00000000-0000-0000-0000-00000000001a"), "Vectorworks" },
                    { new Guid("00000000-0000-0000-0000-00000000001b"), "ArchiCAD" },
                    { new Guid("00000000-0000-0000-0000-00000000001c"), "SketchUp" },
                    { new Guid("00000000-0000-0000-0000-00000000001d"), "Photoshop Lightroom" },
                    { new Guid("00000000-0000-0000-0000-00000000001e"), "Photoshop Elements" },
                    { new Guid("00000000-0000-0000-0000-00000000001f"), "PaintShop Pro" },
                    { new Guid("00000000-0000-0000-0000-000000000020"), "GIMP" },
                    { new Guid("00000000-0000-0000-0000-000000000021"), "Affinity Photo" },
                    { new Guid("00000000-0000-0000-0000-000000000022"), "Affinity Designer" },
                    { new Guid("00000000-0000-0000-0000-000000000023"), "Affinity Publisher" },
                    { new Guid("00000000-0000-0000-0000-000000000024"), "Clip Studio Paint" },
                    { new Guid("00000000-0000-0000-0000-000000000025"), "Krita" },
                    { new Guid("00000000-0000-0000-0000-000000000026"), "MediBang Paint" },
                    { new Guid("00000000-0000-0000-0000-000000000027"), "Procreate" },
                    { new Guid("00000000-0000-0000-0000-000000000028"), "ArtRage" },
                    { new Guid("00000000-0000-0000-0000-000000000029"), "Rebelle" },
                    { new Guid("00000000-0000-0000-0000-00000000002a"), "TwistedBrush Pro Studio" },
                    { new Guid("00000000-0000-0000-0000-00000000002b"), "Canva" },
                    { new Guid("00000000-0000-0000-0000-00000000002c"), "Paint Tool SAI" },
                    { new Guid("00000000-0000-0000-0000-00000000002d"), "Artweaver" },
                    { new Guid("00000000-0000-0000-0000-00000000002e"), "MyPaint" },
                    { new Guid("00000000-0000-0000-0000-00000000002f"), "FireAlpaca" },
                    { new Guid("00000000-0000-0000-0000-000000000030"), "OpenCanvas" },
                    { new Guid("00000000-0000-0000-0000-000000000031"), "Paint.NET" },
                    { new Guid("00000000-0000-0000-0000-000000000032"), "Pixia" },
                    { new Guid("00000000-0000-0000-0000-000000000033"), "SmoothDraw" },
                    { new Guid("00000000-0000-0000-0000-000000000034"), "Tayasui Sketches" },
                    { new Guid("00000000-0000-0000-0000-000000000035"), "Unity" },
                    { new Guid("00000000-0000-0000-0000-000000000036"), "Unreal Engine" },
                    { new Guid("00000000-0000-0000-0000-000000000037"), "GameMaker Studio" },
                    { new Guid("00000000-0000-0000-0000-000000000038"), "Godot Engine" },
                    { new Guid("00000000-0000-0000-0000-000000000039"), "Stable Diffusion" },
                    { new Guid("00000000-0000-0000-0000-00000000003a"), "Midjourney" }
                });

            migrationBuilder.InsertData(
                table: "Tag",
                columns: new[] { "Id", "TagName" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "Màu sắc" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "Trừu tượng" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "Phong cảnh" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "Thiên nhiên" },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "Hình học" },
                    { new Guid("00000000-0000-0000-0000-000000000006"), "Anime" },
                    { new Guid("00000000-0000-0000-0000-000000000007"), "Chân dung" },
                    { new Guid("00000000-0000-0000-0000-000000000008"), "Sáng tạo" },
                    { new Guid("00000000-0000-0000-0000-000000000009"), "Kỹ thuật số" },
                    { new Guid("00000000-0000-0000-0000-00000000000a"), "Nhật Bản" },
                    { new Guid("00000000-0000-0000-0000-00000000000b"), "Dự án cá nhân" },
                    { new Guid("00000000-0000-0000-0000-00000000000c"), "Nghệ thuật số hóa" },
                    { new Guid("00000000-0000-0000-0000-00000000000d"), "Thể thao" },
                    { new Guid("00000000-0000-0000-0000-00000000000e"), "Xã hội" },
                    { new Guid("00000000-0000-0000-0000-00000000000f"), "Vintage" },
                    { new Guid("00000000-0000-0000-0000-000000000010"), "Ảo" },
                    { new Guid("00000000-0000-0000-0000-000000000011"), "Tối giản" },
                    { new Guid("00000000-0000-0000-0000-000000000012"), "Figma" },
                    { new Guid("00000000-0000-0000-0000-000000000013"), "Mèo" },
                    { new Guid("00000000-0000-0000-0000-000000000014"), "Động vật" },
                    { new Guid("00000000-0000-0000-0000-000000000015"), "Sơn dầu" },
                    { new Guid("00000000-0000-0000-0000-000000000016"), "Nghệ thuật đương đại" },
                    { new Guid("00000000-0000-0000-0000-000000000017"), "Khoa học" },
                    { new Guid("00000000-0000-0000-0000-000000000018"), "Kiến trúc" },
                    { new Guid("00000000-0000-0000-0000-000000000019"), "AI Tạo Sinh" },
                    { new Guid("00000000-0000-0000-0000-00000000001a"), "Lịch sử" },
                    { new Guid("00000000-0000-0000-0000-00000000001b"), "Chính trị" },
                    { new Guid("00000000-0000-0000-0000-00000000001c"), "Việt Nam" },
                    { new Guid("00000000-0000-0000-0000-00000000001d"), "Nhiếp ảnh" },
                    { new Guid("00000000-0000-0000-0000-00000000001e"), "Ảnh bìa" },
                    { new Guid("00000000-0000-0000-0000-00000000001f"), "Tâm lý" },
                    { new Guid("00000000-0000-0000-0000-000000000020"), "Vũ trụ" },
                    { new Guid("00000000-0000-0000-0000-000000000021"), "Tương lai" },
                    { new Guid("00000000-0000-0000-0000-000000000022"), "Nhân vật" },
                    { new Guid("00000000-0000-0000-0000-000000000023"), "Trò chơi" },
                    { new Guid("00000000-0000-0000-0000-000000000024"), "Khoa học viễn tưởng" },
                    { new Guid("00000000-0000-0000-0000-000000000025"), "Kinh điển" },
                    { new Guid("00000000-0000-0000-0000-000000000026"), "Dễ thương" },
                    { new Guid("00000000-0000-0000-0000-000000000027"), "Phát họa" },
                    { new Guid("00000000-0000-0000-0000-000000000028"), "Logo" }
                });

            migrationBuilder.InsertData(
                table: "Artwork",
                columns: new[] { "Id", "CommentCount", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "Description", "IsAIGenerated", "LastModificatedBy", "LicenseTypeId", "LikeCount", "Note", "Privacy", "State", "Thumbnail", "ThumbnailName", "Title", "ViewCount" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), 6, new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 12, 2, 9, 37, 42, 345, DateTimeKind.Utc), null, null, "Tuyển tập những bức vẽ về hoàng hôn", true, null, new Guid("00000000-0000-0000-0000-000000000001"), 8, null, 0, 1, "https://i.pinimg.com/736x/8d/f7/be/8df7be5e052e97b824e6b0f783309161.jpg", "00000000-0000-0000-0000-000000000001_t.jpg", "Hoàng hôn rực nắng", 1344 },
                    { new Guid("00000000-0000-0000-0000-000000000002"), 0, new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 12, 3, 22, 20, 45, 890, DateTimeKind.Utc), null, null, "Khám phá sâu hơn về cảm xúc và tâm trạng trong cuộc sống", true, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://th.bing.com/th/id/OIG.mMOt1xWJJCHsRoPJXtHQ?pid=ImgGn", "00000000-0000-0000-0000-000000000001_t.jpg", "Hành trình sâu cảm xúc", 327 },
                    { new Guid("00000000-0000-0000-0000-000000000003"), 0, new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2023, 12, 4, 20, 55, 30, 456, DateTimeKind.Utc), null, null, "Minh họa cuộc chiến tiêu biểu của thời đại", false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://vov2-media.solidtech.vn/sites/default/files/styles/large/public/2022-05/dien-bien-phu_-_bia.jpg", "00000000-0000-0000-0000-000000000002_t.jpg", "Chiến thắng Điện Biên Phủ", 2138 },
                    { new Guid("00000000-0000-0000-0000-000000000004"), 0, new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 12, 5, 15, 30, 3, 678, DateTimeKind.Utc), null, null, "Tác phẩm thể hiện sự ảnh hưởng của quá khứ đối với hiện tại", false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://bizweb.dktcdn.net/100/378/894/files/5.jpg?v=1627978886825", "00000000-0000-0000-0000-000000000004_t.jpg", "Dấu vết của quá khứ", 2063 },
                    { new Guid("00000000-0000-0000-0000-000000000005"), 0, new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2023, 12, 6, 7, 30, 15, 567, DateTimeKind.Utc), null, null, "Minh họa cho hành trình không ngừng của sự sáng tạo", true, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://th.bing.com/th/id/OIG.LTaVFacabNQc22SAk1r1?pid=ImgGn", "00000000-0000-0000-0000-000000000005_t.jpg", "Hành trình của sự sáng tạo", 779 },
                    { new Guid("00000000-0000-0000-0000-000000000006"), 0, new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 12, 7, 12, 40, 28, 901, DateTimeKind.Utc), null, null, "Tượng trưng cho sự đồng hành và hỗ trợ của đối tác tâm lý", true, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://th.bing.com/th/id/OIG.AoM9akz.gT4RMZ9R6DOh?pid=ImgGn", "00000000-0000-0000-0000-000000000006_t.jpg", "Sự đồng hành của đối tác tâm lý", 245 },
                    { new Guid("00000000-0000-0000-0000-000000000007"), 0, new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 12, 9, 1, 30, 15, 567, DateTimeKind.Utc), null, null, "Hình ảnh tượng trưng cho ánh sáng và năng lượng bên trong chúng ta", true, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://i.pinimg.com/564x/61/59/6c/61596cae1a6dc2dede171e59fb5787aa.jpg", "00000000-0000-0000-0000-000000000007_t.jpg", "Mặt trời bên trong", 356 },
                    { new Guid("00000000-0000-0000-0000-000000000008"), 0, new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2023, 12, 1, 15, 30, 3, 678, DateTimeKind.Utc), null, null, "Đây là tuyển tập tâm huyết của mình, nhớ like và comment để ủng hộ mình nha", false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://static-company.waka.vn/img.media/tin%20t%E1%BB%A9c/38540.jpg", "00000000-0000-0000-0000-000000000008_t.jpg", "Tuyển tập minh hoạ sách tâm lý", 342 },
                    { new Guid("00000000-0000-0000-0000-000000000009"), 0, new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2023, 12, 10, 4, 20, 10, 234, DateTimeKind.Utc), null, null, "Minh họa cho tâm trạng lạc quan và hy vọng về tương lai", true, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 1, 1, "https://i.pinimg.com/originals/8d/97/66/8d9766d16e7cf36ade29a0e307ce81be.jpg", "00000000-0000-0000-0000-000000000009_t.jpg", "Sự lạc quan của tương lai", 0 },
                    { new Guid("00000000-0000-0000-0000-00000000000a"), 0, new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 12, 10, 20, 55, 30, 456, DateTimeKind.Utc), null, null, "Tượng trưng cho nơi gặp gỡ và kết nối tâm hồn con người", true, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://th.bing.com/th/id/OIG.78MNqZpoa6ReKmvZCHPI?pid=ImgGn", "00000000-0000-0000-0000-00000000000a_t.jpg", "Nơi gặp gỡ tâm hồn", 145 },
                    { new Guid("00000000-0000-0000-0000-00000000000b"), 0, new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2023, 12, 11, 15, 30, 3, 678, DateTimeKind.Utc), null, null, "Tượng trưng cho vũ trụ rộng lớn và không gian của tâm trí con người", true, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://cdn.pixabay.com/photo/2024/02/03/11/41/ai-generated-8550098_1280.jpg", "00000000-0000-0000-0000-00000000000b_t.jpg", "Vũ trụ tâm trí", 565 },
                    { new Guid("00000000-0000-0000-0000-00000000000c"), 0, new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 12, 11, 9, 37, 42, 345, DateTimeKind.Utc), null, null, "Anh như con cáo Em như cành nho xanh Khi em còn trẻ và đẹp em lại ko dành cho anhhhhh.", false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 1, 1, "https://th.bing.com/th/id/OIG.MxQxUggA0RKmKdTjwAqw?pid=ImgGn", "00000000-0000-0000-0000-00000000000c_t.jpg", "Cáo già", 234 },
                    { new Guid("00000000-0000-0000-0000-00000000000d"), 0, new Guid("00000000-0000-0000-0000-000000000015"), new DateTime(2024, 3, 5, 9, 37, 42, 345, DateTimeKind.Utc), null, null, null, false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://i.pinimg.com/originals/7f/b2/ab/7fb2abc0dddd2aa40a86ce6c318b369a.jpg", "00000000-0000-0000-0000-00000000000d_t.jpg", "Làng quê", 2034 },
                    { new Guid("00000000-0000-0000-0000-00000000000e"), 0, new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 12, 13, 15, 40, 28, 901, DateTimeKind.Utc), null, null, "Bộ sưu tập người máy - biểu tượng của tương lai.", true, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://i.pinimg.com/564x/98/08/55/980855dafc9c24ad9b0687adb4b29f0b.jpg", "00000000-0000-0000-0000-00000000000e_t.jpg", "Kỷ nguyên mới", 1934 },
                    { new Guid("00000000-0000-0000-0000-00000000000f"), 0, new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2023, 12, 14, 15, 20, 45, 890, DateTimeKind.Utc), null, null, "Cánh cụt cute", false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 0, "https://th.bing.com/th/id/OIG.MC3PObbEmuJhfsPJ8biQ?pid=ImgGn", "00000000-0000-0000-0000-00000000000f_t.jpg", "Tuyển tập ảnh cánh cụt cute", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000010"), 0, new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 12, 14, 17, 55, 30, 456, DateTimeKind.Utc), null, null, "Tượng trưng cho biển cả tri thức sâu rộng và không ngừng mở rộng", true, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 0, "https://th.bing.com/th/id/OIG.5gNG99_0Acz4Y8CGOYlg?pid=ImgGn", "00000000-0000-0000-0000-000000000010_t.jpg", "Biển cả của tri thức", 344 },
                    { new Guid("00000000-0000-0000-0000-000000000011"), 0, new Guid("00000000-0000-0000-0000-000000000008"), new DateTime(2023, 11, 10, 15, 30, 3, 678, DateTimeKind.Utc), null, null, "Bán đất ngay ngã tư ga, Hà Huy Giáp vào 1/sẹc, phường Thạnh Xuân,quận 12.\r\n-  Khu phân lô đại phú, gần Bánh Mỳ Hà Nội.\r\n- Diện tích: 4,10m x 14m\r\n- Hướng Tây nam\r\n- Giá: 3,5 tỷ còn thương lượng \r\n- Đường nhựa xe tải vào tận nơi .\r\n-Liên hệ: 0347307890 Nguyễn Hoàng.", false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://firebasestorage.googleapis.com/v0/b/artworkia-init.appspot.com/o/Artwork%2FThumbnail%2F00000000-0000-0000-0000-000000000011_t.jpg?alt=media&token=aad2c767-acdf-470f-bc35-9c55947cc9af", "00000000-0000-0000-0000-000000000011_t.jpg", "Bất Động Sản Thạnh Lộc-Thạnh Xuân", 70 },
                    { new Guid("00000000-0000-0000-0000-000000000012"), 0, new Guid("00000000-0000-0000-0000-000000000019"), new DateTime(2023, 12, 12, 16, 30, 3, 678, DateTimeKind.Utc), null, null, "Mặc dù nghe có vẻ không đặc biệt, nhưng việc xuất bản visual novel được cấp phép đầu tiên tại Việt Nam là một cột mốc quan trọng trong sự phát triển của thể loại cực kén người chơi này trong cộng đồng của tôi. Đây là một dự án tuyệt vời và tôi rất vinh dự được tham gia vào quá trình thực hiện.", false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://firebasestorage.googleapis.com/v0/b/artworkia-init.appspot.com/o/Artwork%2FImage%2F1.png?alt=media&token=61dea5b6-4462-49b3-b58a-3acc6f9861fc", "00000000-0000-0000-0000-000000000012_t.jpg", "Hình ảnh giới thiệu nhân vật & Đếm ngược ra mắt game", 1011 },
                    { new Guid("00000000-0000-0000-0000-000000000013"), 0, new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2023, 12, 13, 18, 11, 3, 678, DateTimeKind.Utc), null, null, "Là một họa sĩ được sinh ra và lớn lên khi đất nước đã thống nhất, Mai Duy Minh đã cố gắng hết sức có thể trong việc tìm kiếm một câu trả lời cho riêng cá nhân anh về cách mà các thế hệ người Việt Nam bền bỉ đi qua mọi gian khổ để bảo vệ độc lập dân tộc. Tất cả những nỗ lực tìm kiếm và lắng nghe mọi vang vọng từ quá khứ chiến đấu anh dũng ấy của cha anh đi trước đã được hội tụ trong bức tranh “Điện Biên Phủ”..", false, null, new Guid("00000000-0000-0000-0000-000000000003"), 0, null, 0, 1, "https://vov2.vov.vn/sites/default/files/styles/large/public/2022-05/vo-nguyen-giap.jpg", "00000000-0000-0000-0000-000000000013_t.jpg", "Tranh sơn dầu Đại tướng Võ Nguyên Giáp", 3045 },
                    { new Guid("00000000-0000-0000-0000-000000000014"), 0, new Guid("00000000-0000-0000-0000-00000000000b"), new DateTime(2023, 12, 14, 0, 11, 3, 678, DateTimeKind.Utc), null, null, "A collection of 173 wallpapers in 4K resolution from THE SIGHTS OF SPACE", false, null, new Guid("00000000-0000-0000-0000-000000000002"), 0, null, 0, 1, "https://f4.bcbits.com/img/a0127149981_16.jpg", "00000000-0000-0000-0000-000000000014_t.jpg", "Không gian vũ trụ", 1725 },
                    { new Guid("00000000-0000-0000-0000-000000000015"), 0, new Guid("00000000-0000-0000-0000-00000000000b"), new DateTime(2023, 12, 14, 21, 11, 3, 678, DateTimeKind.Utc), null, null, "Save the date.  THE HUMAN FUTURE drops on August 15.  Casting my stone in the opposite direction of all the pessimists out there.  see you soon :)", false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://yt3.ggpht.com/7CFh0HeCsVSD9UfK1LSc9imflCpKDZT41gNjj_qehvRKt0J9fgmjw2tYjf4oMpbsn0BagczFhx_TTQ=s1600-rw-nd-v1", "00000000-0000-0000-0000-000000000015_t.jpg", "Tương lai loài người", 435 },
                    { new Guid("00000000-0000-0000-0000-000000000016"), 0, new Guid("00000000-0000-0000-0000-00000000000b"), new DateTime(2023, 12, 20, 15, 20, 45, 890, DateTimeKind.Utc), null, null, "How might we discover intelligent life?\r\n\r\nInstead of waiting for radio signals or scanning alien atmospheres, we could look to distant asteroid fields for evidence.  Advanced civilizations would have numerous reasons to probe asteroids for materials. They are rich in resources and easier to manipulate because of their lower gravity.\r\n\r\nSpotting chemical anomalies or infrared waste heat around distant asteroid fields could lead us to the ultimate discovery.\r\n\r\nPresenting THE OUTPOST: a digital art piece from Life Beyond 3", false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://yt3.ggpht.com/gD2jfNal37ZYf6zkA7CX8CdBQ3WJDP9wFxA-JgrTl0RMGmWh7QcaWG1L2dzBWHUV_qd20ddxXJSMi90=s1024-c-fcrop64=1,0000199affffe666-rw-nd-v1", "00000000-0000-0000-0000-000000000016_t.jpg", "THE OUTPOST", 1564 },
                    { new Guid("00000000-0000-0000-0000-000000000017"), 0, new Guid("00000000-0000-0000-0000-000000000010"), new DateTime(2023, 12, 21, 15, 30, 3, 678, DateTimeKind.Utc), null, null, null, false, null, new Guid("00000000-0000-0000-0000-000000000002"), 0, null, 0, 1, "https://firebasestorage.googleapis.com/v0/b/artworkia-init.appspot.com/o/Artwork%2FImage%2F106127234_p0.jpg?alt=media&token=86c5c12c-bee7-4a57-8db1-402e317d5c23", "00000000-0000-0000-0000-000000000017_t.jpg", "Mọt sách", 725 },
                    { new Guid("00000000-0000-0000-0000-000000000018"), 0, new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2023, 12, 22, 15, 20, 45, 890, DateTimeKind.Utc), null, null, "Bức tranh hấp dẫn này thường được so sánh với \"Mona Lisa\". Bên cạnh sự khác biệt về phong cách, về mặt kỹ thuật \"Girl With a Pearl Earring\" thậm chí không phải là một bức chân dung, mà là một \"tronie\" - một từ tiếng Hà Lan để chỉ bức tranh của một nhân vật tưởng tượng với các đặc điểm phóng đại.", false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://www.kiettacnghethuat.com/wp-content/uploads/Girl-With-a-Pearl-Earring.jpg", "00000000-0000-0000-0000-000000000018_t.jpg", "Cô Gái Đeo Bông Tai Ngọc Trai", 1204 },
                    { new Guid("00000000-0000-0000-0000-000000000019"), 0, new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2023, 12, 23, 15, 20, 45, 890, DateTimeKind.Utc), null, null, "Bức tranh \"The Starry Night\" của Vincent van Gogh là một trong những bức tranh nổi tiếng nhất của ông. Bức tranh này được vẽ vào năm 1889 và hiện đang được trưng bày tại Bảo tàng Nghệ thuật Modern ở New York.", false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://www.kiettacnghethuat.com/wp-content/uploads/The-Starry-Night.jpg", "00000000-0000-0000-0000-000000000019_t.jpg", "Đêm đầy sao", 1462 },
                    { new Guid("00000000-0000-0000-0000-00000000001a"), 0, new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2023, 12, 24, 15, 20, 45, 890, DateTimeKind.Utc), null, null, "Thiếu nữ bên hoa huệ là một tác phẩm tranh sơn dầu do họa sĩ Tô Ngọc Vân sáng tác năm 1943. Bức tranh mô tả chân dung một thiếu nữ mặc áo dài trắng bên cạnh lọ hoa huệ trắng.", false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://upload.wikimedia.org/wikipedia/commons/thumb/6/62/To_Ngoc_Van_thieu_nu_ben_hoa_hue.jpg/800px-To_Ngoc_Van_thieu_nu_ben_hoa_hue.jpg", "00000000-0000-0000-0000-00000000001a_t.jpg", "Thiếu nữ bên hoa huệ", 1874 },
                    { new Guid("00000000-0000-0000-0000-00000000001b"), 0, new Guid("00000000-0000-0000-0000-000000000011"), new DateTime(2024, 2, 25, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, true, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://i.pinimg.com/originals/5b/43/fc/5b43fc89abad25aa3359bfe0f27923f9.jpg", "00000000-0000-0000-0000-00000000001b_t.jpg", "Bầu trời", 1124 },
                    { new Guid("00000000-0000-0000-0000-00000000001c"), 0, new Guid("00000000-0000-0000-0000-000000000017"), new DateTime(2023, 12, 26, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://upload.wikimedia.org/wikipedia/commons/e/e5/Adolf_Hitler_-_Wien_Oper.jpg", "00000000-0000-0000-0000-00000000001c_t.jpg", "Nhà hát Opera Vienna", 1666 },
                    { new Guid("00000000-0000-0000-0000-00000000001d"), 0, new Guid("00000000-0000-0000-0000-000000000017"), new DateTime(2023, 12, 27, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://upload.wikimedia.org/wikipedia/commons/9/93/Adolf_Hitler_-_Hofbr%C3%A4uhaus.jpg", "00000000-0000-0000-0000-00000000001d_t.jpg", "Hofbräuhaus, Munich", 1144 },
                    { new Guid("00000000-0000-0000-0000-00000000001e"), 0, new Guid("00000000-0000-0000-0000-000000000017"), new DateTime(2023, 12, 28, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://upload.wikimedia.org/wikipedia/commons/thumb/b/ba/Adolf_Hitler_-_Standesamt_M%C3%BCnchen.jpg/1024px-Adolf_Hitler_-_Standesamt_M%C3%BCnchen.jpg", "00000000-0000-0000-0000-00000000001e_t.jpg", "Lâu đài Old Town ở Munich", 452 },
                    { new Guid("00000000-0000-0000-0000-00000000001f"), 0, new Guid("00000000-0000-0000-0000-000000000017"), new DateTime(2023, 12, 29, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7d/Adolf_Hitler_Der_Alte_Hof.jpg/1024px-Adolf_Hitler_Der_Alte_Hof.jpg", "00000000-0000-0000-0000-00000000001f_t.jpg", "Khoảng sân trong ở phủ thống sứ cũ tại Munich", 1124 },
                    { new Guid("00000000-0000-0000-0000-000000000020"), 0, new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2023, 12, 29, 15, 20, 45, 890, DateTimeKind.Utc), null, null, "Hầu hết các bức ảnh đều chụp cách đây mấy chục năm, nước ảnh ố mờ, bay màu. Có nhiều bức ảnh được vẽ lại theo trí nhớ người thân nên để phục chế lại phải mất nhiều thời gian, phải cẩn thận chỉnh sửa từng chi tiết", false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://cafebiz.cafebizcdn.vn/162123310254002176/2022/4/27/photo-1-1651050727352925703922.jpeg", "00000000-0000-0000-0000-000000000020_t.jpg", "Phục chế ảnh liệt sĩ", 1968 },
                    { new Guid("00000000-0000-0000-0000-000000000021"), 0, new Guid("00000000-0000-0000-0000-000000000010"), new DateTime(2024, 1, 1, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://img3.gelbooru.com//images/db/56/db56fdd06f55f97ab9c884d5539e7e99.jpeg", "00000000-0000-0000-0000-000000000021_t.jpg", "Bạch thiếu nữ", 694 },
                    { new Guid("00000000-0000-0000-0000-000000000022"), 0, new Guid("00000000-0000-0000-0000-000000000018"), new DateTime(2024, 1, 3, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://www.4gamer.net/games/411/G041111/20180227026/SS/002.jpg", "00000000-0000-0000-0000-000000000022_t.jpg", "Kushima Kamome", 634 },
                    { new Guid("00000000-0000-0000-0000-000000000023"), 0, new Guid("00000000-0000-0000-0000-000000000018"), new DateTime(2024, 1, 5, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://www.4gamer.net/games/411/G041111/20180227026/SS/003.jpg", "00000000-0000-0000-0000-000000000023_t.jpg", "Ao Sorakado", 234 },
                    { new Guid("00000000-0000-0000-0000-000000000024"), 0, new Guid("00000000-0000-0000-0000-000000000018"), new DateTime(2024, 1, 6, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://www.4gamer.net/games/411/G041111/20180227026/SS/004.jpg", "00000000-0000-0000-0000-000000000024_t.jpg", "Tsumugi Wenders", 345 },
                    { new Guid("00000000-0000-0000-0000-000000000025"), 0, new Guid("00000000-0000-0000-0000-000000000018"), new DateTime(2024, 1, 9, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://www.4gamer.net/games/411/G041111/20180227026/SS/005.jpg", "00000000-0000-0000-0000-000000000025_t.jpg", "Shiroha Naruse", 370 },
                    { new Guid("00000000-0000-0000-0000-000000000026"), 0, new Guid("00000000-0000-0000-0000-000000000010"), new DateTime(2024, 1, 9, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://firebasestorage.googleapis.com/v0/b/artworkia-init.appspot.com/o/Artwork%2FImage%2F110149990_p0.jpg?alt=media&token=4668e7eb-f2ba-47f2-add9-96bb40acd22e", "00000000-0000-0000-0000-000000000026_t.jpg", "Bánh vòng", 814 },
                    { new Guid("00000000-0000-0000-0000-000000000027"), 0, new Guid("00000000-0000-0000-0000-000000000011"), new DateTime(2023, 12, 6, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://firebasestorage.googleapis.com/v0/b/artworkia-init.appspot.com/o/Artwork%2FImage%2Fwp3790967.jpg?alt=media&token=203e265e-427f-4aa3-a664-2a39479bf61e", "00000000-0000-0000-0000-000000000027_t.jpg", "Rặng hoa anh đào", 518 },
                    { new Guid("00000000-0000-0000-0000-000000000028"), 0, new Guid("00000000-0000-0000-0000-000000000018"), new DateTime(2023, 12, 7, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://i0.wp.com/images1.wikia.nocookie.net/mydata/vi/images/b/be/Anemoi_main_viet.png?ssl=1", "00000000-0000-0000-0000-000000000028_t.jpg", "Một lời hứa cuốn trong làn gió", 327 },
                    { new Guid("00000000-0000-0000-0000-000000000029"), 0, new Guid("00000000-0000-0000-0000-000000000011"), new DateTime(2023, 12, 8, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, true, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://i.pinimg.com/originals/57/da/1d/57da1db47a30fe8d608da6d3b25dfc08.jpg", "00000000-0000-0000-0000-000000000029_t.jpg", "Trời xanh", 1489 },
                    { new Guid("00000000-0000-0000-0000-00000000002a"), 0, new Guid("00000000-0000-0000-0000-000000000011"), new DateTime(2023, 12, 9, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, true, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://i.pinimg.com/originals/56/a4/58/56a45858390e3726d2848d3efa696d6e.jpg", "00000000-0000-0000-0000-00000000002a_t.jpg", "Thung lũng xanh", 1034 },
                    { new Guid("00000000-0000-0000-0000-00000000002b"), 0, new Guid("00000000-0000-0000-0000-000000000011"), new DateTime(2023, 12, 10, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, true, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://i.pinimg.com/originals/51/59/8e/51598eb8f62f868e95556fc316873f05.jpg", "00000000-0000-0000-0000-00000000002b_t.jpg", "Đồng lúa", 1345 },
                    { new Guid("00000000-0000-0000-0000-00000000002c"), 0, new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2023, 12, 11, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, false, null, new Guid("00000000-0000-0000-0000-000000000002"), 0, null, 0, 1, "https://i.pinimg.com/originals/33/b5/96/33b596908f23b4b2c3f3e64f032e51b6.png", "00000000-0000-0000-0000-00000000002c_t.jpg", "Hệ mặt trời", 145 },
                    { new Guid("00000000-0000-0000-0000-00000000002d"), 0, new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2023, 12, 12, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, false, null, new Guid("00000000-0000-0000-0000-000000000002"), 0, null, 0, 1, "https://i.pinimg.com/originals/02/f2/28/02f2283c2a59d3e7d7287a95fae5c2f5.jpg", "00000000-0000-0000-0000-00000000002d_t.jpg", "Lập phương rubik", 595 },
                    { new Guid("00000000-0000-0000-0000-00000000002e"), 0, new Guid("00000000-0000-0000-0000-000000000015"), new DateTime(2023, 12, 13, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, false, null, new Guid("00000000-0000-0000-0000-000000000002"), 0, null, 0, 1, "https://i.pinimg.com/originals/85/3e/bf/853ebff19a985aae38e65c8111e59ef8.png", "00000000-0000-0000-0000-00000000002e_t.jpg", "Ngọn hải đăng", 320 },
                    { new Guid("00000000-0000-0000-0000-00000000002f"), 0, new Guid("00000000-0000-0000-0000-000000000015"), new DateTime(2024, 1, 20, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, true, null, new Guid("00000000-0000-0000-0000-000000000002"), 0, null, 0, 1, "https://i.pinimg.com/originals/d2/54/3a/d2543a52f10afe5696d68346d212d34e.jpg", "00000000-0000-0000-0000-00000000002f_t.jpg", "Mèo đen", 1020 },
                    { new Guid("00000000-0000-0000-0000-000000000030"), 0, new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2023, 12, 15, 15, 20, 45, 890, DateTimeKind.Utc), null, null, "\"The Nation of Vietnam is one, the People of Vietnam are one\", said by Hồ Chí Minh. The Vietnamese people's aspiration for independence, freedom, unity and happiness in the 20th century helped that nation win all wars of the aggressor ... ", false, null, new Guid("00000000-0000-0000-0000-000000000002"), 0, null, 0, 1, "https://i.pinimg.com/564x/41/e1/51/41e151608d67227d7265e7026faa48c1.jpg", "00000000-0000-0000-0000-000000000030_t.jpg", "Chủ tịch Hồ Chí Minh", 1645 },
                    { new Guid("00000000-0000-0000-0000-000000000031"), 0, new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2024, 1, 27, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, false, null, new Guid("00000000-0000-0000-0000-000000000008"), 0, null, 0, 1, "https://i.pinimg.com/originals/3d/e0/d0/3de0d0e553793ec3ee39cf1e7404e96e.jpg", "00000000-0000-0000-0000-000000000031_t.jpg", "Bác bảo thắng là thắng", 1999 },
                    { new Guid("00000000-0000-0000-0000-000000000032"), 0, new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2023, 12, 17, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, false, null, new Guid("00000000-0000-0000-0000-000000000008"), 0, null, 1, 1, "https://i.pinimg.com/564x/49/40/56/494056c9b3f314dad493bac63265f296.jpg", "00000000-0000-0000-0000-000000000032_t.jpg", "4575", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000033"), 0, new Guid("00000000-0000-0000-0000-000000000012"), new DateTime(2023, 12, 20, 15, 20, 45, 890, DateTimeKind.Utc), null, null, "Travo Apps - Bộ công cụ giao diện người dùng cho Đặt vé máy bay và Khách sạn du lịch", false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://i.pinimg.com/564x/1e/6f/85/1e6f852b5f25cd76c962c5affc55fcef.jpg", "00000000-0000-0000-0000-000000000033_t.jpg", "Travo Apps - UI KIT for Travel Flight and Hotel", 1499 },
                    { new Guid("00000000-0000-0000-0000-000000000034"), 0, new Guid("00000000-0000-0000-0000-000000000012"), new DateTime(2024, 1, 6, 15, 20, 45, 890, DateTimeKind.Utc), null, null, "Thiết kế giao diện người dùng (UI) cho ứng dụng di động MyNovel, một nền tảng đọc truyện trực tuyến Thái Lan cho mọi loại tiểu thuyết tuyệt vời, sách điện tử và truyện tranh. Nó cập nhật nội dung theo dạng kịch bản hàng ngày và nhiều hơn nữa.", false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://i.pinimg.com/564x/18/57/2c/18572cd01747f618fbf837b7b4459437.jpg", "00000000-0000-0000-0000-000000000034_t.jpg", "MyNovel Mobile App UI", 1734 },
                    { new Guid("00000000-0000-0000-0000-000000000035"), 0, new Guid("00000000-0000-0000-0000-000000000012"), new DateTime(2023, 12, 22, 15, 20, 45, 890, DateTimeKind.Utc), null, null, "tái thiết kế ứng dụng gặp gỡ người mới phổ biến nhất thế giới. Tinder là một ứng dụng cho phép người dùng vuốt sang trái hoặc phải để thích hoặc không thích các hồ sơ khác dựa trên ảnh, tiểu sử ngắn và sở thích chung. Khi hai người dùng \"match\" (bật cặp) với nhau thì họ có thể nhắn tin.", false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://mir-s3-cdn-cf.behance.net/project_modules/1400/8cf9b997945993.5ee39b4959c00.jpg", "00000000-0000-0000-0000-000000000035_t.jpg", "Tinder mobile app redesign", 1499 },
                    { new Guid("00000000-0000-0000-0000-000000000036"), 0, new Guid("00000000-0000-0000-0000-00000000000d"), new DateTime(2023, 12, 23, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://i.pinimg.com/564x/66/b8/59/66b859b3949564d6e3f63cfcf2b6ef93.jpg", "00000000-0000-0000-0000-000000000036_t.jpg", "Góc phố Nhật Bản", 1599 },
                    { new Guid("00000000-0000-0000-0000-000000000037"), 0, new Guid("00000000-0000-0000-0000-00000000000d"), new DateTime(2023, 12, 24, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, false, null, new Guid("00000000-0000-0000-0000-000000000008"), 0, null, 0, 1, "https://i.pinimg.com/564x/c8/a1/5f/c8a15ff316f7c74b789d21651b0891f8.jpg", "00000000-0000-0000-0000-000000000037_t.jpg", "Sapa mộng mer", 539 },
                    { new Guid("00000000-0000-0000-0000-000000000038"), 0, new Guid("00000000-0000-0000-0000-00000000000d"), new DateTime(2024, 1, 13, 15, 20, 45, 890, DateTimeKind.Utc), null, null, "Bãi tắm đảo Bé Lý Sơn nơi checkin sống ảo đúng chất", false, null, new Guid("00000000-0000-0000-0000-000000000008"), 0, null, 0, 1, "https://i.pinimg.com/564x/61/9f/65/619f65d4700c390f1e47ab42237ce450.jpg", "00000000-0000-0000-0000-000000000038_t.jpg", "Bãi tắm đảo Bé huyện đảo Lý Sơn", 999 },
                    { new Guid("00000000-0000-0000-0000-000000000039"), 0, new Guid("00000000-0000-0000-0000-00000000000d"), new DateTime(2024, 1, 6, 15, 20, 45, 890, DateTimeKind.Utc), null, null, "Looking for the best island to visit in Vietnam? This is the ultimate guide to visiting the Con Dao Islands. Find out what makes Con Dao is a must-visit!", false, null, new Guid("00000000-0000-0000-0000-000000000008"), 0, null, 0, 1, "https://i.pinimg.com/564x/ff/78/08/ff7808be1114e07dc1065245bd8bfc7f.jpg", "00000000-0000-0000-0000-000000000039_t.jpg", "Côn đảo, Việt Nam", 919 },
                    { new Guid("00000000-0000-0000-0000-00000000003a"), 0, new Guid("00000000-0000-0000-0000-00000000000d"), new DateTime(2023, 12, 27, 15, 20, 45, 890, DateTimeKind.Utc), null, null, "Review Landmark 81 - tòa nhà cao và sang trọng bậc nhất Việt Nam", false, null, new Guid("00000000-0000-0000-0000-000000000008"), 0, null, 0, 1, "https://ik.imagekit.io/tvlk/blog/2024/01/landmark-81-3-841x1024.jpg?tr=dpr-2,w-675", "00000000-0000-0000-0000-00000000003a_t.jpg", "Landmark 81", 231 },
                    { new Guid("00000000-0000-0000-0000-00000000003b"), 0, new Guid("00000000-0000-0000-0000-00000000000d"), new DateTime(2023, 12, 28, 15, 20, 45, 890, DateTimeKind.Utc), null, null, "Bãi biển Mỹ Khê Đà Nẵng - Địa điểm du lịch nổi tiếng tại Việt Nam", false, null, new Guid("00000000-0000-0000-0000-000000000008"), 0, null, 0, 1, "https://i.pinimg.com/564x/78/37/52/783752783478df60a339c4389697fe88.jpg", "00000000-0000-0000-0000-00000000003b_t.jpg", "Bãi biển Mỹ Khê", 999 },
                    { new Guid("00000000-0000-0000-0000-00000000003c"), 0, new Guid("00000000-0000-0000-0000-00000000000b"), new DateTime(2023, 12, 29, 15, 20, 45, 890, DateTimeKind.Utc), null, null, "Lỗ đen hay hố đen, là một vùng không–thời gian nơi trường hấp dẫn mạnh đến mức không có gì—không hạt vật chất hay cả bức xạ điện từ như ánh sáng có thể thoát khỏi nó.", false, null, new Guid("00000000-0000-0000-0000-000000000003"), 0, null, 0, 1, "https://i.pinimg.com/564x/d2/d1/17/d2d1176fbec2f7573eb1023c518e1105.jpg", "00000000-0000-0000-0000-00000000003c_t.jpg", "Hố Đen", 1899 },
                    { new Guid("00000000-0000-0000-0000-00000000003d"), 0, new Guid("00000000-0000-0000-0000-000000000013"), new DateTime(2023, 12, 30, 15, 20, 45, 890, DateTimeKind.Utc), null, null, "Adorable 3D Character by AI. Follow for more!", false, null, new Guid("00000000-0000-0000-0000-000000000003"), 0, null, 0, 1, "https://i.pinimg.com/564x/d4/37/b3/d437b301b2408a5772d9be18d2dcbdee.jpg", "00000000-0000-0000-0000-00000000003d_t.jpg", "Thiết kế nhân vật 3D", 1562 },
                    { new Guid("00000000-0000-0000-0000-00000000003e"), 0, new Guid("00000000-0000-0000-0000-000000000013"), new DateTime(2023, 12, 31, 15, 20, 45, 890, DateTimeKind.Utc), null, null, "Geralt of Rivia (tiếng Ba Lan: Geralt z Rivii) là một witcher và là nhân vật chính trong loạt tiểu thuyết The Witcher của nhà văn Andrzej Sapkowski", false, null, new Guid("00000000-0000-0000-0000-000000000004"), 0, null, 0, 1, "https://i.pinimg.com/736x/a9/5c/0f/a95c0f2de7561a34fbccc7af102b1af5.jpg", "00000000-0000-0000-0000-00000000003e_t.jpg", "Geralt xứ Rivia", 1262 },
                    { new Guid("00000000-0000-0000-0000-00000000003f"), 0, new Guid("00000000-0000-0000-0000-000000000013"), new DateTime(2024, 1, 5, 15, 20, 45, 890, DateTimeKind.Utc), null, null, "Ai Representation of the Dragonborn from Elderscrolls Skyrim wearing daedric Armor", true, null, new Guid("00000000-0000-0000-0000-000000000004"), 0, null, 0, 1, "https://i.pinimg.com/736x/68/18/e5/6818e59fe9c1b059d679cbf35ab122c9.jpg", "00000000-0000-0000-0000-00000000003e_t.jpg", "The Dragonborn Daedric Armor", 835 },
                    { new Guid("00000000-0000-0000-0000-000000000040"), 0, new Guid("00000000-0000-0000-0000-000000000013"), new DateTime(2024, 1, 7, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, true, null, new Guid("00000000-0000-0000-0000-000000000005"), 0, null, 0, 1, "https://i.pinimg.com/originals/2b/ef/bd/2befbd3f91aa23db55eea433151c7992.jpg", "00000000-0000-0000-0000-000000000040_t.jpg", "Giáp thiên thần", 999 },
                    { new Guid("00000000-0000-0000-0000-000000000041"), 0, new Guid("00000000-0000-0000-0000-000000000011"), new DateTime(2024, 2, 8, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, false, null, new Guid("00000000-0000-0000-0000-000000000003"), 0, null, 0, 1, "https://i.pinimg.com/564x/00/ed/65/00ed65b58c95c4d694fc95598af0a885.jpg", "00000000-0000-0000-0000-000000000041_t.jpg", "Hoàng hôn", 452 },
                    { new Guid("00000000-0000-0000-0000-000000000042"), 0, new Guid("00000000-0000-0000-0000-00000000000f"), new DateTime(2024, 2, 9, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, false, null, new Guid("00000000-0000-0000-0000-000000000003"), 0, null, 0, 1, "https://i.pinimg.com/564x/25/dc/d6/25dcd6a770856b37bdd3fd69551d626d.jpg", "00000000-0000-0000-0000-000000000042_t.jpg", "Ngẫu hứng phát họa", 1052 },
                    { new Guid("00000000-0000-0000-0000-000000000043"), 0, new Guid("00000000-0000-0000-0000-00000000000f"), new DateTime(2024, 2, 10, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, false, null, new Guid("00000000-0000-0000-0000-000000000003"), 0, null, 0, 1, "https://i.pinimg.com/736x/d1/f7/e1/d1f7e116c4dc23c61f9523cea80ee909.jpg", "00000000-0000-0000-0000-000000000043_t.jpg", "Cô gái vàng", 599 },
                    { new Guid("00000000-0000-0000-0000-000000000044"), 0, new Guid("00000000-0000-0000-0000-00000000000f"), new DateTime(2024, 1, 15, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, false, null, new Guid("00000000-0000-0000-0000-000000000003"), 0, null, 0, 1, "https://i.pinimg.com/736x/cf/7e/ff/cf7eff78ddcdeeed6b6074bd441eb721.jpg", "00000000-0000-0000-0000-000000000044_t.jpg", "Chàng trai", 654 },
                    { new Guid("00000000-0000-0000-0000-000000000045"), 0, new Guid("00000000-0000-0000-0000-000000000013"), new DateTime(2024, 1, 20, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, false, null, new Guid("00000000-0000-0000-0000-000000000003"), 0, null, 0, 1, "https://i.pinimg.com/564x/47/3e/f9/473ef931430e161d83f2aa5c8844d56a.jpg", "00000000-0000-0000-0000-000000000045_t.jpg", "Red dead Redemption", 654 },
                    { new Guid("00000000-0000-0000-0000-000000000046"), 0, new Guid("00000000-0000-0000-0000-00000000000a"), new DateTime(2024, 1, 25, 15, 20, 45, 890, DateTimeKind.Utc), null, null, "Universal / Fantasy Pixel Art GUI Kit for your new project, featuring Windowed and Fullscreen views to fit all your needs!", false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://assetstorev1-prd-cdn.unity3d.com/key-image/da31eccd-b255-4898-bec2-2e1b1eb39092.webp", "00000000-0000-0000-0000-000000000046_t.jpg", "Pixel Art GUI / UI Kit + 151 icons!", 1444 },
                    { new Guid("00000000-0000-0000-0000-000000000047"), 0, new Guid("00000000-0000-0000-0000-00000000000c"), new DateTime(2023, 12, 30, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://storage.googleapis.com/artworkia-storage-public/Artwork/VSCode.png", "00000000-0000-0000-0000-000000000047_t.jpg", "Visual Studio Code Redesign", 928 },
                    { new Guid("00000000-0000-0000-0000-000000000048"), 0, new Guid("00000000-0000-0000-0000-00000000000c"), new DateTime(2024, 2, 5, 15, 20, 45, 890, DateTimeKind.Utc), null, null, "Mẫu thiết kế ứng dụng di động đẹp mắt cho các dự án của bạn", false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://i.pinimg.com/564x/2c/5d/80/2c5d80e9c0240a65d6397f9991352855.jpg", "00000000-0000-0000-0000-000000000048_t.jpg", "Fluid Background", 109 },
                    { new Guid("00000000-0000-0000-0000-000000000049"), 0, new Guid("00000000-0000-0000-0000-00000000000c"), new DateTime(2023, 12, 5, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://i.pinimg.com/564x/71/01/da/7101da0949d85dfbf7c24200f1fcbdfd.jpg", "00000000-0000-0000-0000-000000000048_t.jpg", "Moona hoshinova", 376 },
                    { new Guid("00000000-0000-0000-0000-00000000004a"), 0, new Guid("00000000-0000-0000-0000-000000000014"), new DateTime(2024, 1, 10, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, true, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://i.pinimg.com/564x/61/29/93/61299356ecd161b0ac86c94869960084.jpg", "00000000-0000-0000-0000-00000000004a_t.jpg", "Artificial Intelligence Robot Cyberpunk High Tech Sci-fi Poster", 567 },
                    { new Guid("00000000-0000-0000-0000-00000000004b"), 0, new Guid("00000000-0000-0000-0000-000000000014"), new DateTime(2024, 2, 1, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, true, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://i.pinimg.com/564x/a3/ef/9c/a3ef9c4379f92f56607f1f685e7835ce.jpg", "00000000-0000-0000-0000-00000000004b_t.jpg", "Intelligent Robots Metaverse Cyberpunk Cat Robot Poster", 833 },
                    { new Guid("00000000-0000-0000-0000-00000000004c"), 0, new Guid("00000000-0000-0000-0000-000000000014"), new DateTime(2024, 1, 15, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, true, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://i.pinimg.com/564x/19/d5/fb/19d5fbfe0a970510bfe47aa148e0b71e.jpg", "00000000-0000-0000-0000-00000000004c_t.jpg", "Cyberpunk Robot Poster", 1133 },
                    { new Guid("00000000-0000-0000-0000-00000000004d"), 0, new Guid("00000000-0000-0000-0000-00000000000c"), new DateTime(2024, 1, 7, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://storage.googleapis.com/artworkia-storage-public/Artwork/IntellijLogo.png", "00000000-0000-0000-0000-00000000004d_t.jpg", "IntelliJ Redesign", 1228 },
                    { new Guid("00000000-0000-0000-0000-00000000004e"), 0, new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2024, 3, 2, 15, 20, 45, 890, DateTimeKind.Utc), null, null, null, false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://storage.googleapis.com/artworkia-storage-public/Artwork/phucche1.png", "00000000-0000-0000-0000-00000000004e_t.jpg", "Phục chế ảnh nữ liệt sĩ", 433 },
                    { new Guid("00000000-0000-0000-0000-00000000004f"), 0, new Guid("00000000-0000-0000-0000-000000000008"), new DateTime(2024, 1, 25, 15, 30, 3, 678, DateTimeKind.Utc), null, null, "Chính chủ bán BT Phong Lan 04 - 34. DT 343.8m² nhà thô, sông sau nhà rộng miên man. Sân trước nhà 1000m.\r\n                                Hướng Tây Nam.\r\n                                Giá 220tr/m².", false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://file4.batdongsan.com.vn/resize/1275x717/2024/03/07/20240307144316-1cca_wm.jpg", "00000000-0000-0000-0000-00000000004f_t.jpg", "Chính chủ bán BT Phong Lan 04-34, 343.8m2 nhà thô, sông sau nhà rộng miên man, sân trước nhà 1000m2", 110 },
                    { new Guid("00000000-0000-0000-0000-000000000050"), 0, new Guid("00000000-0000-0000-0000-000000000015"), new DateTime(2024, 3, 5, 15, 30, 3, 678, DateTimeKind.Utc), null, null, null, false, null, new Guid("00000000-0000-0000-0000-000000000001"), 0, null, 0, 1, "https://i.pinimg.com/originals/86/15/50/8615509334c99ba0c11a9feac151a79e.jpg", "00000000-0000-0000-0000-000000000050_t.jpg", "Mèo con", 1160 }
                });

            migrationBuilder.InsertData(
                table: "Block",
                columns: new[] { "BlockedId", "BlockingId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000017"), new Guid("00000000-0000-0000-0000-000000000005") }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CategoryName", "ParentCategory" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000009"), "Nghệ thuật số", new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-00000000000d"), "Phác họa", new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-00000000000e"), "Thiết kế nhân vật", new Guid("00000000-0000-0000-0000-000000000001") }
                });

            migrationBuilder.InsertData(
                table: "ChatBox",
                columns: new[] { "Id", "AccountId_1", "AccountId_2" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000005"), new Guid("00000000-0000-0000-0000-000000000002") });

            migrationBuilder.InsertData(
                table: "Collection",
                columns: new[] { "Id", "CollectionName", "CreatedBy", "CreatedOn", "Privacy" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "Trò chơi yêu thích", new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "Hoàng hôn", new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "Ảnh bìa đẹp", new Guid("00000000-0000-0000-0000-00000000000f"), new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0 }
                });

            migrationBuilder.InsertData(
                table: "Follow",
                columns: new[] { "FollowedId", "FollowingId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000005"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-000000000005") }
                });

            migrationBuilder.InsertData(
                table: "Notification",
                columns: new[] { "Id", "Content", "CreatedOn", "IsSeen", "NotifyType", "ReferencedAccountId", "ReferencedArtworkId", "SentToAccount" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "Chào mừng đến với Artworkia", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 0, null, null, new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "Chào mừng đến với Artworkia", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 0, null, null, new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "Chào mừng đến với Artworkia", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 0, null, null, new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "Chào mừng đến với Artworkia", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 0, null, null, new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "Chào mừng đến với Artworkia", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 0, null, null, new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("00000000-0000-0000-0000-000000000006"), "Chào mừng đến với Artworkia", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 0, null, null, new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-000000000007"), "Chào mừng đến với Artworkia", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 0, null, null, new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000008"), "Chào mừng đến với Artworkia", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 0, null, null, new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-000000000009"), "Chào mừng đến với Artworkia", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 0, null, null, new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-00000000000a"), "Chào mừng đến với Artworkia", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 0, null, null, new Guid("00000000-0000-0000-0000-00000000000a") },
                    { new Guid("00000000-0000-0000-0000-00000000000b"), "Chào mừng đến với Artworkia", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 0, null, null, new Guid("00000000-0000-0000-0000-00000000000b") },
                    { new Guid("00000000-0000-0000-0000-00000000000c"), "Chào mừng đến với Artworkia", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 0, null, null, new Guid("00000000-0000-0000-0000-00000000000c") },
                    { new Guid("00000000-0000-0000-0000-00000000000d"), "Chào mừng đến với Artworkia", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 0, null, null, new Guid("00000000-0000-0000-0000-00000000000d") },
                    { new Guid("00000000-0000-0000-0000-00000000000e"), "Chào mừng đến với Artworkia", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 0, null, null, new Guid("00000000-0000-0000-0000-00000000000e") },
                    { new Guid("00000000-0000-0000-0000-00000000000f"), "Chào mừng đến với Artworkia", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 0, null, null, new Guid("00000000-0000-0000-0000-00000000000f") },
                    { new Guid("00000000-0000-0000-0000-000000000010"), "Chào mừng đến với Artworkia", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 0, null, null, new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000011"), "Chào mừng đến với Artworkia", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 0, null, null, new Guid("00000000-0000-0000-0000-000000000011") },
                    { new Guid("00000000-0000-0000-0000-000000000012"), "Chào mừng đến với Artworkia", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 0, null, null, new Guid("00000000-0000-0000-0000-000000000012") },
                    { new Guid("00000000-0000-0000-0000-000000000013"), "Chào mừng đến với Artworkia", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 0, null, null, new Guid("00000000-0000-0000-0000-000000000013") },
                    { new Guid("00000000-0000-0000-0000-000000000014"), "Chào mừng đến với Artworkia", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 0, null, null, new Guid("00000000-0000-0000-0000-000000000014") },
                    { new Guid("00000000-0000-0000-0000-000000000015"), "Chào mừng đến với Artworkia", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 0, null, null, new Guid("00000000-0000-0000-0000-000000000015") },
                    { new Guid("00000000-0000-0000-0000-000000000016"), "Chào mừng đến với Artworkia", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 0, null, null, new Guid("00000000-0000-0000-0000-000000000016") },
                    { new Guid("00000000-0000-0000-0000-000000000017"), "Chào mừng đến với Artworkia", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 0, null, null, new Guid("00000000-0000-0000-0000-000000000017") },
                    { new Guid("00000000-0000-0000-0000-000000000018"), "Chào mừng đến với Artworkia", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 0, null, null, new Guid("00000000-0000-0000-0000-000000000018") },
                    { new Guid("00000000-0000-0000-0000-000000000019"), "Chào mừng đến với Artworkia", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, 0, null, null, new Guid("00000000-0000-0000-0000-000000000019") },
                    { new Guid("00000000-0000-0000-0000-00000000001a"), "Cảnh báo bảo mật", new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), true, 1, null, null, new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("00000000-0000-0000-0000-00000000001b"), "Người dùng [hoanganh] vừa theo dõi bạn", new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), true, 2, null, null, new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("00000000-0000-0000-0000-00000000001c"), "Người dùng [lamlam] vừa theo dõi bạn", new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), true, 2, null, null, new Guid("00000000-0000-0000-0000-000000000005") }
                });

            migrationBuilder.InsertData(
                table: "Report",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Note", "Reason", "ReportEntity", "ReportType", "State", "TargetId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 11, 27, 15, 40, 28, 901, DateTimeKind.Utc), null, "Ảnh AI", 2, 4, 1, new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 12, 7, 15, 40, 28, 901, DateTimeKind.Utc), "Đã cảnh cáo", "Nội dung không phù hợp", 2, 4, 1, new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 12, 27, 15, 40, 28, 901, DateTimeKind.Utc), "Đã kiểm tra", "Không thuộc về tác giả", 2, 3, 2, new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 12, 28, 15, 40, 28, 901, DateTimeKind.Utc), null, "Nội dung không cho phép", 2, 5, 1, new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 12, 29, 15, 40, 28, 901, DateTimeKind.Utc), null, "Ảnh AI", 2, 4, 2, new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("00000000-0000-0000-0000-000000000006"), new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 11, 27, 15, 40, 28, 901, DateTimeKind.Utc), null, "Không có nghệ thuật", 2, 4, 2, new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-000000000007"), new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 11, 28, 15, 40, 28, 901, DateTimeKind.Utc), null, "Đây là spam", 2, 2, 1, new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000100"), new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2024, 1, 1, 15, 40, 28, 901, DateTimeKind.Utc), null, "Tài khoản spam", 0, 2, 2, new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000101"), new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2024, 1, 2, 15, 40, 28, 901, DateTimeKind.Utc), null, "Tài khoản đăng tác phẩm trùng lặp", 0, 4, 1, new Guid("00000000-0000-0000-0000-000000000017") }
                });

            migrationBuilder.InsertData(
                table: "Service",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "DeliveryTime", "Description", "LastModificatedBy", "LastModificatedOn", "NumberOfConcept", "NumberOfRevision", "ServiceName", "StartingPrice", "Thumbnail" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "2 - 3 tuần", "Mô tả Dịch vụ thiết kế", null, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), 2, 2, "Dịch vụ thiết kế", 100000.0, "https://3.imimg.com/data3/SQ/DN/MY-16602737/banner-design-services.png" });

            migrationBuilder.InsertData(
                table: "Service",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "DeliveryTime", "Description", "LastModificatedBy", "NumberOfConcept", "NumberOfRevision", "ServiceName", "StartingPrice", "Thumbnail" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "4 - 6 tuần", "Mô tả Dịch vụ phát triển website", null, 3, 3, "Dịch vụ phát triển website", 150000.0, "https://laptopdieplinh.com/uploads/7%20c%C3%B4ng%20c%E1%BB%A5%20ph%C3%A1t%20tri%E1%BB%83n%20website%20b%E1%BA%A1n%20c%E1%BA%A7n%20bi%E1%BA%BFt%20-%200.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "1 - 2 tháng", "Dịch vụ thiết kế nhân vật trò chơi 2D, từ indie tới AAA", null, 2, 2, "Dịch vụ thiết kế nhân vật trò chơi 2D", 100000.0, "https://static.vecteezy.com/system/resources/previews/023/289/956/original/cute-monster-doodle-character-design-flat-illustration-suitable-for-poster-banner-mascot-or-event-related-template-free-vector.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "3 - 4 tháng", "Dịch vụ thiết kế nhân vật trò chơi 3D, từ indie tới AAA", null, 2, 2, "Dịch vụ thiết kế nhân vật trò chơi 3D", 120000.0, "https://masterbundles.com/wp-content/uploads/2023/11/media-2-816.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "1 - 2 tuần", "Tôi không muốn làm việc phục dựng, phục chế ảnh như một động tác khô khan về kỹ thuật, tôi còn muốn kể lại những câu chuyện thực tế về liệt sĩ bằng ngôn ngữ hội họa và nhiếp ảnh", null, 2, 1, "Phục chế chân dung liệt sĩ", 20000.0, "https://media-cdn-v2.laodong.vn/storage/newsportal/2023/7/26/1221583/CAF7870B-5832-44DC-B.jpeg" },
                    { new Guid("00000000-0000-0000-0000-000000000006"), new Guid("00000000-0000-0000-0000-00000000000b"), new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "1 - 2 tháng", "Thiết kế hình ảnh khoa học viễn tưởng phong phú và độc đáo, tùy chỉnh cho không gian của bạn. Từ các bức tranh tường tường đồ sộ đến các tác phẩm nghệ thuật nhỏ hơn, chúng tôi mang đến sự sáng tạo và phong cách cho mọi dự án.", null, 3, 2, "Thiết kế hình ảnh khoa học viễn tưởng", 50000.0, "https://cdn.musicbed.com/image/upload/c_fill,dpr_auto,f_auto,g_auto,q_40,w_1200,h_630/v1658956186/production/albums/9887" },
                    { new Guid("00000000-0000-0000-0000-000000000007"), new Guid("00000000-0000-0000-0000-000000000012"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "1 - 2 tháng", "Bạn đang muốn tạo ra một ứng dụng di động ấn tượng và thu hút người dùng? Figma chính là công cụ hoàn hảo để biến ý tưởng của bạn thành hiện thực.", null, 3, 2, "Thiết kế Mobile App bằng Figma", 80000.0, "https://bs-uploads.toptal.io/blackfish-uploads/components/seo/5914508/og_image/optimized/figma-design-tool-e09b94850458e37b90442beb2a9192cc.png" },
                    { new Guid("00000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-000000000013"), new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "1 - 2 tuần", "Mang đến sức sống cho những thế giới ảo diệu! Bạn đang tìm kiếm dịch vụ thiết kế nhân vật game 3D chất lượng cao để nâng tầm dự án của mình ? Hãy đến với chúng tôi!", null, 1, 2, "Thiết kế nhân vật game 3D", 30000.0, "https://i.pinimg.com/564x/d4/37/b3/d437b301b2408a5772d9be18d2dcbdee.jpg" },
                    { new Guid("00000000-0000-0000-0000-000000000009"), new Guid("00000000-0000-0000-0000-000000000014"), new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "1 - 2 tuần", "Mang đến sức sống cho những thế giới ảo diệu! Bạn đang tìm kiếm dịch vụ thiết kế nhân vật game 3D chất lượng cao để nâng tầm dự án của mình ? Hãy đến với chúng tôi!", null, 1, 2, "Thiết kế đồ họa Cyberpunk", 40000.0, "https://www.cyberpunk.net/build/images/pre-order/buy-b/keyart-ue-en@2x-cd66fd0d.jpg" },
                    { new Guid("00000000-0000-0000-0000-00000000000a"), new Guid("00000000-0000-0000-0000-00000000000c"), new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "1 - 2 tuần", "Chuyên gia thiết kế logo cho các trò chơi, nhãn hàng", null, 2, 2, "Thiết kế logo thương hiệu", 60000.0, "https://digihubmedia.in/wp-content/uploads/2021/05/logo-design-service-in-sangli-pune-scaled.jpg" },
                    { new Guid("00000000-0000-0000-0000-00000000000b"), new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, "1 - 2 tháng", "Dịch vụ thiết kế bìa sách theo yêu cầu", null, 2, 2, "Thiết kế bìa sách", 80000.0, "https://cms.typenetwork.com/media/original_images/banner_KvvXKmF.png" }
                });

            migrationBuilder.InsertData(
                table: "Wallet",
                columns: new[] { "Id", "AccountId", "Balance", "BankCode", "BankName", "WithdrawInformation", "WithdrawMethod" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), 10000.0, null, null, "0902287461", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000002"), 185000.0, null, null, "0939959417", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-000000000003"), 10000.0, null, null, "0912695680", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new Guid("00000000-0000-0000-0000-000000000004"), 21000.0, null, null, "0365960823", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new Guid("00000000-0000-0000-0000-000000000005"), 670000.0, null, null, "0398550944", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000009"), new Guid("00000000-0000-0000-0000-000000000009"), 0.0, null, null, "0365960823", 0 },
                    { new Guid("00000000-0000-0000-0000-00000000000a"), new Guid("00000000-0000-0000-0000-00000000000a"), 142500.0, null, null, "0365960823", 0 },
                    { new Guid("00000000-0000-0000-0000-00000000000b"), new Guid("00000000-0000-0000-0000-00000000000b"), 47500.0, null, null, "0365960823", 0 },
                    { new Guid("00000000-0000-0000-0000-00000000000c"), new Guid("00000000-0000-0000-0000-00000000000c"), 0.0, null, null, "0365960823", 0 },
                    { new Guid("00000000-0000-0000-0000-00000000000d"), new Guid("00000000-0000-0000-0000-00000000000d"), 0.0, null, null, "0365960823", 0 },
                    { new Guid("00000000-0000-0000-0000-00000000000e"), new Guid("00000000-0000-0000-0000-00000000000e"), 0.0, null, null, "0365960823", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000010"), new Guid("00000000-0000-0000-0000-000000000010"), 0.0, null, null, "0365960823", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000011"), new Guid("00000000-0000-0000-0000-000000000011"), 0.0, null, null, "0365960823", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000012"), new Guid("00000000-0000-0000-0000-000000000012"), 85500.0, null, null, "0365960823", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000013"), new Guid("00000000-0000-0000-0000-000000000013"), 0.0, null, null, "0365960823", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000014"), new Guid("00000000-0000-0000-0000-000000000014"), 0.0, null, null, "0365960823", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000015"), new Guid("00000000-0000-0000-0000-000000000015"), 0.0, null, null, "0365960823", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000016"), new Guid("00000000-0000-0000-0000-000000000016"), 0.0, null, null, "0365960823", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000017"), new Guid("00000000-0000-0000-0000-000000000017"), 0.0, null, null, "0365960823", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000018"), new Guid("00000000-0000-0000-0000-000000000018"), 0.0, null, null, "0365960823", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000019"), new Guid("00000000-0000-0000-0000-000000000019"), 0.0, null, null, "0365960823", 0 }
                });

            migrationBuilder.InsertData(
                table: "WalletHistory",
                columns: new[] { "Id", "Amount", "AppTransId", "CreatedBy", "CreatedOn", "Fee", "PaymentMethod", "TransactionStatus", "Type", "WalletBalance" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), 100000.0, "240128_7635981", new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 11, 7, 15, 30, 3, 678, DateTimeKind.Utc), 0.0, 0, 1, 1, 100000.0 },
                    { new Guid("00000000-0000-0000-0000-000000000002"), -50000.0, "180623_2054176", new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 11, 10, 21, 20, 10, 234, DateTimeKind.Utc), 0.0, 0, 1, 0, 50000.0 },
                    { new Guid("00000000-0000-0000-0000-000000000003"), 60000.0, "210430_6849203", new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 11, 14, 2, 59, 59, 0, DateTimeKind.Utc), 0.0, 0, 1, 1, 110000.0 },
                    { new Guid("00000000-0000-0000-0000-000000000005"), 10000.0, "220506_1478963", new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2023, 11, 16, 9, 37, 42, 345, DateTimeKind.Utc), 0.0, 0, 1, 1, 10000.0 },
                    { new Guid("00000000-0000-0000-0000-000000000008"), 100000.0, "171212_4357692", new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2024, 1, 1, 12, 40, 28, 901, DateTimeKind.Utc), 0.0, 0, 1, 1, 100000.0 },
                    { new Guid("00000000-0000-0000-0000-000000000009"), -80000.0, "160509_9270134", new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2024, 1, 4, 1, 30, 15, 567, DateTimeKind.Utc), 0.0, 0, 1, 0, 20000.0 },
                    { new Guid("00000000-0000-0000-0000-00000000000a"), 42000.0, "250321_4685027", new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2024, 1, 4, 12, 40, 28, 901, DateTimeKind.Utc), 0.0, 0, 1, 1, 62000.0 },
                    { new Guid("00000000-0000-0000-0000-00000000000b"), 1000000.0, "231205_7890123", new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2023, 12, 20, 12, 37, 42, 345, DateTimeKind.Utc), 0.0, 0, 1, 1, 1000000.0 },
                    { new Guid("00000000-0000-0000-0000-00000000000e"), 10000.0, "210817_8901234", new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2023, 11, 3, 17, 45, 20, 123, DateTimeKind.Utc), 0.0, 0, 1, 1, 10000.0 }
                });

            migrationBuilder.InsertData(
                table: "Asset",
                columns: new[] { "Id", "ArtworkId", "AssetName", "AssetTitle", "ContentType", "DeletedBy", "DeletedOn", "Description", "LastModificatedBy", "Location", "Price", "Size" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000004"), "CommercialAsset.rar", "Dấu vết của quá khứ", "rar", null, null, "Ảnh gốc, phục hồi chất lượng", null, "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4", 10000.0, 8000000m },
                    { new Guid("00000000-0000-0000-0000-000000000010"), new Guid("00000000-0000-0000-0000-000000000008"), "CommercialAsset.rar", "File PTS tuyển tập minh hoạ sách tâm lý", "rar", null, null, "tập tin PTS tuyển tập minh hoạ sách tâm lý sẽ cung cấp một cái nhìn tổng quan và thú vị.", null, "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4", 20000.0, 8000000m },
                    { new Guid("00000000-0000-0000-0000-000000000020"), new Guid("00000000-0000-0000-0000-00000000000e"), "CommercialAsset.rar", "Robot PTS", "rar", null, null, "Tặng các bạn", null, "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4", 0.0, 8000000m },
                    { new Guid("00000000-0000-0000-0000-000000000030"), new Guid("00000000-0000-0000-0000-00000000000f"), "CommercialAsset.rar", "Ảnh cánh cụt", "rar", null, null, "Tổng hợp cánh cụt fullsize", null, "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4", 12000.0, 8000000m },
                    { new Guid("00000000-0000-0000-0000-000000000031"), new Guid("00000000-0000-0000-0000-00000000000f"), "CommercialAsset.rar", "Ảnh cánh cụt khác", "rar", null, null, "Tổng hợp cánh cụt fullsize", null, "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4", 12000.0, 8000000m },
                    { new Guid("00000000-0000-0000-0000-000000000040"), new Guid("00000000-0000-0000-0000-000000000012"), "CommercialAsset.rar", "Hình ảnh giới thiệu nhân vật & Đếm ngược ra mắt game", "rar", null, null, "File PTS, tùy ý chỉnh sửa", null, "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4", 30000.0, 8000000m },
                    { new Guid("00000000-0000-0000-0000-000000000050"), new Guid("00000000-0000-0000-0000-000000000014"), "CommercialAsset.rar", "Không gian vũ trụ", "rar", null, null, "File PTS, A collection of 173 wallpapers in 4K resolution from THE SIGHTS OF SPACE", null, "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4", 50000.0, 8000000m },
                    { new Guid("00000000-0000-0000-0000-000000000060"), new Guid("00000000-0000-0000-0000-000000000015"), "CommercialAsset.rar", "Tương lai loài người", "rar", null, null, "File PTS, A collection of 173 wallpapers in 4K resolution", null, "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4", 50000.0, 8000000m },
                    { new Guid("00000000-0000-0000-0000-000000000063"), new Guid("00000000-0000-0000-0000-00000000001b"), "CommercialAsset.rar", "Ảnh gốc", "rar", null, null, "File PTS, tùy ý chỉnh sửa", null, "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4", 0.0, 8000000m },
                    { new Guid("00000000-0000-0000-0000-000000000068"), new Guid("00000000-0000-0000-0000-000000000021"), "CommercialAsset.rar", "Ảnh gốc", "rar", null, null, "File PTS, tùy ý chỉnh sửa", null, "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4", 0.0, 8000000m },
                    { new Guid("00000000-0000-0000-0000-000000000069"), new Guid("00000000-0000-0000-0000-000000000022"), "CommercialAsset.rar", "Ảnh gốc (xóa)", "rar", new Guid("00000000-0000-0000-0000-000000000018"), new DateTime(2024, 1, 3, 15, 20, 45, 890, DateTimeKind.Utc), "File PTS, tùy ý chỉnh sửa", null, "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4", 20000.0, 8000000m },
                    { new Guid("00000000-0000-0000-0000-00000000006a"), new Guid("00000000-0000-0000-0000-000000000022"), "CommercialAsset.rar", "Ảnh gốc (thêm)", "rar", null, null, "File PTS, tùy ý chỉnh sửa", null, "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4", 25000.0, 8000000m },
                    { new Guid("00000000-0000-0000-0000-000000000070"), new Guid("00000000-0000-0000-0000-000000000033"), "CommercialAsset.rar", "Figma Full", "rar", null, null, "All 100 pages + 200 icons in Figma", null, "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4", 100000.0, 8000000m },
                    { new Guid("00000000-0000-0000-0000-000000000080"), new Guid("00000000-0000-0000-0000-000000000034"), "CommercialAsset.rar", "Figma Full", "rar", null, null, "All 100 pages + 200 icons in Figma", null, "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4", 90000.0, 8000000m },
                    { new Guid("00000000-0000-0000-0000-000000000081"), new Guid("00000000-0000-0000-0000-000000000035"), "CommercialAsset.rar", "Figma Full", "rar", null, null, "All 100 pages + 200 icons in Figma", null, "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4", 120000.0, 8000000m },
                    { new Guid("00000000-0000-0000-0000-000000000082"), new Guid("00000000-0000-0000-0000-000000000035"), "CommercialAsset.rar", "Figma Full", "rar", null, null, "All 100 pages + 200 icons in Figma", null, "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4", 120000.0, 8000000m },
                    { new Guid("00000000-0000-0000-0000-000000000083"), new Guid("00000000-0000-0000-0000-000000000046"), "CommercialAsset.rar", "Pixel Art GUI / UI Kit + 151 icons!", "rar", null, null, "Universal / Fantasy Pixel Art GUI Kit for your new project, featuring Windowed and Fullscreen views to fit all your needs!", null, "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4", 150000.0, 8000000m },
                    { new Guid("00000000-0000-0000-0000-000000000084"), new Guid("00000000-0000-0000-0000-000000000047"), "CommercialAsset.rar", "Logo Asset", "rar", null, null, "Original PNG", null, "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4", 10000.0, 8000000m },
                    { new Guid("00000000-0000-0000-0000-000000000085"), new Guid("00000000-0000-0000-0000-000000000048"), "CommercialAsset.rar", "Background Fullsize", "rar", null, null, "Original PNG", null, "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4", 0.0, 8000000m },
                    { new Guid("00000000-0000-0000-0000-000000000090"), new Guid("00000000-0000-0000-0000-00000000004d"), "CommercialAsset.rar", "Logo Asset", "rar", null, null, "Original PNG", null, "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4", 10000.0, 8000000m },
                    { new Guid("00000000-0000-0000-0000-000000000091"), new Guid("00000000-0000-0000-0000-00000000003f"), "CommercialAsset.rar", "The Dragonborn Daedric Armor", "rar", null, null, "All file asset 3d model", null, "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4", 0.0, 8000000m }
                });

            migrationBuilder.InsertData(
                table: "Bookmark",
                columns: new[] { "ArtworkId", "CollectionId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000024"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-00000000003e"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-00000000003f"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000041"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000045"), new Guid("00000000-0000-0000-0000-000000000001") }
                });

            migrationBuilder.InsertData(
                table: "CategoryArtworkDetail",
                columns: new[] { "ArtworkId", "CategoryId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000006"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-00000000000a"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-00000000000b"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-00000000000d"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-00000000000e"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-00000000000f"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000011"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000013"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000017"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000018"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000019"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-00000000001a"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-00000000001b"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-00000000001c"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-00000000001d"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-00000000001e"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-00000000001f"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000021"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000022"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000023"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000024"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000025"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000026"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000027"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000028"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000029"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-00000000002a"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-00000000002b"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-00000000002e"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-00000000002f"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000031"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000041"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000050"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000007"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-00000000000e"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000010"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000012"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000014"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000015"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000016"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000020"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-00000000002c"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-00000000002d"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000030"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000031"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000032"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-00000000003c"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-00000000003d"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-00000000003e"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-00000000003f"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000040"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000045"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000046"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000047"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000048"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000049"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-00000000004a"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-00000000004b"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-00000000004c"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-00000000004d"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000033"), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000034"), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000035"), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000046"), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000048"), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-00000000001c"), new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-00000000001d"), new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-00000000001e"), new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-00000000001f"), new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-00000000004f"), new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000022"), new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-00000000000e"), new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000048"), new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-00000000004d"), new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000014"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-000000000015"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-000000000016"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-00000000003c"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-00000000003d"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-00000000003e"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-00000000003f"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-000000000040"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-000000000045"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-00000000004a"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-00000000004b"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-00000000004c"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000006"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000009"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-00000000000c"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-00000000000f"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000014"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000015"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000016"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-00000000004a"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-00000000004b"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-00000000004c"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-00000000004e"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new Guid("00000000-0000-0000-0000-00000000000a") },
                    { new Guid("00000000-0000-0000-0000-000000000036"), new Guid("00000000-0000-0000-0000-00000000000a") },
                    { new Guid("00000000-0000-0000-0000-000000000037"), new Guid("00000000-0000-0000-0000-00000000000a") },
                    { new Guid("00000000-0000-0000-0000-000000000038"), new Guid("00000000-0000-0000-0000-00000000000a") },
                    { new Guid("00000000-0000-0000-0000-000000000039"), new Guid("00000000-0000-0000-0000-00000000000a") },
                    { new Guid("00000000-0000-0000-0000-00000000003a"), new Guid("00000000-0000-0000-0000-00000000000a") },
                    { new Guid("00000000-0000-0000-0000-00000000003b"), new Guid("00000000-0000-0000-0000-00000000000a") },
                    { new Guid("00000000-0000-0000-0000-00000000004f"), new Guid("00000000-0000-0000-0000-00000000000a") },
                    { new Guid("00000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-00000000000b") },
                    { new Guid("00000000-0000-0000-0000-000000000012"), new Guid("00000000-0000-0000-0000-00000000000b") },
                    { new Guid("00000000-0000-0000-0000-000000000033"), new Guid("00000000-0000-0000-0000-00000000000b") },
                    { new Guid("00000000-0000-0000-0000-000000000034"), new Guid("00000000-0000-0000-0000-00000000000b") },
                    { new Guid("00000000-0000-0000-0000-000000000035"), new Guid("00000000-0000-0000-0000-00000000000b") },
                    { new Guid("00000000-0000-0000-0000-000000000046"), new Guid("00000000-0000-0000-0000-00000000000b") },
                    { new Guid("00000000-0000-0000-0000-000000000047"), new Guid("00000000-0000-0000-0000-00000000000b") },
                    { new Guid("00000000-0000-0000-0000-00000000004d"), new Guid("00000000-0000-0000-0000-00000000000b") },
                    { new Guid("00000000-0000-0000-0000-000000000049"), new Guid("00000000-0000-0000-0000-00000000000c") },
                    { new Guid("00000000-0000-0000-0000-000000000042"), new Guid("00000000-0000-0000-0000-00000000000d") },
                    { new Guid("00000000-0000-0000-0000-000000000043"), new Guid("00000000-0000-0000-0000-00000000000d") },
                    { new Guid("00000000-0000-0000-0000-000000000044"), new Guid("00000000-0000-0000-0000-00000000000d") },
                    { new Guid("00000000-0000-0000-0000-000000000012"), new Guid("00000000-0000-0000-0000-00000000000e") },
                    { new Guid("00000000-0000-0000-0000-00000000003d"), new Guid("00000000-0000-0000-0000-00000000000e") },
                    { new Guid("00000000-0000-0000-0000-00000000003e"), new Guid("00000000-0000-0000-0000-00000000000e") },
                    { new Guid("00000000-0000-0000-0000-00000000003f"), new Guid("00000000-0000-0000-0000-00000000000e") },
                    { new Guid("00000000-0000-0000-0000-000000000040"), new Guid("00000000-0000-0000-0000-00000000000e") }
                });

            migrationBuilder.InsertData(
                table: "CategoryServiceDetail",
                columns: new[] { "CategoryId", "ServiceId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-000000000006") }
                });

            migrationBuilder.InsertData(
                table: "Comment",
                columns: new[] { "Id", "ArtworkId", "Content", "CreatedBy", "CreatedOn", "DeletedBy", "DeletedOn", "LastModificatedBy", "ReplyId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), "Đây là một bức tranh rất đẹp", new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, null },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000001"), "Minh hoạ xuất sắc", new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, null },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-000000000001"), "10 điểm", new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, null },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new Guid("00000000-0000-0000-0000-000000000001"), "Cute and funny", new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, null },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new Guid("00000000-0000-0000-0000-000000000001"), "Like", new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, null },
                    { new Guid("00000000-0000-0000-0000-000000000006"), new Guid("00000000-0000-0000-0000-000000000001"), "Hoàng hôn lấp lánh quá điiii!", new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2024, 3, 21, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, null },
                    { new Guid("00000000-0000-0000-0000-000000000007"), new Guid("00000000-0000-0000-0000-000000000005"), "Đây là một bức tranh rất đẹp", new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2023, 12, 7, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, null },
                    { new Guid("00000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-000000000005"), "Minh hoạ xuất sắc", new Guid("00000000-0000-0000-0000-000000000009"), new DateTime(2023, 12, 8, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, null },
                    { new Guid("00000000-0000-0000-0000-000000000009"), new Guid("00000000-0000-0000-0000-000000000005"), "10 điểm", new Guid("00000000-0000-0000-0000-00000000000a"), new DateTime(2023, 12, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, null },
                    { new Guid("00000000-0000-0000-0000-00000000000a"), new Guid("00000000-0000-0000-0000-000000000005"), "So peak", new Guid("00000000-0000-0000-0000-00000000000b"), new DateTime(2023, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Image",
                columns: new[] { "Id", "ArtworkId", "ImageHash", "ImageName", "LastModificatedBy", "LastModificatedOn", "Location", "Order" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), null, "00000000-0000-0000-0000-000000000001_i0.jpg", null, null, "https://i.pinimg.com/736x/8d/f7/be/8df7be5e052e97b824e6b0f783309161.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000001"), null, "00000000-0000-0000-0000-000000000001_i1.jpg", null, null, "https://i.pinimg.com/564x/18/f5/01/18f50109029ade270f0759724a0c19f1.jpg", 1 },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-000000000002"), null, "00000000-0000-0000-0000-000000000002_i0.jpg", null, null, "https://th.bing.com/th/id/OIG.mMOt1xWJJCHsRoPJXtHQ?pid=ImgGn", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new Guid("00000000-0000-0000-0000-000000000003"), null, "00000000-0000-0000-0000-000000000003_i0.jpg", null, null, "https://vov2-media.solidtech.vn/sites/default/files/styles/large/public/2022-05/dien-bien-phu_-_bia.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000007"), new Guid("00000000-0000-0000-0000-000000000004"), null, "00000000-0000-0000-0000-000000000004_i0.jpg", null, null, "https://bizweb.dktcdn.net/100/378/894/files/5.jpg?v=1627978886825", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-000000000004"), null, "00000000-0000-0000-0000-000000000004_i1.jpg", null, null, "https://bizweb.dktcdn.net/100/378/894/files/4.jpg?v=1627978828806", 1 },
                    { new Guid("00000000-0000-0000-0000-00000000000a"), new Guid("00000000-0000-0000-0000-000000000005"), null, "00000000-0000-0000-0000-000000000005_i0.jpg", null, null, "https://th.bing.com/th/id/OIG.LTaVFacabNQc22SAk1r1?pid=ImgGn", 0 },
                    { new Guid("00000000-0000-0000-0000-00000000000c"), new Guid("00000000-0000-0000-0000-000000000006"), null, "00000000-0000-0000-0000-000000000006_i0.jpg", null, null, "https://th.bing.com/th/id/OIG.AoM9akz.gT4RMZ9R6DOh?pid=ImgGn", 0 },
                    { new Guid("00000000-0000-0000-0000-00000000000e"), new Guid("00000000-0000-0000-0000-000000000007"), null, "00000000-0000-0000-0000-000000000007_i0.jpg", null, null, "https://th.bing.com/th/id/OIG..gPcXaRan.FdnEjYCvT3?pid=ImgGn", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000010"), new Guid("00000000-0000-0000-0000-000000000008"), null, "00000000-0000-0000-0000-000000000008_i0.jpg", null, null, "https://static-company.waka.vn/img.media/tin%20t%E1%BB%A9c/38540.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000012"), new Guid("00000000-0000-0000-0000-000000000009"), null, "00000000-0000-0000-0000-000000000009_i0.jpg", null, null, "https://th.bing.com/th/id/OIG.Uz66_wn15hsKPirpv6Pb?pid=ImgGn", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000014"), new Guid("00000000-0000-0000-0000-00000000000a"), null, "00000000-0000-0000-0000-00000000000a_i0.jpg", null, null, "https://th.bing.com/th/id/OIG.78MNqZpoa6ReKmvZCHPI?pid=ImgGn", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000016"), new Guid("00000000-0000-0000-0000-00000000000b"), null, "00000000-0000-0000-0000-00000000000b_i0.jpg", null, null, "https://cdn.pixabay.com/photo/2024/02/03/11/41/ai-generated-8550098_1280.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-00000000001d"), new Guid("00000000-0000-0000-0000-00000000000c"), null, "00000000-0000-0000-0000-00000000000c_i0.jpg", null, null, "https://th.bing.com/th/id/OIG.MxQxUggA0RKmKdTjwAqw?pid=ImgGn", 0 },
                    { new Guid("00000000-0000-0000-0000-00000000001f"), new Guid("00000000-0000-0000-0000-00000000000d"), null, "00000000-0000-0000-0000-00000000000d_i0.jpg", null, null, "https://i.pinimg.com/originals/7f/b2/ab/7fb2abc0dddd2aa40a86ce6c318b369a.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000020"), new Guid("00000000-0000-0000-0000-00000000000e"), null, "00000000-0000-0000-0000-00000000000e_i0.jpg", null, null, "https://i.pinimg.com/564x/98/08/55/980855dafc9c24ad9b0687adb4b29f0b.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000021"), new Guid("00000000-0000-0000-0000-00000000000e"), null, "00000000-0000-0000-0000-00000000000e_i1.jpg", null, null, "https://i.pinimg.com/originals/b5/7e/14/b57e14aa401d41db2072d1b0ccfbde2b.jpg", 1 },
                    { new Guid("00000000-0000-0000-0000-000000000022"), new Guid("00000000-0000-0000-0000-00000000000e"), null, "00000000-0000-0000-0000-00000000000e_i2.jpg", null, null, "https://i.pinimg.com/originals/d5/5e/e1/d55ee127c8dc1c7f9d94edc0ec596758.jpg", 2 },
                    { new Guid("00000000-0000-0000-0000-000000000024"), new Guid("00000000-0000-0000-0000-00000000000f"), null, "00000000-0000-0000-0000-00000000000f_i0.jpg", null, null, "https://i.pinimg.com/originals/db/93/a1/db93a131d59201ed997d606ea33c4933.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000025"), new Guid("00000000-0000-0000-0000-00000000000f"), null, "00000000-0000-0000-0000-00000000000f_i1.jpg", null, null, "https://i.pinimg.com/originals/b5/d4/7e/b5d47e1cf4555983a8017e59409b4d4a.jpg", 1 },
                    { new Guid("00000000-0000-0000-0000-000000000027"), new Guid("00000000-0000-0000-0000-000000000010"), null, "00000000-0000-0000-0000-000000000010_i0.jpg", null, null, "https://th.bing.com/th/id/OIG.5gNG99_0Acz4Y8CGOYlg?pid=ImgGn", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000029"), new Guid("00000000-0000-0000-0000-000000000011"), null, "00000000-0000-0000-0000-000000000011_i0.jpg", null, null, "https://firebasestorage.googleapis.com/v0/b/artworkia-init.appspot.com/o/Artwork%2FImage%2F00000000-0000-0000-0000-000000000011_i0.jpg?alt=media&token=e9a93f6f-4bcf-4517-824a-dacd402bdcec", 0 },
                    { new Guid("00000000-0000-0000-0000-00000000002a"), new Guid("00000000-0000-0000-0000-000000000011"), null, "00000000-0000-0000-0000-000000000011_i1.jpg", null, null, "https://firebasestorage.googleapis.com/v0/b/artworkia-init.appspot.com/o/Artwork%2FImage%2F00000000-0000-0000-0000-000000000011_i1.jpg?alt=media&token=3442240e-37f5-42f9-be58-5c359ebcde5c", 1 },
                    { new Guid("00000000-0000-0000-0000-00000000002b"), new Guid("00000000-0000-0000-0000-000000000011"), null, "00000000-0000-0000-0000-000000000011_i2.jpg", null, null, "https://firebasestorage.googleapis.com/v0/b/artworkia-init.appspot.com/o/Artwork%2FImage%2F00000000-0000-0000-0000-000000000011_i2.jpg?alt=media&token=4d6ad124-294d-4059-b21d-a37cfb3d0c6a", 2 },
                    { new Guid("00000000-0000-0000-0000-00000000002d"), new Guid("00000000-0000-0000-0000-000000000012"), null, "00000000-0000-0000-0000-000000000012_i0.png", null, null, "https://firebasestorage.googleapis.com/v0/b/artworkia-init.appspot.com/o/Artwork%2FImage%2F1.png?alt=media&token=61dea5b6-4462-49b3-b58a-3acc6f9861fc", 0 },
                    { new Guid("00000000-0000-0000-0000-00000000002e"), new Guid("00000000-0000-0000-0000-000000000012"), null, "00000000-0000-0000-0000-000000000012_i1.png", null, null, "https://firebasestorage.googleapis.com/v0/b/artworkia-init.appspot.com/o/Artwork%2FImage%2F2.png?alt=media&token=10e991c2-11ac-469b-9933-3b223fe17f5b", 1 },
                    { new Guid("00000000-0000-0000-0000-00000000002f"), new Guid("00000000-0000-0000-0000-000000000012"), null, "00000000-0000-0000-0000-000000000012_i2.png", null, null, "https://firebasestorage.googleapis.com/v0/b/artworkia-init.appspot.com/o/Artwork%2FImage%2F3.png?alt=media&token=13f47f61-2372-4b87-b8f4-806c5ef956ee", 2 },
                    { new Guid("00000000-0000-0000-0000-000000000030"), new Guid("00000000-0000-0000-0000-000000000012"), null, "00000000-0000-0000-0000-000000000012_i3.png", null, null, "https://firebasestorage.googleapis.com/v0/b/artworkia-init.appspot.com/o/Artwork%2FImage%2F4.png?alt=media&token=53a9b98e-1875-48d6-a880-6b935081afe5", 3 },
                    { new Guid("00000000-0000-0000-0000-000000000031"), new Guid("00000000-0000-0000-0000-000000000012"), null, "00000000-0000-0000-0000-000000000012_i4.png", null, null, "https://firebasestorage.googleapis.com/v0/b/artworkia-init.appspot.com/o/Artwork%2FImage%2F5.png?alt=media&token=4d9137a2-5836-4cee-8e36-c1b87f9236c6", 4 },
                    { new Guid("00000000-0000-0000-0000-000000000032"), new Guid("00000000-0000-0000-0000-000000000012"), null, "00000000-0000-0000-0000-000000000012_i4.png", null, null, "https://firebasestorage.googleapis.com/v0/b/artworkia-init.appspot.com/o/Artwork%2FImage%2F6.png?alt=media&token=3b05c46f-6ed0-42d6-aa84-1ef96a880f99", 5 },
                    { new Guid("00000000-0000-0000-0000-000000000034"), new Guid("00000000-0000-0000-0000-000000000013"), null, "00000000-0000-0000-0000-000000000013_i0.png", null, null, "https://vov2.vov.vn/sites/default/files/styles/large/public/2022-05/vo-nguyen-giap.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000035"), new Guid("00000000-0000-0000-0000-000000000014"), null, "00000000-0000-0000-0000-000000000014_i0.png", null, null, "https://public-files.gumroad.com/ydorwqbnmsl1yueuyhnr90ceo7sq", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000036"), new Guid("00000000-0000-0000-0000-000000000014"), null, "00000000-0000-0000-0000-000000000014_i1.png", null, null, "https://public-files.gumroad.com/1tmubanfhbhfs7fi4mwbw1s5nyc0", 1 },
                    { new Guid("00000000-0000-0000-0000-000000000037"), new Guid("00000000-0000-0000-0000-000000000014"), null, "00000000-0000-0000-0000-000000000014_i2.png", null, null, "https://public-files.gumroad.com/7ml9boypxaoisvvaio5eajau57ts", 2 },
                    { new Guid("00000000-0000-0000-0000-000000000038"), new Guid("00000000-0000-0000-0000-000000000014"), null, "00000000-0000-0000-0000-000000000014_i3.png", null, null, "https://public-files.gumroad.com/t1tx8r7s2jhhedtr2zqyc3r9edc6", 3 },
                    { new Guid("00000000-0000-0000-0000-00000000003a"), new Guid("00000000-0000-0000-0000-000000000015"), null, "00000000-0000-0000-0000-000000000015_i0.png", null, null, "https://yt3.ggpht.com/7CFh0HeCsVSD9UfK1LSc9imflCpKDZT41gNjj_qehvRKt0J9fgmjw2tYjf4oMpbsn0BagczFhx_TTQ=s1600-rw-nd-v1", 0 },
                    { new Guid("00000000-0000-0000-0000-00000000003c"), new Guid("00000000-0000-0000-0000-000000000016"), null, "00000000-0000-0000-0000-000000000016_i0.png", null, null, "https://yt3.ggpht.com/gD2jfNal37ZYf6zkA7CX8CdBQ3WJDP9wFxA-JgrTl0RMGmWh7QcaWG1L2dzBWHUV_qd20ddxXJSMi90=s1024-c-fcrop64=1,0000199affffe666-rw-nd-v1", 0 },
                    { new Guid("00000000-0000-0000-0000-00000000003d"), new Guid("00000000-0000-0000-0000-000000000016"), null, "00000000-0000-0000-0000-000000000016_i1.png", null, null, "https://yt3.ggpht.com/soXomuCz4k_xBxpb_p6K7nAht6BlCfzh8p3PfTPU2dt3iGX25Ga-W-Noiow1GPr5ii9seYFsCci-=s1024-c-fcrop64=1,0000199affffe666-rw-nd-v1", 1 },
                    { new Guid("00000000-0000-0000-0000-00000000003e"), new Guid("00000000-0000-0000-0000-000000000016"), null, "00000000-0000-0000-0000-000000000016_i2.png", null, null, "https://yt3.ggpht.com/An5n_i_kbv45-ijvkogN9T98slicRZYEsxmyallrHtsILGoNgwdOs0_93C94duiwdClNGWFtoG-f=s1024-c-fcrop64=1,0000199affffe666-rw-nd-v1", 2 },
                    { new Guid("00000000-0000-0000-0000-00000000003f"), new Guid("00000000-0000-0000-0000-000000000016"), null, "00000000-0000-0000-0000-000000000016_i3.png", null, null, "https://yt3.ggpht.com/togZdny_IHrLbAa73ZmBXhUlt-Br4Rdrjpq2iySYPgt7S3bATckHvipkjv6gsDmjkJtXnM8DazDgPg=s1024-c-fcrop64=1,0000199affffe666-rw-nd-v1", 3 },
                    { new Guid("00000000-0000-0000-0000-000000000040"), new Guid("00000000-0000-0000-0000-000000000017"), null, "00000000-0000-0000-0000-000000000017_i0.png", null, null, "https://firebasestorage.googleapis.com/v0/b/artworkia-init.appspot.com/o/Artwork%2FImage%2F106127234_p0.jpg?alt=media&token=86c5c12c-bee7-4a57-8db1-402e317d5c23", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000042"), new Guid("00000000-0000-0000-0000-000000000018"), null, "00000000-0000-0000-0000-000000000018_i0.png", null, null, "https://www.kiettacnghethuat.com/wp-content/uploads/Girl-With-a-Pearl-Earring.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000044"), new Guid("00000000-0000-0000-0000-000000000019"), null, "00000000-0000-0000-0000-000000000019_i0.jpg", null, null, "https://www.kiettacnghethuat.com/wp-content/uploads/The-Starry-Night.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000048"), new Guid("00000000-0000-0000-0000-00000000001a"), null, "00000000-0000-0000-0000-00000000001a_i0.jpg", null, null, "https://upload.wikimedia.org/wikipedia/commons/thumb/6/62/To_Ngoc_Van_thieu_nu_ben_hoa_hue.jpg/800px-To_Ngoc_Van_thieu_nu_ben_hoa_hue.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-00000000004a"), new Guid("00000000-0000-0000-0000-00000000001b"), null, "00000000-0000-0000-0000-00000000001b_i0.jpg", null, null, "https://i.pinimg.com/originals/5b/43/fc/5b43fc89abad25aa3359bfe0f27923f9.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-00000000004d"), new Guid("00000000-0000-0000-0000-00000000001c"), null, "00000000-0000-0000-0000-00000000001c_i0.jpg", null, null, "https://upload.wikimedia.org/wikipedia/commons/e/e5/Adolf_Hitler_-_Wien_Oper.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-00000000004e"), new Guid("00000000-0000-0000-0000-00000000001d"), null, "00000000-0000-0000-0000-00000000001d_i0.jpg", null, null, "https://upload.wikimedia.org/wikipedia/commons/9/93/Adolf_Hitler_-_Hofbr%C3%A4uhaus.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-00000000004f"), new Guid("00000000-0000-0000-0000-00000000001e"), null, "00000000-0000-0000-0000-00000000001e_i0.jpg", null, null, "https://upload.wikimedia.org/wikipedia/commons/thumb/b/ba/Adolf_Hitler_-_Standesamt_M%C3%BCnchen.jpg/1024px-Adolf_Hitler_-_Standesamt_M%C3%BCnchen.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000050"), new Guid("00000000-0000-0000-0000-00000000001f"), null, "00000000-0000-0000-0000-00000000001f_i0.jpg", null, null, "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7d/Adolf_Hitler_Der_Alte_Hof.jpg/1024px-Adolf_Hitler_Der_Alte_Hof.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000051"), new Guid("00000000-0000-0000-0000-000000000020"), null, "00000000-0000-0000-0000-000000000020_i0.png", null, null, "https://cafebiz.cafebizcdn.vn/162123310254002176/2022/4/27/photo-1-1651050727352925703922.jpeg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000052"), new Guid("00000000-0000-0000-0000-000000000020"), null, "00000000-0000-0000-0000-000000000020_i1.png", null, null, "https://cafebiz.cafebizcdn.vn/162123310254002176/2022/4/27/photo-1-16510507247002018514994.jpeg", 1 },
                    { new Guid("00000000-0000-0000-0000-000000000053"), new Guid("00000000-0000-0000-0000-000000000021"), null, "00000000-0000-0000-0000-000000000021_i0.jpg", null, null, "https://img3.gelbooru.com//images/db/56/db56fdd06f55f97ab9c884d5539e7e99.jpeg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000054"), new Guid("00000000-0000-0000-0000-000000000022"), null, "00000000-0000-0000-0000-000000000022_i0.jpg", null, null, "https://www.4gamer.net/games/411/G041111/20180227026/SS/002.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000055"), new Guid("00000000-0000-0000-0000-000000000022"), null, "00000000-0000-0000-0000-000000000022_i1.jpg", null, null, "https://firebasestorage.googleapis.com/v0/b/artworkia-init.appspot.com/o/Artwork%2FImage%2Ftenor.gif?alt=media&token=270214ac-b289-4608-ba5c-9ea68e7ae97a", 1 },
                    { new Guid("00000000-0000-0000-0000-000000000056"), new Guid("00000000-0000-0000-0000-000000000023"), null, "00000000-0000-0000-0000-000000000023_i0.jpg", null, null, "https://www.4gamer.net/games/411/G041111/20180227026/SS/003.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000057"), new Guid("00000000-0000-0000-0000-000000000024"), null, "00000000-0000-0000-0000-000000000024_i0.jpg", null, null, "https://www.4gamer.net/games/411/G041111/20180227026/SS/004.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000058"), new Guid("00000000-0000-0000-0000-000000000025"), null, "00000000-0000-0000-0000-000000000025_i0.jpg", null, null, "https://www.4gamer.net/games/411/G041111/20180227026/SS/005.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-00000000005a"), new Guid("00000000-0000-0000-0000-000000000026"), null, "00000000-0000-0000-0000-000000000026_i0.jpg", null, null, "https://firebasestorage.googleapis.com/v0/b/artworkia-init.appspot.com/o/Artwork%2FImage%2F110149990_p0.jpg?alt=media&token=4668e7eb-f2ba-47f2-add9-96bb40acd22e", 0 },
                    { new Guid("00000000-0000-0000-0000-00000000005c"), new Guid("00000000-0000-0000-0000-000000000027"), null, "00000000-0000-0000-0000-000000000027_i0.jpg", null, null, "https://firebasestorage.googleapis.com/v0/b/artworkia-init.appspot.com/o/Artwork%2FImage%2Fwp3790967.jpg?alt=media&token=203e265e-427f-4aa3-a664-2a39479bf61e", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000060"), new Guid("00000000-0000-0000-0000-000000000028"), null, "00000000-0000-0000-0000-000000000028_i0.jpg", null, null, "https://i0.wp.com/images1.wikia.nocookie.net/mydata/vi/images/b/be/Anemoi_main_viet.png?ssl=1", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000061"), new Guid("00000000-0000-0000-0000-000000000029"), null, "00000000-0000-0000-0000-000000000029_i0.jpg", null, null, "https://i.pinimg.com/originals/57/da/1d/57da1db47a30fe8d608da6d3b25dfc08.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000062"), new Guid("00000000-0000-0000-0000-00000000002a"), null, "00000000-0000-0000-0000-00000000002a_i0.jpg", null, null, "https://i.pinimg.com/originals/56/a4/58/56a45858390e3726d2848d3efa696d6e.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000063"), new Guid("00000000-0000-0000-0000-00000000002b"), null, "00000000-0000-0000-0000-00000000002b_i0.jpg", null, null, "https://i.pinimg.com/originals/51/59/8e/51598eb8f62f868e95556fc316873f05.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000064"), new Guid("00000000-0000-0000-0000-00000000002c"), null, "00000000-0000-0000-0000-00000000002c_i0.jpg", null, null, "https://i.pinimg.com/originals/33/b5/96/33b596908f23b4b2c3f3e64f032e51b6.png", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000065"), new Guid("00000000-0000-0000-0000-00000000002d"), null, "00000000-0000-0000-0000-00000000002d_i0.jpg", null, null, "https://i.pinimg.com/originals/02/f2/28/02f2283c2a59d3e7d7287a95fae5c2f5.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000066"), new Guid("00000000-0000-0000-0000-00000000002e"), null, "00000000-0000-0000-0000-00000000002e_i0.jpg", null, null, "https://i.pinimg.com/originals/85/3e/bf/853ebff19a985aae38e65c8111e59ef8.png", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000067"), new Guid("00000000-0000-0000-0000-00000000002f"), null, "00000000-0000-0000-0000-00000000002f_i0.jpg", null, null, "https://i.pinimg.com/originals/d2/54/3a/d2543a52f10afe5696d68346d212d34e.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000068"), new Guid("00000000-0000-0000-0000-000000000030"), null, "00000000-0000-0000-0000-000000000030_i0.jpg", null, null, "https://i.pinimg.com/564x/41/e1/51/41e151608d67227d7265e7026faa48c1.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000069"), new Guid("00000000-0000-0000-0000-000000000031"), null, "00000000-0000-0000-0000-000000000031_i0.jpg", null, null, "https://i.pinimg.com/originals/3d/e0/d0/3de0d0e553793ec3ee39cf1e7404e96e.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-00000000006a"), new Guid("00000000-0000-0000-0000-000000000032"), null, "00000000-0000-0000-0000-000000000032_i0.jpg", null, null, "https://i.pinimg.com/564x/49/40/56/494056c9b3f314dad493bac63265f296.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-00000000006b"), new Guid("00000000-0000-0000-0000-000000000033"), null, "00000000-0000-0000-0000-000000000033_i0.jpg", null, null, "https://images.ui8.net/uploads/frame-12_1580960103603.png", 0 },
                    { new Guid("00000000-0000-0000-0000-00000000006c"), new Guid("00000000-0000-0000-0000-000000000033"), null, "00000000-0000-0000-0000-000000000033_i1.jpg", null, null, "https://images.ui8.net/uploads/frame-12_1580960103638.png", 1 },
                    { new Guid("00000000-0000-0000-0000-00000000006d"), new Guid("00000000-0000-0000-0000-000000000033"), null, "00000000-0000-0000-0000-000000000033_i2.jpg", null, null, "https://images.ui8.net/uploads/frame-12_1580960103637.png", 2 },
                    { new Guid("00000000-0000-0000-0000-00000000006e"), new Guid("00000000-0000-0000-0000-000000000033"), null, "00000000-0000-0000-0000-000000000033_i3.jpg", null, null, "https://images.ui8.net/uploads/frame-12_1580960103566.png", 3 },
                    { new Guid("00000000-0000-0000-0000-00000000006f"), new Guid("00000000-0000-0000-0000-000000000033"), null, "00000000-0000-0000-0000-000000000033_i3.jpg", null, null, "https://images.ui8.net/uploads/frame-12_1580960103592.png", 4 },
                    { new Guid("00000000-0000-0000-0000-000000000070"), new Guid("00000000-0000-0000-0000-000000000033"), null, "00000000-0000-0000-0000-000000000033_i3.jpg", null, null, "https://images.ui8.net/uploads/frame-12_1580960103587.png", 5 },
                    { new Guid("00000000-0000-0000-0000-000000000071"), new Guid("00000000-0000-0000-0000-000000000034"), null, "00000000-0000-0000-0000-000000000034_i0.jpg", null, null, "https://www.sinthaistudio.com/wp-content/uploads/2021/08/MN1-uai-516x568.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000072"), new Guid("00000000-0000-0000-0000-000000000034"), null, "00000000-0000-0000-0000-000000000034_i1.jpg", null, null, "https://www.sinthaistudio.com/wp-content/uploads/2021/08/MN2.jpg", 1 },
                    { new Guid("00000000-0000-0000-0000-000000000073"), new Guid("00000000-0000-0000-0000-000000000034"), null, "00000000-0000-0000-0000-000000000034_i2.jpg", null, null, "https://www.sinthaistudio.com/wp-content/uploads/2021/08/MN3.jpg", 2 },
                    { new Guid("00000000-0000-0000-0000-000000000074"), new Guid("00000000-0000-0000-0000-000000000035"), null, "00000000-0000-0000-0000-000000000034_i3.jpg", null, null, "https://www.sinthaistudio.com/wp-content/uploads/2021/08/MN4.jpg", 3 },
                    { new Guid("00000000-0000-0000-0000-000000000075"), new Guid("00000000-0000-0000-0000-000000000035"), null, "00000000-0000-0000-0000-000000000035_i0.jpg", null, null, "https://mir-s3-cdn-cf.behance.net/project_modules/1400/8cf9b997945993.5ee39b4959c00.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000076"), new Guid("00000000-0000-0000-0000-000000000036"), null, "00000000-0000-0000-0000-000000000036_i0.jpg", null, null, "https://i.pinimg.com/564x/66/b8/59/66b859b3949564d6e3f63cfcf2b6ef93.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000077"), new Guid("00000000-0000-0000-0000-000000000037"), null, "00000000-0000-0000-0000-000000000037_i0.jpg", null, null, "https://i.pinimg.com/564x/c8/a1/5f/c8a15ff316f7c74b789d21651b0891f8.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000078"), new Guid("00000000-0000-0000-0000-000000000038"), null, "00000000-0000-0000-0000-000000000038_i0.jpg", null, null, "https://i.pinimg.com/564x/61/9f/65/619f65d4700c390f1e47ab42237ce450.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000079"), new Guid("00000000-0000-0000-0000-000000000039"), null, "00000000-0000-0000-0000-000000000039_i0.jpg", null, null, "https://i.pinimg.com/564x/ff/78/08/ff7808be1114e07dc1065245bd8bfc7f.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-00000000007a"), new Guid("00000000-0000-0000-0000-00000000003a"), null, "00000000-0000-0000-0000-00000000003a_i0.jpg", null, null, "https://ik.imagekit.io/tvlk/blog/2024/01/landmark-81-3-841x1024.jpg?tr=dpr-2,w-675", 0 },
                    { new Guid("00000000-0000-0000-0000-00000000007b"), new Guid("00000000-0000-0000-0000-00000000003b"), null, "00000000-0000-0000-0000-00000000003b_i0.jpg", null, null, "https://i.pinimg.com/564x/78/37/52/783752783478df60a339c4389697fe88.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-00000000007c"), new Guid("00000000-0000-0000-0000-00000000003c"), null, "00000000-0000-0000-0000-00000000003c_i0.jpg", null, null, "https://i.pinimg.com/564x/d2/d1/17/d2d1176fbec2f7573eb1023c518e1105.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-00000000007d"), new Guid("00000000-0000-0000-0000-00000000003d"), null, "00000000-0000-0000-0000-00000000003d_i0.jpg", null, null, "https://i.pinimg.com/564x/d4/37/b3/d437b301b2408a5772d9be18d2dcbdee.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000080"), new Guid("00000000-0000-0000-0000-00000000003e"), null, "00000000-0000-0000-0000-00000000003e_i0.jpg", null, null, "https://i.pinimg.com/736x/a9/5c/0f/a95c0f2de7561a34fbccc7af102b1af5.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000081"), new Guid("00000000-0000-0000-0000-00000000003f"), null, "00000000-0000-0000-0000-00000000003f_i0.jpg", null, null, "https://i.pinimg.com/736x/68/18/e5/6818e59fe9c1b059d679cbf35ab122c9.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000082"), new Guid("00000000-0000-0000-0000-000000000040"), null, "00000000-0000-0000-0000-000000000040_i0.jpg", null, null, "https://i.pinimg.com/originals/2b/ef/bd/2befbd3f91aa23db55eea433151c7992.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000083"), new Guid("00000000-0000-0000-0000-000000000041"), null, "00000000-0000-0000-0000-000000000041_i0.jpg", null, null, "https://i.pinimg.com/564x/00/ed/65/00ed65b58c95c4d694fc95598af0a885.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000084"), new Guid("00000000-0000-0000-0000-000000000042"), null, "00000000-0000-0000-0000-000000000042_i0.jpg", null, null, "https://i.pinimg.com/564x/25/dc/d6/25dcd6a770856b37bdd3fd69551d626d.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000085"), new Guid("00000000-0000-0000-0000-000000000043"), null, "00000000-0000-0000-0000-000000000043_i0.jpg", null, null, "https://i.pinimg.com/736x/d1/f7/e1/d1f7e116c4dc23c61f9523cea80ee909.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000086"), new Guid("00000000-0000-0000-0000-000000000044"), null, "00000000-0000-0000-0000-000000000044_i0.jpg", null, null, "https://i.pinimg.com/736x/cf/7e/ff/cf7eff78ddcdeeed6b6074bd441eb721.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000087"), new Guid("00000000-0000-0000-0000-000000000045"), null, "00000000-0000-0000-0000-000000000045_i0.jpg", null, null, "https://i.pinimg.com/564x/47/3e/f9/473ef931430e161d83f2aa5c8844d56a.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000088"), new Guid("00000000-0000-0000-0000-000000000046"), null, "00000000-0000-0000-0000-000000000046_i0.jpg", null, null, "https://assetstorev1-prd-cdn.unity3d.com/key-image/da31eccd-b255-4898-bec2-2e1b1eb39092.webp", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000089"), new Guid("00000000-0000-0000-0000-000000000046"), null, "00000000-0000-0000-0000-000000000046_i1.jpg", null, null, "https://assetstorev1-prd-cdn.unity3d.com/package-screenshot/f62a52ea-5ee7-406c-abcf-5b91970f964a.webp", 1 },
                    { new Guid("00000000-0000-0000-0000-00000000008a"), new Guid("00000000-0000-0000-0000-000000000046"), null, "00000000-0000-0000-0000-000000000046_i2.jpg", null, null, "https://assetstorev1-prd-cdn.unity3d.com/package-screenshot/f19fb81d-cfeb-41d5-a092-9b61d6530ba8.webp", 2 },
                    { new Guid("00000000-0000-0000-0000-00000000008b"), new Guid("00000000-0000-0000-0000-000000000046"), null, "00000000-0000-0000-0000-000000000046_i3.jpg", null, null, "https://assetstorev1-prd-cdn.unity3d.com/package-screenshot/1cb97919-ca37-40f4-90db-765eee1ff653.webp", 3 },
                    { new Guid("00000000-0000-0000-0000-000000000090"), new Guid("00000000-0000-0000-0000-000000000047"), null, "00000000-0000-0000-0000-000000000047_i0.jpg", null, null, "https://storage.googleapis.com/artworkia-storage-public/Artwork/VSCode.png", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000091"), new Guid("00000000-0000-0000-0000-000000000047"), null, "00000000-0000-0000-0000-000000000047_i1.jpg", null, null, "https://storage.googleapis.com/artworkia-storage-public/Artwork/VSCode-Thick.png", 1 },
                    { new Guid("00000000-0000-0000-0000-000000000093"), new Guid("00000000-0000-0000-0000-000000000048"), null, "00000000-0000-0000-0000-000000000048_i0.jpg", null, null, "https://i.pinimg.com/564x/2c/5d/80/2c5d80e9c0240a65d6397f9991352855.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000094"), new Guid("00000000-0000-0000-0000-000000000049"), null, "00000000-0000-0000-0000-000000000049_i0.jpg", null, null, "https://i.pinimg.com/564x/71/01/da/7101da0949d85dfbf7c24200f1fcbdfd.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000095"), new Guid("00000000-0000-0000-0000-00000000004a"), null, "00000000-0000-0000-0000-00000000004a_i0.jpg", null, null, "https://i.pinimg.com/564x/61/29/93/61299356ecd161b0ac86c94869960084.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000096"), new Guid("00000000-0000-0000-0000-00000000004b"), null, "00000000-0000-0000-0000-00000000004b_i0.jpg", null, null, "https://i.pinimg.com/564x/a3/ef/9c/a3ef9c4379f92f56607f1f685e7835ce.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000097"), new Guid("00000000-0000-0000-0000-00000000004c"), null, "00000000-0000-0000-0000-00000000004c_i0.jpg", null, null, "https://i.pinimg.com/564x/19/d5/fb/19d5fbfe0a970510bfe47aa148e0b71e.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000098"), new Guid("00000000-0000-0000-0000-00000000004d"), null, "00000000-0000-0000-0000-00000000004d_i0.jpg", null, null, "https://storage.googleapis.com/artworkia-storage-public/Artwork/IntellijLogo.png", 0 },
                    { new Guid("00000000-0000-0000-0000-000000000099"), new Guid("00000000-0000-0000-0000-00000000004d"), null, "00000000-0000-0000-0000-00000000004d_i1.jpg", null, null, "https://storage.googleapis.com/artworkia-storage-public/Artwork/intelliJShadow.png", 1 },
                    { new Guid("00000000-0000-0000-0000-00000000009b"), new Guid("00000000-0000-0000-0000-00000000004e"), null, "00000000-0000-0000-0000-00000000004e_i0.jpg", null, null, "https://storage.googleapis.com/artworkia-storage-public/Artwork/phucche1.png", 0 },
                    { new Guid("00000000-0000-0000-0000-00000000009c"), new Guid("00000000-0000-0000-0000-00000000004e"), null, "00000000-0000-0000-0000-00000000004e_i1.jpg", null, null, "https://storage.googleapis.com/artworkia-storage-public/Artwork/phucche2.png", 1 },
                    { new Guid("00000000-0000-0000-0000-0000000000a0"), new Guid("00000000-0000-0000-0000-00000000004f"), null, "00000000-0000-0000-0000-00000000004f_i0.jpg", null, null, "https://file4.batdongsan.com.vn/2024/03/07/20240307144316-1cca_wm.jpg", 0 },
                    { new Guid("00000000-0000-0000-0000-0000000000a1"), new Guid("00000000-0000-0000-0000-00000000004f"), null, "00000000-0000-0000-0000-00000000004f_i1.jpg", null, null, "https://file4.batdongsan.com.vn/2024/03/07/20240307144319-ee94_wm.jpg", 1 },
                    { new Guid("00000000-0000-0000-0000-0000000000a2"), new Guid("00000000-0000-0000-0000-00000000004f"), null, "00000000-0000-0000-0000-00000000004f_i2.jpg", null, null, "https://file4.batdongsan.com.vn/2024/03/07/20240307144319-f0da_wm.jpg", 2 },
                    { new Guid("00000000-0000-0000-0000-0000000000a3"), new Guid("00000000-0000-0000-0000-000000000050"), null, "00000000-0000-0000-0000-000000000050_i0.jpg", null, null, "https://i.pinimg.com/originals/86/15/50/8615509334c99ba0c11a9feac151a79e.jpg", 0 }
                });

            migrationBuilder.InsertData(
                table: "Like",
                columns: new[] { "AccountId", "ArtworkId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000009"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-00000000000a"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-00000000000b"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-00000000000c"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-00000000000d"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-00000000000e"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-00000000000f"), new Guid("00000000-0000-0000-0000-000000000001") }
                });

            migrationBuilder.InsertData(
                table: "Message",
                columns: new[] { "Id", "ChatBoxId", "CreatedBy", "CreatedOn", "FileLocation", "FileName", "ProposalId", "RequestId", "Text" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, null, "Chào bạn" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 9, 11, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, null, "Mình muốn bạn làm website mua bán" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 9, 12, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, null, "Nếu được hãy chấp nhận nhé" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2023, 9, 13, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, null, "Yêu cầu khác đi" },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 9, 15, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, null, "Vầy được chưa?" },
                    { new Guid("00000000-0000-0000-0000-000000000006"), new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2023, 9, 16, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, null, "OK, mình muốn 100k, giá vậy được ko?" },
                    { new Guid("00000000-0000-0000-0000-000000000007"), new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 9, 17, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, null, "80k được ko?" },
                    { new Guid("00000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2023, 9, 18, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, null, "Thôi cũng được, bạn muốn khi nào xong?" },
                    { new Guid("00000000-0000-0000-0000-000000000009"), new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 9, 19, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, null, "Cuối tháng 1" },
                    { new Guid("00000000-0000-0000-0000-000000000010"), new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2023, 9, 20, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, null, "Để mình tạo thỏa thuận" },
                    { new Guid("00000000-0000-0000-0000-000000000012"), new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, null, "ok mình làm đây" }
                });

            migrationBuilder.InsertData(
                table: "Proposal",
                columns: new[] { "Id", "ActualDelivery", "Category", "CreatedBy", "CreatedOn", "Description", "InitialPrice", "NumberOfConcept", "NumberOfRevision", "OrdererId", "ProjectTitle", "ProposalStatus", "ServiceId", "TargetDelivery", "Total" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Website", new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), "Yêu cầu làm website mua bán thiệp đám cưới. \r\n                                Giao diện trực quan, dễ sử dụng. \r\n                                Tìm kiếm và bộ lọc sản phẩm.\r\n                                Hệ thống thanh toán an toàn.", 0.25, 2, 3, new Guid("00000000-0000-0000-0000-000000000005"), "Yêu cầu làm website mua bán thiệp đám cưới.", 4, new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), 80000.0 },
                    { new Guid("00000000-0000-0000-0000-00000000000a"), new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Thiết kế", new Guid("00000000-0000-0000-0000-00000000000b"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Yêu cầu thiết kế một bức tranh tường với chủ đề khoa học viễn tưởng cho không gian phòng khách.", 0.25, 2, 3, new Guid("00000000-0000-0000-0000-00000000000a"), "Thiết kế Hình Ảnh Khoa Học - Dự Án Tranh Tường.", 4, new Guid("00000000-0000-0000-0000-000000000006"), new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), 50000.0 },
                    { new Guid("00000000-0000-0000-0000-00000000000b"), new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Thiết kế", new Guid("00000000-0000-0000-0000-00000000000b"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), " Yêu cầu thiết kế các tác phẩm nghệ thuật nhỏ về khoa học viễn tưởng để trang trí không gian phòng làm việc.", 0.25, 2, 3, new Guid("00000000-0000-0000-0000-00000000000c"), "Thiết kế Hình Ảnh Khoa Học - Dự Án Trang Trí Phòng Làm Việc", 4, new Guid("00000000-0000-0000-0000-000000000006"), new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Utc), 60000.0 },
                    { new Guid("00000000-0000-0000-0000-00000000000c"), new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Thiết kế", new Guid("00000000-0000-0000-0000-00000000000b"), new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Yêu cầu thiết kế một loạt các poster khoa học viễn tưởng để trưng bày trong sự kiện khoa học của trường địa phương.", 0.25, 2, 3, new Guid("00000000-0000-0000-0000-00000000000d"), "Thiết kế Hình Ảnh Khoa Học - Dự Án Poster Khoa Học", 4, new Guid("00000000-0000-0000-0000-000000000006"), new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), 80000.0 },
                    { new Guid("00000000-0000-0000-0000-00000000000d"), new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Thiết kế", new Guid("00000000-0000-0000-0000-00000000000b"), new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Yêu cầu thiết kế bìa và các hình minh họa cho cuốn sách truyện khoa học viễn tưởng mới.", 0.25, 2, 3, new Guid("00000000-0000-0000-0000-00000000000e"), "Thiết kế Hình Ảnh Khoa Học - Dự Án Sách Truyện Khoa Học", 4, new Guid("00000000-0000-0000-0000-000000000006"), new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Utc), 50000.0 },
                    { new Guid("00000000-0000-0000-0000-00000000000e"), new DateTime(2024, 3, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Thiết kế", new Guid("00000000-0000-0000-0000-00000000000b"), new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Yêu cầu thiết kế các cảnh và hiệu ứng đặc biệt cho một bộ phim ngắn với chủ đề khoa học.", 0.25, 2, 3, new Guid("00000000-0000-0000-0000-00000000000f"), "Hợp đồng Thiết kế Hình Ảnh Khoa Học - Dự Án Phim Ngắn", 4, new Guid("00000000-0000-0000-0000-000000000006"), new DateTime(2024, 3, 29, 0, 0, 0, 0, DateTimeKind.Utc), 100000.0 }
                });

            migrationBuilder.InsertData(
                table: "Request",
                columns: new[] { "Id", "Budget", "CreatedBy", "CreatedOn", "Message", "RequestStatus", "ServiceId", "Timeline" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), 69000.0, new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2023, 9, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Yêu cầu làm website mua bán.", 2, new Guid("00000000-0000-0000-0000-000000000001"), "2 - 3 tuần" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), 69000.0, new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2023, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Yêu cầu làm website mua bán thiệp đám cưới.", 1, new Guid("00000000-0000-0000-0000-000000000001"), "2 - 3 tuần" }
                });

            migrationBuilder.InsertData(
                table: "ServiceDetail",
                columns: new[] { "ArtworkId", "ServiceId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-00000000000e"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000012"), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000009"), new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000020"), new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("00000000-0000-0000-0000-000000000033"), new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000034"), new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000035"), new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-00000000003d"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-00000000003e"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-00000000003f"), new Guid("00000000-0000-0000-0000-000000000008") }
                });

            migrationBuilder.InsertData(
                table: "SoftwareDetail",
                columns: new[] { "ArtworkId", "SoftwareUsedId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000012"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000014"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000015"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000016"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000017"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000020"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000021"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000022"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000023"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000024"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000025"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000026"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000027"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000028"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000030"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000031"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000032"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-00000000003e"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000045"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000047"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000049"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-00000000004e"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000050"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000014"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-00000000004d"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000033"), new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000034"), new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000035"), new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000048"), new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-00000000003d"), new Guid("00000000-0000-0000-0000-00000000000b") },
                    { new Guid("00000000-0000-0000-0000-00000000003c"), new Guid("00000000-0000-0000-0000-00000000000c") },
                    { new Guid("00000000-0000-0000-0000-000000000046"), new Guid("00000000-0000-0000-0000-000000000035") },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000039") },
                    { new Guid("00000000-0000-0000-0000-000000000006"), new Guid("00000000-0000-0000-0000-000000000039") },
                    { new Guid("00000000-0000-0000-0000-000000000007"), new Guid("00000000-0000-0000-0000-000000000039") },
                    { new Guid("00000000-0000-0000-0000-000000000009"), new Guid("00000000-0000-0000-0000-000000000039") },
                    { new Guid("00000000-0000-0000-0000-00000000000a"), new Guid("00000000-0000-0000-0000-000000000039") },
                    { new Guid("00000000-0000-0000-0000-00000000000b"), new Guid("00000000-0000-0000-0000-000000000039") },
                    { new Guid("00000000-0000-0000-0000-00000000000c"), new Guid("00000000-0000-0000-0000-000000000039") },
                    { new Guid("00000000-0000-0000-0000-00000000000e"), new Guid("00000000-0000-0000-0000-000000000039") },
                    { new Guid("00000000-0000-0000-0000-00000000000f"), new Guid("00000000-0000-0000-0000-000000000039") },
                    { new Guid("00000000-0000-0000-0000-000000000010"), new Guid("00000000-0000-0000-0000-000000000039") },
                    { new Guid("00000000-0000-0000-0000-00000000003f"), new Guid("00000000-0000-0000-0000-000000000039") },
                    { new Guid("00000000-0000-0000-0000-000000000040"), new Guid("00000000-0000-0000-0000-000000000039") },
                    { new Guid("00000000-0000-0000-0000-00000000004a"), new Guid("00000000-0000-0000-0000-000000000039") },
                    { new Guid("00000000-0000-0000-0000-00000000004b"), new Guid("00000000-0000-0000-0000-000000000039") },
                    { new Guid("00000000-0000-0000-0000-00000000004c"), new Guid("00000000-0000-0000-0000-000000000039") },
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-00000000003a") },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new Guid("00000000-0000-0000-0000-00000000003a") },
                    { new Guid("00000000-0000-0000-0000-00000000000c"), new Guid("00000000-0000-0000-0000-00000000003a") },
                    { new Guid("00000000-0000-0000-0000-00000000001b"), new Guid("00000000-0000-0000-0000-00000000003a") },
                    { new Guid("00000000-0000-0000-0000-000000000029"), new Guid("00000000-0000-0000-0000-00000000003a") },
                    { new Guid("00000000-0000-0000-0000-00000000002a"), new Guid("00000000-0000-0000-0000-00000000003a") },
                    { new Guid("00000000-0000-0000-0000-00000000002b"), new Guid("00000000-0000-0000-0000-00000000003a") },
                    { new Guid("00000000-0000-0000-0000-00000000002e"), new Guid("00000000-0000-0000-0000-00000000003a") },
                    { new Guid("00000000-0000-0000-0000-00000000002f"), new Guid("00000000-0000-0000-0000-00000000003a") },
                    { new Guid("00000000-0000-0000-0000-000000000041"), new Guid("00000000-0000-0000-0000-00000000003a") }
                });

            migrationBuilder.InsertData(
                table: "TagDetail",
                columns: new[] { "ArtworkId", "TagId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000006"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000007"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-00000000000b"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-00000000000d"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000010"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000017"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-00000000001b"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000036"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000048"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-00000000004d"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000006"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000007"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000009"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-00000000000a"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-00000000000c"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-00000000000e"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000010"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000015"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000019"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-00000000000c"), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-00000000000d"), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000011"), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000014"), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-00000000001b"), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-00000000001c"), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-00000000001d"), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-00000000001e"), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-00000000001f"), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000027"), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000028"), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000029"), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-00000000002a"), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-00000000002b"), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-00000000002e"), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000037"), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000038"), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000039"), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-00000000003b"), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000041"), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-00000000004f"), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-00000000000c"), new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-00000000000d"), new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-00000000001b"), new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000027"), new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000037"), new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000038"), new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000039"), new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-00000000003b"), new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000041"), new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-00000000000e"), new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("00000000-0000-0000-0000-00000000002d"), new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("00000000-0000-0000-0000-000000000048"), new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("00000000-0000-0000-0000-00000000000d"), new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-000000000012"), new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-000000000017"), new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-00000000001b"), new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-000000000021"), new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-000000000022"), new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-000000000023"), new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-000000000024"), new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-000000000025"), new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-000000000026"), new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-000000000027"), new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-000000000028"), new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-000000000029"), new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-00000000002a"), new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-00000000002b"), new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-000000000049"), new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-000000000013"), new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000018"), new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-00000000001a"), new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000020"), new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000030"), new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000031"), new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000042"), new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000043"), new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000044"), new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-00000000004e"), new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-00000000000e"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-000000000012"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-000000000015"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-000000000016"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-000000000033"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-000000000034"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-000000000035"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-00000000003d"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-00000000003e"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-00000000003f"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-000000000040"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-000000000045"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-000000000046"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-000000000047"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-000000000048"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-00000000004a"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-00000000004b"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-00000000004c"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-00000000004d"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000006"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000007"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-00000000000e"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-00000000000f"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000010"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000012"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000014"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000015"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000016"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000020"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000033"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000034"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000035"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000036"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-00000000003d"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-00000000003e"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-00000000003f"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000040"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000045"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000047"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000048"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-00000000004a"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-00000000004b"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-00000000004c"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-00000000004e"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000017"), new Guid("00000000-0000-0000-0000-00000000000a") },
                    { new Guid("00000000-0000-0000-0000-00000000001b"), new Guid("00000000-0000-0000-0000-00000000000a") },
                    { new Guid("00000000-0000-0000-0000-000000000022"), new Guid("00000000-0000-0000-0000-00000000000a") },
                    { new Guid("00000000-0000-0000-0000-000000000023"), new Guid("00000000-0000-0000-0000-00000000000a") },
                    { new Guid("00000000-0000-0000-0000-000000000024"), new Guid("00000000-0000-0000-0000-00000000000a") },
                    { new Guid("00000000-0000-0000-0000-000000000025"), new Guid("00000000-0000-0000-0000-00000000000a") },
                    { new Guid("00000000-0000-0000-0000-000000000026"), new Guid("00000000-0000-0000-0000-00000000000a") },
                    { new Guid("00000000-0000-0000-0000-000000000027"), new Guid("00000000-0000-0000-0000-00000000000a") },
                    { new Guid("00000000-0000-0000-0000-000000000028"), new Guid("00000000-0000-0000-0000-00000000000a") },
                    { new Guid("00000000-0000-0000-0000-000000000036"), new Guid("00000000-0000-0000-0000-00000000000a") },
                    { new Guid("00000000-0000-0000-0000-00000000003d"), new Guid("00000000-0000-0000-0000-00000000000a") },
                    { new Guid("00000000-0000-0000-0000-00000000003e"), new Guid("00000000-0000-0000-0000-00000000000a") },
                    { new Guid("00000000-0000-0000-0000-00000000003f"), new Guid("00000000-0000-0000-0000-00000000000a") },
                    { new Guid("00000000-0000-0000-0000-000000000040"), new Guid("00000000-0000-0000-0000-00000000000a") },
                    { new Guid("00000000-0000-0000-0000-000000000049"), new Guid("00000000-0000-0000-0000-00000000000a") },
                    { new Guid("00000000-0000-0000-0000-00000000000e"), new Guid("00000000-0000-0000-0000-00000000000b") },
                    { new Guid("00000000-0000-0000-0000-000000000012"), new Guid("00000000-0000-0000-0000-00000000000b") },
                    { new Guid("00000000-0000-0000-0000-000000000014"), new Guid("00000000-0000-0000-0000-00000000000b") },
                    { new Guid("00000000-0000-0000-0000-000000000015"), new Guid("00000000-0000-0000-0000-00000000000b") },
                    { new Guid("00000000-0000-0000-0000-000000000017"), new Guid("00000000-0000-0000-0000-00000000000b") },
                    { new Guid("00000000-0000-0000-0000-000000000020"), new Guid("00000000-0000-0000-0000-00000000000b") },
                    { new Guid("00000000-0000-0000-0000-000000000033"), new Guid("00000000-0000-0000-0000-00000000000b") },
                    { new Guid("00000000-0000-0000-0000-000000000034"), new Guid("00000000-0000-0000-0000-00000000000b") },
                    { new Guid("00000000-0000-0000-0000-000000000035"), new Guid("00000000-0000-0000-0000-00000000000b") },
                    { new Guid("00000000-0000-0000-0000-00000000003d"), new Guid("00000000-0000-0000-0000-00000000000b") },
                    { new Guid("00000000-0000-0000-0000-00000000003e"), new Guid("00000000-0000-0000-0000-00000000000b") },
                    { new Guid("00000000-0000-0000-0000-00000000003f"), new Guid("00000000-0000-0000-0000-00000000000b") },
                    { new Guid("00000000-0000-0000-0000-000000000040"), new Guid("00000000-0000-0000-0000-00000000000b") },
                    { new Guid("00000000-0000-0000-0000-000000000042"), new Guid("00000000-0000-0000-0000-00000000000b") },
                    { new Guid("00000000-0000-0000-0000-000000000043"), new Guid("00000000-0000-0000-0000-00000000000b") },
                    { new Guid("00000000-0000-0000-0000-000000000044"), new Guid("00000000-0000-0000-0000-00000000000b") },
                    { new Guid("00000000-0000-0000-0000-000000000046"), new Guid("00000000-0000-0000-0000-00000000000b") },
                    { new Guid("00000000-0000-0000-0000-000000000047"), new Guid("00000000-0000-0000-0000-00000000000b") },
                    { new Guid("00000000-0000-0000-0000-000000000048"), new Guid("00000000-0000-0000-0000-00000000000b") },
                    { new Guid("00000000-0000-0000-0000-000000000049"), new Guid("00000000-0000-0000-0000-00000000000b") },
                    { new Guid("00000000-0000-0000-0000-00000000004d"), new Guid("00000000-0000-0000-0000-00000000000b") },
                    { new Guid("00000000-0000-0000-0000-00000000004e"), new Guid("00000000-0000-0000-0000-00000000000b") },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-00000000000c") },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new Guid("00000000-0000-0000-0000-00000000000c") },
                    { new Guid("00000000-0000-0000-0000-000000000009"), new Guid("00000000-0000-0000-0000-00000000000c") },
                    { new Guid("00000000-0000-0000-0000-000000000014"), new Guid("00000000-0000-0000-0000-00000000000c") },
                    { new Guid("00000000-0000-0000-0000-00000000001a"), new Guid("00000000-0000-0000-0000-00000000000c") },
                    { new Guid("00000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-00000000000e") },
                    { new Guid("00000000-0000-0000-0000-00000000001a"), new Guid("00000000-0000-0000-0000-00000000000e") },
                    { new Guid("00000000-0000-0000-0000-000000000030"), new Guid("00000000-0000-0000-0000-00000000000e") },
                    { new Guid("00000000-0000-0000-0000-000000000031"), new Guid("00000000-0000-0000-0000-00000000000e") },
                    { new Guid("00000000-0000-0000-0000-000000000032"), new Guid("00000000-0000-0000-0000-00000000000e") },
                    { new Guid("00000000-0000-0000-0000-00000000003a"), new Guid("00000000-0000-0000-0000-00000000000e") },
                    { new Guid("00000000-0000-0000-0000-000000000018"), new Guid("00000000-0000-0000-0000-00000000000f") },
                    { new Guid("00000000-0000-0000-0000-00000000001a"), new Guid("00000000-0000-0000-0000-00000000000f") },
                    { new Guid("00000000-0000-0000-0000-000000000020"), new Guid("00000000-0000-0000-0000-00000000000f") },
                    { new Guid("00000000-0000-0000-0000-000000000036"), new Guid("00000000-0000-0000-0000-00000000000f") },
                    { new Guid("00000000-0000-0000-0000-00000000004e"), new Guid("00000000-0000-0000-0000-00000000000f") },
                    { new Guid("00000000-0000-0000-0000-000000000019"), new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-00000000003c"), new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000033"), new Guid("00000000-0000-0000-0000-000000000012") },
                    { new Guid("00000000-0000-0000-0000-000000000034"), new Guid("00000000-0000-0000-0000-000000000012") },
                    { new Guid("00000000-0000-0000-0000-000000000035"), new Guid("00000000-0000-0000-0000-000000000012") },
                    { new Guid("00000000-0000-0000-0000-000000000048"), new Guid("00000000-0000-0000-0000-000000000012") },
                    { new Guid("00000000-0000-0000-0000-000000000006"), new Guid("00000000-0000-0000-0000-000000000013") },
                    { new Guid("00000000-0000-0000-0000-00000000002f"), new Guid("00000000-0000-0000-0000-000000000013") },
                    { new Guid("00000000-0000-0000-0000-00000000004b"), new Guid("00000000-0000-0000-0000-000000000013") },
                    { new Guid("00000000-0000-0000-0000-000000000050"), new Guid("00000000-0000-0000-0000-000000000013") },
                    { new Guid("00000000-0000-0000-0000-00000000000c"), new Guid("00000000-0000-0000-0000-000000000014") },
                    { new Guid("00000000-0000-0000-0000-00000000002f"), new Guid("00000000-0000-0000-0000-000000000014") },
                    { new Guid("00000000-0000-0000-0000-000000000050"), new Guid("00000000-0000-0000-0000-000000000014") },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-000000000015") },
                    { new Guid("00000000-0000-0000-0000-000000000013"), new Guid("00000000-0000-0000-0000-000000000015") },
                    { new Guid("00000000-0000-0000-0000-000000000018"), new Guid("00000000-0000-0000-0000-000000000015") },
                    { new Guid("00000000-0000-0000-0000-000000000019"), new Guid("00000000-0000-0000-0000-000000000015") },
                    { new Guid("00000000-0000-0000-0000-00000000001c"), new Guid("00000000-0000-0000-0000-000000000015") },
                    { new Guid("00000000-0000-0000-0000-00000000001d"), new Guid("00000000-0000-0000-0000-000000000015") },
                    { new Guid("00000000-0000-0000-0000-00000000001e"), new Guid("00000000-0000-0000-0000-000000000015") },
                    { new Guid("00000000-0000-0000-0000-00000000001f"), new Guid("00000000-0000-0000-0000-000000000015") },
                    { new Guid("00000000-0000-0000-0000-00000000000f"), new Guid("00000000-0000-0000-0000-000000000016") },
                    { new Guid("00000000-0000-0000-0000-000000000013"), new Guid("00000000-0000-0000-0000-000000000016") },
                    { new Guid("00000000-0000-0000-0000-000000000027"), new Guid("00000000-0000-0000-0000-000000000016") },
                    { new Guid("00000000-0000-0000-0000-00000000002e"), new Guid("00000000-0000-0000-0000-000000000016") },
                    { new Guid("00000000-0000-0000-0000-000000000041"), new Guid("00000000-0000-0000-0000-000000000016") },
                    { new Guid("00000000-0000-0000-0000-000000000009"), new Guid("00000000-0000-0000-0000-000000000017") },
                    { new Guid("00000000-0000-0000-0000-00000000000b"), new Guid("00000000-0000-0000-0000-000000000017") },
                    { new Guid("00000000-0000-0000-0000-00000000000e"), new Guid("00000000-0000-0000-0000-000000000017") },
                    { new Guid("00000000-0000-0000-0000-000000000014"), new Guid("00000000-0000-0000-0000-000000000017") },
                    { new Guid("00000000-0000-0000-0000-000000000016"), new Guid("00000000-0000-0000-0000-000000000017") },
                    { new Guid("00000000-0000-0000-0000-000000000017"), new Guid("00000000-0000-0000-0000-000000000017") },
                    { new Guid("00000000-0000-0000-0000-00000000002c"), new Guid("00000000-0000-0000-0000-000000000017") },
                    { new Guid("00000000-0000-0000-0000-00000000003c"), new Guid("00000000-0000-0000-0000-000000000017") },
                    { new Guid("00000000-0000-0000-0000-00000000004a"), new Guid("00000000-0000-0000-0000-000000000017") },
                    { new Guid("00000000-0000-0000-0000-00000000004b"), new Guid("00000000-0000-0000-0000-000000000017") },
                    { new Guid("00000000-0000-0000-0000-00000000004c"), new Guid("00000000-0000-0000-0000-000000000017") },
                    { new Guid("00000000-0000-0000-0000-000000000011"), new Guid("00000000-0000-0000-0000-000000000018") },
                    { new Guid("00000000-0000-0000-0000-00000000001c"), new Guid("00000000-0000-0000-0000-000000000018") },
                    { new Guid("00000000-0000-0000-0000-00000000001d"), new Guid("00000000-0000-0000-0000-000000000018") },
                    { new Guid("00000000-0000-0000-0000-00000000001e"), new Guid("00000000-0000-0000-0000-000000000018") },
                    { new Guid("00000000-0000-0000-0000-00000000001f"), new Guid("00000000-0000-0000-0000-000000000018") },
                    { new Guid("00000000-0000-0000-0000-00000000003a"), new Guid("00000000-0000-0000-0000-000000000018") },
                    { new Guid("00000000-0000-0000-0000-00000000004f"), new Guid("00000000-0000-0000-0000-000000000018") },
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000019") },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000019") },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new Guid("00000000-0000-0000-0000-000000000019") },
                    { new Guid("00000000-0000-0000-0000-000000000006"), new Guid("00000000-0000-0000-0000-000000000019") },
                    { new Guid("00000000-0000-0000-0000-000000000007"), new Guid("00000000-0000-0000-0000-000000000019") },
                    { new Guid("00000000-0000-0000-0000-000000000009"), new Guid("00000000-0000-0000-0000-000000000019") },
                    { new Guid("00000000-0000-0000-0000-00000000000a"), new Guid("00000000-0000-0000-0000-000000000019") },
                    { new Guid("00000000-0000-0000-0000-00000000000b"), new Guid("00000000-0000-0000-0000-000000000019") },
                    { new Guid("00000000-0000-0000-0000-00000000000c"), new Guid("00000000-0000-0000-0000-000000000019") },
                    { new Guid("00000000-0000-0000-0000-00000000000d"), new Guid("00000000-0000-0000-0000-000000000019") },
                    { new Guid("00000000-0000-0000-0000-00000000000f"), new Guid("00000000-0000-0000-0000-000000000019") },
                    { new Guid("00000000-0000-0000-0000-000000000010"), new Guid("00000000-0000-0000-0000-000000000019") },
                    { new Guid("00000000-0000-0000-0000-00000000001b"), new Guid("00000000-0000-0000-0000-000000000019") },
                    { new Guid("00000000-0000-0000-0000-00000000002e"), new Guid("00000000-0000-0000-0000-000000000019") },
                    { new Guid("00000000-0000-0000-0000-00000000002f"), new Guid("00000000-0000-0000-0000-000000000019") },
                    { new Guid("00000000-0000-0000-0000-00000000004a"), new Guid("00000000-0000-0000-0000-000000000019") },
                    { new Guid("00000000-0000-0000-0000-00000000004b"), new Guid("00000000-0000-0000-0000-000000000019") },
                    { new Guid("00000000-0000-0000-0000-00000000004c"), new Guid("00000000-0000-0000-0000-000000000019") },
                    { new Guid("00000000-0000-0000-0000-000000000050"), new Guid("00000000-0000-0000-0000-000000000019") },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-00000000001a") },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new Guid("00000000-0000-0000-0000-00000000001a") },
                    { new Guid("00000000-0000-0000-0000-000000000013"), new Guid("00000000-0000-0000-0000-00000000001a") },
                    { new Guid("00000000-0000-0000-0000-00000000001c"), new Guid("00000000-0000-0000-0000-00000000001a") },
                    { new Guid("00000000-0000-0000-0000-00000000001d"), new Guid("00000000-0000-0000-0000-00000000001a") },
                    { new Guid("00000000-0000-0000-0000-00000000001e"), new Guid("00000000-0000-0000-0000-00000000001a") },
                    { new Guid("00000000-0000-0000-0000-00000000001f"), new Guid("00000000-0000-0000-0000-00000000001a") },
                    { new Guid("00000000-0000-0000-0000-000000000020"), new Guid("00000000-0000-0000-0000-00000000001a") },
                    { new Guid("00000000-0000-0000-0000-000000000030"), new Guid("00000000-0000-0000-0000-00000000001a") },
                    { new Guid("00000000-0000-0000-0000-000000000031"), new Guid("00000000-0000-0000-0000-00000000001a") },
                    { new Guid("00000000-0000-0000-0000-000000000032"), new Guid("00000000-0000-0000-0000-00000000001a") },
                    { new Guid("00000000-0000-0000-0000-00000000004e"), new Guid("00000000-0000-0000-0000-00000000001a") },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-00000000001b") },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new Guid("00000000-0000-0000-0000-00000000001b") },
                    { new Guid("00000000-0000-0000-0000-000000000013"), new Guid("00000000-0000-0000-0000-00000000001b") },
                    { new Guid("00000000-0000-0000-0000-000000000030"), new Guid("00000000-0000-0000-0000-00000000001b") },
                    { new Guid("00000000-0000-0000-0000-000000000031"), new Guid("00000000-0000-0000-0000-00000000001b") },
                    { new Guid("00000000-0000-0000-0000-000000000032"), new Guid("00000000-0000-0000-0000-00000000001b") },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-00000000001c") },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new Guid("00000000-0000-0000-0000-00000000001c") },
                    { new Guid("00000000-0000-0000-0000-000000000013"), new Guid("00000000-0000-0000-0000-00000000001c") },
                    { new Guid("00000000-0000-0000-0000-000000000020"), new Guid("00000000-0000-0000-0000-00000000001c") },
                    { new Guid("00000000-0000-0000-0000-000000000030"), new Guid("00000000-0000-0000-0000-00000000001c") },
                    { new Guid("00000000-0000-0000-0000-000000000031"), new Guid("00000000-0000-0000-0000-00000000001c") },
                    { new Guid("00000000-0000-0000-0000-000000000032"), new Guid("00000000-0000-0000-0000-00000000001c") },
                    { new Guid("00000000-0000-0000-0000-000000000037"), new Guid("00000000-0000-0000-0000-00000000001c") },
                    { new Guid("00000000-0000-0000-0000-000000000038"), new Guid("00000000-0000-0000-0000-00000000001c") },
                    { new Guid("00000000-0000-0000-0000-000000000039"), new Guid("00000000-0000-0000-0000-00000000001c") },
                    { new Guid("00000000-0000-0000-0000-00000000003a"), new Guid("00000000-0000-0000-0000-00000000001c") },
                    { new Guid("00000000-0000-0000-0000-00000000003b"), new Guid("00000000-0000-0000-0000-00000000001c") },
                    { new Guid("00000000-0000-0000-0000-00000000004e"), new Guid("00000000-0000-0000-0000-00000000001c") },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new Guid("00000000-0000-0000-0000-00000000001d") },
                    { new Guid("00000000-0000-0000-0000-000000000011"), new Guid("00000000-0000-0000-0000-00000000001d") },
                    { new Guid("00000000-0000-0000-0000-000000000036"), new Guid("00000000-0000-0000-0000-00000000001d") },
                    { new Guid("00000000-0000-0000-0000-000000000037"), new Guid("00000000-0000-0000-0000-00000000001d") },
                    { new Guid("00000000-0000-0000-0000-000000000038"), new Guid("00000000-0000-0000-0000-00000000001d") },
                    { new Guid("00000000-0000-0000-0000-000000000039"), new Guid("00000000-0000-0000-0000-00000000001d") },
                    { new Guid("00000000-0000-0000-0000-00000000003a"), new Guid("00000000-0000-0000-0000-00000000001d") },
                    { new Guid("00000000-0000-0000-0000-00000000003b"), new Guid("00000000-0000-0000-0000-00000000001d") },
                    { new Guid("00000000-0000-0000-0000-00000000004f"), new Guid("00000000-0000-0000-0000-00000000001d") },
                    { new Guid("00000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-00000000001e") },
                    { new Guid("00000000-0000-0000-0000-000000000032"), new Guid("00000000-0000-0000-0000-00000000001e") },
                    { new Guid("00000000-0000-0000-0000-000000000045"), new Guid("00000000-0000-0000-0000-00000000001e") },
                    { new Guid("00000000-0000-0000-0000-000000000047"), new Guid("00000000-0000-0000-0000-00000000001e") },
                    { new Guid("00000000-0000-0000-0000-000000000048"), new Guid("00000000-0000-0000-0000-00000000001e") },
                    { new Guid("00000000-0000-0000-0000-000000000006"), new Guid("00000000-0000-0000-0000-00000000001f") },
                    { new Guid("00000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-00000000001f") },
                    { new Guid("00000000-0000-0000-0000-00000000000a"), new Guid("00000000-0000-0000-0000-00000000001f") },
                    { new Guid("00000000-0000-0000-0000-00000000000b"), new Guid("00000000-0000-0000-0000-000000000020") },
                    { new Guid("00000000-0000-0000-0000-000000000014"), new Guid("00000000-0000-0000-0000-000000000020") },
                    { new Guid("00000000-0000-0000-0000-000000000016"), new Guid("00000000-0000-0000-0000-000000000020") },
                    { new Guid("00000000-0000-0000-0000-00000000003c"), new Guid("00000000-0000-0000-0000-000000000020") },
                    { new Guid("00000000-0000-0000-0000-000000000009"), new Guid("00000000-0000-0000-0000-000000000021") },
                    { new Guid("00000000-0000-0000-0000-00000000000e"), new Guid("00000000-0000-0000-0000-000000000021") },
                    { new Guid("00000000-0000-0000-0000-000000000014"), new Guid("00000000-0000-0000-0000-000000000021") },
                    { new Guid("00000000-0000-0000-0000-000000000015"), new Guid("00000000-0000-0000-0000-000000000021") },
                    { new Guid("00000000-0000-0000-0000-000000000016"), new Guid("00000000-0000-0000-0000-000000000021") },
                    { new Guid("00000000-0000-0000-0000-00000000004a"), new Guid("00000000-0000-0000-0000-000000000021") },
                    { new Guid("00000000-0000-0000-0000-00000000004b"), new Guid("00000000-0000-0000-0000-000000000021") },
                    { new Guid("00000000-0000-0000-0000-00000000004c"), new Guid("00000000-0000-0000-0000-000000000021") },
                    { new Guid("00000000-0000-0000-0000-000000000012"), new Guid("00000000-0000-0000-0000-000000000022") },
                    { new Guid("00000000-0000-0000-0000-000000000017"), new Guid("00000000-0000-0000-0000-000000000022") },
                    { new Guid("00000000-0000-0000-0000-000000000021"), new Guid("00000000-0000-0000-0000-000000000022") },
                    { new Guid("00000000-0000-0000-0000-000000000022"), new Guid("00000000-0000-0000-0000-000000000022") },
                    { new Guid("00000000-0000-0000-0000-000000000023"), new Guid("00000000-0000-0000-0000-000000000022") },
                    { new Guid("00000000-0000-0000-0000-000000000024"), new Guid("00000000-0000-0000-0000-000000000022") },
                    { new Guid("00000000-0000-0000-0000-000000000025"), new Guid("00000000-0000-0000-0000-000000000022") },
                    { new Guid("00000000-0000-0000-0000-000000000026"), new Guid("00000000-0000-0000-0000-000000000022") },
                    { new Guid("00000000-0000-0000-0000-000000000028"), new Guid("00000000-0000-0000-0000-000000000022") },
                    { new Guid("00000000-0000-0000-0000-000000000029"), new Guid("00000000-0000-0000-0000-000000000022") },
                    { new Guid("00000000-0000-0000-0000-00000000002a"), new Guid("00000000-0000-0000-0000-000000000022") },
                    { new Guid("00000000-0000-0000-0000-00000000002b"), new Guid("00000000-0000-0000-0000-000000000022") },
                    { new Guid("00000000-0000-0000-0000-00000000003d"), new Guid("00000000-0000-0000-0000-000000000022") },
                    { new Guid("00000000-0000-0000-0000-00000000003e"), new Guid("00000000-0000-0000-0000-000000000022") },
                    { new Guid("00000000-0000-0000-0000-00000000003f"), new Guid("00000000-0000-0000-0000-000000000022") },
                    { new Guid("00000000-0000-0000-0000-000000000040"), new Guid("00000000-0000-0000-0000-000000000022") },
                    { new Guid("00000000-0000-0000-0000-000000000045"), new Guid("00000000-0000-0000-0000-000000000022") },
                    { new Guid("00000000-0000-0000-0000-000000000046"), new Guid("00000000-0000-0000-0000-000000000022") },
                    { new Guid("00000000-0000-0000-0000-000000000049"), new Guid("00000000-0000-0000-0000-000000000022") },
                    { new Guid("00000000-0000-0000-0000-000000000012"), new Guid("00000000-0000-0000-0000-000000000023") },
                    { new Guid("00000000-0000-0000-0000-00000000002d"), new Guid("00000000-0000-0000-0000-000000000023") },
                    { new Guid("00000000-0000-0000-0000-00000000003d"), new Guid("00000000-0000-0000-0000-000000000023") },
                    { new Guid("00000000-0000-0000-0000-00000000003e"), new Guid("00000000-0000-0000-0000-000000000023") },
                    { new Guid("00000000-0000-0000-0000-00000000003f"), new Guid("00000000-0000-0000-0000-000000000023") },
                    { new Guid("00000000-0000-0000-0000-000000000040"), new Guid("00000000-0000-0000-0000-000000000023") },
                    { new Guid("00000000-0000-0000-0000-000000000045"), new Guid("00000000-0000-0000-0000-000000000023") },
                    { new Guid("00000000-0000-0000-0000-000000000046"), new Guid("00000000-0000-0000-0000-000000000023") },
                    { new Guid("00000000-0000-0000-0000-00000000004a"), new Guid("00000000-0000-0000-0000-000000000023") },
                    { new Guid("00000000-0000-0000-0000-00000000004b"), new Guid("00000000-0000-0000-0000-000000000023") },
                    { new Guid("00000000-0000-0000-0000-00000000004c"), new Guid("00000000-0000-0000-0000-000000000023") },
                    { new Guid("00000000-0000-0000-0000-000000000014"), new Guid("00000000-0000-0000-0000-000000000024") },
                    { new Guid("00000000-0000-0000-0000-000000000015"), new Guid("00000000-0000-0000-0000-000000000024") },
                    { new Guid("00000000-0000-0000-0000-000000000016"), new Guid("00000000-0000-0000-0000-000000000024") },
                    { new Guid("00000000-0000-0000-0000-00000000002c"), new Guid("00000000-0000-0000-0000-000000000024") },
                    { new Guid("00000000-0000-0000-0000-00000000003c"), new Guid("00000000-0000-0000-0000-000000000024") },
                    { new Guid("00000000-0000-0000-0000-00000000004a"), new Guid("00000000-0000-0000-0000-000000000024") },
                    { new Guid("00000000-0000-0000-0000-00000000004b"), new Guid("00000000-0000-0000-0000-000000000024") },
                    { new Guid("00000000-0000-0000-0000-00000000004c"), new Guid("00000000-0000-0000-0000-000000000024") },
                    { new Guid("00000000-0000-0000-0000-000000000018"), new Guid("00000000-0000-0000-0000-000000000025") },
                    { new Guid("00000000-0000-0000-0000-000000000019"), new Guid("00000000-0000-0000-0000-000000000025") },
                    { new Guid("00000000-0000-0000-0000-00000000001a"), new Guid("00000000-0000-0000-0000-000000000025") },
                    { new Guid("00000000-0000-0000-0000-00000000001c"), new Guid("00000000-0000-0000-0000-000000000025") },
                    { new Guid("00000000-0000-0000-0000-00000000001d"), new Guid("00000000-0000-0000-0000-000000000025") },
                    { new Guid("00000000-0000-0000-0000-00000000001e"), new Guid("00000000-0000-0000-0000-000000000025") },
                    { new Guid("00000000-0000-0000-0000-00000000001f"), new Guid("00000000-0000-0000-0000-000000000025") },
                    { new Guid("00000000-0000-0000-0000-00000000000f"), new Guid("00000000-0000-0000-0000-000000000026") },
                    { new Guid("00000000-0000-0000-0000-000000000017"), new Guid("00000000-0000-0000-0000-000000000026") },
                    { new Guid("00000000-0000-0000-0000-000000000021"), new Guid("00000000-0000-0000-0000-000000000026") },
                    { new Guid("00000000-0000-0000-0000-00000000002f"), new Guid("00000000-0000-0000-0000-000000000026") },
                    { new Guid("00000000-0000-0000-0000-000000000049"), new Guid("00000000-0000-0000-0000-000000000026") },
                    { new Guid("00000000-0000-0000-0000-000000000050"), new Guid("00000000-0000-0000-0000-000000000026") },
                    { new Guid("00000000-0000-0000-0000-000000000042"), new Guid("00000000-0000-0000-0000-000000000027") },
                    { new Guid("00000000-0000-0000-0000-000000000043"), new Guid("00000000-0000-0000-0000-000000000027") },
                    { new Guid("00000000-0000-0000-0000-000000000044"), new Guid("00000000-0000-0000-0000-000000000027") },
                    { new Guid("00000000-0000-0000-0000-00000000004d"), new Guid("00000000-0000-0000-0000-000000000028") }
                });

            migrationBuilder.InsertData(
                table: "Message",
                columns: new[] { "Id", "ChatBoxId", "CreatedBy", "CreatedOn", "FileLocation", "FileName", "ProposalId", "RequestId", "Text" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-00000000000a"), new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2023, 9, 9, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, new Guid("00000000-0000-0000-0000-000000000001"), null },
                    { new Guid("00000000-0000-0000-0000-00000000000b"), new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2023, 9, 14, 0, 0, 0, 0, DateTimeKind.Utc), null, null, null, new Guid("00000000-0000-0000-0000-000000000002"), null },
                    { new Guid("00000000-0000-0000-0000-000000000011"), new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc), null, null, new Guid("00000000-0000-0000-0000-000000000001"), null, null }
                });

            migrationBuilder.InsertData(
                table: "Milestone",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "MilestoneName", "ProposalId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Thỏa thuận đã được tạo", new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Đặt cọc thành công 20000 xu (25%)", new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Thỏa thuận đã được chấp nhận", new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Gửi tài nguyên thỏa thuận (phác thảo)", new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Gửi tài nguyên thỏa thuận (phác thảo)", new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000006"), new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Gửi tài nguyên thỏa thuận (cuối cùng)", new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000007"), new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Gửi tài nguyên thỏa thuận (chỉnh sửa)", new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Hoàn tất thanh toán 60000 xu (75%)", new Guid("00000000-0000-0000-0000-000000000001") }
                });

            migrationBuilder.InsertData(
                table: "ProposalAsset",
                columns: new[] { "Id", "ContentType", "CreatedBy", "CreatedOn", "Location", "ProposalAssetName", "ProposalId", "Size", "Type" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "rar", new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Utc), "https://storage.cloud.google.com/artworkia-storage/ProposalAsset/ConceptAsset.rar?authuser=4", "ConceptAsset.rar", new Guid("00000000-0000-0000-0000-000000000001"), 8000000m, 0 },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "rar", new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "https://storage.cloud.google.com/artworkia-storage/ProposalAsset/ConceptAsset.rar?authuser=4", "ConceptAsset.rar", new Guid("00000000-0000-0000-0000-000000000001"), 8000000m, 0 },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "rar", new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Utc), "https://storage.cloud.google.com/artworkia-storage/ProposalAsset/FinalAsset.rar?authuser=4", "FinalAsset.rar", new Guid("00000000-0000-0000-0000-000000000001"), 14000000m, 1 },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "rar", new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), "https://storage.cloud.google.com/artworkia-storage/ProposalAsset/RevisionAsset.rar?authuser=4", "RevisionAsset.rar", new Guid("00000000-0000-0000-0000-000000000001"), 14000000m, 2 }
                });

            migrationBuilder.InsertData(
                table: "Review",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Detail", "ProposalId", "Vote" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Dịch vụ xuất sắc", new Guid("00000000-0000-0000-0000-000000000001"), 5.0 });

            migrationBuilder.InsertData(
                table: "TransactionHistory",
                columns: new[] { "Id", "AssetId", "CreatedBy", "CreatedOn", "Detail", "Fee", "PaymentMethod", "PaymentOrderCode", "PaymentTransactionId", "Price", "ProposalId", "ToAccountId", "TransactionStatus", "WalletBalance" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000083"), new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2023, 12, 29, 22, 30, 3, 678, DateTimeKind.Utc), "Mở khóa tài nguyên \"Pixel Art GUI / UI Kit + 151 icons!\"", 0.0, "Wallet", null, null, -150000.0, null, new Guid("00000000-0000-0000-0000-00000000000a"), 1, 850000.0 },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000083"), new Guid("00000000-0000-0000-0000-00000000000a"), new DateTime(2023, 12, 29, 22, 30, 3, 678, DateTimeKind.Utc), "Mở khóa tài nguyên \"Pixel Art GUI / UI Kit + 151 icons!\"", 7500.0, "Wallet", null, null, 142500.0, null, new Guid("00000000-0000-0000-0000-000000000005"), 1, 142500.0 },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2024, 1, 16, 22, 30, 3, 678, DateTimeKind.Utc), "Mở khóa tài nguyên \"Dấu vết của quá khứ\"", 0.0, "Wallet", null, null, -10000.0, null, new Guid("00000000-0000-0000-0000-000000000002"), 1, 71000.0 },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2024, 1, 16, 22, 30, 3, 678, DateTimeKind.Utc), "Mở khóa tài nguyên \"Dấu vết của quá khứ\"", 500.0, "Wallet", null, null, 9500.0, null, new Guid("00000000-0000-0000-0000-000000000004"), 1, 128000.0 },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new Guid("00000000-0000-0000-0000-000000000010"), new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2024, 1, 15, 9, 59, 59, 0, DateTimeKind.Utc), "Mở khóa tài nguyên \"File PTS tuyển tập minh hoạ sách tâm lý\"", 0.0, "Wallet", null, null, -20000.0, null, new Guid("00000000-0000-0000-0000-000000000004"), 1, 118500.0 },
                    { new Guid("00000000-0000-0000-0000-000000000006"), new Guid("00000000-0000-0000-0000-000000000010"), new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2024, 1, 15, 9, 59, 59, 0, DateTimeKind.Utc), "Mở khóa tài nguyên \"File PTS tuyển tập minh hoạ sách tâm lý\"", 1000.0, "Wallet", null, null, 19000.0, null, new Guid("00000000-0000-0000-0000-000000000002"), 1, 81000.0 },
                    { new Guid("00000000-0000-0000-0000-000000000007"), new Guid("00000000-0000-0000-0000-000000000050"), new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2024, 1, 16, 22, 30, 3, 678, DateTimeKind.Utc), "Mở khóa tài nguyên \"Không gian vũ trụ\"", 0.0, "Wallet", null, null, -50000.0, null, new Guid("00000000-0000-0000-0000-00000000000b"), 1, 21000.0 },
                    { new Guid("00000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-000000000050"), new Guid("00000000-0000-0000-0000-00000000000b"), new DateTime(2024, 1, 16, 22, 30, 3, 678, DateTimeKind.Utc), "Mở khóa tài nguyên \"Không gian vũ trụ\"", 2500.0, "Wallet", null, null, 47500.0, null, new Guid("00000000-0000-0000-0000-000000000004"), 1, 47500.0 },
                    { new Guid("00000000-0000-0000-0000-000000000009"), new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2024, 1, 1, 22, 30, 3, 678, DateTimeKind.Utc), "Mở khóa tài nguyên \"Dấu vết của quá khứ\"", 0.0, "Wallet", null, null, -10000.0, null, new Guid("00000000-0000-0000-0000-000000000002"), 1, 840000.0 },
                    { new Guid("00000000-0000-0000-0000-000000000010"), new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2024, 1, 1, 22, 30, 3, 678, DateTimeKind.Utc), "Mở khóa tài nguyên \"Dấu vết của quá khứ\"", 500.0, "Wallet", null, null, 9500.0, null, new Guid("00000000-0000-0000-0000-000000000005"), 1, 119500.0 },
                    { new Guid("00000000-0000-0000-0000-000000000011"), new Guid("00000000-0000-0000-0000-000000000080"), new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2024, 1, 2, 22, 30, 3, 678, DateTimeKind.Utc), "Mở khóa tài nguyên \"Figma Full\"", 0.0, "Wallet", null, null, -90000.0, null, new Guid("00000000-0000-0000-0000-000000000012"), 1, 750000.0 },
                    { new Guid("00000000-0000-0000-0000-000000000012"), new Guid("00000000-0000-0000-0000-000000000080"), new Guid("00000000-0000-0000-0000-000000000012"), new DateTime(2024, 1, 2, 22, 30, 3, 678, DateTimeKind.Utc), "Mở khóa tài nguyên \"Figma Full\"", 4500.0, "Wallet", null, null, 85500.0, null, new Guid("00000000-0000-0000-0000-000000000005"), 1, 85500.0 },
                    { new Guid("00000000-0000-0000-0000-000000000101"), null, new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2024, 1, 3, 9, 59, 59, 0, DateTimeKind.Utc), "Đặt cọc thỏa thuận \"Làm web thiệp cưới\" (25%)", 0.0, "Wallet", null, null, -20000.0, new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000002"), 1, 730000.0 },
                    { new Guid("00000000-0000-0000-0000-000000000102"), null, new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2024, 1, 3, 9, 59, 59, 0, DateTimeKind.Utc), "Đặt cọc thỏa thuận \"Làm web thiệp cưới\" (25%)", 1000.0, "Wallet", null, null, 19000.0, new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000005"), 1, 138500.0 },
                    { new Guid("00000000-0000-0000-0000-000000000103"), null, new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2024, 2, 1, 9, 59, 59, 0, DateTimeKind.Utc), "Hoàn tất thanh toán thỏa thuận \"Làm web thiệp cưới\" (75%)", 0.0, "Wallet", null, null, -60000.0, new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000002"), 1, 670000.0 },
                    { new Guid("00000000-0000-0000-0000-000000000104"), null, new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2024, 2, 1, 9, 59, 59, 0, DateTimeKind.Utc), "Hoàn tất thanh toán thỏa thuận \"Làm web thiệp cưới\" (75%)", 3000.0, "Wallet", null, null, 57000.0, new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000005"), 1, 185000.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_Email",
                table: "Account",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Account_Username",
                table: "Account",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArtistCertificate_AccountId",
                table: "ArtistCertificate",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Artwork_CreatedBy",
                table: "Artwork",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Artwork_LicenseTypeId",
                table: "Artwork",
                column: "LicenseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_ArtworkId",
                table: "Asset",
                column: "ArtworkId");

            migrationBuilder.CreateIndex(
                name: "IX_Block_BlockedId",
                table: "Block",
                column: "BlockedId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookmark_CollectionId",
                table: "Bookmark",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentCategory",
                table: "Category",
                column: "ParentCategory");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryArtworkDetail_ArtworkId",
                table: "CategoryArtworkDetail",
                column: "ArtworkId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryServiceDetail_ServiceId",
                table: "CategoryServiceDetail",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatBox_AccountId_1",
                table: "ChatBox",
                column: "AccountId_1");

            migrationBuilder.CreateIndex(
                name: "IX_ChatBox_AccountId_2",
                table: "ChatBox",
                column: "AccountId_2");

            migrationBuilder.CreateIndex(
                name: "IX_Collection_CreatedBy",
                table: "Collection",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ArtworkId",
                table: "Comment",
                column: "ArtworkId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_CreatedBy",
                table: "Comment",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ReplyId",
                table: "Comment",
                column: "ReplyId");

            migrationBuilder.CreateIndex(
                name: "IX_Follow_FollowedId",
                table: "Follow",
                column: "FollowedId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_ArtworkId",
                table: "Image",
                column: "ArtworkId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_ArtworkId",
                table: "Like",
                column: "ArtworkId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ChatBoxId",
                table: "Message",
                column: "ChatBoxId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_CreatedBy",
                table: "Message",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ProposalId",
                table: "Message",
                column: "ProposalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Message_RequestId",
                table: "Message",
                column: "RequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Milestone_CreatedBy",
                table: "Milestone",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Milestone_ProposalId",
                table: "Milestone",
                column: "ProposalId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_SentToAccount",
                table: "Notification",
                column: "SentToAccount");

            migrationBuilder.CreateIndex(
                name: "IX_Proposal_CreatedBy",
                table: "Proposal",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Proposal_OrdererId",
                table: "Proposal",
                column: "OrdererId");

            migrationBuilder.CreateIndex(
                name: "IX_Proposal_ServiceId",
                table: "Proposal",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProposalAsset_ProposalId",
                table: "ProposalAsset",
                column: "ProposalId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_CreatedBy",
                table: "Report",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Request_CreatedBy",
                table: "Request",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Request_ServiceId",
                table: "Request",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_CreatedBy",
                table: "Review",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Review_ProposalId",
                table: "Review",
                column: "ProposalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Service_CreatedBy",
                table: "Service",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceDetail_ArtworkId",
                table: "ServiceDetail",
                column: "ArtworkId");

            migrationBuilder.CreateIndex(
                name: "IX_SoftwareDetail_ArtworkId",
                table: "SoftwareDetail",
                column: "ArtworkId");

            migrationBuilder.CreateIndex(
                name: "IX_SoftwareUsed_SoftwareName",
                table: "SoftwareUsed",
                column: "SoftwareName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tag_TagName",
                table: "Tag",
                column: "TagName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TagDetail_ArtworkId",
                table: "TagDetail",
                column: "ArtworkId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistory_AssetId",
                table: "TransactionHistory",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistory_CreatedBy",
                table: "TransactionHistory",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistory_ProposalId",
                table: "TransactionHistory",
                column: "ProposalId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistory_ToAccountId",
                table: "TransactionHistory",
                column: "ToAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToken_ATid",
                table: "UserToken",
                column: "ATid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserToken_RTid",
                table: "UserToken",
                column: "RTid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserToken_UserId",
                table: "UserToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_AccountId",
                table: "Wallet",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WalletHistory_AppTransId",
                table: "WalletHistory",
                column: "AppTransId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WalletHistory_CreatedBy",
                table: "WalletHistory",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_WithdrawalRequests_CreatedOn",
                table: "WithdrawalRequests",
                column: "CreatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_WithdrawalRequests_Status",
                table: "WithdrawalRequests",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_WithdrawalRequests_WalletId",
                table: "WithdrawalRequests",
                column: "WalletId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistCertificate");

            migrationBuilder.DropTable(
                name: "Block");

            migrationBuilder.DropTable(
                name: "Bookmark");

            migrationBuilder.DropTable(
                name: "CategoryArtworkDetail");

            migrationBuilder.DropTable(
                name: "CategoryServiceDetail");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Follow");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Like");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Milestone");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "ProposalAsset");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "ServiceDetail");

            migrationBuilder.DropTable(
                name: "SoftwareDetail");

            migrationBuilder.DropTable(
                name: "TagDetail");

            migrationBuilder.DropTable(
                name: "TransactionHistory");

            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DropTable(
                name: "WalletHistory");

            migrationBuilder.DropTable(
                name: "WithdrawalRequests");

            migrationBuilder.DropTable(
                name: "Collection");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "ChatBox");

            migrationBuilder.DropTable(
                name: "Request");

            migrationBuilder.DropTable(
                name: "SoftwareUsed");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Asset");

            migrationBuilder.DropTable(
                name: "Proposal");

            migrationBuilder.DropTable(
                name: "Wallet");

            migrationBuilder.DropTable(
                name: "Artwork");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "LicenseType");

            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
