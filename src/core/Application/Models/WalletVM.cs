namespace Application.Models;

public class WalletVM
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public double Balance { get; set; } = 0;
    public string WithdrawMethod { get; set; } = default!;
    public string WithdrawInformation { get; set; } = default!;
    public string? BankCode { get; set; }
    public string? BankName { get; set; }
}
