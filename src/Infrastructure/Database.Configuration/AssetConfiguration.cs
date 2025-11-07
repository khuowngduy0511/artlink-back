using Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class AssetConfiguration : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
        builder.ToTable(nameof(Asset));

        builder.Property(x => x.Id).HasDefaultValueSql("newid()");
        builder.Property(x => x.LastModificatedOn).HasDefaultValueSql("getutcdate()");

        // relationship
        builder.HasOne(x => x.Artwork).WithMany(a => a.Assets).HasForeignKey(x => x.ArtworkId);
        builder.HasMany(x => x.TransactionHistories).WithOne(t => t.Asset).HasForeignKey(t => t.AssetId);

        #region init data
        builder.HasData
        (
            // Artwork 4 lamlam
            new Asset()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                ArtworkId = new Guid("00000000-0000-0000-0000-000000000004"),
                AssetTitle = "Dấu vết của quá khứ",
                Description = "Ảnh gốc, phục hồi chất lượng",
                AssetName = "CommercialAsset.rar",
                Location = "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4",
                Price = 10000,
                ContentType = "rar",
                Size = 8000000
            },

            // Artwork 8 lamlam
            new Asset()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                ArtworkId = new Guid("00000000-0000-0000-0000-000000000008"),
                AssetTitle = "File PTS tuyển tập minh hoạ sách tâm lý",
                Description = "tập tin PTS tuyển tập minh hoạ sách tâm lý " +
                "sẽ cung cấp một cái nhìn tổng quan và thú vị.",
                AssetName = "CommercialAsset.rar",
                Location = "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4",
                Price = 20000,
                ContentType = "rar",
                Size = 8000000
            },

            // Artwork 14 lamlam
            new Asset()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000020"),
                ArtworkId = new Guid("00000000-0000-0000-0000-00000000000e"),
                AssetTitle = "Robot PTS",
                Description = "Tặng các bạn",
                AssetName = "CommercialAsset.rar",
                Location = "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4",
                Price = 0,
                ContentType = "rar",
                Size = 8000000
            },

            // Artwork 15 phu
            new Asset()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000030"),
                ArtworkId = new Guid("00000000-0000-0000-0000-00000000000f"),
                AssetTitle = "Ảnh cánh cụt",
                Description = "Tổng hợp cánh cụt fullsize",
                AssetName = "CommercialAsset.rar",
                Location = "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4",
                Price = 12000,
                ContentType = "rar",
                Size = 8000000
            },
            new Asset()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000031"),
                ArtworkId = new Guid("00000000-0000-0000-0000-00000000000f"),
                AssetTitle = "Ảnh cánh cụt khác",
                Description = "Tổng hợp cánh cụt fullsize",
                AssetName = "CommercialAsset.rar",
                Location = "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4",
                Price = 12000,
                ContentType = "rar",
                Size = 8000000
            },


            // Artwork 18 manhkbrady
            new Asset()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000040"),
                ArtworkId = new Guid("00000000-0000-0000-0000-000000000012"),
                AssetTitle = "Hình ảnh giới thiệu nhân vật & Đếm ngược ra mắt game",
                Description = "File PTS, tùy ý chỉnh sửa",
                AssetName = "CommercialAsset.rar",
                Location = "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4",
                Price = 30000,
                ContentType = "rar",
                Size = 8000000
            },

            // Artwork 20 melodysheep
            new Asset()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000050"),
                ArtworkId = new Guid("00000000-0000-0000-0000-000000000014"),
                AssetTitle = "Không gian vũ trụ",
                Description = "File PTS, A collection of 173 wallpapers in 4K resolution from THE SIGHTS OF SPACE",
                AssetName = "CommercialAsset.rar",
                Location = "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4",
                Price = 50000,
                ContentType = "rar",
                Size = 8000000
            },

            // Artwork 21 melodysheep
            new Asset()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000060"),
                ArtworkId = new Guid("00000000-0000-0000-0000-000000000015"),
                AssetTitle = "Tương lai loài người",
                Description = "File PTS, A collection of 173 wallpapers in 4K resolution",
                AssetName = "CommercialAsset.rar",
                Location = "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4",
                Price = 50000,
                ContentType = "rar",
                Size = 8000000
            },

            // Artwork 27 phamha
            new Asset()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000063"),
                ArtworkId = new Guid("00000000-0000-0000-0000-00000000001b"),
                AssetTitle = "Ảnh gốc",
                Description = "File PTS, tùy ý chỉnh sửa",
                AssetName = "CommercialAsset.rar",
                Location = "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4",
                Price = 0,
                ContentType = "rar",
                Size = 8000000
            },

            // Artwork 33 buiduong
            new Asset()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000068"),
                ArtworkId = new Guid("00000000-0000-0000-0000-000000000021"),
                AssetTitle = "Ảnh gốc",
                Description = "File PTS, tùy ý chỉnh sửa",
                AssetName = "CommercialAsset.rar",
                Location = "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4",
                Price = 0,
                ContentType = "rar",
                Size = 8000000
            },

            // Artwork 34 minhhuy
            new Asset()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000069"),
                ArtworkId = new Guid("00000000-0000-0000-0000-000000000022"),
                AssetTitle = "Ảnh gốc (xóa)",
                Description = "File PTS, tùy ý chỉnh sửa",
                AssetName = "CommercialAsset.rar",
                Location = "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4",
                Price = 20000,
                ContentType = "rar",
                Size = 8000000,
                DeletedOn = DateTime.Parse("2024-01-03T08:20:45.890Z"),
                DeletedBy = Guid.Parse("00000000-0000-0000-0000-000000000018"), //minhhuy
            },
            new Asset()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000006a"),
                ArtworkId = new Guid("00000000-0000-0000-0000-000000000022"),
                AssetTitle = "Ảnh gốc (thêm)",
                Description = "File PTS, tùy ý chỉnh sửa",
                AssetName = "CommercialAsset.rar",
                Location = "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4",
                Price = 25000,
                ContentType = "rar",
                Size = 8000000
            },

            // Artwork 51 doantrang
            new Asset()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000070"),
                ArtworkId = new Guid("00000000-0000-0000-0000-000000000033"),
                AssetTitle = "Figma Full",
                Description = "All 100 pages + 200 icons in Figma",
                AssetName = "CommercialAsset.rar",
                Location = "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4",
                Price = 100000,
                ContentType = "rar",
                Size = 8000000
            },

            // Artwork 52 doantrang
            new Asset()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000080"),
                ArtworkId = new Guid("00000000-0000-0000-0000-000000000034"),
                AssetTitle = "Figma Full",
                Description = "All 100 pages + 200 icons in Figma",
                AssetName = "CommercialAsset.rar",
                Location = "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4",
                Price = 90000,
                ContentType = "rar",
                Size = 8000000
            },

            // Artwork 53 doantrang
            new Asset()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000081"),
                ArtworkId = new Guid("00000000-0000-0000-0000-000000000035"),
                AssetTitle = "Figma Full",
                Description = "All 100 pages + 200 icons in Figma",
                AssetName = "CommercialAsset.rar",
                Location = "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4",
                Price = 120000,
                ContentType = "rar",
                Size = 8000000
            },

            // Artwork 61 tranduc
            new Asset()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000082"),
                ArtworkId = new Guid("00000000-0000-0000-0000-000000000035"),
                AssetTitle = "Figma Full",
                Description = "All 100 pages + 200 icons in Figma",
                AssetName = "CommercialAsset.rar",
                Location = "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4",
                Price = 120000,
                ContentType = "rar",
                Size = 8000000
            },

            // Artwork 70 phamthanh
            new Asset()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000083"),
                ArtworkId = new Guid("00000000-0000-0000-0000-000000000046"),
                AssetTitle = "Pixel Art GUI / UI Kit + 151 icons!",
                Description = "Universal / Fantasy Pixel Art GUI Kit for your new project, " +
                "featuring Windowed and Fullscreen views to fit all your needs!",
                AssetName = "CommercialAsset.rar",
                Location = "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4",
                Price = 150000,
                ContentType = "rar",
                Size = 8000000
            },

            // Artwork 71 truongthu
            new Asset()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000084"),
                ArtworkId = new Guid("00000000-0000-0000-0000-000000000047"),
                AssetTitle = "Logo Asset",
                Description = "Original PNG",
                AssetName = "CommercialAsset.rar",
                Location = "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4",
                Price = 10000,
                ContentType = "rar",
                Size = 8000000
            },

            // Artwork 72 truongthu
            new Asset()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000085"),
                ArtworkId = new Guid("00000000-0000-0000-0000-000000000048"),
                AssetTitle = "Background Fullsize",
                Description = "Original PNG",
                AssetName = "CommercialAsset.rar",
                Location = "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4",
                Price = 0,
                ContentType = "rar",
                Size = 8000000
            },

            // Artwork 77 truongthu
            new Asset()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000090"),
                ArtworkId = new Guid("00000000-0000-0000-0000-00000000004d"),
                AssetTitle = "Logo Asset",
                Description = "Original PNG",
                AssetName = "CommercialAsset.rar",
                Location = "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4",
                Price = 10000,
                ContentType = "rar",
                Size = 8000000
            },

            // Artwork 63 tranduc
            new Asset()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000091"),
                ArtworkId = new Guid("00000000-0000-0000-0000-00000000003f"),
                AssetTitle = "The Dragonborn Daedric Armor",
                Description = "All file asset 3d model",
                AssetName = "CommercialAsset.rar",
                Location = "https://storage.cloud.google.com/artworkia-storage/Asset/CommercialAsset.rar?authuser=4",
                Price = 0,
                ContentType = "rar",
                Size = 8000000
            }

        );
        #endregion
    }
}
