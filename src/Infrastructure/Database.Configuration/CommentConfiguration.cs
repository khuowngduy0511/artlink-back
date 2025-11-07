using Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comment");
            builder.Property(x => x.Id).HasDefaultValueSql("newid()");
            builder.Property(x => x.CreatedOn).HasDefaultValueSql("getutcdate()");
            builder.Property(x => x.LastModificatedOn).HasDefaultValueSql("getutcdate()");

            // relationship
            builder.HasOne(x => x.Reply).WithMany(p => p.Replies).HasForeignKey(x => x.ReplyId);
            builder.HasOne(x => x.Account).WithMany(a => a.Comments).HasForeignKey(x => x.CreatedBy);
            builder.HasOne(x => x.Artwork).WithMany(a => a.Comments).HasForeignKey(x => x.ArtworkId);


            #region init data
            builder.HasData
            (

                // Artwork 1
                new Comment()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Content = "Đây là một bức tranh rất đẹp",
                    CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    CreatedOn = DateTime.Parse("2024-1-1"),
                },
                new Comment()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Content = "Minh hoạ xuất sắc",
                    CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    CreatedOn = DateTime.Parse("2024-1-1"),
                },
                new Comment()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Content = "10 điểm",
                    CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    CreatedOn = DateTime.Parse("2024-1-1"),
                },
                new Comment()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                    ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Content = "Cute and funny",
                    CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                    CreatedOn = DateTime.Parse("2024-1-1"),
                },
                new Comment()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                    ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Content = "Like",
                    CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                    CreatedOn = DateTime.Parse("2024-1-1"),
                },
                new Comment()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                    ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Content = "Hoàng hôn lấp lánh quá điiii!",
                    CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    CreatedOn = DateTime.Parse("2024-3-21"),
                },

                // Artwork 5
                new Comment()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                    ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                    Content = "Đây là một bức tranh rất đẹp",
                    CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    CreatedOn = DateTime.Parse("2023-12-07"),
                },
                new Comment()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                    ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                    Content = "Minh hoạ xuất sắc",
                    CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                    CreatedOn = DateTime.Parse("2023-12-08"),
                },
                new Comment()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                    ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                    Content = "10 điểm",
                    CreatedBy = Guid.Parse("00000000-0000-0000-0000-00000000000a"),
                    CreatedOn = DateTime.Parse("2023-12-09"),
                },
                new Comment()
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-00000000000a"),
                    ArtworkId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                    Content = "So peak",
                    CreatedBy = Guid.Parse("00000000-0000-0000-0000-00000000000b"),
                    CreatedOn = DateTime.Parse("2023-12-10"),
                }

            );
            #endregion
        }
    }
}
