using Application.Models;

namespace Application.Services.Abstractions;

public interface IAssetTransactionService
{
    Task<IEnumerable<AssetTransactionVM>> GetAllAssetTransactionsAsync();
    Task<AssetTransactionVM> GetAssetTransactionByIdAsync(Guid assetTransactionId);
    Task<AssetTransactionVM> CreateAssetTransactionAsync(AssetTransactionModel assetTransactionModel);
    Task<IEnumerable<AssetTransactionVM>> GetAllAssetTransactionsByAssetIdAsync(Guid assetId);
    Task<IEnumerable<AssetTransactionVM>> GetAllAssetTransactionsByAccountIdAsync(Guid accountId);
}
