using Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;
public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.ToTable(nameof(Image));
        builder.Property(x => x.Id).HasDefaultValueSql("newid()");
        builder.HasOne(x => x.Artwork).WithMany(w => w.Images).HasForeignKey(x => x.ArtworkId);


        #region init data
        builder.HasData
        (

            // artwork 1
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Location = "https://i.pinimg.com/736x/8d/f7/be/8df7be5e052e97b824e6b0f783309161.jpg",
                ImageName = "00000000-0000-0000-0000-000000000001_i0.jpg",
                Order = 0
            },
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Location = "https://i.pinimg.com/564x/18/f5/01/18f50109029ade270f0759724a0c19f1.jpg",
                ImageName = "00000000-0000-0000-0000-000000000001_i1.jpg",
                Order = 1
            },

            // artwork 2
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                Location = "https://th.bing.com/th/id/OIG.mMOt1xWJJCHsRoPJXtHQ?pid=ImgGn",
                ImageName = "00000000-0000-0000-0000-000000000002_i0.jpg",
                Order = 0
            },

            // artwork 3
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                Location = "https://vov2-media.solidtech.vn/sites/default/files/styles/large/public/2022-05/dien-bien-phu_-_bia.jpg",
                ImageName = "00000000-0000-0000-0000-000000000003_i0.jpg",
                Order = 0
            },

            // artwork 4
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                Location = "https://bizweb.dktcdn.net/100/378/894/files/5.jpg?v=1627978886825",
                ImageName = "00000000-0000-0000-0000-000000000004_i0.jpg",
                Order = 0
            },
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                Location = "https://bizweb.dktcdn.net/100/378/894/files/4.jpg?v=1627978828806",
                ImageName = "00000000-0000-0000-0000-000000000004_i1.jpg",
                Order = 1
            },

            // artwork 5
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000a"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                Location = "https://th.bing.com/th/id/OIG.LTaVFacabNQc22SAk1r1?pid=ImgGn",
                ImageName = "00000000-0000-0000-0000-000000000005_i0.jpg",
                Order = 0
            },

            // artwork 6
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000c"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                Location = "https://th.bing.com/th/id/OIG.AoM9akz.gT4RMZ9R6DOh?pid=ImgGn",
                ImageName = "00000000-0000-0000-0000-000000000006_i0.jpg",
                Order = 0
            },

            // artwork 7
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000e"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                Location = "https://th.bing.com/th/id/OIG..gPcXaRan.FdnEjYCvT3?pid=ImgGn",
                ImageName = "00000000-0000-0000-0000-000000000007_i0.jpg",
                Order = 0
            },

            // artwork 8
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                Location = "https://static-company.waka.vn/img.media/tin%20t%E1%BB%A9c/38540.jpg",
                ImageName = "00000000-0000-0000-0000-000000000008_i0.jpg",
                Order = 0
            },

            // artwork 9
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000012"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                Location = "https://th.bing.com/th/id/OIG.Uz66_wn15hsKPirpv6Pb?pid=ImgGn",
                ImageName = "00000000-0000-0000-0000-000000000009_i0.jpg",
                Order = 0
            },

            // artwork 10
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000014"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000a"),
                Location = "https://th.bing.com/th/id/OIG.78MNqZpoa6ReKmvZCHPI?pid=ImgGn",
                ImageName = "00000000-0000-0000-0000-00000000000a_i0.jpg",
                Order = 0
            },

            // artwork 11
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000016"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000b"),
                Location = "https://cdn.pixabay.com/photo/2024/02/03/11/41/ai-generated-8550098_1280.jpg",
                ImageName = "00000000-0000-0000-0000-00000000000b_i0.jpg",
                Order = 0
            },

            // artwork 12
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000001d"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000c"),
                Location = "https://th.bing.com/th/id/OIG.MxQxUggA0RKmKdTjwAqw?pid=ImgGn",
                ImageName = "00000000-0000-0000-0000-00000000000c_i0.jpg",
                Order = 0
            },

            // artwork 13
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000001f"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000d"),
                Location = "https://i.pinimg.com/originals/7f/b2/ab/7fb2abc0dddd2aa40a86ce6c318b369a.jpg",
                ImageName = "00000000-0000-0000-0000-00000000000d_i0.jpg",
                Order = 0
            },

            // artwork 14
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000020"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000e"),
                Location = "https://i.pinimg.com/564x/98/08/55/980855dafc9c24ad9b0687adb4b29f0b.jpg",
                ImageName = "00000000-0000-0000-0000-00000000000e_i0.jpg",
                Order = 0
            },
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000021"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000e"),
                Location = "https://i.pinimg.com/originals/b5/7e/14/b57e14aa401d41db2072d1b0ccfbde2b.jpg",
                ImageName = "00000000-0000-0000-0000-00000000000e_i1.jpg",
                Order = 1
            },
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000022"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000e"),
                Location = "https://i.pinimg.com/originals/d5/5e/e1/d55ee127c8dc1c7f9d94edc0ec596758.jpg",
                ImageName = "00000000-0000-0000-0000-00000000000e_i2.jpg",
                Order = 2
            },

            // artwork 15
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000024"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000f"),
                Location = "https://i.pinimg.com/originals/db/93/a1/db93a131d59201ed997d606ea33c4933.jpg",
                ImageName = "00000000-0000-0000-0000-00000000000f_i0.jpg",
                Order = 0
            },
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000025"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000000f"),
                Location = "https://i.pinimg.com/originals/b5/d4/7e/b5d47e1cf4555983a8017e59409b4d4a.jpg",
                ImageName = "00000000-0000-0000-0000-00000000000f_i1.jpg",
                Order = 1
            },

            // artwork 16
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000027"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                Location = "https://th.bing.com/th/id/OIG.5gNG99_0Acz4Y8CGOYlg?pid=ImgGn",
                ImageName = "00000000-0000-0000-0000-000000000010_i0.jpg",
                Order = 0
            },

            // artwork 17 (bds)
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000029"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000011"),
                Location = "https://firebasestorage.googleapis.com/v0/b/artworkia-init.appspot.com/o/Artwork%2FImage%2F00000000-0000-0000-0000-000000000011_i0.jpg?alt=media&token=e9a93f6f-4bcf-4517-824a-dacd402bdcec",
                ImageName = "00000000-0000-0000-0000-000000000011_i0.jpg",
                Order = 0
            },
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000002a"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000011"),
                Location = "https://firebasestorage.googleapis.com/v0/b/artworkia-init.appspot.com/o/Artwork%2FImage%2F00000000-0000-0000-0000-000000000011_i1.jpg?alt=media&token=3442240e-37f5-42f9-be58-5c359ebcde5c",
                ImageName = "00000000-0000-0000-0000-000000000011_i1.jpg",
                Order = 1
            },
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000002b"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000011"),
                Location = "https://firebasestorage.googleapis.com/v0/b/artworkia-init.appspot.com/o/Artwork%2FImage%2F00000000-0000-0000-0000-000000000011_i2.jpg?alt=media&token=4d6ad124-294d-4059-b21d-a37cfb3d0c6a",
                ImageName = "00000000-0000-0000-0000-000000000011_i2.jpg",
                Order = 2
            },

            // artwork 18
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000002d"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000012"),
                Location = "https://firebasestorage.googleapis.com/v0/b/artworkia-init.appspot.com/o/Artwork%2FImage%2F1.png?alt=media&token=61dea5b6-4462-49b3-b58a-3acc6f9861fc",
                ImageName = "00000000-0000-0000-0000-000000000012_i0.png",
                Order = 0
            },
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000002e"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000012"),
                Location = "https://firebasestorage.googleapis.com/v0/b/artworkia-init.appspot.com/o/Artwork%2FImage%2F2.png?alt=media&token=10e991c2-11ac-469b-9933-3b223fe17f5b",
                ImageName = "00000000-0000-0000-0000-000000000012_i1.png",
                Order = 1
            },
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000002f"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000012"),
                Location = "https://firebasestorage.googleapis.com/v0/b/artworkia-init.appspot.com/o/Artwork%2FImage%2F3.png?alt=media&token=13f47f61-2372-4b87-b8f4-806c5ef956ee",
                ImageName = "00000000-0000-0000-0000-000000000012_i2.png",
                Order = 2
            },
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000030"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000012"),
                Location = "https://firebasestorage.googleapis.com/v0/b/artworkia-init.appspot.com/o/Artwork%2FImage%2F4.png?alt=media&token=53a9b98e-1875-48d6-a880-6b935081afe5",
                ImageName = "00000000-0000-0000-0000-000000000012_i3.png",
                Order = 3
            },
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000031"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000012"),
                Location = "https://firebasestorage.googleapis.com/v0/b/artworkia-init.appspot.com/o/Artwork%2FImage%2F5.png?alt=media&token=4d9137a2-5836-4cee-8e36-c1b87f9236c6",
                ImageName = "00000000-0000-0000-0000-000000000012_i4.png",
                Order = 4
            },
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000032"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000012"),
                Location = "https://firebasestorage.googleapis.com/v0/b/artworkia-init.appspot.com/o/Artwork%2FImage%2F6.png?alt=media&token=3b05c46f-6ed0-42d6-aa84-1ef96a880f99",
                ImageName = "00000000-0000-0000-0000-000000000012_i4.png",
                Order = 5
            },

            // artwork 19
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000034"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000013"),
                Location = "https://vov2.vov.vn/sites/default/files/styles/large/public/2022-05/vo-nguyen-giap.jpg",
                ImageName = "00000000-0000-0000-0000-000000000013_i0.png",
                Order = 0
            },

            // artwork 20
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000035"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000014"),
                Location = "https://public-files.gumroad.com/ydorwqbnmsl1yueuyhnr90ceo7sq",
                ImageName = "00000000-0000-0000-0000-000000000014_i0.png",
                Order = 0
            },
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000036"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000014"),
                Location = "https://public-files.gumroad.com/1tmubanfhbhfs7fi4mwbw1s5nyc0",
                ImageName = "00000000-0000-0000-0000-000000000014_i1.png",
                Order = 1
            },
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000037"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000014"),
                Location = "https://public-files.gumroad.com/7ml9boypxaoisvvaio5eajau57ts",
                ImageName = "00000000-0000-0000-0000-000000000014_i2.png",
                Order = 2
            },
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000038"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000014"),
                Location = "https://public-files.gumroad.com/t1tx8r7s2jhhedtr2zqyc3r9edc6",
                ImageName = "00000000-0000-0000-0000-000000000014_i3.png",
                Order = 3
            },

            // artwork 21
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000003a"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000015"),
                Location = "https://yt3.ggpht.com/7CFh0HeCsVSD9UfK1LSc9imflCpKDZT41gNjj_qehvRKt0J9fgmjw2tYjf4oMpbsn0BagczFhx_TTQ=s1600-rw-nd-v1",
                ImageName = "00000000-0000-0000-0000-000000000015_i0.png",
                Order = 0
            },

            // artwork 22
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000003c"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000016"),
                Location = "https://yt3.ggpht.com/gD2jfNal37ZYf6zkA7CX8CdBQ3WJDP9wFxA-JgrTl0RMGmWh7QcaWG1L2dzBWHUV_qd20ddxXJSMi90=s1024-c-fcrop64=1,0000199affffe666-rw-nd-v1",
                ImageName = "00000000-0000-0000-0000-000000000016_i0.png",
                Order = 0
            },
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000003d"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000016"),
                Location = "https://yt3.ggpht.com/soXomuCz4k_xBxpb_p6K7nAht6BlCfzh8p3PfTPU2dt3iGX25Ga-W-Noiow1GPr5ii9seYFsCci-=s1024-c-fcrop64=1,0000199affffe666-rw-nd-v1",
                ImageName = "00000000-0000-0000-0000-000000000016_i1.png",
                Order = 1
            },
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000003e"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000016"),
                Location = "https://yt3.ggpht.com/An5n_i_kbv45-ijvkogN9T98slicRZYEsxmyallrHtsILGoNgwdOs0_93C94duiwdClNGWFtoG-f=s1024-c-fcrop64=1,0000199affffe666-rw-nd-v1",
                ImageName = "00000000-0000-0000-0000-000000000016_i2.png",
                Order = 2
            },
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000003f"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000016"),
                Location = "https://yt3.ggpht.com/togZdny_IHrLbAa73ZmBXhUlt-Br4Rdrjpq2iySYPgt7S3bATckHvipkjv6gsDmjkJtXnM8DazDgPg=s1024-c-fcrop64=1,0000199affffe666-rw-nd-v1",
                ImageName = "00000000-0000-0000-0000-000000000016_i3.png",
                Order = 3
            },

            // artwork 23
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000040"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000017"),
                Location = "https://firebasestorage.googleapis.com/v0/b/artworkia-init.appspot.com/o/Artwork%2FImage%2F106127234_p0.jpg?alt=media&token=86c5c12c-bee7-4a57-8db1-402e317d5c23",
                ImageName = "00000000-0000-0000-0000-000000000017_i0.png",
                Order = 0
            },

            // artwork 24
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000042"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000018"),
                Location = "https://www.kiettacnghethuat.com/wp-content/uploads/Girl-With-a-Pearl-Earring.jpg",
                ImageName = "00000000-0000-0000-0000-000000000018_i0.png",
                Order = 0
            },

            // artwork 25
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000044"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000019"),
                Location = "https://www.kiettacnghethuat.com/wp-content/uploads/The-Starry-Night.jpg",
                ImageName = "00000000-0000-0000-0000-000000000019_i0.jpg",
                Order = 0
            },

            // artwork 26
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000048"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001a"),
                Location = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/62/To_Ngoc_Van_thieu_nu_ben_hoa_hue.jpg/800px-To_Ngoc_Van_thieu_nu_ben_hoa_hue.jpg",
                ImageName = "00000000-0000-0000-0000-00000000001a_i0.jpg",
                Order = 0
            },

            // artwork 27
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000004a"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001b"),
                Location = "https://i.pinimg.com/originals/5b/43/fc/5b43fc89abad25aa3359bfe0f27923f9.jpg",
                ImageName = "00000000-0000-0000-0000-00000000001b_i0.jpg",
                Order = 0
            },

            // artwork 28
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000004d"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001c"),
                Location = "https://upload.wikimedia.org/wikipedia/commons/e/e5/Adolf_Hitler_-_Wien_Oper.jpg",
                ImageName = "00000000-0000-0000-0000-00000000001c_i0.jpg",
                Order = 0
            },

            // artwork 29
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000004e"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001d"),
                Location = "https://upload.wikimedia.org/wikipedia/commons/9/93/Adolf_Hitler_-_Hofbr%C3%A4uhaus.jpg",
                ImageName = "00000000-0000-0000-0000-00000000001d_i0.jpg",
                Order = 0
            },

            // artwork 30
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000004f"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001e"),
                Location = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/ba/Adolf_Hitler_-_Standesamt_M%C3%BCnchen.jpg/1024px-Adolf_Hitler_-_Standesamt_M%C3%BCnchen.jpg",
                ImageName = "00000000-0000-0000-0000-00000000001e_i0.jpg",
                Order = 0
            },

            // artwork 31
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000050"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000001f"),
                Location = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7d/Adolf_Hitler_Der_Alte_Hof.jpg/1024px-Adolf_Hitler_Der_Alte_Hof.jpg",
                ImageName = "00000000-0000-0000-0000-00000000001f_i0.jpg",
                Order = 0
            },

            // artwork 32
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000051"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000020"),
                Location = "https://cafebiz.cafebizcdn.vn/162123310254002176/2022/4/27/photo-1-1651050727352925703922.jpeg",
                ImageName = "00000000-0000-0000-0000-000000000020_i0.png",
                Order = 0
            },
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000052"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000020"),
                Location = "https://cafebiz.cafebizcdn.vn/162123310254002176/2022/4/27/photo-1-16510507247002018514994.jpeg",
                ImageName = "00000000-0000-0000-0000-000000000020_i1.png",
                Order = 1
            },

            // artwork 33
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000053"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000021"),
                Location = "https://img3.gelbooru.com//images/db/56/db56fdd06f55f97ab9c884d5539e7e99.jpeg",
                ImageName = "00000000-0000-0000-0000-000000000021_i0.jpg",
                Order = 0
            },

            // artwork 34
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000054"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000022"),
                Location = "https://www.4gamer.net/games/411/G041111/20180227026/SS/002.jpg",
                ImageName = "00000000-0000-0000-0000-000000000022_i0.jpg",
                Order = 0
            },
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000055"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000022"),
                Location = "https://firebasestorage.googleapis.com/v0/b/artworkia-init.appspot.com/o/Artwork%2FImage%2Ftenor.gif?alt=media&token=270214ac-b289-4608-ba5c-9ea68e7ae97a",
                ImageName = "00000000-0000-0000-0000-000000000022_i1.jpg",
                Order = 1
            },


            // artwork 35
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000056"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000023"),
                Location = "https://www.4gamer.net/games/411/G041111/20180227026/SS/003.jpg",
                ImageName = "00000000-0000-0000-0000-000000000023_i0.jpg",
                Order = 0
            },

            // artwork 36
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000057"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000024"),
                Location = "https://www.4gamer.net/games/411/G041111/20180227026/SS/004.jpg",
                ImageName = "00000000-0000-0000-0000-000000000024_i0.jpg",
                Order = 0
            },

            // artwork 37
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000058"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000025"),
                Location = "https://www.4gamer.net/games/411/G041111/20180227026/SS/005.jpg",
                ImageName = "00000000-0000-0000-0000-000000000025_i0.jpg",
                Order = 0
            },

            // artwork 38
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000005a"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000026"),
                Location = "https://firebasestorage.googleapis.com/v0/b/artworkia-init.appspot.com/o/Artwork%2FImage%2F110149990_p0.jpg?alt=media&token=4668e7eb-f2ba-47f2-add9-96bb40acd22e",
                ImageName = "00000000-0000-0000-0000-000000000026_i0.jpg",
                Order = 0
            },

            // artwork 39
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000005c"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000027"),
                Location = "https://firebasestorage.googleapis.com/v0/b/artworkia-init.appspot.com/o/Artwork%2FImage%2Fwp3790967.jpg?alt=media&token=203e265e-427f-4aa3-a664-2a39479bf61e",
                ImageName = "00000000-0000-0000-0000-000000000027_i0.jpg",
                Order = 0
            },

            // artwork 40
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000060"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000028"),
                Location = "https://i0.wp.com/images1.wikia.nocookie.net/mydata/vi/images/b/be/Anemoi_main_viet.png?ssl=1",
                ImageName = "00000000-0000-0000-0000-000000000028_i0.jpg",
                Order = 0
            },

            // artwork 41
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000061"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000029"),
                Location = "https://i.pinimg.com/originals/57/da/1d/57da1db47a30fe8d608da6d3b25dfc08.jpg",
                ImageName = "00000000-0000-0000-0000-000000000029_i0.jpg",
                Order = 0
            },

            // artwork 42
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000062"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000002a"),
                Location = "https://i.pinimg.com/originals/56/a4/58/56a45858390e3726d2848d3efa696d6e.jpg",
                ImageName = "00000000-0000-0000-0000-00000000002a_i0.jpg",
                Order = 0
            },

            // artwork 43
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000063"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000002b"),
                Location = "https://i.pinimg.com/originals/51/59/8e/51598eb8f62f868e95556fc316873f05.jpg",
                ImageName = "00000000-0000-0000-0000-00000000002b_i0.jpg",
                Order = 0,
            },

            // artwork 44
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000064"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000002c"),
                Location = "https://i.pinimg.com/originals/33/b5/96/33b596908f23b4b2c3f3e64f032e51b6.png",
                ImageName = "00000000-0000-0000-0000-00000000002c_i0.jpg",
                Order = 0
            },

            // artwork 45
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000065"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000002d"),
                Location = "https://i.pinimg.com/originals/02/f2/28/02f2283c2a59d3e7d7287a95fae5c2f5.jpg",
                ImageName = "00000000-0000-0000-0000-00000000002d_i0.jpg",
                Order = 0
            },

            // artwork 46
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000066"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000002e"),
                Location = "https://i.pinimg.com/originals/85/3e/bf/853ebff19a985aae38e65c8111e59ef8.png",
                ImageName = "00000000-0000-0000-0000-00000000002e_i0.jpg",
                Order = 0
            },

            // artwork 47
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000067"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000002f"),
                Location = "https://i.pinimg.com/originals/d2/54/3a/d2543a52f10afe5696d68346d212d34e.jpg",
                ImageName = "00000000-0000-0000-0000-00000000002f_i0.jpg",
                Order = 0
            },

            // artwork 48
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000068"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000030"),
                Location = "https://i.pinimg.com/564x/41/e1/51/41e151608d67227d7265e7026faa48c1.jpg",
                ImageName = "00000000-0000-0000-0000-000000000030_i0.jpg",
                Order = 0
            },

            // artwork 49
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000069"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000031"),
                Location = "https://i.pinimg.com/originals/3d/e0/d0/3de0d0e553793ec3ee39cf1e7404e96e.jpg",
                ImageName = "00000000-0000-0000-0000-000000000031_i0.jpg",
                Order = 0
            },

            // artwork 50
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000006a"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000032"),
                Location = "https://i.pinimg.com/564x/49/40/56/494056c9b3f314dad493bac63265f296.jpg",
                ImageName = "00000000-0000-0000-0000-000000000032_i0.jpg",
                Order = 0
            },

            // artwork 51
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000006b"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000033"),
                Location = "https://images.ui8.net/uploads/frame-12_1580960103603.png",
                ImageName = "00000000-0000-0000-0000-000000000033_i0.jpg",
                Order = 0
            },
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000006c"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000033"),
                Location = "https://images.ui8.net/uploads/frame-12_1580960103638.png",
                ImageName = "00000000-0000-0000-0000-000000000033_i1.jpg",
                Order = 1
            },
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000006d"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000033"),
                Location = "https://images.ui8.net/uploads/frame-12_1580960103637.png",
                ImageName = "00000000-0000-0000-0000-000000000033_i2.jpg",
                Order = 2
            },
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000006e"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000033"),
                Location = "https://images.ui8.net/uploads/frame-12_1580960103566.png",
                ImageName = "00000000-0000-0000-0000-000000000033_i3.jpg",
                Order = 3
            },
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000006f"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000033"),
                Location = "https://images.ui8.net/uploads/frame-12_1580960103592.png",
                ImageName = "00000000-0000-0000-0000-000000000033_i3.jpg",
                Order = 4
            },
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000070"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000033"),
                Location = "https://images.ui8.net/uploads/frame-12_1580960103587.png",
                ImageName = "00000000-0000-0000-0000-000000000033_i3.jpg",
                Order = 5
            },

            // artwork 52
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000071"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000034"),
                Location = "https://www.sinthaistudio.com/wp-content/uploads/2021/08/MN1-uai-516x568.jpg",
                ImageName = "00000000-0000-0000-0000-000000000034_i0.jpg",
                Order = 0
            },
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000072"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000034"),
                Location = "https://www.sinthaistudio.com/wp-content/uploads/2021/08/MN2.jpg",
                ImageName = "00000000-0000-0000-0000-000000000034_i1.jpg",
                Order = 1
            },
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000073"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000034"),
                Location = "https://www.sinthaistudio.com/wp-content/uploads/2021/08/MN3.jpg",
                ImageName = "00000000-0000-0000-0000-000000000034_i2.jpg",
                Order = 2
            },
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000074"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000035"),
                Location = "https://www.sinthaistudio.com/wp-content/uploads/2021/08/MN4.jpg",
                ImageName = "00000000-0000-0000-0000-000000000034_i3.jpg",
                Order = 3
            },

            // artwork 53
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000075"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000035"),
                Location = "https://mir-s3-cdn-cf.behance.net/project_modules/1400/8cf9b997945993.5ee39b4959c00.jpg",
                ImageName = "00000000-0000-0000-0000-000000000035_i0.jpg",
                Order = 0
            },

            // artwork 54
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000076"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000036"),
                Location = "https://i.pinimg.com/564x/66/b8/59/66b859b3949564d6e3f63cfcf2b6ef93.jpg",
                ImageName = "00000000-0000-0000-0000-000000000036_i0.jpg",
                Order = 0
            },

            // artwork 55
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000077"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000037"),
                Location = "https://i.pinimg.com/564x/c8/a1/5f/c8a15ff316f7c74b789d21651b0891f8.jpg",
                ImageName = "00000000-0000-0000-0000-000000000037_i0.jpg",
                Order = 0
            },

            // artwork 56
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000078"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000038"),
                Location = "https://i.pinimg.com/564x/61/9f/65/619f65d4700c390f1e47ab42237ce450.jpg",
                ImageName = "00000000-0000-0000-0000-000000000038_i0.jpg",
                Order = 0
            },

            // artwork 57
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000079"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000039"),
                Location = "https://i.pinimg.com/564x/ff/78/08/ff7808be1114e07dc1065245bd8bfc7f.jpg",
                ImageName = "00000000-0000-0000-0000-000000000039_i0.jpg",
                Order = 0
            },

            // artwork 58   
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000007a"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003a"),
                Location = "https://ik.imagekit.io/tvlk/blog/2024/01/landmark-81-3-841x1024.jpg?tr=dpr-2,w-675",
                ImageName = "00000000-0000-0000-0000-00000000003a_i0.jpg",
                Order = 0
            },

            // artwork 59
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000007b"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003b"),
                Location = "https://i.pinimg.com/564x/78/37/52/783752783478df60a339c4389697fe88.jpg",
                ImageName = "00000000-0000-0000-0000-00000000003b_i0.jpg",
                Order = 0
            },

            // artwork 60
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000007c"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003c"),
                Location = "https://i.pinimg.com/564x/d2/d1/17/d2d1176fbec2f7573eb1023c518e1105.jpg",
                ImageName = "00000000-0000-0000-0000-00000000003c_i0.jpg",
                Order = 0
            },

            // artwork 61
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000007d"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003d"),
                Location = "https://i.pinimg.com/564x/d4/37/b3/d437b301b2408a5772d9be18d2dcbdee.jpg",
                ImageName = "00000000-0000-0000-0000-00000000003d_i0.jpg",
                Order = 0
            },

            // artwork 62
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000080"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003e"),
                Location = "https://i.pinimg.com/736x/a9/5c/0f/a95c0f2de7561a34fbccc7af102b1af5.jpg",
                ImageName = "00000000-0000-0000-0000-00000000003e_i0.jpg",
                Order = 0
            },

            // artwork 63
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000081"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000003f"),
                Location = "https://i.pinimg.com/736x/68/18/e5/6818e59fe9c1b059d679cbf35ab122c9.jpg",
                ImageName = "00000000-0000-0000-0000-00000000003f_i0.jpg",
                Order = 0
            },

            // artwork 64
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000082"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000040"),
                Location = "https://i.pinimg.com/originals/2b/ef/bd/2befbd3f91aa23db55eea433151c7992.jpg",
                ImageName = "00000000-0000-0000-0000-000000000040_i0.jpg",
                Order = 0
            },

            // artwork 65
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000083"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000041"),
                Location = "https://i.pinimg.com/564x/00/ed/65/00ed65b58c95c4d694fc95598af0a885.jpg",
                ImageName = "00000000-0000-0000-0000-000000000041_i0.jpg",
                Order = 0
            },

            // artwork 66
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000084"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000042"),
                Location = "https://i.pinimg.com/564x/25/dc/d6/25dcd6a770856b37bdd3fd69551d626d.jpg",
                ImageName = "00000000-0000-0000-0000-000000000042_i0.jpg",
                Order = 0
            },

            // artwork 67
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000085"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000043"),
                Location = "https://i.pinimg.com/736x/d1/f7/e1/d1f7e116c4dc23c61f9523cea80ee909.jpg",
                ImageName = "00000000-0000-0000-0000-000000000043_i0.jpg",
                Order = 0
            },

            // artwork 68
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000086"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000044"),
                Location = "https://i.pinimg.com/736x/cf/7e/ff/cf7eff78ddcdeeed6b6074bd441eb721.jpg",
                ImageName = "00000000-0000-0000-0000-000000000044_i0.jpg",
                Order = 0
            },

            // artwork 69
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000087"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000045"),
                Location = "https://i.pinimg.com/564x/47/3e/f9/473ef931430e161d83f2aa5c8844d56a.jpg",
                ImageName = "00000000-0000-0000-0000-000000000045_i0.jpg",
                Order = 0
            },

            // artwork 70
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000088"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000046"),
                Location = "https://assetstorev1-prd-cdn.unity3d.com/key-image/da31eccd-b255-4898-bec2-2e1b1eb39092.webp",
                ImageName = "00000000-0000-0000-0000-000000000046_i0.jpg",
                Order = 0
            },
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000089"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000046"),
                Location = "https://assetstorev1-prd-cdn.unity3d.com/package-screenshot/f62a52ea-5ee7-406c-abcf-5b91970f964a.webp",
                ImageName = "00000000-0000-0000-0000-000000000046_i1.jpg",
                Order = 1
            },
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000008a"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000046"),
                Location = "https://assetstorev1-prd-cdn.unity3d.com/package-screenshot/f19fb81d-cfeb-41d5-a092-9b61d6530ba8.webp",
                ImageName = "00000000-0000-0000-0000-000000000046_i2.jpg",
                Order = 2
            },
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000008b"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000046"),
                Location = "https://assetstorev1-prd-cdn.unity3d.com/package-screenshot/1cb97919-ca37-40f4-90db-765eee1ff653.webp",
                ImageName = "00000000-0000-0000-0000-000000000046_i3.jpg",
                Order = 3
            },

            // artwork 71
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000090"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000047"),
                Location = "https://storage.googleapis.com/artworkia-storage-public/Artwork/VSCode.png",
                ImageName = "00000000-0000-0000-0000-000000000047_i0.jpg",
                Order = 0
            },
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000091"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000047"),
                Location = "https://storage.googleapis.com/artworkia-storage-public/Artwork/VSCode-Thick.png",
                ImageName = "00000000-0000-0000-0000-000000000047_i1.jpg",
                Order = 1
            },

            // artwork 72
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000093"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000048"),
                Location = "https://i.pinimg.com/564x/2c/5d/80/2c5d80e9c0240a65d6397f9991352855.jpg",
                ImageName = "00000000-0000-0000-0000-000000000048_i0.jpg",
                Order = 0
            },

            // artwork 73
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000094"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000049"),
                Location = "https://i.pinimg.com/564x/71/01/da/7101da0949d85dfbf7c24200f1fcbdfd.jpg",
                ImageName = "00000000-0000-0000-0000-000000000049_i0.jpg",
                Order = 0
            },

            // artwork 74
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000095"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004a"),
                Location = "https://i.pinimg.com/564x/61/29/93/61299356ecd161b0ac86c94869960084.jpg",
                ImageName = "00000000-0000-0000-0000-00000000004a_i0.jpg",
                Order = 0
            },

            // artwork 75
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000096"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004b"),
                Location = "https://i.pinimg.com/564x/a3/ef/9c/a3ef9c4379f92f56607f1f685e7835ce.jpg",
                ImageName = "00000000-0000-0000-0000-00000000004b_i0.jpg",
                Order = 0
            },

            // artwork 76
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000097"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004c"),
                Location = "https://i.pinimg.com/564x/19/d5/fb/19d5fbfe0a970510bfe47aa148e0b71e.jpg",
                ImageName = "00000000-0000-0000-0000-00000000004c_i0.jpg",
                Order = 0
            },

            // artwork 77
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000098"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004d"),
                Location = "https://storage.googleapis.com/artworkia-storage-public/Artwork/IntellijLogo.png",
                ImageName = "00000000-0000-0000-0000-00000000004d_i0.jpg",
                Order = 0
            },
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000099"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004d"),
                Location = "https://storage.googleapis.com/artworkia-storage-public/Artwork/intelliJShadow.png",
                ImageName = "00000000-0000-0000-0000-00000000004d_i1.jpg",
                Order = 1
            },

            // artwork 78
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000009b"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004e"),
                Location = "https://storage.googleapis.com/artworkia-storage-public/Artwork/phucche1.png",
                ImageName = "00000000-0000-0000-0000-00000000004e_i0.jpg",
                Order = 0
            },
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000009c"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004e"),
                Location = "https://storage.googleapis.com/artworkia-storage-public/Artwork/phucche2.png",
                ImageName = "00000000-0000-0000-0000-00000000004e_i1.jpg",
                Order = 1
            },

            // artwork 79
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-0000000000a0"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004f"),
                Location = "https://file4.batdongsan.com.vn/2024/03/07/20240307144316-1cca_wm.jpg",
                ImageName = "00000000-0000-0000-0000-00000000004f_i0.jpg",
                Order = 0
            },
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-0000000000a1"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004f"),
                Location = "https://file4.batdongsan.com.vn/2024/03/07/20240307144319-ee94_wm.jpg",
                ImageName = "00000000-0000-0000-0000-00000000004f_i1.jpg",
                Order = 1
            },
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-0000000000a2"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-00000000004f"),
                Location = "https://file4.batdongsan.com.vn/2024/03/07/20240307144319-f0da_wm.jpg",
                ImageName = "00000000-0000-0000-0000-00000000004f_i2.jpg",
                Order = 2
            },

            // artwork 80
            new Image()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-0000000000a3"),
                ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000050"),
                Location = "https://i.pinimg.com/originals/86/15/50/8615509334c99ba0c11a9feac151a79e.jpg",
                ImageName = "00000000-0000-0000-0000-000000000050_i0.jpg",
                Order = 0
            }


        );
        #endregion
    }
}
