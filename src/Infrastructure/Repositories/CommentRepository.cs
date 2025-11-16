using Application.Commons;
using Application.Services.Abstractions;
using Domain.Entities.Commons;
using Domain.Entitites;
using Domain.Repositories.Abstractions;
using Infrastructure.Database;
using Infrastructure.Repositories.Commons;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class CommentRepository : GenericAuditableRepository<Comment>, ICommentRepository
{
    public CommentRepository(AppDBContext dBContext, IClaimService claimService) : base(dBContext, claimService)
    {
    }
    public async Task<List<Comment>> GetReplyCommentsAsync(Guid commentId)
    {
        return await _dbContext.Comments
            .Where(x => x.ReplyId == commentId
                && x.DeletedOn == null)
            .Include(x => x.Account)
            .OrderByDescending(x => x.CreatedOn)
            .ToListAsync();
    }
    public async Task<List<Comment>> GetCommentsWithRepliesAsync(Guid artworkId)
    {
        // Only get parent comments (ReplyId == null), then include their replies
        return await _dbContext.Comments
            .Where(x => x.ArtworkId == artworkId
                    && x.ReplyId == null  // Only parent comments
                    && x.DeletedOn == null)
            .Include(x => x.Account)
            .Include(x => x.Replies.Where(r => r.DeletedOn == null))  // Include only non-deleted replies
                .ThenInclude(r => r.Account)  // Include Account for each reply
            .OrderByDescending(x => x.CreatedOn)
            .ToListAsync();
    }

    public async Task<Comment?> GetCommentById(Guid commentId)
    {
        return await _dbContext.Comments
            .Include(x => x.Account)
            .Include(x => x.Replies.Where(r => r.DeletedOn == null))  // Include only non-deleted replies
                .ThenInclude(r => r.Account)  // Include Account for each reply
            .FirstOrDefaultAsync(x => x.Id == commentId);
    }

    public async Task<IPagedList<Comment>> GetCommentsWithRepliesPaginationAsync(Guid artworkId, int pageNumber, int pageSize)
    {
        // Only get parent comments (ReplyId == null), then include their replies
        var commentsList = _dbContext.Comments
            .Where(x => x.ArtworkId == artworkId
                    && x.ReplyId == null  // Only parent comments
                    && x.DeletedOn == null)
            .Include(x => x.Account)
            .Include(x => x.Replies.Where(r => r.DeletedOn == null))  // Include only non-deleted replies
                .ThenInclude(r => r.Account)  // Include Account for each reply
            .OrderByDescending(x => x.CreatedOn);

        var result = await ToPaginationAsync(commentsList, pageNumber, pageSize);
        return result;
    }

    public int SoftDeleteReplies(Guid commentId)
    {
        var currentUserId = _claimService.GetCurrentUserId;
        var replies = _dbContext.Comments
            .Where(x => x.ReplyId == commentId
                && x.CreatedBy == currentUserId);
        var count = 0;
        if (replies.Any())
        {
            foreach (var reply in replies)
            {
                reply.DeletedOn = CurrentTime.GetCurrentTime;
                reply.DeletedBy = currentUserId;
                count++;
            }
            _dbContext.UpdateRange(replies);
        }
        return count;
    }
}
