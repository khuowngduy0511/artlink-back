using Domain.Entities.Commons;
using Domain.Enums;

namespace Domain.Entitites;

public class ProposalAsset : BaseEntity, ICreation
{
    public Guid ProposalId { get; set; }
    public ProposalAssetEnum Type { get; set; }
    public string ProposalAssetName { get; set; } = default!;
    public string Location { get; set; } = default!;
    public string ContentType { get; set; } = default!; // loai duoi file (rar, zip)
    public ulong Size { get; set; } = default!; // dung luong asset, don vi byte
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }

    public virtual Proposal Proposal { get; set; } = default!;
}
