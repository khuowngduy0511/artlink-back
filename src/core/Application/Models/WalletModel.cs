using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Models;

public class WalletModel
{
    public double Balance { get; set; } = 0;
    [Required]
    public WithdrawMethodEnum WithdrawMethod { get; set; }
    [Required]
    public string WithdrawInformation { get; set; } = default!;
}

public class WalletEM
{
    public WithdrawMethodEnum WithdrawMethod { get; set; } = WithdrawMethodEnum.BankAccount;
    [Required]
    public string WithdrawInformation { get; set; } = default!;
    public string? BankCode { get; set; }
    public string? BankName { get; set; }
}