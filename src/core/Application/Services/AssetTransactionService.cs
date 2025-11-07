using Application.Commons;
using Application.Models;
using Application.Services.Abstractions;
using AutoMapper;
using Domain.Entitites;
using Domain.Enums;
using Domain.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Application.Services;

public class AssetTransactionService : IAssetTransactionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IClaimService _claimService;

    public AssetTransactionService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IClaimService claimService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _claimService = claimService;
    }

    public async Task<AssetTransactionVM> CreateAssetTransactionAsync(AssetTransactionModel assetTransactionModel)
    {
        // Check if user already has this asset
        var assetTransaction = await _unitOfWork.TransactionHistoryRepository
            .GetSingleByConditionAsync(x => x.CreatedBy == _claimService.GetCurrentUserId
            && x.AssetId == assetTransactionModel.AssetId);
        if (assetTransaction is not null)
            throw new BadHttpRequestException("Bạn đã mua tài nguyên này.");

        // Check if asset is not found or deleted
        var asset = await _unitOfWork.AssetRepository.GetAssetAndItsCreatorAsync(assetTransactionModel.AssetId);
        if (asset is null)
            throw new KeyNotFoundException("Tài nguyên không tìm thấy.");
        if (asset.DeletedOn != null)
            throw new KeyNotFoundException("Tài nguyên đã bị xóa");


        Guid accountId = _claimService.GetCurrentUserId ?? default;
        if (asset.Artwork.CreatedBy == accountId)
        {
            throw new BadHttpRequestException("Không thể mua tài nguyên của chính mình.");
        }

        // Check if user has enough money to buy this asset
        Wallet? wallet = await _unitOfWork.WalletRepository.GetSingleByConditionAsync(x => x.AccountId == accountId);
        Wallet? sellerWallet = await _unitOfWork.WalletRepository.GetSingleByConditionAsync(x => x.AccountId == asset.Artwork.CreatedBy);

        if (wallet is null)
        {
            throw new KeyNotFoundException("Không tìm thấy ví của người mua.");
        }
        if (sellerWallet is null)
        {
            throw new KeyNotFoundException("Không tìm thấy ví của người bán.");
        }
        if (wallet.Balance < asset.Price)
            throw new BadHttpRequestException("Bạn không đủ tiền để mua tài nguyên này");        

        TransactionHistory newAssetTransactionForBuyer = new TransactionHistory()
        {
            CreatedBy = accountId,
            AssetId = assetTransactionModel.AssetId,
            Detail = $"Mở khóa tài nguyên \"{asset.AssetTitle}\"",
            Price = -asset.Price,
            TransactionStatus = TransactionStatusEnum.Success,
            ToAccountId = asset.Artwork.CreatedBy,
            WalletBalance = wallet.Balance - asset.Price
        };
        TransactionHistory newAssetTransactionForSeller = new TransactionHistory()
        {            
            AssetId = assetTransactionModel.AssetId,
            Detail = $"Mở khóa tài nguyên \"{asset.AssetTitle}\"",
            Price = asset.Price * (1 - AppConstant.PLATFORM_FEE),
            TransactionStatus = TransactionStatusEnum.Success,
            ToAccountId = accountId,
            WalletBalance = sellerWallet.Balance + asset.Price * (1 - AppConstant.PLATFORM_FEE),
            Fee = asset.Price * AppConstant.PLATFORM_FEE
        };

        wallet.Balance -= asset.Price;
        sellerWallet.Balance += asset.Price * (1 - AppConstant.PLATFORM_FEE);

        await _unitOfWork.TransactionHistoryRepository.AddAsync(newAssetTransactionForBuyer);
        await _unitOfWork.TransactionHistoryRepository.AddAsync(newAssetTransactionForSeller);

        newAssetTransactionForSeller.CreatedBy = asset.Artwork.CreatedBy;

        _unitOfWork.WalletRepository.Update(wallet);
        _unitOfWork.WalletRepository.Update(sellerWallet);
        await _unitOfWork.SaveChangesAsync();
        var result = _mapper.Map<AssetTransactionVM>(newAssetTransactionForBuyer);
        return result;
    }

    public async Task<IEnumerable<AssetTransactionVM>> GetAllAssetTransactionsAsync()
    {
        var allAssetTransactions = await _unitOfWork.TransactionHistoryRepository.GetListByConditionAsync(x => x.AssetId != null);
        var result = _mapper.Map<IEnumerable<AssetTransactionVM>>(allAssetTransactions);
        return result;
    }

    public Task<IEnumerable<AssetTransactionVM>> GetAllAssetTransactionsByAccountIdAsync(Guid accountId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AssetTransactionVM>> GetAllAssetTransactionsByAssetIdAsync(Guid assetId)
    {
        throw new NotImplementedException();
    }

    public async Task<AssetTransactionVM> GetAssetTransactionByIdAsync(Guid assetTransactionId)
    {
        var assetTransaction = await _unitOfWork.TransactionHistoryRepository.GetByIdAsync(assetTransactionId);
        if (assetTransaction is null)
            throw new KeyNotFoundException("Giao dịch tài nguyên không tìm thấy.");
        var result = _mapper.Map<AssetTransactionVM>(assetTransaction);
        return result;
    }
}
