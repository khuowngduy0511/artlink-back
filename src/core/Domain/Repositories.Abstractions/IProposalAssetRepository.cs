using Domain.Entitites;

namespace Domain.Repositories.Abstractions;
public interface IProposalAssetRepository : IGenericRepository<ProposalAsset>
{
    Task<List<ProposalAsset>> GetProposalAssetsOfProposalAsync(Guid proposalAssetId);
    Task<ProposalAsset?> GetProposalAssetsWithProposalAsync(Guid proposalAssetId);

}
