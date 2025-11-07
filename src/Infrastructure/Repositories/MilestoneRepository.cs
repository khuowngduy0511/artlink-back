using Application.Services.Abstractions;
using Domain.Entitites;
using Domain.Repositories.Abstractions;
using Infrastructure.Database;
using Infrastructure.Repositories.Commons;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MilestoneRepository : GenericCreationRepository<Milestone>, IMilestoneRepository
{
    public MilestoneRepository(AppDBContext dBContext, IClaimService claimService)
        : base(dBContext, claimService)
    {
    }

    public async Task<List<Milestone>> GetAllMilstoneOfProposalAsync(Guid proposalId)
    {
        var milestones = await _dbContext.Milestones
                            .Include(x => x.CreatedAccount)
                            .Where(x => x.ProposalId == proposalId)
                            .ToListAsync();
        return milestones;
    }
}
