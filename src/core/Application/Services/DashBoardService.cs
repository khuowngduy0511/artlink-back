using Application.Commons;
using Application.Models;
using Application.Services.Abstractions;
using AutoMapper;
using Domain.Models;
using Domain.Repositories.Abstractions;
namespace Application.Services;

public class DashBoardService : IDashBoardService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DashBoardService(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PagedList<TransactionHistoryVM>> GetAllTransactionHistoriesAsync(int pageNumber, int pgaeSize)
    {
        var result = await _unitOfWork.TransactionHistoryRepository
            .GetAllTransacrionHistoriesPaginationAsync(pageNumber, pgaeSize);
        return _mapper.Map<PagedList<TransactionHistoryVM>>(result);

    }

    public async Task<PagedList<WalletHistoryVM>> GetAllWalletHistoriesAsync(int pageNumber, int pgaeSize)
    {
        var result = await _unitOfWork.WalletHistoryRepository
            .GetAllWalletHistoriesPaginationAsync(pageNumber, pgaeSize);
        return _mapper.Map<PagedList<WalletHistoryVM>>(result);
    }

    public async Task<List<AssetTransByDate>> GetAssetTransactionStatisticAsync(DateTime? startTime = null, DateTime? endTime = null)
    {
        var result = await _unitOfWork.TransactionHistoryRepository.GetAssetTransactionStatisticAsync(startTime, endTime);
        DateTime startDate = startTime ?? result.First().Date;
        DateTime endDate = endTime ?? result.Last().Date;
        
        // 1. Create a full date range
        var dateRange = Enumerable.Range(0, (endDate - startDate).Days + 1)
                                  .Select(days => startDate.AddDays(days))
                                  .ToList();

        // 2. Convert result into a dictionary for easy lookup
        var resultDict = result.ToDictionary(x => x.Date, x => x);

        // 3. Build newResult with all dates, filling gaps and calculating values
        var newResult = new List<AssetTransByDate>();
        int? runningTotal = null; // Initialize runningTotal

        foreach (var date in dateRange)
        {
            var item = new AssetTransByDate
            {
                Date = date,
                Count = resultDict.ContainsKey(date) ? resultDict[date].Count : 0,
                Total = runningTotal,
                IncreaseRate = resultDict.ContainsKey(date) ? resultDict[date].IncreaseRate : 0,
            };

            runningTotal = item.Count + (runningTotal ?? 0); // Update runningTotal

            newResult.Add(item);
        }

        return newResult;
    }

    public async Task<List<PercentageCategoryOfAssetTrans>> GetPercentageCategoryOfAssetTransStatisticAsync(DateTime? startTime = null, DateTime? endTime = null)
    {
        var result = await _unitOfWork.TransactionHistoryRepository.GetPercentageCategoryOfAssetTransStatisticAsync(startTime, endTime);
        return result;
    }

    public async Task<List<TopCreatorOfAssetTransVM>> GetTopCreatorOfAssetTransStatisticAsync(int topNumber = 10, DateTime? startTime = null, DateTime? endTime = null)
    {
        var result = await _unitOfWork.TransactionHistoryRepository.GetTopCreatorOfAssetTransStatisticAsync(topNumber, startTime, endTime);
        return _mapper.Map<List<TopCreatorOfAssetTransVM>>(result);
    }

    public async Task<List<ProposalByDate>> GetProposalByDateStatisticAsync(DateTime? startTime = null, DateTime? endTime = null)
    {
        var result = await _unitOfWork.ProposalRepository.GetProposalStatisticAsync(startTime, endTime);
        DateTime startDate = startTime ?? result.First().Date;
        DateTime endDate = endTime ?? result.Last().Date;

        // 1. Create a full date range
        var dateRange = Enumerable.Range(0, (endDate - startDate).Days + 1)
                                  .Select(days => startDate.AddDays(days))
                                  .ToList();

        // 2. Convert result into a dictionary for easy lookup
        var resultDict = result.ToDictionary(x => x.Date, x => x);

        // 3. Build newResult with all dates, filling gaps and calculating values
        var newResult = new List<ProposalByDate>();
        int? runningTotal = null; // Initialize runningTotal

        foreach (var date in dateRange)
        {
            var item = new ProposalByDate
            {
                Date = date,
                Count = resultDict.ContainsKey(date) ? resultDict[date].Count : 0,
                Total = runningTotal,
                IncreaseRate = resultDict.ContainsKey(date) ? resultDict[date].IncreaseRate : 0,
            };

            runningTotal = item.Count + (runningTotal ?? 0); // Update runningTotal

            newResult.Add(item);
        }
        return newResult;
    }

    public async Task<List<PercentageCategoryOfProposal>> GetPercentageCategoryOfProposalStatisticAsync(DateTime? startTime = null, DateTime? endTime = null)
    {
        var result = await _unitOfWork.ProposalRepository.GetPercentageCategoryOfProposalStatisticAsync(startTime, endTime);
        return result;
    }

    public async Task<List<TopCreatorOfProposalVM>> GetTopCreatorOfProposalStatisticAsync(int topNumber = 10, DateTime? startTime = null, DateTime? endTime = null)
    {
        var result = await _unitOfWork.ProposalRepository.GetTopCreatorOfProposalStatisticAsync(topNumber, startTime, endTime);
        return _mapper.Map<List<TopCreatorOfProposalVM>>(result);
    }

    public async Task<List<TopServiceOfCreatorVM>> GetTopServiceOfCreatorStatisticAsync(int topNumber = 10, DateTime? startTime = null, DateTime? endTime = null)
    {
        var result = await _unitOfWork.ProposalRepository.GetTopServiceOfCreatorStatisticAsync(topNumber, startTime, endTime);
        return _mapper.Map<List<TopServiceOfCreatorVM>>(result);
    }
}
