using Domain.Entities.Commons;

namespace Domain.Entitites;

public class Milestone : BaseEntity, ICreation
{
    public Guid ProposalId { get; set; }
    public string MilestoneName { get; set; } = default!;
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }

    public virtual Proposal Proposal { get; set; } = default!;
    public virtual Account CreatedAccount { get; set; } = default!;
}
