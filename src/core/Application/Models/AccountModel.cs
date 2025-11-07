using Domain.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Application.Models;

public class AccountModel
{
    [Required]
    [MaxLength(255)]
    public string? Username { get; set; }
    [Required]
    [MaxLength(255)]
    public string? Email { get; set; }
    [Required]
    [MaxLength(255)]
    public string? Fullname { get; set; }
    public string? Avatar { get; set; }
    [Required]
    [MaxLength(300)]
    public string? Bio { get; set; }
    [Birthdate]
    public DateTime? Birthdate { get; set; }
}

public class AccountChangePasswordModel
{
    [Required]
    [MaxLength(255)]
    public string OldPassword { get; set; } = default!;
    [Required]
    [MaxLength(255)]
    public string NewPassword { get; set; } = default!;
}