using Application.Models;
using Application.Services.Abstractions;
using AutoMapper;
using Domain.Entitites;
using Domain.Enums;
using Domain.Repositories.Abstractions;

namespace Application.Services;

public class TransactionHistoryService : ITransactionHistoryService
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    private IClaimService _claimService;
    public TransactionHistoryService(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _claimService = claimService;
    }

    public async Task<TransactionHistoryVM> CreateTransactionHistory(TransactionModel model)
    {
        if (model.AssetId == null && model.ProposalId == null)
        {
            throw new ArgumentNullException("AssetId và proposalId không thể null cùng lúc.");
        }
        if (model.AssetId != null)
        {
            var assetExist = await _unitOfWork.AssetRepository.IsExistedAsync(model.AssetId.Value);
            if (!assetExist)
            {
                throw new KeyNotFoundException("Không tìm thấy tài nguyên.");
            }
        }
        if (model.ProposalId != null)
        {
            var proposalExist = await _unitOfWork.ProposalRepository.IsExistedAsync(model.ProposalId.Value);
            if (!proposalExist)
            {
                throw new KeyNotFoundException("Không tìn thấy proposalId.");
            }
        }
        var transaction = _mapper.Map<TransactionHistory>(model);
        await _unitOfWork.TransactionHistoryRepository.AddAsync(transaction);

        return _mapper.Map<TransactionHistoryVM>(transaction);
    }

    public async Task<List<TransactionHistoryForUserVM>> GetTransactionHistoriesOfAccount(Guid accountId)
    {
        // check if current account authorized to use this function
        var currentAccountId = _claimService.GetCurrentUserId ?? default;
        if (_claimService.GetCurrentRole != RoleEnum.Admin.ToString())
        {
            if (currentAccountId != accountId)
            {
                throw new KeyNotFoundException("Bạn không đủ quyền để truy cập tính năng này.");
            }
        }
        var result = await _unitOfWork.TransactionHistoryRepository
            .GetTransactionHistoriesOfAccountAsync(accountId);

        var viewModels = _mapper.Map<List<TransactionHistoryForUserVM>>(result);        
        return viewModels;
    }
}
