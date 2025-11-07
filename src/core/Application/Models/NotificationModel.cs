using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Models;

public class NotificationModel
{
    public Guid SentToAccount { get; set; }
    [MaxLength(255)]
    public string Content { get; set; } = default!;
    public NotifyTypeEnum NotifyType { get; set; }
    public Guid? ReferencedArtworkId { get; set; }
    public Guid? ReferencedAccountId { get; set; }
}

public class NotificationVM
{
    public Guid Id { get; set; }
    public Guid SentToAccount { get; set; }
    [MaxLength(255)]
    public string Content { get; set; } = default!;
    public NotifyTypeEnum NotifyType { get; set; }
    public bool IsSeen { get; set; } = false;
    public DateTime CreatedOn { get; set; }
    public Guid? ReferencedArtworkId { get; set; }
    public Guid? ReferencedAccountId { get; set; }
}
