using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Models;

public class ProposalModel
{
    [Required]
    public Guid OrdererId { get; set; }
    [Required]
    public Guid ServiceId { get; set; }
    [Required]
    [MaxLength(150)]
    public string ProjectTitle { get; set; } = default!;
    [Required]
    [MaxLength(255)]
    public string Category { get; set; } = default!;
    [Required]
    [MaxLength(1000)]
    public string Description { get; set; } = default!;
    [Required]
    public DateTime TargetDelivery { get; set; }
    public int NumberOfConcept { get; set; } = 1;
    public int NumberOfRevision { get; set; } = 0;
    [Required]
    public double InitialPrice { get; set; }
    [Required]
    public double Total { get; set; }
    public ProposalStateEnum ProposalStatus { get; set; } = ProposalStateEnum.Waiting;
}

public class ProposalVM
{
    public Guid Id { get; set; }
    public Guid OrdererId { get; set; }
    public Guid ChatBoxId { get; set; }
    public Guid ServiceId { get; set; }
    public string ProjectTitle { get; set; } = default!;
    public string Category { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime TargetDelivery { get; set; }
    public DateTime? ActualDelivery { get; set; } //ngay hoan thanh thuc te
    public int NumberOfConcept { get; set; }
    public int NumberOfRevision { get; set; }
    public double InitialPrice { get; set; }
    public double Total { get; set; }
    public string ProposalStatus { get; set; } = default!;
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public bool IsReviewed { get; set; } = false;
    public AccountBasicInfoVM Creator { get; set; } = default!;
    public AccountBasicInfoVM Orderer { get; set; } = default!;
}

public class ProposalUpdateStatusModel
{
    [Required]
    public ProposalStateEnum Status { get; set; }
}
