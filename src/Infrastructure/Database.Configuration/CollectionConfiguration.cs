using Domain.Entitites;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration
{
    public class CollectionConfiguration : IEntityTypeConfiguration<Collection>
    {
        public void Configure(EntityTypeBuilder<Collection> builder)
        {
            builder.ToTable(nameof(Collection));

            builder.Property(x => x.Id).HasDefaultValueSql("newid()");

            // relationship
            builder.HasOne(x => x.Account).WithMany(p => p.Collections).HasForeignKey(x => x.CreatedBy);
            builder.HasMany(x => x.Bookmarks).WithOne(b => b.Collection).HasForeignKey(b => b.CollectionId);


            builder.HasData(

                // collection 1 (phuhuynh)
                new Collection
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    CollectionName = "Trò chơi yêu thích",
                    Privacy = PrivacyEnum.Public,
                    CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                    CreatedOn = DateTime.Parse("2024-01-01")
                },

                // collection 2 (phuhuynh)
                new Collection
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    CollectionName = "Hoàng hôn",
                    Privacy = PrivacyEnum.Private,
                    CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                    CreatedOn = DateTime.Parse("2024-01-01")
                },

                // collection 3 (hoangtuan)
                new Collection
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    CollectionName = "Ảnh bìa đẹp",
                    Privacy = PrivacyEnum.Public,
                    CreatedBy = Guid.Parse("00000000-0000-0000-0000-00000000000f"),
                    CreatedOn = DateTime.Parse("2024-02-01")
                }
            );
        }
    }
}
