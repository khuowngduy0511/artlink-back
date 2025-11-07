using Domain.Enums;

namespace Application.Models;
public class AssetTransactionVM
{
    public Guid Id { get; set; }
    public Guid AssetId { get; set; }
    public string Detail { get; set; } = default!;
    public double Price { get; set; }
    public TransactionStatusEnum TransactionStatus { get; set; }
    public DateTime CreatedOn { get; set; }

    //public virtual Account Account { get; set; } = default!;
    //public virtual Asset? Asset { get; set; }
    //public virtual Proposal? Proposal { get; set; }
}
