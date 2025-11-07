using Domain.Entitites;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class ProposalAssetConfiguration : IEntityTypeConfiguration<ProposalAsset>
{
    public void Configure(EntityTypeBuilder<ProposalAsset> builder)
    {
        builder.ToTable(nameof(ProposalAsset));

        builder.Property(x => x.Id).HasDefaultValueSql("newid()");
        builder.Property(x => x.CreatedOn).HasDefaultValueSql("getutcdate()");

        // relationship
        builder.HasOne(x => x.Proposal).WithMany(p => p.ProposalAssets).HasForeignKey(x => x.ProposalId);

        #region data
        builder.HasData(
            new ProposalAsset()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                ProposalId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Type = ProposalAssetEnum.Concept,
                ProposalAssetName = "ConceptAsset.rar",
                Location = "https://storage.cloud.google.com/artworkia-storage/ProposalAsset/ConceptAsset.rar?authuser=4",
                ContentType = "rar",
                Size = 8000000,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                CreatedOn = DateTime.Parse("2024-1-5"),
            },
            new ProposalAsset()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                ProposalId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Type = ProposalAssetEnum.Concept,
                ProposalAssetName = "ConceptAsset.rar",
                Location = "https://storage.cloud.google.com/artworkia-storage/ProposalAsset/ConceptAsset.rar?authuser=4",
                ContentType = "rar",
                Size = 8000000,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                CreatedOn = DateTime.Parse("2024-1-10"),
            },
            new ProposalAsset()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                ProposalId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Type = ProposalAssetEnum.Final,
                ProposalAssetName = "FinalAsset.rar",
                Location = "https://storage.cloud.google.com/artworkia-storage/ProposalAsset/FinalAsset.rar?authuser=4",
                ContentType = "rar",
                Size = 14000000,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                CreatedOn = DateTime.Parse("2024-1-25"),
            },
            new ProposalAsset()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                ProposalId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Type = ProposalAssetEnum.Revision,
                ProposalAssetName = "RevisionAsset.rar",
                Location = "https://storage.cloud.google.com/artworkia-storage/ProposalAsset/RevisionAsset.rar?authuser=4",
                ContentType = "rar",
                Size = 14000000,
                CreatedBy = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                CreatedOn = DateTime.Parse("2024-1-30"),
            }
        );
        #endregion
    }
}
