using Domain.Entitites;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable(nameof(Account));

        builder.Property(x => x.Id).HasDefaultValueSql("newid()");
        builder.Property(x => x.CreatedOn).HasDefaultValueSql("getutcdate()");
        builder.Property(x => x.LastModificatedOn).HasDefaultValueSql("getutcdate()");

        builder.HasIndex(x => x.Username).IsUnique();
        builder.HasIndex(x => x.Email).IsUnique();

        #region relationships
        builder.HasOne(x => x.Wallet).WithOne(w => w.Account).HasForeignKey<Wallet>(x => x.AccountId);
        builder.HasMany(x => x.ChatBoxes_1).WithOne(c => c.Account_1).HasForeignKey(c => c.AccountId_1);
        builder.HasMany(x => x.ChatBoxes_2).WithOne(c => c.Account_2).HasForeignKey(c => c.AccountId_2);
        builder.HasMany(x => x.Messages).WithOne(m => m.Account).HasForeignKey(m => m.CreatedBy);
        builder.HasMany(x => x.Proposals).WithOne(p => p.Account).HasForeignKey(p => p.CreatedBy);
        builder.HasMany(x => x.Reviews).WithOne(r => r.Account).HasForeignKey(r => r.CreatedBy);
        builder.HasMany(x => x.Services).WithOne(s => s.Account).HasForeignKey(s => s.CreatedBy);
        builder.HasMany(x => x.Requests).WithOne(r => r.Account).HasForeignKey(r => r.CreatedBy);
        builder.HasMany(x => x.UserTokens).WithOne(t => t.User).HasForeignKey(t => t.UserId);
        builder.HasMany(x => x.Artworks).WithOne(a => a.Account).HasForeignKey(a => a.CreatedBy);
        builder.HasMany(x => x.Notifications).WithOne(n => n.Account).HasForeignKey(n => n.SentToAccount);
        builder.HasMany(x => x.TransactionHistories).WithOne(t => t.Account).HasForeignKey(t => t.CreatedBy);
        builder.HasMany(x => x.TransactionHistoriesReceivedCoins).WithOne(t => t.ToAccount).HasForeignKey(t => t.ToAccountId);
        builder.HasMany(x => x.Likes).WithOne(l => l.Account).HasForeignKey(l => l.AccountId);
        builder.HasMany(x => x.Comments).WithOne(c => c.Account).HasForeignKey(c => c.CreatedBy);
        builder.HasMany(x => x.Followings).WithOne(f => f.Followed).HasForeignKey(f => f.FollowedId);
        builder.HasMany(x => x.Followers).WithOne(f => f.Following).HasForeignKey(f => f.FollowingId);
        builder.HasMany(x => x.WalletHistories).WithOne(h => h.Account).HasForeignKey(h => h.CreatedBy);
        builder.HasMany(x => x.Milestones).WithOne(m => m.CreatedAccount).HasForeignKey(m => m.CreatedBy);
        builder.HasMany(x => x.ArtistCertificates).WithOne(a => a.Account).HasForeignKey(a => a.AccountId);
        #endregion

        #region init data
        builder.HasData(
            // account 1
            new Account()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Username = "user",
                Password = "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", //Matkhausieumanh123
                Fullname = "Nguyễn Văn A",
                Email = "user@example.com",
                Bio = "Tôi là người dùng, không có gì đặc biệt",
                Avatar = "https://i.pinimg.com/564x/ed/de/aa/eddeaaf250c19489e25bd0a2dd3e7756.jpg",
                Birthdate = DateTime.Parse("2000-10-14"),
                Role = RoleEnum.CommonUser,
                CreatedOn = DateTime.Parse("2023-09-14T05:37:42.345Z")
            },

            // account 2
            new Account()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                Username = "lamlam",
                Password = "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", //Matkhausieumanh123
                Fullname = "Trúc Lam Võ",
                Email = "lamlam@example.com",
                Bio = "Tôi là Trúc Lam Võ, tôi là một nghệ sĩ đầy tài năng",
                Avatar = "https://i.pinimg.com/564x/be/85/2f/be852fd4ad1cb76b83ce962f618895bd.jpg",
                Birthdate = DateTime.Parse("2002-10-4"),
                Role = RoleEnum.CommonUser,
                CreatedOn = DateTime.Parse("2023-09-15T10:15:47.890Z"),
                VerifiedOn = DateTime.Parse("2023-10-15T10:15:47.890Z")
            },

            // account 3
            new Account()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                Username = "hoanganh",
                Password = "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", //Matkhausieumanh123
                Fullname = "Đặng Hoàng Anh",
                Email = "anhdhse160846@fpt.edu.vn",
                Bio = "Tôi là Đặng Hoàng Anh, tôi là một nghệ sĩ đầy tài năng",
                Avatar = "https://i.pinimg.com/564x/db/02/67/db02679d039a230d9a37caec679d1b3b.jpg",
                Birthdate = DateTime.Parse("2002-10-4"),
                Role = RoleEnum.CommonUser,
                CreatedOn = DateTime.Parse("2023-09-21T12:20:47.890Z")
            },

            // account 4
            new Account()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                Username = "thong",
                Password = "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", //Matkhausieumanh123
                Fullname = "Nguyễn Trung Thông",
                Email = "thong@example.com",
                Bio = "Tôi là Nguyễn Trung Thông, tôi là một nghệ sĩ đầy tài năng",
                Avatar = "https://i.pinimg.com/564x/6c/a3/4b/6ca34beddfbd279418c915d2258d698b.jpg",
                Birthdate = DateTime.Parse("2002-1-4"),
                Role = RoleEnum.CommonUser,
                CreatedOn = DateTime.Parse("2023-09-27T12:23:47.890Z")
            },

            // account 5
            new Account()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                Username = "phuhuynh",
                Password = "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", //Matkhausieumanh123
                Fullname = "Huỳnh Vạn Phú",
                Email = "phuhuynh923@gmail.com",
                Bio = "Tôi là Huỳnh Vạn Phú, tôi là một nghệ sĩ đầy tài năng",
                Avatar = "https://i.pinimg.com/736x/81/3c/57/813c57fcb969d58fac1672594da05532.jpg",
                Birthdate = DateTime.Parse("2002-5-6"),
                Role = RoleEnum.CommonUser,
                CreatedOn = DateTime.Parse("2023-09-30T03:21:47.890Z"),
                VerifiedOn = DateTime.Parse("2023-10-16T10:15:47.890Z"),
            },

            // account 6: moderators
            new Account()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                Username = "mod",
                Password = "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", //Matkhausieumanh123
                Fullname = "Kiểm soát viên",
                Email = "mod@example.com",
                Bio = "Tôi là kiểm soát viên hệ thống",
                Avatar = "https://i.pinimg.com/564x/7d/cd/61/7dcd61988b0add83b5ba9a656512593e.jpg",
                Birthdate = DateTime.Parse("2001-10-4"),
                Role = RoleEnum.Moderator,
                CreatedOn = DateTime.Parse("2023-09-14T05:37:42.345Z")
            },

            // account 7: admin
            new Account()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                Username = "admin",
                Password = "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", //Matkhausieumanh123
                Fullname = "Quản trị viên hệ thống",
                Email = "admin@example.com",
                Bio = "Tôi là quản trị viên hệ thống",
                Avatar = "https://i.pinimg.com/564x/0e/4b/7a/0e4b7aef4834bfc646775d8fd3705825.jpg",
                Birthdate = DateTime.Parse("2000-10-4"),
                Role = RoleEnum.Admin,
                CreatedOn = DateTime.Parse("2023-09-02T03:21:47.890Z")
            },

            // account 8: spam
            new Account()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                Username = "nguyenhoang",
                Password = "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", //Matkhausieumanh123
                Fullname = "Nguyễn Hoàng",
                Email = "trieuhan282@gmail.com",
                Bio = "Tôi là một thiết kế UI/UX tài năng, đã có nhiều dự án thành công với " +
                "các công ty lớn, cũng là người sáng lập một công ty thiết kế đồ họa.",
                Avatar = "https://i.pinimg.com/564x/79/ba/4f/79ba4f6c73168efb975a2d43cc4272a3.jpg",
                Birthdate = DateTime.Parse("2002-10-12"),
                Role = RoleEnum.CommonUser,
                CreatedOn = DateTime.Parse("2023-09-02T03:21:47.890Z")
            },

            // account 9
            new Account()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                Username = "tranminh",
                Password = "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", //Matkhausieumanh123
                Fullname = "Trần Minh",
                Email = "tranminh@example.com",
                Bio = "Tôi là một nhà thiết kế web có kinh nghiệm, đã tham gia vào nhiều " +
                "dự án phức tạp và mang lại sự sáng tạo đặc biệt.",
                Avatar = "https://i.pinimg.com/originals/4b/59/75/4b5975b01115a778a6d8016bf4d0ddc7.jpg",
                Birthdate = DateTime.Parse("2002-10-24"),
                Role = RoleEnum.CommonUser,
                CreatedOn = DateTime.Parse("2023-09-02T03:21:47.890Z")
            },

            // account 10
            new Account()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000a"),
                Username = "phamthanh",
                Password = "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", //Matkhausieumanh123
                Fullname = "Phạm Thanh",
                Email = "phamthanh@example.com",
                Bio = "Tôi là một thiết kế 2D và 3D, đã tạo ra nhiều tác phẩm ấn tượng " +
                "trong lĩnh vực phim hoạt hình và trò chơi điện tử.",
                Avatar = "https://i.pinimg.com/564x/62/4a/2f/624a2fda3e0da8e55b4ea60b0949affa.jpg",
                Birthdate = DateTime.Parse("2000-10-5"),
                Role = RoleEnum.CommonUser,
                CreatedOn = DateTime.Parse("2023-09-02T03:21:47.890Z")
            },

            // account 11
            new Account()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000b"),
                Username = "melodysheep",
                Password = "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", //Matkhausieumanh123
                Fullname = "Ngô Thanh Tú",
                Email = "ngothanhtu@example.com",
                Bio = "Tôi là một họa sĩ chuyên về tranh kỹ thuật số, đã có nhiều triển lãm cá nhân " +
                "và tham gia vào dự án nghệ thuật trên toàn thế giới.",
                Avatar = "https://yt3.googleusercontent.com/ytc/AIdro_klHVaP6_ZcnT8VyPFedRHgJOPOym_tLSxoFCL0KJSZL1k=s900-c-k-c0x00ffffff-no-rj",
                Birthdate = DateTime.Parse("2001-1-1"),
                Role = RoleEnum.CommonUser,
                CreatedOn = DateTime.Parse("2023-09-02T03:21:47.890Z")
            },

            // account 12
            new Account()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000c"),
                Username = "truongthu",
                Password = "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", //Matkhausieumanh123
                Fullname = "Trương Thu",
                Email = "truongthu@example.com",
                Bio = "Tôi là một thiết kế đồ họa sáng tạo, đã tham gia vào nhiều dự án quảng cáo và " +
                "branding cho các thương hiệu lớn.",
                Avatar = "https://i.pinimg.com/564x/ad/c2/95/adc2953d7533371d1cdb95303d70babe.jpg",
                Birthdate = DateTime.Parse("2002-7-2"),
                Role = RoleEnum.CommonUser,
                CreatedOn = DateTime.Parse("2023-09-02T03:21:47.890Z")
            },

            // account 13
            new Account()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000d"),
                Username = "levantan",
                Password = "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", //Matkhausieumanh123
                Fullname = "Lê Văn Tân",
                Email = "levan@example.com",
                Bio = "Một nhiếp ảnh gia tự xưng, thích du lịch, chụp nhiều bức ảnh độc đáo về văn " +
                "hóa và cảnh đẹp Việt Nam và thế giới.",
                Avatar = "https://i.pinimg.com/564x/9c/28/19/9c2819e41426236d748392299cd20246.jpg",
                Birthdate = DateTime.Parse("2002-8-4"),
                Role = RoleEnum.CommonUser,
                CreatedOn = DateTime.Parse("2023-09-02T03:21:47.890Z")
            },

            // account 14
            new Account()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000e"),
                Username = "nguyenminh",
                Password = "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", //Matkhausieumanh123
                Fullname = "Nguyễn Minh",
                Email = "nguyenminh@example.com",
                Bio = "Tôi là một nhà thiết kế đồ họa sáng tạo, đã tham gia vào nhiều dự án " +
                "quảng cáo, in ấn và branding.",
                Avatar = "https://i.pinimg.com/564x/ae/ca/78/aeca78f2453767acdbd8398c4f310025.jpg",
                Birthdate = DateTime.Parse("2002-3-6"),
                Role = RoleEnum.CommonUser,
                CreatedOn = DateTime.Parse("2023-09-02T03:21:47.890Z")
            },

            // account 15
            new Account()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000f"),
                Username = "hoangtuan",
                Password = "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", //Matkhausieumanh123
                Fullname = "Hoàng Tuấn",
                Email = "hoangtuan@example.com",
                Bio = "Tôi là một họa sĩ có gu thẩm mỹ độc đáo, tạo ra những tác phẩm nghệ " +
                "thuật đa dạng và phong phú.",
                Avatar = "https://i.pinimg.com/564x/2a/1c/40/2a1c400fa2d814b78ed36fd21a5316f5.jpg",
                Birthdate = DateTime.Parse("2002-12-24"),
                Role = RoleEnum.CommonUser,
                CreatedOn = DateTime.Parse("2023-09-02T03:21:47.890Z")
            },

            // account 16
            new Account()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                Username = "buiduong",
                Password = "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", //Matkhausieumanh123
                Fullname = "Bùi Dương",
                Email = "buiduong@example.com",
                Bio = "Sinh viên đại học mỹ thuật HCM, thích vẽ tranh phong cách Nhật Bản, nếu thích hãy theo dõi mình nhé.",
                Avatar = "https://i.pinimg.com/736x/58/29/bd/5829bdfa438410a86cf9b180c077939c.jpg",
                Birthdate = DateTime.Parse("1999-10-14"),
                Role = RoleEnum.CommonUser,
                CreatedOn = DateTime.Parse("2023-10-02T03:21:47.890Z")
            },

            // account 17
            new Account()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000011"),
                Username = "phamha",
                Password = "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", //Matkhausieumanh123
                Fullname = "Phạm Hà",
                Email = "phamha@example.com",
                Bio = "Tôi là một họa sĩ chuyên về tranh nghệ thuật, tạo ra những tác phẩm tươi " +
                "sáng và lôi cuốn.",
                Avatar = "https://i.pinimg.com/564x/17/f4/97/17f497af6f6b67bd9dbcb93c04dced89.jpg",
                Birthdate = DateTime.Parse("2003-10-4"),
                Role = RoleEnum.CommonUser,
                CreatedOn = DateTime.Parse("2023-10-02T03:21:47.890Z")
            },

            // account 18
            new Account()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000012"),
                Username = "doantrang",
                Password = "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", //Matkhausieumanh123
                Fullname = "Đoàn Trang",
                Email = "doantrang@example.com",
                Bio = "Tôi là một thiết kế UI/UX đam mê và sáng tạo, đã tham gia vào nhiều " +
                "dự án thành công trong lĩnh vực công nghệ.",
                Avatar = "https://i.pinimg.com/564x/ba/74/40/ba744092fe6e7222d44a5e89cf483d6d.jpg",
                Birthdate = DateTime.Parse("2002-10-7"),
                Role = RoleEnum.CommonUser,
                CreatedOn = DateTime.Parse("2023-10-02T03:21:47.890Z")
            },

            // account 19
            new Account()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000013"),
                Username = "tranduc",
                Password = "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", //Matkhausieumanh123
                Fullname = "Trần Đức",
                Email = "tranduc@example.com",
                Bio = "Tôi là một nghệ sĩ 3D tài năng, đã tham gia vào việc tạo ra các mô " +
                "hình 3D ấn tượng cho phim và trò chơi.",
                Avatar = "https://i.pinimg.com/564x/de/09/b1/de09b1839700e9988e605df833a5450a.jpg",
                Birthdate = DateTime.Parse("2002-10-3"),
                Role = RoleEnum.CommonUser,
                CreatedOn = DateTime.Parse("2023-10-02T03:21:47.890Z")
            },

            // account 20
            new Account()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000014"),
                Username = "nguyenhieu",
                Password = "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", //Matkhausieumanh123
                Fullname = "Nguyễn Hiếu",
                Email = "nguyenhieu@example.com",
                Bio = "Tôi là một nhà thiết kế đồ họa có tầm nhìn sáng tạo, đã đạt được " +
                "nhiều giải thưởng trong ngành.",
                Avatar = "https://i.pinimg.com/564x/1e/a0/59/1ea05967bf1e5e2054aaecd109a3c662.jpg",
                Birthdate = DateTime.Parse("2003-12-4"),
                Role = RoleEnum.CommonUser,
                CreatedOn = DateTime.Parse("2023-10-02T03:21:47.890Z")
            },

            // account 21
            new Account()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000015"),
                Username = "vuthao",
                Password = "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", //Matkhausieumanh123
                Fullname = "Vũ Thảo",
                Email = "vuthao@example.com",
                Bio = "Tôi là một họa sĩ trẻ có sức sáng tạo và tinh thần nghệ thuật cao, " +
                "đã tham gia vào nhiều dự án nghệ thuật và thiết kế.",
                Avatar = "https://i.pinimg.com/564x/7b/78/42/7b784268d117a6d57a8d9a83c7eaa977.jpg",
                Birthdate = DateTime.Parse("2002-6-20"),
                Role = RoleEnum.CommonUser,
                CreatedOn = DateTime.Parse("2023-10-02T03:21:47.890Z")
            },

            // account 22
            new Account()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000016"),
                Username = "nguyentien",
                Password = "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", //Matkhausieumanh123
                Fullname = "Nguyễn Tiến",
                Email = "nguyentien@example.com",
                Bio = "Tôi là một nhà thiết kế đồ họa có kinh nghiệm, đã tham gia vào việc " +
                "phát triển các ứng dụng di động và giao diện người dùng.",
                Avatar = "https://i.pinimg.com/originals/30/33/0b/30330b5e8e0f772f0edaa310294703a2.jpg",
                Birthdate = DateTime.Parse("2004-11-4"),
                Role = RoleEnum.CommonUser,
                CreatedOn = DateTime.Parse("2023-10-02T03:21:47.890Z")
            },

            // account 23
            new Account()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000017"),
                Username = "vudang",
                Password = "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", //Matkhausieumanh123
                Fullname = "Vũ Đăng",
                Email = "vudang@example.com",
                Bio = "Tôi là một họa sĩ người Áo có ước mơ vào trường Mỹ Thuật",
                Avatar = "https://i.pinimg.com/564x/f9/7f/c4/f97fc4762b0ca1c3ba76c3b2e6c5041c.jpg",
                Birthdate = DateTime.Parse("2002-4-4"),
                Role = RoleEnum.CommonUser,
                CreatedOn = DateTime.Parse("2023-10-02T03:21:47.890Z")
            },

            // account 24
            new Account()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000018"),
                Username = "minhhuy",
                Password = "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", //Matkhausieumanh123
                Fullname = "Trần Nguyễn Minh Huy",
                Email = "minhhuy@example.com",
                Bio = "Living and working in Japan / big fan of Key (Kagikko - 鍵っ子). A guy of social, cultural, and natural.",
                Avatar = "https://i.pinimg.com/564x/af/65/88/af6588a1cb6be3602190e4c223b79318.jpg",
            },

            // account 25
            new Account()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000019"),
                Username = "manhkbrady",
                Password = "A5tzNn90k1cgMCIWicwomDz/Wb1/BAWIDIVelEKhM6lHvuwh", //Matkhausieumanh123
                Fullname = "Nguyễn Đức Mạnh",
                Email = "manhkbrady@example.com",
                Bio = "Nole của công ty NaiNovel - công ty game đầu hàng Việt Nam",
                Avatar = "https://i.pinimg.com/564x/d9/03/0a/d9030a5696d2507a1dfb38a686ac93c2.jpg",
            }
        );
        #endregion
    }
}
