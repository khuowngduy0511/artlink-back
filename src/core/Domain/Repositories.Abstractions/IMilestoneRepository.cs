using Domain.Entitites;

namespace Domain.Repositories.Abstractions;

public interface IMilestoneRepository : IGenericRepository<Milestone>
{
    Task<List<Milestone>> GetAllMilstoneOfProposalAsync(Guid proposalId);
}
