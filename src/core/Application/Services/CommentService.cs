using Application.Commons;
using Application.Filters;
using Application.Models;
using Application.Services.Abstractions;
using AutoMapper;
using Domain.Entitites;
using Domain.Enums;
using Domain.Repositories.Abstractions;

namespace Application.Services;

public class CommentService : ICommentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClaimService _claimService;
    private readonly INotificationService _notificationService;
    private readonly IMapper _mapper;

    public CommentService(
        IUnitOfWork unitOfWork, 
        IClaimService claimService,
        IMapper mapper,
        INotificationService notificationService)
    {
        _unitOfWork = unitOfWork;
        _claimService = claimService;

        _mapper = mapper;
        _notificationService = notificationService;
    }
    public async Task<Comment> AddCommentAsync(Guid artworkId, string commentText)
    {
        var currentUserId = _claimService.GetCurrentUserId ?? default;
        var currentUsername = _claimService.GetCurrentUserName ?? default;

        var artwork = await _unitOfWork.ArtworkRepository.GetByIdAsync(artworkId) 
            ?? throw new KeyNotFoundException("Không tìm thấy tác phẩm.");
        var comment = new Comment
        {
            ArtworkId = artworkId,
            Content = commentText
        };
        await _unitOfWork.CommentRepository.AddAsync(comment);
        // update artwork comment count
        await _unitOfWork.ArtworkRepository.IncreaseCommentCountAsync(artworkId);

        await _unitOfWork.SaveChangesAsync();

        // add new notification
        var notification = new NotificationModel
        {
            SentToAccount = artwork.CreatedBy!.Value,
            Content = $"Người dùng [{currentUsername}] bình luận tác phẩm [{artwork.Title}]",
            NotifyType = NotifyTypeEnum.Information,
            ReferencedAccountId = currentUserId,
            ReferencedArtworkId = artworkId
        };
        await _notificationService.AddNotificationAsync(notification);

        return comment;
    }

    public async Task DeleteCommentAsync(Guid commentId)
    {
        // theo co che xoa comment cua facebook
        var comment = await _unitOfWork.CommentRepository.GetByIdAsync(commentId);
        if (comment == null || comment.DeletedOn != null)
        {
            throw new KeyNotFoundException("Không tìm thấy bình luận.");
        }
        // check authorize
        if (_claimService.GetCurrentRole.Equals(RoleEnum.CommonUser.ToString())
                && _claimService.GetCurrentUserId != comment.CreatedBy)
        {
            throw new UnauthorizedAccessException();
        }
        _unitOfWork.CommentRepository.SoftDelete(comment);
        // update artwork comment count
        await _unitOfWork.ArtworkRepository.DecreaseCommentCountAsync(comment.ArtworkId);
        _unitOfWork.CommentRepository.SoftDeleteReplies(commentId);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task EditCommentAsync(Guid commentId, string newCommentText)
    {
        var comment = await _unitOfWork.CommentRepository.GetByIdAsync(commentId);
        if (comment == null || comment.DeletedOn != null)
        {
            throw new KeyNotFoundException("Không tìm thấy bình luận.");
        }
        // edit comment
        comment.Content = newCommentText;
        _unitOfWork.CommentRepository.Update(comment);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<List<CommentVM>> GetCommentsByArtworkAsync(Guid artworkId)
    {
        var comments = await _unitOfWork.CommentRepository.GetCommentsWithRepliesAsync(artworkId);
        
        // Filter: Only get parent comments (ReplyId == null) and filter deleted replies
        var parentComments = comments
            .Where(c => c.ReplyId == null)
            .ToList();
        
        // Filter deleted replies for each parent comment
        foreach (var comment in parentComments)
        {
            if (comment.Replies != null)
            {
                comment.Replies = comment.Replies.Where(r => r.DeletedOn == null).ToList();
            }
        }
        
        var commentVM = _mapper.Map<List<CommentVM>>(parentComments);
        return commentVM;
    }

    public async Task<List<CommentVM>> GetCommentsByArtworkWithRepliesAsync(Guid artworkId)
    {
        var comments = await _unitOfWork.CommentRepository.GetCommentsWithRepliesAsync(artworkId);
        
        // Filter: Only get parent comments (ReplyId == null) and filter deleted replies
        var parentComments = comments
            .Where(c => c.ReplyId == null)
            .ToList();
        
        // Filter deleted replies for each parent comment
        foreach (var comment in parentComments)
        {
            if (comment.Replies != null)
            {
                comment.Replies = comment.Replies.Where(r => r.DeletedOn == null).ToList();
            }
        }
        
        var commentVMs = _mapper.Map<List<CommentVM>>(parentComments);
        return commentVMs;
    }

    public async Task<List<Comment>> GetReplyCommentsAsync(Guid commentId)
    {
        var comment = await _unitOfWork.CommentRepository.GetByIdAsync(commentId);
        if (comment == null || comment.DeletedOn != null)
        {
            throw new KeyNotFoundException("Không tìm thấy bình luận.");
        }
        return await _unitOfWork.CommentRepository.GetReplyCommentsAsync(commentId);
    }

    public async Task<CommentVM?> GetCommentByIdAsync(Guid commentId)
    {
        var comment = await _unitOfWork.CommentRepository.GetCommentById(commentId);
        if (comment == null || comment.DeletedOn != null)
        {
            return null;
        }
        
        // Filter deleted replies
        if (comment.Replies != null)
        {
            comment.Replies = comment.Replies.Where(r => r.DeletedOn == null).ToList();
        }
        
        return _mapper.Map<CommentVM?>(comment);
    }

    public async Task<Comment> ReplyCommentAsync(Guid commentId, string replyCommentText)
    {
        var comment = await _unitOfWork.CommentRepository.GetByIdAsync(commentId);
        if (comment == null || comment.DeletedOn != null)
        {
            throw new KeyNotFoundException("Không tìm thấy bình luận.");
        }
        // reply comment
        var replyComment = new Comment
        {
            ReplyId = commentId,
            ArtworkId = comment.ArtworkId,
            Content = replyCommentText
        };
        await _unitOfWork.CommentRepository.AddAsync(replyComment);
        // update artwork comment count
        await _unitOfWork.ArtworkRepository.IncreaseCommentCountAsync(comment.ArtworkId);
        await _unitOfWork.SaveChangesAsync();
        return (replyComment);
    }

    public async Task<PagedList<CommentVM>> GetCommentsByArtworkWithRepliesPaginationAsync(Guid artworkId, PagedCriteria pagecriteria)
    {
        var comments = await _unitOfWork.CommentRepository
            .GetCommentsWithRepliesPaginationAsync(artworkId,
                                            pagecriteria.PageNumber, pagecriteria.PageSize);
        
        // Filter: Only get parent comments (ReplyId == null) and filter deleted replies
        var filteredComments = comments.Items
            .Where(c => c.ReplyId == null)
            .ToList();
        
        // Filter deleted replies for each parent comment
        foreach (var comment in filteredComments)
        {
            if (comment.Replies != null)
            {
                comment.Replies = comment.Replies.Where(r => r.DeletedOn == null).ToList();
            }
        }
        
        var commentVMs = _mapper.Map<List<CommentVM>>(filteredComments);
        return new PagedList<CommentVM>(
            commentVMs,
            filteredComments.Count, // Update total count to reflect filtered parent comments
            comments.PageNumber,
            comments.PageSize);
    }
}
