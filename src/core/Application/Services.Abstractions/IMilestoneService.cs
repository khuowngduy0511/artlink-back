using Application.Models;
using Domain.Enums;

namespace Application.Services.Abstractions;

public interface IMilestoneService
{
    Task<List<MilestoneVM>> GetMilestonesAsync(Guid proposalId);
    Task AddMilestoneToProposalAsync(Guid proposalId,
            string? details = "", ProposalStateEnum? state = null);
}
