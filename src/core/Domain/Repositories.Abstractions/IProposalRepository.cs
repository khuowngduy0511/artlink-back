using Domain.Entitites;
using Domain.Models;

namespace Domain.Repositories.Abstractions;
public interface IProposalRepository : IGenericRepository<Proposal>
{
    Task<Proposal?> GetProposalDetailAsync(Guid proposalId);
    Task<Proposal?> GetProposalDetailWithReviewAsync(Guid proposalId);
    Task<List<Proposal>> GetProposalsByChatIdAsync(Guid ChatId);
    Task<List<Proposal>> GetProposalsByCreatorIdAsync(Guid creatorId);
    Task<List<Proposal>> GetProposalsByAudienceIdAsync(Guid audienceId);
    //Task<List<Proposal>> GetProposalsByServiceIdAsync(Guid serviceId);
    Task<List<ProposalByDate>> GetProposalStatisticAsync(DateTime? startTime = null, DateTime? endTime = null);
    Task<List<PercentageCategoryOfProposal>> GetPercentageCategoryOfProposalStatisticAsync(DateTime? startTime = null, DateTime? endTime = null);
    Task<List<TopCreatorOfProposal>> GetTopCreatorOfProposalStatisticAsync(int topNumber, DateTime? startTime = null, DateTime? endTime = null);
    Task<List<TopServiceOfCreator>> GetTopServiceOfCreatorStatisticAsync(int topNumber, DateTime? startTime = null, DateTime? endTime = null);
}
