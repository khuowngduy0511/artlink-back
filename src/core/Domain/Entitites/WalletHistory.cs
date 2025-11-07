using Domain.Entities.Commons;
using Domain.Enums;

namespace Domain.Entitites;

public class WalletHistory : BaseEntity, ICreation
{
    public double Amount { get; set; }
    public double WalletBalance { get; set; }
    public double Fee { get; set; } = 0;
    public WalletHistoryTypeEnum Type { get; set; }
    public PaymentMethodEnum PaymentMethod { get; set; } = PaymentMethodEnum.ZaloPay;
    public TransactionStatusEnum TransactionStatus { get; set; } = TransactionStatusEnum.InProgress;
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string? AppTransId { get; set; } = default!; // nho update db Unique

    public virtual Account Account { get; set; } = default!;
}
