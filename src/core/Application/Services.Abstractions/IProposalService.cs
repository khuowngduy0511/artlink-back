using Application.Models;

namespace Application.Services.Abstractions;

public interface IProposalService
{
    Task<List<ProposalVM>> GetAllProposalsAsync();
    Task<ProposalVM?> GetProposalByIdAsync(Guid proposalId);
    Task<List<ProposalVM>> GetProposalsByChatIdAsync(Guid chatId);
    Task<List<ProposalVM>> GetProposalsByCreatorIdIdAsync(Guid creatorId);
    Task<List<ProposalVM>> GetProposalsByAudienceIdIdAsync(Guid audienceId);
    Task<List<ProposalVM>> GetProposalsByServiceIdAsync(Guid serviceId);
    Task<ProposalVM> CreateProposalAsync(ProposalModel model);
    Task<ProposalVM> UpdateProposalStatusAsync(Guid id, ProposalUpdateStatusModel model);
    Task DeleteProposalAsync(Guid proposalId);
    Task<TransactionHistoryVM> InitPaymentProposalAsync(Guid proposalId);
    Task<TransactionHistoryVM> CompletePaymentProposalAsync(Guid proposalId);
    Task ConfirmPaymentProposalAsync(Guid proposalId);
}
