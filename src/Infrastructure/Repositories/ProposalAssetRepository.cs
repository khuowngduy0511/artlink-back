using Application.Services.Abstractions;
using Domain.Entitites;
using Domain.Repositories.Abstractions;
using Infrastructure.Database;
using Infrastructure.Repositories.Commons;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class ProposalAssetRepository : GenericCreationRepository<ProposalAsset>, IProposalAssetRepository
{
    public ProposalAssetRepository(AppDBContext dBContext, IClaimService claimService) : base(dBContext, claimService)
    {
    }

    public Task<List<ProposalAsset>> GetProposalAssetsOfProposalAsync(Guid proposalAssetId)
    {
        return _dbContext.ProposalAssets
            .Where(x => x.ProposalId == proposalAssetId)
            .ToListAsync();
    }

    public Task<ProposalAsset?> GetProposalAssetsWithProposalAsync(Guid proposalAssetId)
    {
        return _dbContext.ProposalAssets
            .Include(x => x.Proposal)
            .FirstOrDefaultAsync(x => x.Id == proposalAssetId);
    }
}
