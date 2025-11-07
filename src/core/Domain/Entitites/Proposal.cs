using Domain.Entities.Commons;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entitites;

public class Proposal : BaseEntity, ICreation
{
    public Guid ServiceId { get; set; }
    [MaxLength(150)]
    public string ProjectTitle { get; set; } = default!;
    [MaxLength(255)]
    public string Category { get; set; } = default!;
    [MaxLength(1000)]
    public string Description { get; set; } = default!;
    public DateTime TargetDelivery { get; set; } // ngay hoan thanh du kien
    public DateTime? ActualDelivery { get; set; } //ngay hoan thanh thuc te
    public int NumberOfConcept { get; set; } = default!;
    public int NumberOfRevision { get; set; } = default!;
    public double InitialPrice { get; set; }
    public double Total { get; set; }
    public ProposalStateEnum ProposalStatus { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid OrdererId { get; set; }

    public virtual Account Account { get; set; } = default!;
    public virtual Account Orderer { get; set; } = default!;
    public virtual Service Service { get; set; } = default!;
    public virtual ICollection<ProposalAsset> ProposalAssets { get; set; } = new List<ProposalAsset>();
    public virtual Review? Review { get; set; }
    public virtual ICollection<TransactionHistory> TransactionHistories { get; set; } = new List<TransactionHistory>();
    public virtual ICollection<Milestone> Milestones { get; set; } = new List<Milestone>();
    public virtual Message MessageObj { get; set; } = default!;
}
