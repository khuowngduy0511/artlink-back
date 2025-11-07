using Domain.Entities.Commons;
using Domain.Entitites;

namespace Domain.Repositories.Abstractions;
public interface IWalletHistoryRepository : IGenericRepository<WalletHistory>
{
    Task<IPagedList<WalletHistory>> GetAllWalletHistoriesPaginationAsync(int pageNumber, int pageSize);
    Task<List<WalletHistory>> GetWalletHistoriesOfAccountAsync(Guid accountId);
}
