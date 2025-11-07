using Application.Models;
using Domain.Entitites;

namespace Application.Services.Abstractions;

public interface IWalletService
{
    Task<WalletVM?> GetWalletByIdAsync(Guid walletId);
    Task<WalletVM?> GetWalletByAccountIdAsync(Guid accountId);
    Task UpdateWalletAsync(Guid walletId, WalletEM walletEM);
    Task UpdateCurrentWalletAsync(WalletEM walletEM);
    Task<Wallet> AddWalletAsync(Guid acocuntId, WalletEM walletModel);
    Task AddCoinsToWallet(Guid accountId, double amount, bool isSaveChange = true);
    Task SubtrasctCoinsFromWallet(Guid accountId, double amount, bool isSaveChange = true);
    Task<bool> CheckWithdrawBalance(Guid accountId, double amount);
}