using Application.Commons;
using Application.Models;
using Domain.Models;

namespace Application.Services.Abstractions;

public interface IDashBoardService
{
    Task<PagedList<WalletHistoryVM>> GetAllWalletHistoriesAsync(int pageNumber, int pgaeSize);
    Task<PagedList<TransactionHistoryVM>> GetAllTransactionHistoriesAsync(int pageNumber, int pgaeSize);

    Task<List<AssetTransByDate>> GetAssetTransactionStatisticAsync(DateTime? startTime = null, DateTime? endTime = null);
    Task<List<PercentageCategoryOfAssetTrans>> GetPercentageCategoryOfAssetTransStatisticAsync(
        DateTime? startTime = null, DateTime? endTime = null);
    Task<List<TopCreatorOfAssetTransVM>> GetTopCreatorOfAssetTransStatisticAsync(int topNumber = 10, DateTime? startTime = null, DateTime? endTime = null);

    Task<List<ProposalByDate>> GetProposalByDateStatisticAsync(DateTime? startTime = null, DateTime? endTime = null);
    Task<List<PercentageCategoryOfProposal>> GetPercentageCategoryOfProposalStatisticAsync(
               DateTime? startTime = null, DateTime? endTime = null);
    Task<List<TopCreatorOfProposalVM>> GetTopCreatorOfProposalStatisticAsync(int topNumber = 10, DateTime? startTime = null, DateTime? endTime = null);
    Task<List<TopServiceOfCreatorVM>> GetTopServiceOfCreatorStatisticAsync(int topNumber = 10, DateTime? startTime = null, DateTime? endTime = null);
}
