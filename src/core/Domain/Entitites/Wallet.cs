using Domain.Entities.Commons;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entitites;

public class Wallet : BaseEntity
{
    public Guid AccountId { get; set; }
    public double Balance { get; set; } = 0;
    public WithdrawMethodEnum WithdrawMethod { get; set; }
    
    /// <summary>
    /// Số điện thoại ZaloPay HOẶC Số tài khoản ngân hàng
    /// </summary>
    [MaxLength(150)]
    public string WithdrawInformation { get; set; } = string.Empty;
    
    /// <summary>
    /// Mã ngân hàng (VD: "970415" cho VietinBank, "970422" cho MB Bank)
    /// Chỉ dùng khi WithdrawMethod = BankAccount
    /// </summary>
    [MaxLength(50)]
    public string? BankCode { get; set; }
    
    /// <summary>
    /// Tên ngân hàng đầy đủ (VD: "Ngân hàng TMCP Công Thương Việt Nam")
    /// Chỉ dùng khi WithdrawMethod = BankAccount
    /// </summary>
    [MaxLength(200)]
    public string? BankName { get; set; }

    public virtual Account Account { get; set; } = default!;
    //public virtual ICollection<WalletHistory> WalletHistories { get; set; } = new List<WalletHistory>();
}
