using Domain.Entities.Commons;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entitites;

public class WithdrawalRequest : BaseEntity
{
    public Guid WalletId { get; set; }
    public virtual Wallet Wallet { get; set; } = default!;

    [Required]
    public double Amount { get; set; }

    [MaxLength(50)]
    public string BankCode { get; set; } = default!;

    [MaxLength(200)]
    public string BankName { get; set; } = default!;

    [MaxLength(50)]
    public string BankAccountNumber { get; set; } = default!;

    [MaxLength(200)]
    public string? BankAccountName { get; set; }

    [MaxLength(20)]
    public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected, Completed

    [MaxLength(500)]
    public string? AdminNote { get; set; }

    public Guid? ProcessedBy { get; set; }

    public DateTime? ProcessedOn { get; set; }

    public double WalletBalanceSnapshot { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
