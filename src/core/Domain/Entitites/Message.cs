using Domain.Entities.Commons;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entitites;

public class Message : BaseEntity, ICreation
{
    public Guid ChatBoxId { get; set; }
    [MaxLength(1000)]
    public string? Text { get; set; }     // text, fileLocation, RequestId & ProposalId can not null at the same time
    public string? FileLocation { get; set; }
    public string? FileName { get; set; }
    public Guid? RequestId { get; set; }
    public Guid? ProposalId { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }

    public virtual Account Account { get; set; } = default!;
    public virtual ChatBox ChatBox { get; set; } = default!;
    public virtual Request? Request { get; set; }
    public virtual Proposal? Proposal { get; set; }
}
