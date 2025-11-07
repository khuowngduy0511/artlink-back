using Application.Services.Abstractions;
using Domain.Entitites;
using Domain.Enums;
using Domain.Models;
using Domain.Repositories.Abstractions;
using Infrastructure.Database;
using Infrastructure.Repositories.Commons;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class ProposalRepository : GenericCreationRepository<Proposal>, IProposalRepository
{
    public ProposalRepository(AppDBContext dBContext, IClaimService claimService) : base(dBContext, claimService)
    {
    }

    public async Task<Proposal?> GetProposalDetailAsync(Guid proposalId)
    {
        return await _dbContext.Proposals
            .Include(x => x.Service)
            .Include(x => x.Account)
            //.Include(x => x.ChatBox)
            .Include(x => x.ProposalAssets)
            .Include(x => x.Review)
            .Include(x => x.TransactionHistories)
            .Include(x => x.Milestones)
            .FirstOrDefaultAsync(x => x.Id == proposalId);
    }

    public async Task<Proposal?> GetProposalDetailWithReviewAsync(Guid proposalId)
    {
        return await _dbContext.Proposals
            .Include(x => x.Review)
            .FirstOrDefaultAsync(x => x.Id == proposalId);
    }

    public async Task<List<Proposal>> GetProposalsByChatIdAsync(Guid ChatId)
    {
        return await _dbContext.Proposals
            .Include(x => x.Review)
            .Include(x => x.MessageObj)
            .Where(x => x.MessageObj.ChatBoxId == ChatId)
            .ToListAsync();

    }

    public async Task<List<Proposal>> GetProposalsByCreatorIdAsync(Guid creatorId)
    {
        return await _dbContext.Proposals
            .Include(x => x.Account)
            .Include(x => x.Orderer)
            .Include(x => x.Review)
            .Where(x => x.CreatedBy == creatorId)
            .ToListAsync();
    }

    public async Task<List<Proposal>> GetProposalsByAudienceIdAsync(Guid audienceId)
    {
        return await _dbContext.Proposals
            .Include(x => x.Account)
            .Include(x => x.Orderer)
            .Include(x => x.Review)
            .Where(x => x.OrdererId == audienceId)
            .ToListAsync();
    }

    public async Task<List<ProposalByDate>> GetProposalStatisticAsync(DateTime? startTime = null, DateTime? endTime = null)
    {
        // tong trans truoc starttime
        int totalUntilStartTime = 0;

        var proposalCompleteds = _dbContext.Proposals
            .Where(x => (x.ProposalStatus == ProposalStateEnum.CompletePayment || 
            x.ProposalStatus == ProposalStateEnum.ConfirmPayment)
            && x.ActualDelivery != null);

        if (startTime != null)
        {
            totalUntilStartTime = _dbContext.Proposals
                    .Where(x => (x.ProposalStatus == ProposalStateEnum.CompletePayment ||
                x.ProposalStatus == ProposalStateEnum.ConfirmPayment) && x.ActualDelivery < startTime)
                    .Count();

            proposalCompleteds = proposalCompleteds.Where(x => x.ActualDelivery >= startTime.Value.Date);
        }

        if (endTime != null)
        {
            proposalCompleteds = proposalCompleteds.Where(x => x.ActualDelivery <= endTime.Value.Date.AddDays(1).AddTicks(-1));
        }

        var proposalCountsByDayList = await proposalCompleteds
            .GroupBy(t => t.ActualDelivery!.Value.Date)
            .OrderBy(g => g.Key)
            .Select(g => new ProposalByDate
            {
                Date = g.Key,
                Count = g.Count()
            })
            .ToListAsync();
        foreach (var transaction in proposalCountsByDayList)
        {
            transaction.Total = totalUntilStartTime + transaction.Count;
            transaction.IncreaseRate = Math.Round((double)transaction.Count /
                (totalUntilStartTime == 0 ? 1 : totalUntilStartTime), 2);
            totalUntilStartTime += transaction.Count;
        }
        return proposalCountsByDayList;
    }

    public async Task<List<PercentageCategoryOfProposal>> GetPercentageCategoryOfProposalStatisticAsync(DateTime? startTime = null, DateTime? endTime = null)
    {
        var proposalsQuery = _dbContext.Proposals
            .Include(x => x.Service)
                .ThenInclude(x => x.CategoryServiceDetails)
                    .ThenInclude(x => x.Category)
            .Where(x => x.ProposalStatus == ProposalStateEnum.CompletePayment);

        if (startTime != null)
        {
            proposalsQuery = proposalsQuery.Where(x => x.ActualDelivery >= startTime.Value.Date);
        }
        if (endTime != null)
        {
            proposalsQuery = proposalsQuery.Where(x => x.ActualDelivery <= endTime.Value.Date.AddDays(1).AddTicks(-1));
        }
        var proposals = await proposalsQuery.ToListAsync();

        // Đếm số lần xuất hiện của mỗi thể loại
        var categoryCounts = proposals
            .SelectMany(t => t.Service.CategoryServiceDetails)
            .GroupBy(c => c.Category)
            .Select(g => new { Category = g.Key, Count = g.Count() })
            .ToList();

        var sum = categoryCounts.Sum(x => x.Count);

        // Tính phần trăm thể loại
        List<PercentageCategoryOfProposal> list = new();
        foreach (var categoryCount in categoryCounts)
        {
            var percentage = (double)categoryCount.Count / sum * 100;
            PercentageCategoryOfProposal item = new()
            {
                Category = categoryCount.Category.CategoryName,
                Percentage = percentage

            };
            list.Add(item);
        }

        return list;
    }

    public async Task<List<TopCreatorOfProposal>> GetTopCreatorOfProposalStatisticAsync(int topNumber, DateTime? startTime = null, DateTime? endTime = null)
    {
        var proposalsQuery = _dbContext.Proposals
            .Where(x => x.ProposalStatus == ProposalStateEnum.CompletePayment);

        if (startTime != null)
        {
            proposalsQuery = proposalsQuery.Where(x => x.ActualDelivery >= startTime.Value.Date);
        }
        if (endTime != null)
        {
            proposalsQuery = proposalsQuery.Where(x => x.ActualDelivery <= endTime.Value.Date.AddDays(1).AddTicks(-1));
        }
        var proposals = await proposalsQuery
            .GroupBy(t => t.Account)
            .Select(g => new TopCreatorOfProposal
            {
                Creator = g.Key,
                TotalProposal = g.Count(),
                TotalRevenue = g.Sum(x => x.Total)
            })
            .OrderByDescending(x => x.TotalRevenue)
            .Take(topNumber)
            .ToListAsync();


        return proposals;
    }

    public async Task<List<TopServiceOfCreator>> GetTopServiceOfCreatorStatisticAsync(int topNumber, DateTime? startTime = null, DateTime? endTime = null)
    {
        var proposalsQuery = _dbContext.Proposals
            .Include(x => x.Service)
            .Where(x => x.ProposalStatus == ProposalStateEnum.CompletePayment);

        if (startTime != null)
        {
            proposalsQuery = proposalsQuery.Where(x => x.ActualDelivery >= startTime.Value.Date);
        }
        if (endTime != null)
        {
            proposalsQuery = proposalsQuery.Where(x => x.ActualDelivery <= endTime.Value.Date.AddDays(1).AddTicks(-1));
        }
        var proposals = await proposalsQuery
            .GroupBy(t => t.Service)
            .Select(g => new TopServiceOfCreator
            {
                Service = g.Key,
                TotalService = g.Count(),
                TotalRevenue = g.Sum(x => x.Total)
            })
            .OrderByDescending(x => x.TotalRevenue)
            .Take(topNumber)
            .ToListAsync();


        return proposals;
    }
}
