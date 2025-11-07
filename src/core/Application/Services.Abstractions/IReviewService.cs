using Application.Commons;
using Application.Filters;
using Application.Models;

namespace Application.Services.Abstractions;

public interface IReviewService
{
    public Task<ReviewVM> GetReviewAsync(Guid id);
    public Task<PagedList<ReviewVM>> GetReviewsAsync(ReviewCriteria criteria);
    public Task<ReviewVM> AddReviewAsync(ReviewModel model);
    public Task<ReviewVM> UpdateReviewAsync(Guid reviewId, ReviewEM model);
    public Task<ReviewVM> GetReviewByProposalIdAsync(Guid proposalId);
    public Task<PagedList<ReviewVM>> GetReviewsByAccountIdAsync(Guid accountId, PagedCriteria criteria);
    public Task<PagedList<ReviewVM>> GetReviewsByServiceIdAsync(Guid serviceId, PagedCriteria criteria);
    public Task DeleteReviewAsync(Guid id);
}
