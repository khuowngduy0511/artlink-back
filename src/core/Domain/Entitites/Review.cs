using Domain.Entities.Commons;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entitites;

public class Review : BaseEntity, ICreation
{
    public Guid ProposalId { get; set; }
    public double Vote { get; set; }
    [MaxLength(150)]
    public string Detail { get; set; } = string.Empty;
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }

    public virtual Account Account { get; set; } = default!;
    public virtual Proposal Proposal { get; set; } = default!;
}
