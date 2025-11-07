using Domain.Entities.Commons;
using Domain.Entitites;

namespace Domain.Repositories.Abstractions;
public interface ICommentRepository : IGenericRepository<Comment>
{
    Task<List<Comment>> GetCommentsWithRepliesAsync(Guid artworkId);
    Task<IPagedList<Comment>> GetCommentsWithRepliesPaginationAsync(Guid artworkId, int pageNumber, int pageSize);
    Task<List<Comment>> GetReplyCommentsAsync(Guid commentId);
    Task<Comment?> GetCommentById(Guid commentId);

    int SoftDeleteReplies(Guid commentId);

}
