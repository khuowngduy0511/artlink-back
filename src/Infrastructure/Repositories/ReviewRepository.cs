using Application.Services.Abstractions;
using Domain.Entities.Commons;
using Domain.Entitites;
using Domain.Repositories.Abstractions;
using Infrastructure.Database;
using Infrastructure.Repositories.Commons;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;
public class ReviewRepository : GenericCreationRepository<Review>, IReviewRepository
{
    public ReviewRepository(AppDBContext dBContext, IClaimService claimService) : base(dBContext, claimService)
    {
    }

    public async Task<Review?> GetReviewByProposalIdAsync(Guid proposalId)
    {
        return await _dbContext.Reviews
            .Include(r => r.Proposal)
            .Include(r => r.Account)
            .FirstOrDefaultAsync(x => x.ProposalId == proposalId);
    }

    public async Task<Review?> GetReviewDetailAsync(Guid reviewId)
    {
        return await _dbContext.Reviews
            .Include(r => r.Proposal)
            .Include(r => r.Account)
            .FirstOrDefaultAsync(r => r.Id == reviewId);
    }

    public async Task<IPagedList<Review>> GetReviewsAsync(string? keyword, string? sortColumn, string? sortOrder, int page, int pageSize, Guid? proposalId = null, Guid? accountId = null)
    {
        var allReviews = _dbContext.Reviews
            .Include(r => r.Proposal)
            .Include(r => r.Account).AsQueryable();

        if (proposalId != null)
        {
            allReviews = allReviews.Where(r => r.ProposalId == proposalId);
        }
        if (accountId != null)
        {
            allReviews = allReviews.Where(r => r.CreatedBy == accountId);
        }
        if (!string.IsNullOrEmpty(keyword))
        {
            keyword = keyword.ToLower();
            allReviews = allReviews.Where(r => r.Detail.ToLower().Contains(keyword));
        }

        Expression<Func<Review, object>> orderSelector = sortColumn switch
        {
            "createdOn" => r => r.CreatedOn,
            "vote" => r => r.Vote,
            _ => r => r.CreatedOn
        };
        if (sortOrder == "asc")
        {
            allReviews = allReviews.OrderBy(orderSelector);
        }
        else
        {
            allReviews = allReviews.OrderByDescending(orderSelector);
        }

        var result = await ToPaginationAsync(allReviews, page, pageSize);
        return result;
    }

    public async Task<IPagedList<Review>> GetReviewsByAccountIdAsync(Guid accountId, int page, int pageSize)
    {
        var allReviews = _dbContext.Reviews
            .Include(r => r.Proposal).AsQueryable();
        allReviews = allReviews.OrderByDescending(x => x.CreatedOn);
        var result = await ToPaginationAsync(allReviews, page, pageSize);
        return result;
    }

    public async Task<IPagedList<Review>> GetReviewsByServiceIdAsync(Guid serviceId, int page, int pageSize)
    {
        var allReviews = _dbContext.Reviews
            .Include(r => r.Proposal)
            .Include(r => r.Account)
            .Where(x => x.Proposal.ServiceId == serviceId);
        allReviews = allReviews.OrderByDescending(x => x.CreatedOn);
        var result = await ToPaginationAsync(allReviews, page, pageSize);
        return result;
    }
}
