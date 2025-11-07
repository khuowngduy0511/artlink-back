using Domain.Entitites;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nest;

namespace Infrastructure.Database.Configuration;

public class ArtworkConfiguration : IEntityTypeConfiguration<Artwork>
{
    public void Configure(EntityTypeBuilder<Artwork> builder)
    {
        builder.ToTable(nameof(Artwork));

        builder.Property(x => x.Id).HasDefaultValueSql("newid()");
        builder.Property(x => x.CreatedOn).HasDefaultValueSql("getutcdate()");
        builder.Property(x => x.LastModificatedOn).HasDefaultValueSql("getutcdate()");

        // relationship
        builder.HasOne(x => x.Account).WithMany(a => a.Artworks).HasForeignKey(x => x.CreatedBy);
        builder.HasMany(x => x.CategoryArtworkDetails).WithOne(d => d.Artwork).HasForeignKey(d => d.ArtworkId);
        builder.HasMany(x => x.Assets).WithOne(a => a.Artwork).HasForeignKey(a => a.ArtworkId);
        builder.HasMany(x => x.Images).WithOne(a => a.Artwork).HasForeignKey(a => a.ArtworkId);
        builder.HasMany(x => x.TagDetails).WithOne(d => d.Artwork).HasForeignKey(d => d.ArtworkId);
        builder.HasMany(x => x.Likes).WithOne(l => l.Artwork).HasForeignKey(l => l.ArtworkId);
        builder.HasMany(x => x.Comments).WithOne(c => c.Artwork).HasForeignKey(c => c.ArtworkId);
        builder.HasMany(x => x.ServiceDetails).WithOne(c => c.Artwork).HasForeignKey(c => c.ArtworkId);

        #region init data 
        // Seed 50 artworks
        builder.HasData
        (
            // Artwork 1
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Title = "Hoàng hôn rực nắng",
                Description = "Tuyển tập những bức vẽ về hoàng hôn",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000002"), //lamlam
                CreatedOn = DateTime.Parse("2023-12-02T02:37:42.345Z"),
                Thumbnail = "https://i.pinimg.com/736x/8d/f7/be/8df7be5e052e97b824e6b0f783309161.jpg",
                ThumbnailName = "00000000-0000-0000-0000-000000000001_t.jpg",
                ViewCount = 1344,
                LikeCount = 8,
                CommentCount = 6,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = true
            },

            // Artwork 2
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                Title = "Hành trình sâu cảm xúc",
                Description = "Khám phá sâu hơn về cảm xúc và tâm trạng trong cuộc sống",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000002"), //lamlam
                CreatedOn = DateTime.Parse("2023-12-03T15:20:45.890Z"),
                Thumbnail = "https://th.bing.com/th/id/OIG.mMOt1xWJJCHsRoPJXtHQ?pid=ImgGn",
                ThumbnailName = "00000000-0000-0000-0000-000000000001_t.jpg",
                ViewCount = 327,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = true
            },

            // Artwork 3
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                Title = "Chiến thắng Điện Biên Phủ",
                Description = "Minh họa cuộc chiến tiêu biểu của thời đại",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000003"), //hoanganh
                CreatedOn = DateTime.Parse("2023-12-04T13:55:30.456Z"),
                Thumbnail = "https://vov2-media.solidtech.vn/sites/default/files/styles/large/public/2022-05/dien-bien-phu_-_bia.jpg",
                ThumbnailName = "00000000-0000-0000-0000-000000000002_t.jpg",
                ViewCount = 2138,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = false
            },

            // Artwork 4
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                Title = "Dấu vết của quá khứ",
                Description = "Tác phẩm thể hiện sự ảnh hưởng của quá khứ đối với hiện tại",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000002"), //lamlam
                CreatedOn = DateTime.Parse("2023-12-05T08:30:03.678Z"),
                Thumbnail = "https://bizweb.dktcdn.net/100/378/894/files/5.jpg?v=1627978886825",
                ThumbnailName = "00000000-0000-0000-0000-000000000004_t.jpg",
                ViewCount = 2063,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = false
            },

            // Artwork 5
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                Title = "Hành trình của sự sáng tạo",
                Description = "Minh họa cho hành trình không ngừng của sự sáng tạo",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000004"), //thong
                CreatedOn = DateTime.Parse("2023-12-06T00:30:15.567Z"),
                Thumbnail = "https://th.bing.com/th/id/OIG.LTaVFacabNQc22SAk1r1?pid=ImgGn",
                ThumbnailName = "00000000-0000-0000-0000-000000000005_t.jpg",
                ViewCount = 779,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = true
            },

            // Artwork 6
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                Title = "Sự đồng hành của đối tác tâm lý",
                Description = "Tượng trưng cho sự đồng hành và hỗ trợ của đối tác tâm lý",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000002"), //lamlam
                CreatedOn = DateTime.Parse("2023-12-07T05:40:28.901Z"),
                Thumbnail = "https://th.bing.com/th/id/OIG.AoM9akz.gT4RMZ9R6DOh?pid=ImgGn",
                ThumbnailName = "00000000-0000-0000-0000-000000000006_t.jpg",
                ViewCount = 245,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = true
            },

            // Artwork 7
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                Title = "Mặt trời bên trong",
                Description = "Hình ảnh tượng trưng cho ánh sáng và năng lượng bên trong chúng ta",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000002"), //lamlam
                CreatedOn = DateTime.Parse("2023-12-08T18:30:15.567Z"),
                Thumbnail = "https://i.pinimg.com/564x/61/59/6c/61596cae1a6dc2dede171e59fb5787aa.jpg",
                ThumbnailName = "00000000-0000-0000-0000-000000000007_t.jpg",
                ViewCount = 356,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = true
            },

            // Artwork 8
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                Title = "Tuyển tập minh hoạ sách tâm lý",
                Description = "Đây là tuyển tập tâm huyết của mình, nhớ like và comment để ủng hộ mình nha",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000004"), //thong
                CreatedOn = DateTime.Parse("2023-12-01T08:30:03.678Z"),
                Thumbnail = "https://static-company.waka.vn/img.media/tin%20t%E1%BB%A9c/38540.jpg",
                ThumbnailName = "00000000-0000-0000-0000-000000000008_t.jpg",
                ViewCount = 342,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = false
            },

            // Artwork 9
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                Title = "Sự lạc quan của tương lai",
                Description = "Minh họa cho tâm trạng lạc quan và hy vọng về tương lai",
                Privacy = PrivacyEnum.Private,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000005"), //phuhuynh
                CreatedOn = DateTime.Parse("2023-12-09T21:20:10.234Z"),
                Thumbnail = "https://i.pinimg.com/originals/8d/97/66/8d9766d16e7cf36ade29a0e307ce81be.jpg",
                ThumbnailName = "00000000-0000-0000-0000-000000000009_t.jpg",
                ViewCount = 0,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = true
            },

            // Artwork 10
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000a"),
                Title = "Nơi gặp gỡ tâm hồn",
                Description = "Tượng trưng cho nơi gặp gỡ và kết nối tâm hồn con người",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000002"), //lamlam
                CreatedOn = DateTime.Parse("2023-12-10T13:55:30.456Z"),
                Thumbnail = "https://th.bing.com/th/id/OIG.78MNqZpoa6ReKmvZCHPI?pid=ImgGn",
                ThumbnailName = "00000000-0000-0000-0000-00000000000a_t.jpg",
                ViewCount = 145,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = true
            },

            // Artwork 11
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000b"),
                Title = "Vũ trụ tâm trí",
                Description = "Tượng trưng cho vũ trụ rộng lớn và không gian của tâm trí con người",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000005"), //phuhuynh
                CreatedOn = DateTime.Parse("2023-12-11T08:30:03.678Z"),
                Thumbnail = "https://cdn.pixabay.com/photo/2024/02/03/11/41/ai-generated-8550098_1280.jpg",
                ThumbnailName = "00000000-0000-0000-0000-00000000000b_t.jpg",
                ViewCount = 565,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = true
            },

            // Artwork 12
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000c"),
                Title = "Cáo già",
                Description = "Anh như con cáo Em như cành nho xanh Khi em còn trẻ và đẹp em lại ko dành cho anhhhhh.",
                Privacy = PrivacyEnum.Private,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000002"), //lamlam
                CreatedOn = DateTime.Parse("2023-12-11T02:37:42.345Z"),
                Thumbnail = "https://th.bing.com/th/id/OIG.MxQxUggA0RKmKdTjwAqw?pid=ImgGn",
                ThumbnailName = "00000000-0000-0000-0000-00000000000c_t.jpg",
                ViewCount = 234,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },

            // Artwork 13
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000d"),
                Title = "Làng quê",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000015"), //vuthao
                CreatedOn = DateTime.Parse("2024-03-05T02:37:42.345Z"),
                Thumbnail = "https://i.pinimg.com/originals/7f/b2/ab/7fb2abc0dddd2aa40a86ce6c318b369a.jpg",
                ThumbnailName = "00000000-0000-0000-0000-00000000000d_t.jpg",
                ViewCount = 2034,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },

            // Artwork 14
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000e"),
                Title = "Kỷ nguyên mới",
                Description = "Bộ sưu tập người máy - biểu tượng của tương lai.",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000002"), //lamlam
                CreatedOn = DateTime.Parse("2023-12-13T08:40:28.901Z"),
                Thumbnail = "https://i.pinimg.com/564x/98/08/55/980855dafc9c24ad9b0687adb4b29f0b.jpg",
                ThumbnailName = "00000000-0000-0000-0000-00000000000e_t.jpg",
                ViewCount = 1934,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = true
            },

            // Artwork 15
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000f"),
                Title = "Tuyển tập ảnh cánh cụt cute",
                Description = "Cánh cụt cute",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000005"), //phu
                CreatedOn = DateTime.Parse("2023-12-14T08:20:45.890Z"),
                Thumbnail = "https://th.bing.com/th/id/OIG.MC3PObbEmuJhfsPJ8biQ?pid=ImgGn",
                ThumbnailName = "00000000-0000-0000-0000-00000000000f_t.jpg",
                ViewCount = 0,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Waiting,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = false
            },

            // Artwork 16
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                Title = "Biển cả của tri thức",
                Description = "Tượng trưng cho biển cả tri thức sâu rộng và không ngừng mở rộng",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000002"), //lamlam
                CreatedOn = DateTime.Parse("2023-12-14T10:55:30.456Z"),
                Thumbnail = "https://th.bing.com/th/id/OIG.5gNG99_0Acz4Y8CGOYlg?pid=ImgGn",
                ThumbnailName = "00000000-0000-0000-0000-000000000010_t.jpg",
                ViewCount = 344,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Waiting,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = true
            },

            // Artwork 17
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000011"),
                Title = "Bất Động Sản Thạnh Lộc-Thạnh Xuân",
                Description = "Bán đất ngay ngã tư ga, Hà Huy Giáp vào 1/sẹc, phường Thạnh Xuân,quận 12.\r\n" +
                "-  Khu phân lô đại phú, gần Bánh Mỳ Hà Nội.\r\n" +
                "- Diện tích: 4,10m x 14m\r\n" +
                "- Hướng Tây nam\r\n" +
                "- Giá: 3,5 tỷ còn thương lượng \r\n" +
                "- Đường nhựa xe tải vào tận nơi .\r\n" +
                "-Liên hệ: 0347307890 Nguyễn Hoàng.",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000008"), //nguyenhoang
                CreatedOn = DateTime.Parse("2023-11-10T08:30:03.678Z"),
                Thumbnail = "https://firebasestorage.googleapis.com/v0/b/artworkia-init.appspot.com/o/Artwork%2FThumbnail%2F00000000-0000-0000-0000-000000000011_t.jpg?alt=media&token=aad2c767-acdf-470f-bc35-9c55947cc9af",
                ThumbnailName = "00000000-0000-0000-0000-000000000011_t.jpg",
                ViewCount = 70,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = false
            },

            // Artwork 18
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000012"),
                Title = "Hình ảnh giới thiệu nhân vật & Đếm ngược ra mắt game",
                Description = "Mặc dù nghe có vẻ không đặc biệt, nhưng việc xuất bản visual novel " +
                "được cấp phép đầu tiên tại Việt Nam là một cột mốc quan trọng trong sự phát triển " +
                "của thể loại cực kén người chơi này trong cộng đồng của tôi. Đây là một dự án tuyệt " +
                "vời và tôi rất vinh dự được tham gia vào quá trình thực hiện.",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000019"), //manhkbrady
                CreatedOn = DateTime.Parse("2023-12-12T09:30:03.678Z"),
                Thumbnail = "https://firebasestorage.googleapis.com/v0/b/artworkia-init.appspot.com/o/Artwork%2FImage%2F1.png?alt=media&token=61dea5b6-4462-49b3-b58a-3acc6f9861fc",
                ThumbnailName = "00000000-0000-0000-0000-000000000012_t.jpg",
                ViewCount = 1011,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = false
            },

            // Artwork 19
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000013"),
                Title = "Tranh sơn dầu Đại tướng Võ Nguyên Giáp",
                Description = "Là một họa sĩ được sinh ra và lớn lên khi đất nước đã thống nhất, Mai Duy Minh " +
                "đã cố gắng hết sức có thể trong việc tìm kiếm một câu trả lời cho riêng cá nhân anh về cách mà " +
                "các thế hệ người Việt Nam bền bỉ đi qua mọi gian khổ để bảo vệ độc lập dân tộc. Tất cả những nỗ " +
                "lực tìm kiếm và lắng nghe mọi vang vọng từ quá khứ chiến đấu anh dũng ấy của cha anh đi trước đã " +
                "được hội tụ trong bức tranh “Điện Biên Phủ”..",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000003"), // hoanganh
                CreatedOn = DateTime.Parse("2023-12-13T11:11:03.678Z"),
                Thumbnail = "https://vov2.vov.vn/sites/default/files/styles/large/public/2022-05/vo-nguyen-giap.jpg",
                ThumbnailName = "00000000-0000-0000-0000-000000000013_t.jpg",
                ViewCount = 3045,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                IsAIGenerated = false
            },

            // Artwork 20
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000014"),
                Title = "Không gian vũ trụ",
                Description = "A collection of 173 wallpapers in 4K resolution from THE SIGHTS OF SPACE",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-00000000000b"), //melodysheep
                CreatedOn = DateTime.Parse("2023-12-13T17:11:03.678Z"),
                Thumbnail = "https://f4.bcbits.com/img/a0127149981_16.jpg",
                ThumbnailName = "00000000-0000-0000-0000-000000000014_t.jpg",
                ViewCount = 1725,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },

            // Artwork 21
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000015"),
                Title = "Tương lai loài người",
                Description = "Save the date.  THE HUMAN FUTURE drops on August 15.  Casting my stone in the opposite direction " +
                "of all the pessimists out there.  see you soon :)",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-00000000000b"), //melodysheep
                CreatedOn = DateTime.Parse("2023-12-14T14:11:03.678Z"),
                Thumbnail = "https://yt3.ggpht.com/7CFh0HeCsVSD9UfK1LSc9imflCpKDZT41gNjj_qehvRKt0J9fgmjw2tYjf4oMpbsn0BagczFhx_TTQ=s1600-rw-nd-v1",
                ThumbnailName = "00000000-0000-0000-0000-000000000015_t.jpg",
                ViewCount = 435,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001")
            },

            // Artwork 22
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000016"),
                Title = "THE OUTPOST",
                Description = "How might we discover intelligent life?\r\n\r\nInstead of waiting for radio " +
                "signals or scanning alien atmospheres, we could look to distant asteroid fields for evidence.  " +
                "Advanced civilizations would have numerous reasons to probe asteroids for materials. " +
                "They are rich in resources and easier to manipulate because of their lower gravity.\r\n\r\n" +
                "Spotting chemical anomalies or infrared waste heat around distant asteroid fields could lead us to the ultimate discovery.\r\n\r\n" +
                "Presenting THE OUTPOST: a digital art piece from Life Beyond 3",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-00000000000b"), //melodysheep
                CreatedOn = DateTime.Parse("2023-12-20T08:20:45.890Z"),
                Thumbnail = "https://yt3.ggpht.com/gD2jfNal37ZYf6zkA7CX8CdBQ3WJDP9wFxA-JgrTl0RMGmWh7QcaWG1L2dzBWHUV_qd20ddxXJSMi90=s1024-c-fcrop64=1,0000199affffe666-rw-nd-v1",
                ThumbnailName = "00000000-0000-0000-0000-000000000016_t.jpg",
                ViewCount = 1564,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = false
            },

            // Artwork 23
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000017"),
                Title = "Mọt sách",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000010"), //buiduong
                CreatedOn = DateTime.Parse("2023-12-21T08:30:03.678Z"),
                Thumbnail = "https://firebasestorage.googleapis.com/v0/b/artworkia-init.appspot.com/o/Artwork%2FImage%2F106127234_p0.jpg?alt=media&token=86c5c12c-bee7-4a57-8db1-402e317d5c23",
                ThumbnailName = "00000000-0000-0000-0000-000000000017_t.jpg",
                ViewCount = 725,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            },

            // Artwork 24
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000018"),
                Title = "Cô Gái Đeo Bông Tai Ngọc Trai",
                Description = "Bức tranh hấp dẫn này thường được so sánh với \"Mona Lisa\". " +
                "Bên cạnh sự khác biệt về phong cách, về mặt kỹ thuật \"Girl With a Pearl Earring\" " +
                "thậm chí không phải là một bức chân dung, mà là một \"tronie\" - một từ tiếng Hà Lan để " +
                "chỉ bức tranh của một nhân vật tưởng tượng với các đặc điểm phóng đại.",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000005"), //phu
                CreatedOn = DateTime.Parse("2023-12-22T08:20:45.890Z"),
                Thumbnail = "https://www.kiettacnghethuat.com/wp-content/uploads/Girl-With-a-Pearl-Earring.jpg",
                ThumbnailName = "00000000-0000-0000-0000-000000000018_t.jpg",
                ViewCount = 1204,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = false
            },

            // Artwork 25
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000019"),
                Title = "Đêm đầy sao",
                Description = "Bức tranh \"The Starry Night\" của Vincent van Gogh là một trong những bức tranh nổi tiếng nhất của ông. " +
                "Bức tranh này được vẽ vào năm 1889 và hiện đang được trưng bày tại Bảo tàng Nghệ thuật Modern ở New York.",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000004"), //thong
                CreatedOn = DateTime.Parse("2023-12-23T08:20:45.890Z"),
                Thumbnail = "https://www.kiettacnghethuat.com/wp-content/uploads/The-Starry-Night.jpg",
                ThumbnailName = "00000000-0000-0000-0000-000000000019_t.jpg",
                ViewCount = 1462,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = false
            },

            // Artwork 26
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000001a"),
                Title = "Thiếu nữ bên hoa huệ",
                Description = "Thiếu nữ bên hoa huệ là một tác phẩm tranh sơn dầu do họa sĩ Tô Ngọc Vân sáng tác năm 1943." +
                " Bức tranh mô tả chân dung một thiếu nữ mặc áo dài trắng bên cạnh lọ hoa huệ trắng.",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000004"), //thong
                CreatedOn = DateTime.Parse("2023-12-24T08:20:45.890Z"),
                Thumbnail = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/62/To_Ngoc_Van_thieu_nu_ben_hoa_hue.jpg/800px-To_Ngoc_Van_thieu_nu_ben_hoa_hue.jpg",
                ThumbnailName = "00000000-0000-0000-0000-00000000001a_t.jpg",
                ViewCount = 1874,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = false
            },

            // Artwork 27
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000001b"),
                Title = "Bầu trời",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000011"), //phamha
                CreatedOn = DateTime.Parse("2024-02-25T08:20:45.890Z"),
                Thumbnail = "https://i.pinimg.com/originals/5b/43/fc/5b43fc89abad25aa3359bfe0f27923f9.jpg",
                ThumbnailName = "00000000-0000-0000-0000-00000000001b_t.jpg",
                ViewCount = 1124,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = true
            },

            // Artwork 28
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000001c"),
                Title = "Nhà hát Opera Vienna",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000017"), //vudang
                CreatedOn = DateTime.Parse("2023-12-26T08:20:45.890Z"),
                Thumbnail = "https://upload.wikimedia.org/wikipedia/commons/e/e5/Adolf_Hitler_-_Wien_Oper.jpg",
                ThumbnailName = "00000000-0000-0000-0000-00000000001c_t.jpg",
                ViewCount = 1666,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = false
            },

            // Artwork 29
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000001d"),
                Title = "Hofbräuhaus, Munich",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000017"), //vudang
                CreatedOn = DateTime.Parse("2023-12-27T08:20:45.890Z"),
                Thumbnail = "https://upload.wikimedia.org/wikipedia/commons/9/93/Adolf_Hitler_-_Hofbr%C3%A4uhaus.jpg",
                ThumbnailName = "00000000-0000-0000-0000-00000000001d_t.jpg",
                ViewCount = 1144,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = false
            },

            // Artwork 30
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000001e"),
                Title = "Lâu đài Old Town ở Munich",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000017"), //vudang
                CreatedOn = DateTime.Parse("2023-12-28T08:20:45.890Z"),
                Thumbnail = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/ba/Adolf_Hitler_-_Standesamt_M%C3%BCnchen.jpg/1024px-Adolf_Hitler_-_Standesamt_M%C3%BCnchen.jpg",
                ThumbnailName = "00000000-0000-0000-0000-00000000001e_t.jpg",
                ViewCount = 452,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = false
            },

            // Artwork 31
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000001f"),
                Title = "Khoảng sân trong ở phủ thống sứ cũ tại Munich",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000017"), //vudang
                CreatedOn = DateTime.Parse("2023-12-29T08:20:45.890Z"),
                Thumbnail = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7d/Adolf_Hitler_Der_Alte_Hof.jpg/1024px-Adolf_Hitler_Der_Alte_Hof.jpg",
                ThumbnailName = "00000000-0000-0000-0000-00000000001f_t.jpg",
                ViewCount = 1124,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = false
            },

            // Artwork 32
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000020"),
                Title = "Phục chế ảnh liệt sĩ",
                Description = "Hầu hết các bức ảnh đều chụp cách đây mấy chục năm, nước ảnh ố mờ, bay màu. " +
                "Có nhiều bức ảnh được vẽ lại theo trí nhớ người thân nên để phục chế lại phải mất nhiều thời gian, " +
                "phải cẩn thận chỉnh sửa từng chi tiết",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000003"), //hoanganh
                CreatedOn = DateTime.Parse("2023-12-29T08:20:45.890Z"),
                Thumbnail = "https://cafebiz.cafebizcdn.vn/162123310254002176/2022/4/27/photo-1-1651050727352925703922.jpeg",
                ThumbnailName = "00000000-0000-0000-0000-000000000020_t.jpg",
                ViewCount = 1968,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = false
            },

            // Artwork 33
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000021"),
                Title = "Bạch thiếu nữ",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000010"), //buiduong
                CreatedOn = DateTime.Parse("2024-01-01T08:20:45.890Z"),
                Thumbnail = "https://img3.gelbooru.com//images/db/56/db56fdd06f55f97ab9c884d5539e7e99.jpeg",
                ThumbnailName = "00000000-0000-0000-0000-000000000021_t.jpg",
                ViewCount = 694,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = false
            },

            // Artwork 34
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000022"),
                Title = "Kushima Kamome",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000018"), //minhhuy
                CreatedOn = DateTime.Parse("2024-01-03T08:20:45.890Z"),
                Thumbnail = "https://www.4gamer.net/games/411/G041111/20180227026/SS/002.jpg",
                ThumbnailName = "00000000-0000-0000-0000-000000000022_t.jpg",
                ViewCount = 634,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = false
            },

            // Artwork 35
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000023"),
                Title = "Ao Sorakado",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000018"), //minhhuy
                CreatedOn = DateTime.Parse("2024-01-05T08:20:45.890Z"),
                Thumbnail = "https://www.4gamer.net/games/411/G041111/20180227026/SS/003.jpg",
                ThumbnailName = "00000000-0000-0000-0000-000000000023_t.jpg",
                ViewCount = 234,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = false
            },

            // Artwork 36
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000024"),
                Title = "Tsumugi Wenders",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000018"), //minhhuy
                CreatedOn = DateTime.Parse("2024-01-06T08:20:45.890Z"),
                Thumbnail = "https://www.4gamer.net/games/411/G041111/20180227026/SS/004.jpg",
                ThumbnailName = "00000000-0000-0000-0000-000000000024_t.jpg",
                ViewCount = 345,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = false
            },

            // Artwork 37
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000025"),
                Title = "Shiroha Naruse",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000018"), //minhhuy
                CreatedOn = DateTime.Parse("2024-01-09T08:20:45.890Z"),
                Thumbnail = "https://www.4gamer.net/games/411/G041111/20180227026/SS/005.jpg",
                ThumbnailName = "00000000-0000-0000-0000-000000000025_t.jpg",
                ViewCount = 370,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = false
            },

            // Artwork 38
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000026"),
                Title = "Bánh vòng",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000010"), //buiduong
                CreatedOn = DateTime.Parse("2024-01-09T08:20:45.890Z"),
                Thumbnail = "https://firebasestorage.googleapis.com/v0/b/artworkia-init.appspot.com/o/Artwork%2FImage%2F110149990_p0.jpg?alt=media&token=4668e7eb-f2ba-47f2-add9-96bb40acd22e",
                ThumbnailName = "00000000-0000-0000-0000-000000000026_t.jpg",
                ViewCount = 814,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = false
            },

            // Artwork 39
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000027"),
                Title = "Rặng hoa anh đào",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000011"), //phamha
                CreatedOn = DateTime.Parse("2023-12-06T08:20:45.890Z"),
                Thumbnail = "https://firebasestorage.googleapis.com/v0/b/artworkia-init.appspot.com/o/Artwork%2FImage%2Fwp3790967.jpg?alt=media&token=203e265e-427f-4aa3-a664-2a39479bf61e",
                ThumbnailName = "00000000-0000-0000-0000-000000000027_t.jpg",
                ViewCount = 518,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = false
            },

            // Artwork 40
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000028"),
                Title = "Một lời hứa cuốn trong làn gió",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000018"), //minhhuy
                CreatedOn = DateTime.Parse("2023-12-07T08:20:45.890Z"),
                Thumbnail = "https://i0.wp.com/images1.wikia.nocookie.net/mydata/vi/images/b/be/Anemoi_main_viet.png?ssl=1",
                ThumbnailName = "00000000-0000-0000-0000-000000000028_t.jpg",
                ViewCount = 327,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = false
            },

            // Artwork 41
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000029"),
                Title = "Trời xanh",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000011"), //phamha
                CreatedOn = DateTime.Parse("2023-12-08T08:20:45.890Z"),
                Thumbnail = "https://i.pinimg.com/originals/57/da/1d/57da1db47a30fe8d608da6d3b25dfc08.jpg",
                ThumbnailName = "00000000-0000-0000-0000-000000000029_t.jpg",
                ViewCount = 1489,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = true
            },

            // Artwork 42
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000002a"),
                Title = "Thung lũng xanh",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000011"), //phamha
                CreatedOn = DateTime.Parse("2023-12-09T08:20:45.890Z"),
                Thumbnail = "https://i.pinimg.com/originals/56/a4/58/56a45858390e3726d2848d3efa696d6e.jpg",
                ThumbnailName = "00000000-0000-0000-0000-00000000002a_t.jpg",
                ViewCount = 1034,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = true
            },

            // Artwork 43
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000002b"),
                Title = "Đồng lúa",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000011"), //phamha
                CreatedOn = DateTime.Parse("2023-12-10T08:20:45.890Z"),
                Thumbnail = "https://i.pinimg.com/originals/51/59/8e/51598eb8f62f868e95556fc316873f05.jpg",
                ThumbnailName = "00000000-0000-0000-0000-00000000002b_t.jpg",
                ViewCount = 1345,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = true
            },

            // Artwork 44
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000002c"),
                Title = "Hệ mặt trời",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000005"), //phuhuynh
                CreatedOn = DateTime.Parse("2023-12-11T08:20:45.890Z"),
                Thumbnail = "https://i.pinimg.com/originals/33/b5/96/33b596908f23b4b2c3f3e64f032e51b6.png",
                ThumbnailName = "00000000-0000-0000-0000-00000000002c_t.jpg",
                ViewCount = 145,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                IsAIGenerated = false
            },

            // Artwork 45
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000002d"),
                Title = "Lập phương rubik",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000005"), //phuhuynh
                CreatedOn = DateTime.Parse("2023-12-12T08:20:45.890Z"),
                Thumbnail = "https://i.pinimg.com/originals/02/f2/28/02f2283c2a59d3e7d7287a95fae5c2f5.jpg",
                ThumbnailName = "00000000-0000-0000-0000-00000000002d_t.jpg",
                ViewCount = 595,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                IsAIGenerated = false
            },

            // Artwork 46
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000002e"),
                Title = "Ngọn hải đăng",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000015"), //vuthao
                CreatedOn = DateTime.Parse("2023-12-13T08:20:45.890Z"),
                Thumbnail = "https://i.pinimg.com/originals/85/3e/bf/853ebff19a985aae38e65c8111e59ef8.png",
                ThumbnailName = "00000000-0000-0000-0000-00000000002e_t.jpg",
                ViewCount = 320,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                IsAIGenerated = false
            },

            // Artwork 47
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000002f"),
                Title = "Mèo đen",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000015"), //vuthao
                CreatedOn = DateTime.Parse("2024-01-20T08:20:45.890Z"),
                Thumbnail = "https://i.pinimg.com/originals/d2/54/3a/d2543a52f10afe5696d68346d212d34e.jpg",
                ThumbnailName = "00000000-0000-0000-0000-00000000002f_t.jpg",
                ViewCount = 1020,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                IsAIGenerated = true
            },

            // Artwork 48
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000030"),
                Title = "Chủ tịch Hồ Chí Minh",
                Description = "\"The Nation of Vietnam is one, the People of Vietnam are one\", said by Hồ Chí Minh. The Vietnamese people's aspiration for independence, freedom, unity and happiness in the 20th century helped that nation win all wars of the aggressor ... ",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000003"), //hoanganh
                CreatedOn = DateTime.Parse("2023-12-15T08:20:45.890Z"),
                Thumbnail = "https://i.pinimg.com/564x/41/e1/51/41e151608d67227d7265e7026faa48c1.jpg",
                ThumbnailName = "00000000-0000-0000-0000-000000000030_t.jpg",
                ViewCount = 1645,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                IsAIGenerated = false
            },

            // Artwork 49
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000031"),
                Title = "Bác bảo thắng là thắng",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000003"), //hoanganh
                CreatedOn = DateTime.Parse("2024-01-27T08:20:45.890Z"),
                Thumbnail = "https://i.pinimg.com/originals/3d/e0/d0/3de0d0e553793ec3ee39cf1e7404e96e.jpg",
                ThumbnailName = "00000000-0000-0000-0000-000000000031_t.jpg",
                ViewCount = 1999,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                IsAIGenerated = false
            },

            // Artwork 50
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000032"),
                Title = "4575",
                Description = null,
                Privacy = PrivacyEnum.Private,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000003"), //hoanganh
                CreatedOn = DateTime.Parse("2023-12-17T08:20:45.890Z"),
                Thumbnail = "https://i.pinimg.com/564x/49/40/56/494056c9b3f314dad493bac63265f296.jpg",
                ThumbnailName = "00000000-0000-0000-0000-000000000032_t.jpg",
                ViewCount = 0,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                IsAIGenerated = false
            },

            // Artwork 51
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000033"),
                Title = "Travo Apps - UI KIT for Travel Flight and Hotel",
                Description = "Travo Apps - Bộ công cụ giao diện người dùng cho Đặt vé máy bay và Khách sạn du lịch",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000012"), //doantrang
                CreatedOn = DateTime.Parse("2023-12-20T08:20:45.890Z"),
                Thumbnail = "https://i.pinimg.com/564x/1e/6f/85/1e6f852b5f25cd76c962c5affc55fcef.jpg",
                ThumbnailName = "00000000-0000-0000-0000-000000000033_t.jpg",
                ViewCount = 1499,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = false
            },

            // Artwork 52
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000034"),
                Title = "MyNovel Mobile App UI",
                Description = "Thiết kế giao diện người dùng (UI) cho ứng dụng di động MyNovel, " +
                "một nền tảng đọc truyện trực tuyến Thái Lan cho mọi loại tiểu thuyết tuyệt vời, " +
                "sách điện tử và truyện tranh. Nó cập nhật nội dung theo dạng kịch bản hàng ngày và nhiều hơn nữa.",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000012"), //doantrang
                CreatedOn = DateTime.Parse("2024-01-06T08:20:45.890Z"),
                Thumbnail = "https://i.pinimg.com/564x/18/57/2c/18572cd01747f618fbf837b7b4459437.jpg",
                ThumbnailName = "00000000-0000-0000-0000-000000000034_t.jpg",
                ViewCount = 1734,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = false
            },

            // Artwork 53
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000035"),
                Title = "Tinder mobile app redesign",
                Description = "tái thiết kế ứng dụng gặp gỡ người mới phổ biến nhất thế giới. " +
                "Tinder là một ứng dụng cho phép người dùng vuốt sang trái hoặc phải để thích " +
                "hoặc không thích các hồ sơ khác dựa trên ảnh, tiểu sử ngắn và sở thích chung. " +
                "Khi hai người dùng \"match\" (bật cặp) với nhau thì họ có thể nhắn tin.",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000012"), //doantrang
                CreatedOn = DateTime.Parse("2023-12-22T08:20:45.890Z"),
                Thumbnail = "https://mir-s3-cdn-cf.behance.net/project_modules/1400/8cf9b997945993.5ee39b4959c00.jpg",
                ThumbnailName = "00000000-0000-0000-0000-000000000035_t.jpg",
                ViewCount = 1499,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = false
            },

            // Artwork 54
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000036"),
                Title = "Góc phố Nhật Bản",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-00000000000d"), //levantan
                CreatedOn = DateTime.Parse("2023-12-23T08:20:45.890Z"),
                Thumbnail = "https://i.pinimg.com/564x/66/b8/59/66b859b3949564d6e3f63cfcf2b6ef93.jpg",
                ThumbnailName = "00000000-0000-0000-0000-000000000036_t.jpg",
                ViewCount = 1599,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = false
            },

            // Artwork 55
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000037"),
                Title = "Sapa mộng mer",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-00000000000d"), //levantan
                CreatedOn = DateTime.Parse("2023-12-24T08:20:45.890Z"),
                Thumbnail = "https://i.pinimg.com/564x/c8/a1/5f/c8a15ff316f7c74b789d21651b0891f8.jpg",
                ThumbnailName = "00000000-0000-0000-0000-000000000037_t.jpg",
                ViewCount = 539,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                IsAIGenerated = false
            },

            // Artwork 56
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000038"),
                Title = "Bãi tắm đảo Bé huyện đảo Lý Sơn",
                Description = "Bãi tắm đảo Bé Lý Sơn nơi checkin sống ảo đúng chất",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-00000000000d"), //levantan
                CreatedOn = DateTime.Parse("2024-01-13T08:20:45.890Z"),
                Thumbnail = "https://i.pinimg.com/564x/61/9f/65/619f65d4700c390f1e47ab42237ce450.jpg",
                ThumbnailName = "00000000-0000-0000-0000-000000000038_t.jpg",
                ViewCount = 999,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                IsAIGenerated = false
            },

            // Artwork 57
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000039"),
                Title = "Côn đảo, Việt Nam",
                Description = "Looking for the best island to visit in Vietnam? This is the ultimate guide to " +
                "visiting the Con Dao Islands. Find out what makes Con Dao is a must-visit!",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-00000000000d"), //levantan
                CreatedOn = DateTime.Parse("2024-01-06T08:20:45.890Z"),
                Thumbnail = "https://i.pinimg.com/564x/ff/78/08/ff7808be1114e07dc1065245bd8bfc7f.jpg",
                ThumbnailName = "00000000-0000-0000-0000-000000000039_t.jpg",
                ViewCount = 919,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                IsAIGenerated = false
            },

            // Artwork 58
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000003a"),
                Title = "Landmark 81",
                Description = "Review Landmark 81 - tòa nhà cao và sang trọng bậc nhất Việt Nam",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-00000000000d"), //levantan
                CreatedOn = DateTime.Parse("2023-12-27T08:20:45.890Z"),
                Thumbnail = "https://ik.imagekit.io/tvlk/blog/2024/01/landmark-81-3-841x1024.jpg?tr=dpr-2,w-675",
                ThumbnailName = "00000000-0000-0000-0000-00000000003a_t.jpg",
                ViewCount = 231,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                IsAIGenerated = false
            },

            // Artwork 59
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000003b"),
                Title = "Bãi biển Mỹ Khê",
                Description = "Bãi biển Mỹ Khê Đà Nẵng - Địa điểm du lịch nổi tiếng tại Việt Nam",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-00000000000d"), //levantan
                CreatedOn = DateTime.Parse("2023-12-28T08:20:45.890Z"),
                Thumbnail = "https://i.pinimg.com/564x/78/37/52/783752783478df60a339c4389697fe88.jpg",
                ThumbnailName = "00000000-0000-0000-0000-00000000003b_t.jpg",
                ViewCount = 999,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                IsAIGenerated = false
            },

            // Artwork 60
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000003c"),
                Title = "Hố Đen",
                Description = "Lỗ đen hay hố đen, là một vùng không–thời gian nơi trường hấp dẫn " +
                "mạnh đến mức không có gì—không hạt vật chất hay cả bức xạ điện từ như ánh sáng c" +
                "ó thể thoát khỏi nó.",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-00000000000b"), //melodysheep
                CreatedOn = DateTime.Parse("2023-12-29T08:20:45.890Z"),
                Thumbnail = "https://i.pinimg.com/564x/d2/d1/17/d2d1176fbec2f7573eb1023c518e1105.jpg",
                ThumbnailName = "00000000-0000-0000-0000-00000000003c_t.jpg",
                ViewCount = 1899,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                IsAIGenerated = false
            },

            // Artwork 61
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000003d"),
                Title = "Thiết kế nhân vật 3D",
                Description = "Adorable 3D Character by AI. Follow for more!",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000013"), //tranduc
                CreatedOn = DateTime.Parse("2023-12-30T08:20:45.890Z"),
                Thumbnail = "https://i.pinimg.com/564x/d4/37/b3/d437b301b2408a5772d9be18d2dcbdee.jpg",
                ThumbnailName = "00000000-0000-0000-0000-00000000003d_t.jpg",
                ViewCount = 1562,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
            },

            // Artwork 62
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000003e"),
                Title = "Geralt xứ Rivia",
                Description = "Geralt of Rivia (tiếng Ba Lan: Geralt z Rivii) là một witcher và là " +
                "nhân vật chính trong loạt tiểu thuyết The Witcher của nhà văn Andrzej Sapkowski",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000013"), //tranduc
                CreatedOn = DateTime.Parse("2023-12-31T08:20:45.890Z"),
                Thumbnail = "https://i.pinimg.com/736x/a9/5c/0f/a95c0f2de7561a34fbccc7af102b1af5.jpg",
                ThumbnailName = "00000000-0000-0000-0000-00000000003e_t.jpg",
                ViewCount = 1262,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                IsAIGenerated = false
            },

            // Artwork 63
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000003f"),
                Title = "The Dragonborn Daedric Armor",
                Description = "Ai Representation of the Dragonborn from Elderscrolls Skyrim wearing daedric Armor",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000013"), //tranduc
                CreatedOn = DateTime.Parse("2024-01-05T08:20:45.890Z"),
                Thumbnail = "https://i.pinimg.com/736x/68/18/e5/6818e59fe9c1b059d679cbf35ab122c9.jpg",
                ThumbnailName = "00000000-0000-0000-0000-00000000003e_t.jpg",
                ViewCount = 835,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                IsAIGenerated = true
            },

            // Artwork 64
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000040"),
                Title = "Giáp thiên thần",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000013"), //tranduc
                CreatedOn = DateTime.Parse("2024-01-07T08:20:45.890Z"),
                Thumbnail = "https://i.pinimg.com/originals/2b/ef/bd/2befbd3f91aa23db55eea433151c7992.jpg",
                ThumbnailName = "00000000-0000-0000-0000-000000000040_t.jpg",
                ViewCount = 999,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                IsAIGenerated = true
            },

            // Artwork 65
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000041"),
                Title = "Hoàng hôn",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000011"), //phamha
                CreatedOn = DateTime.Parse("2024-02-08T08:20:45.890Z"),
                Thumbnail = "https://i.pinimg.com/564x/00/ed/65/00ed65b58c95c4d694fc95598af0a885.jpg",
                ThumbnailName = "00000000-0000-0000-0000-000000000041_t.jpg",
                ViewCount = 452,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                IsAIGenerated = false
            },

            // Artwork 66
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000042"),
                Title = "Ngẫu hứng phát họa",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-00000000000f"), //hoangtuan
                CreatedOn = DateTime.Parse("2024-02-09T08:20:45.890Z"),
                Thumbnail = "https://i.pinimg.com/564x/25/dc/d6/25dcd6a770856b37bdd3fd69551d626d.jpg",
                ThumbnailName = "00000000-0000-0000-0000-000000000042_t.jpg",
                ViewCount = 1052,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                IsAIGenerated = false
            },

            // Artwork 67
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000043"),
                Title = "Cô gái vàng",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-00000000000f"), //hoangtuan
                CreatedOn = DateTime.Parse("2024-02-10T08:20:45.890Z"),
                Thumbnail = "https://i.pinimg.com/736x/d1/f7/e1/d1f7e116c4dc23c61f9523cea80ee909.jpg",
                ThumbnailName = "00000000-0000-0000-0000-000000000043_t.jpg",
                ViewCount = 599,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                IsAIGenerated = false
            },

            // Artwork 68
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000044"),
                Title = "Chàng trai",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-00000000000f"), //hoangtuan
                CreatedOn = DateTime.Parse("2024-01-15T08:20:45.890Z"),
                Thumbnail = "https://i.pinimg.com/736x/cf/7e/ff/cf7eff78ddcdeeed6b6074bd441eb721.jpg",
                ThumbnailName = "00000000-0000-0000-0000-000000000044_t.jpg",
                ViewCount = 654,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                IsAIGenerated = false
            },

            // Artwork 69
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000045"),
                Title = "Red dead Redemption",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000013"), //tranduc
                CreatedOn = DateTime.Parse("2024-01-20T08:20:45.890Z"),
                Thumbnail = "https://i.pinimg.com/564x/47/3e/f9/473ef931430e161d83f2aa5c8844d56a.jpg",
                ThumbnailName = "00000000-0000-0000-0000-000000000045_t.jpg",
                ViewCount = 654,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                IsAIGenerated = false
            },

            // Artwork 70
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000046"),
                Title = "Pixel Art GUI / UI Kit + 151 icons!",
                Description = "Universal / Fantasy Pixel Art GUI Kit for your new project, featuring Windowed and Fullscreen views to fit all your needs!",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-00000000000a"), //phamthanh
                CreatedOn = DateTime.Parse("2024-01-25T08:20:45.890Z"),
                Thumbnail = "https://assetstorev1-prd-cdn.unity3d.com/key-image/da31eccd-b255-4898-bec2-2e1b1eb39092.webp",
                ThumbnailName = "00000000-0000-0000-0000-000000000046_t.jpg",
                ViewCount = 1444,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = false
            },

            // Artwork 71
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000047"),
                Title = "Visual Studio Code Redesign",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-00000000000c"), //truongthu
                CreatedOn = DateTime.Parse("2023-12-30T08:20:45.890Z"),
                Thumbnail = "https://storage.googleapis.com/artworkia-storage-public/Artwork/VSCode.png",
                ThumbnailName = "00000000-0000-0000-0000-000000000047_t.jpg",
                ViewCount = 928,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = false
            },

            // Artwork 72
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000048"),
                Title = "Fluid Background",
                Description = "Mẫu thiết kế ứng dụng di động đẹp mắt cho các dự án của bạn",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-00000000000c"), //truongthu
                CreatedOn = DateTime.Parse("2024-02-05T08:20:45.890Z"),
                Thumbnail = "https://i.pinimg.com/564x/2c/5d/80/2c5d80e9c0240a65d6397f9991352855.jpg",
                ThumbnailName = "00000000-0000-0000-0000-000000000048_t.jpg",
                ViewCount = 109,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = false
            },

            // Artwork 73
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000049"),
                Title = "Moona hoshinova",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-00000000000c"), //truongthu
                CreatedOn = DateTime.Parse("2023-12-05T08:20:45.890Z"),
                Thumbnail = "https://i.pinimg.com/564x/71/01/da/7101da0949d85dfbf7c24200f1fcbdfd.jpg",
                ThumbnailName = "00000000-0000-0000-0000-000000000048_t.jpg",
                ViewCount = 376,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = false
            },

            // Artwork 74
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000004a"),
                Title = "Artificial Intelligence Robot Cyberpunk High Tech Sci-fi Poster",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000014"), //nguyenhieu
                CreatedOn = DateTime.Parse("2024-01-10T08:20:45.890Z"),
                Thumbnail = "https://i.pinimg.com/564x/61/29/93/61299356ecd161b0ac86c94869960084.jpg",
                ThumbnailName = "00000000-0000-0000-0000-00000000004a_t.jpg",
                ViewCount = 567,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = true
            },

            // Artwork 75
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000004b"),
                Title = "Intelligent Robots Metaverse Cyberpunk Cat Robot Poster",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000014"), //nguyenhieu
                CreatedOn = DateTime.Parse("2024-02-01T08:20:45.890Z"),
                Thumbnail = "https://i.pinimg.com/564x/a3/ef/9c/a3ef9c4379f92f56607f1f685e7835ce.jpg",
                ThumbnailName = "00000000-0000-0000-0000-00000000004b_t.jpg",
                ViewCount = 833,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = true
            },

            // Artwork 76
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000004c"),
                Title = "Cyberpunk Robot Poster",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000014"), //nguyenhieu
                CreatedOn = DateTime.Parse("2024-01-15T08:20:45.890Z"),
                Thumbnail = "https://i.pinimg.com/564x/19/d5/fb/19d5fbfe0a970510bfe47aa148e0b71e.jpg",
                ThumbnailName = "00000000-0000-0000-0000-00000000004c_t.jpg",
                ViewCount = 1133,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = true
            },

            // Artwork 77
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000004d"),
                Title = "IntelliJ Redesign",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-00000000000c"), //truongthu
                CreatedOn = DateTime.Parse("2024-01-07T08:20:45.890Z"),
                Thumbnail = "https://storage.googleapis.com/artworkia-storage-public/Artwork/IntellijLogo.png",
                ThumbnailName = "00000000-0000-0000-0000-00000000004d_t.jpg",
                ViewCount = 1228,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = false
            },

            // Artwork 78
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000004e"),
                Title = "Phục chế ảnh nữ liệt sĩ",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000003"), //hoanganh
                CreatedOn = DateTime.Parse("2024-03-02T08:20:45.890Z"),
                Thumbnail = "https://storage.googleapis.com/artworkia-storage-public/Artwork/phucche1.png",
                ThumbnailName = "00000000-0000-0000-0000-00000000004e_t.jpg",
                ViewCount = 433,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = false
            },

            // Artwork 79
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000004f"),
                Title = "Chính chủ bán BT Phong Lan 04-34, 343.8m2 nhà thô, sông sau nhà rộng miên man, sân trước nhà 1000m2",
                Description = @"Chính chủ bán BT Phong Lan 04 - 34. DT 343.8m² nhà thô, sông sau nhà rộng miên man. Sân trước nhà 1000m.
                                Hướng Tây Nam.
                                Giá 220tr/m².",
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000008"), //nguyenhoang
                CreatedOn = DateTime.Parse("2024-01-25T08:30:03.678Z"),
                Thumbnail = "https://file4.batdongsan.com.vn/resize/1275x717/2024/03/07/20240307144316-1cca_wm.jpg",
                ThumbnailName = "00000000-0000-0000-0000-00000000004f_t.jpg",
                ViewCount = 110,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = false
            },

            // Artwork 80
            new Artwork()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000050"),
                Title = "Mèo con",
                Description = null,
                Privacy = PrivacyEnum.Public,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000015"), //vuthao
                CreatedOn = DateTime.Parse("2024-03-05T08:30:03.678Z"),
                Thumbnail = "https://i.pinimg.com/originals/86/15/50/8615509334c99ba0c11a9feac151a79e.jpg",
                ThumbnailName = "00000000-0000-0000-0000-000000000050_t.jpg",
                ViewCount = 1160,
                LikeCount = 0,
                CommentCount = 0,
                State = StateEnum.Accepted,
                LicenseTypeId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                IsAIGenerated = false
            }

        );
        #endregion
    }
}
