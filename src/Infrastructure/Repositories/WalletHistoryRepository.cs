using Application.Services.Abstractions;
using Domain.Entities.Commons;
using Domain.Entitites;
using Domain.Repositories.Abstractions;
using Infrastructure.Database;
using Infrastructure.Repositories.Commons;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class WalletHistoryRepository : GenericCreationRepository<WalletHistory>, IWalletHistoryRepository
{
    public WalletHistoryRepository(AppDBContext dBContext, IClaimService claimService) : base(dBContext, claimService)
    {
    }

    public async Task<IPagedList<WalletHistory>> GetAllWalletHistoriesPaginationAsync(int pageNumber, int pageSize)
    {
        var walletHistories = _dbContext.WalletHistories
                                    .Include(x => x.Account)
                                    .OrderByDescending(x => x.CreatedOn);
        var result = await this.ToPaginationAsync(walletHistories, pageNumber, pageSize);
        return result;
    }

    public async Task<List<WalletHistory>> GetWalletHistoriesOfAccountAsync(Guid accountId)
    {
        return await _dbContext.WalletHistories
            .Include(x => x.Account)
            .Where(x => x.CreatedBy == accountId)
            .OrderByDescending(x => x.CreatedOn)
            .ToListAsync();
    }
}
