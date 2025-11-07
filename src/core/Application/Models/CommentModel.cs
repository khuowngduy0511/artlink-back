using System.ComponentModel.DataAnnotations;

namespace Application.Models;

public class CommentModel
{
    [Required]
    public string CommentText { get; set; } = default!;
}

public class CommentVM
{
    public Guid Id { get; set; }
    public Guid? ReplyId { get; set; }
    public Guid ArtworkId { get; set; }
    public string Content { get; set; } = default!;
    public AccountDisplayModel CreatedBy { get; set; } = default!;
    public DateTime CreatedOn { get; set; }
    public Guid? LastModificatedBy { get; set; }
    public DateTime? LastModificatedOn { get; set; }
    public Guid? DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }
    public int ReplyCount { get; set; } = 0;
    public ICollection<CommentVM> Replies { get; set; } = new List<CommentVM>();
}

