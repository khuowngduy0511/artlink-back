namespace Application.Models;

/// <summary>
/// View Model for Withdrawal Request
/// </summary>
public class WithdrawalRequestVM
{
    public Guid Id { get; set; }
    public Guid WalletId { get; set; }
    public double Amount { get; set; }
    public string BankCode { get; set; } = default!;
    public string BankName { get; set; } = default!;
    public string BankAccountNumber { get; set; } = default!;
    public string? BankAccountName { get; set; }
    public string Status { get; set; } = default!;
    public string? AdminNote { get; set; }
    public Guid? ProcessedBy { get; set; }
    public DateTime? ProcessedOn { get; set; }
    public double WalletBalanceSnapshot { get; set; }
    public DateTime CreatedOn { get; set; }
}

/// <summary>
/// Edit Model for creating Withdrawal Request
/// </summary>
public class CreateWithdrawalRequestEM
{
    public double Amount { get; set; }
}

/// <summary>
/// Edit Model for Admin to process Withdrawal Request
/// </summary>
public class ProcessWithdrawalRequestEM
{
    public string Status { get; set; } = default!; // Approved, Rejected, Completed
    public string? AdminNote { get; set; }
}
