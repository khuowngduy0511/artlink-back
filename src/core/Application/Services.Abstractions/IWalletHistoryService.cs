using Application.Models;
using Domain.Entitites;
using Domain.Enums;

namespace Application.Services.Abstractions;

public interface IWalletHistoryService
{
    Task<List<WalletHistoryVM>> GetWalletHistoriesOfAccount(Guid accountId);
    Task AddWalletHistory(WalletHistory walletHistory);
    Task UpdateWalletHistoryStatus(string appTransId, TransactionStatusEnum status, double walletBalance);
    Task UpdateWalletHistoryStatus(Guid transactionId, TransactionStatusEnum status);
    Task UpdateWalletHistory(Guid transactionId, WalletHistory walletHistory);
}
