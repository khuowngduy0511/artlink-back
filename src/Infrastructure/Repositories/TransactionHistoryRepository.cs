using Application.Services.Abstractions;
using Domain.Entities.Commons;
using Domain.Entitites;
using Domain.Models;
using Domain.Repositories.Abstractions;
using Infrastructure.Database;
using Infrastructure.Repositories.Commons;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class TransactionHistoryRepository : GenericCreationRepository<TransactionHistory>, ITransactionHistoryRepository

{
    public TransactionHistoryRepository(AppDBContext dBContext, IClaimService claimService) : base(dBContext, claimService)
    {
    }

    public async Task<TransactionHistory?> GetAssetTransactionAsync(Guid accountId, Guid assetId)
    {
        return await _dbContext.TransactionHistories.FirstOrDefaultAsync(x => x.CreatedBy == accountId && x.AssetId == assetId);
    }

    public async Task<TransactionHistory?> GetProposalTransactionAsync(Guid accountId, Guid proposalId)
    {
        return await _dbContext.TransactionHistories.FirstOrDefaultAsync(x => x.CreatedBy == accountId && x.ProposalId == proposalId);
    }


    public async Task<IPagedList<TransactionHistory>> GetAssetsBoughtOfAccountAsync(Guid accountId, int page, int pageSize)
    {
        var assetBoughts = _dbContext.TransactionHistories
            .Include(a => a.Asset)
            .Where(a => a.CreatedBy == accountId && a.Price < 0 && a.AssetId != null);

        // sorting
        assetBoughts = assetBoughts.OrderByDescending(x => x.CreatedOn);

        #region paging
        var result = await ToPaginationAsync(assetBoughts, page, pageSize);
        #endregion

        return result;
    }

    public async Task<List<TransactionHistory>> GetTransactionHistoriesOfAccountAsync(Guid accountId)
    {
        var transactionsList = await _dbContext.TransactionHistories
            .Where(x => x.CreatedBy == accountId)
            .Include(x => x.Account)
            .Include(x => x.ToAccount)
            .OrderByDescending(x => x.CreatedOn)
            .ToListAsync();

        return transactionsList;
    }

    public async Task<IPagedList<TransactionHistory>> GetAllTransacrionHistoriesPaginationAsync(int pageNumber, int pageSize)
    {
        var transactionList = _dbContext.TransactionHistories
                                    .Include(x => x.Account)
                                    .Include(x => x.ToAccount)
                                    .OrderByDescending(x => x.CreatedOn);
        var result = await this.ToPaginationAsync(transactionList, pageNumber, pageSize);
        return result;
    }

    public async Task<List<AssetTransByDate>> GetAssetTransactionStatisticAsync(DateTime? startTime = null, DateTime? endTime = null)
    {
        // tong trans truoc starttime
        int totalUntilStartTime = 0;

        var assetTransactions = _dbContext.TransactionHistories
            .Include(x => x.Asset)
            .Where(x => x.AssetId != null && x.Price < 0);

        if (startTime != null)
        {
            totalUntilStartTime = _dbContext.TransactionHistories
                    .Where(t => t.CreatedOn < startTime)
                    .Sum(t => t.AssetId != null ? 1 : 0);

            assetTransactions = assetTransactions.Where(x => x.CreatedOn >= startTime.Value.Date);
        }

        if (endTime != null)
        {
            assetTransactions = assetTransactions.Where(x => x.CreatedOn <= endTime.Value.Date.AddDays(1).AddTicks(-1));
        }

        var assetCountsByDayList = await assetTransactions
            .GroupBy(t => t.CreatedOn.Date)
            .OrderBy(g => g.Key)
            .Select(g => new AssetTransByDate
            {
                Date = g.Key,
                Count = g.Count()
            })
            .ToListAsync();
        foreach (var transaction in assetCountsByDayList)
        {
            transaction.Total = totalUntilStartTime + transaction.Count;
            transaction.IncreaseRate = Math.Round((double)transaction.Count /
                (totalUntilStartTime == 0 ? 1 : totalUntilStartTime), 2);
            totalUntilStartTime += transaction.Count;
        }
        return assetCountsByDayList;
    }

    public async Task<List<PercentageCategoryOfAssetTrans>> GetPercentageCategoryOfAssetTransStatisticAsync(DateTime? startTime = null, DateTime? endTime = null)
    {
        // Truy vấn dữ liệu về các giao dịch liên quan đến artwork cụ thể
        var transactionsQuery = _dbContext.TransactionHistories
            .Include(t => t.Asset!)
                .ThenInclude(a => a.Artwork)
                    .ThenInclude(x => x.CategoryArtworkDetails)
                        .ThenInclude(x => x.Category)
            .Where(x => x.AssetId != null);

        if (startTime != null)
        {
            transactionsQuery = transactionsQuery.Where(x => x.CreatedOn >= startTime.Value.Date);
        }
        if (endTime != null)
        {
            transactionsQuery = transactionsQuery.Where(x => x.CreatedOn <= endTime.Value.Date.AddDays(1).AddTicks(-1));
        }
        var transactions = await transactionsQuery.ToListAsync();

        // Đếm số lần xuất hiện của mỗi thể loại
        var categoryCounts = transactions
            .SelectMany(t => t.Asset!.Artwork.CategoryArtworkDetails)
            .GroupBy(c => c.Category)
            .Select(g => new { Category = g.Key, Count = g.Count() })
            .ToList();

        var sum = categoryCounts.Sum(x => x.Count);

        // Tính phần trăm thể loại
        List<PercentageCategoryOfAssetTrans> list = new();
        foreach (var categoryCount in categoryCounts)
        {
            var percentage = (double)categoryCount.Count / sum * 100;
            PercentageCategoryOfAssetTrans item = new()
            {
                Category = categoryCount.Category.CategoryName,
                Percentage = percentage

            };
            list.Add(item);
        }

        return list;
    }

    public async Task<List<TopCreatorOfAssetTrans>> GetTopCreatorOfAssetTransStatisticAsync(int topNumber, DateTime? startTime = null, DateTime? endTime = null)
    {
        // Truy vấn dữ liệu về các giao dịch liên quan đến artwork cụ thể
        var transactionsQuery = _dbContext.TransactionHistories
            .Include(t => t.Asset!)
                .ThenInclude(a => a.Artwork)
            .Where(x => x.AssetId != null && x.Price > 0);

        if (startTime != null)
        {
            transactionsQuery = transactionsQuery.Where(x => x.CreatedOn >= startTime.Value.Date);
        }
        if (endTime != null)
        {
            transactionsQuery = transactionsQuery.Where(x => x.CreatedOn <= endTime.Value.Date.AddDays(1).AddTicks(-1));
        }
        var transactions = await transactionsQuery
            .GroupBy(t => t.Asset!.Artwork.Account)
            .Select(g => new TopCreatorOfAssetTrans
            {
                Creator = g.Key,
                TotalBought = g.Count(),
                TotalRevenue = g.Sum(x => x.Price)
            })
            .OrderByDescending(x => x.TotalRevenue)
            .Take(topNumber)
            .ToListAsync();


        return transactions;
    }
}
