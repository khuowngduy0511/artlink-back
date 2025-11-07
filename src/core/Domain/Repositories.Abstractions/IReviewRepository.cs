using Domain.Entities.Commons;
using Domain.Entitites;

namespace Domain.Repositories.Abstractions;
public interface IReviewRepository : IGenericRepository<Review>
{
    public Task<IPagedList<Review>> GetReviewsAsync(
        string? keyword, string? sortColumn, string? sortOrder, int page, int pageSize,
        Guid? proposalId = null, Guid? accountId = null);
    public Task<IPagedList<Review>> GetReviewsByServiceIdAsync(Guid serviceId, int page, int pageSize);
    public Task<IPagedList<Review>> GetReviewsByAccountIdAsync(Guid accountId, int page, int pageSize);
    public Task<Review?> GetReviewByProposalIdAsync(Guid proposalId);
    public Task<Review?> GetReviewDetailAsync(Guid reviewId);
}
