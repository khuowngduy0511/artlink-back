namespace Application.Models;

public class ProposalAssetVM
{
    public Guid Id { get; set; }
    public Guid ProposalId { get; set; }
    public string Type { get; set; } = default!;
    public string ProposalAssetName { get; set; } = default!;
    public string Location { get; set; } = default!;
    public string ContentType { get; set; } = default!;
    public ulong Size { get; set; } = default!;
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
}
