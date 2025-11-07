using System.ComponentModel.DataAnnotations;

namespace Application.Models;

public class ReviewVM
{
    public Guid Id { get; set; }
    public Guid ProposalId { get; set; }
    public double Vote { get; set; }
    [MaxLength(150)]
    public string Detail { get; set; } = string.Empty;
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public AccountBasicInfoVM Account { get; set; } = default!;
    public ProposalVM Proposal { get; set; } = default!;
}

