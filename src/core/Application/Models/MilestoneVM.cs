
namespace Application.Models;

public class MilestoneVM
{
    public Guid Id { get; set; }
    public Guid ProposalId { get; set; }
    public string MilestoneName { get; set; } = default!;
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public AccountBasicInfoVM CreatedAccount { get; set; } = default!;
}
