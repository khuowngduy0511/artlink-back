using Application.Models;

namespace Application.Services.Abstractions;

public interface IProposalAssetService
{
    Task<List<ProposalAssetVM>> GetProposalAssetsOfProposalAsync(Guid proposalId);
    Task<string?> GetDownloadUriProposalAssetAsync(Guid proposalAssetId);

    Task<ProposalAssetVM> AddProposalAssetAsync(ProposalAssetModel proposalAssetModel);
}
