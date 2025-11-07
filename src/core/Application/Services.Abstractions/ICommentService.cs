using Application.Commons;
using Application.Filters;
using Application.Models;
using Domain.Entitites;

namespace Application.Services.Abstractions;

public interface ICommentService
{
    Task<Comment> AddCommentAsync(Guid artworkId, string commentText);
    Task DeleteCommentAsync(Guid commentId);
    Task EditCommentAsync(Guid commentId, string newCommentText);
    Task<CommentVM?> GetCommentByIdAsync(Guid commentId);
    Task<List<CommentVM>> GetCommentsByArtworkAsync(Guid artworkId);
    Task<List<CommentVM>> GetCommentsByArtworkWithRepliesAsync(Guid artworkId);
    Task<PagedList<CommentVM>> GetCommentsByArtworkWithRepliesPaginationAsync(Guid artworkId, PagedCriteria pagecriteria);
    Task<List<Comment>> GetReplyCommentsAsync(Guid commentId);
    Task<Comment> ReplyCommentAsync(Guid commentId, string replyCommentText);
}
