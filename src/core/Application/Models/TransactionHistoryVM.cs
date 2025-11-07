using Domain.Enums;

namespace Application.Models;

public class TransactionHistoryVM
{
    public Guid Id { get; set; }
    public AccountBasicInfoVM FromAccount { get; set; } = default!;
    public AccountBasicInfoVM ToAccount { get; set; } = default!;
    public Guid? AssetId { get; set; }
    public Guid? ProposalId { get; set; }
    public string Detail { get; set; } = default!;
    public double Price { get; set; }
    public double Fee { get; set; }
    public string TransactionStatus { get; set; } = default!;
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
}

public class TransactionModel
{
    public Guid? ToAccountId { get; set; }
    public Guid? AssetId { get; set; }
    public Guid? ProposalId { get; set; }
    public string Detail { get; set; } = default!;
    public double Price { get; set; }
    public TransactionStatusEnum TransactionStatus { get; set; }
    public double WalletBalance { get; set; }
    public double Fee { get; set; } = 0;
}

public class TransactionHistoryForUserVM
{
    public Guid Id { get; set; }
    public AccountBasicInfoVM CreatedAccount { get; set; } = default!;
    public AccountBasicInfoVM OtherAccount { get; set; } = default!;
    public Guid? AssetId { get; set; }
    public Guid? ProposalId { get; set; }
    public string Detail { get; set; } = default!;
    public double Price { get; set; }
    public double WalletBalance { get; set; }
    public double Fee { get; set; }
    public string TransactionStatus { get; set; } = default!;
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
}