using Domain.Attributes;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Models;

public class RegisterModel
{
    [MaxLength(150)]
    [Required]
    public string Username { get; set; } = default!;
    [MaxLength(255)]
    [Required]
    public string Password { get; set; } = default!; // remember to hash password before save/check
    [MaxLength(255)]
    [Required]
    public string Email { get; set; } = default!;
    [Required]
    [MaxLength(150)]
    public string Fullname { get; set; } = default!;
    [Birthdate]
    public DateTime? Birthdate { get; set; } = default!;
}

public class AccountCreateModel
{
    [MaxLength(150)]
    [Required]
    public string Username { get; set; } = default!;
    [MaxLength(255)]
    [Required]
    public string Password { get; set; } = default!; // remember to hash password before save/check
    [MaxLength(255)]
    [Required]
    public string Email { get; set; } = default!;
    [Required]
    [MaxLength(150)]
    public string Fullname { get; set; } = default!;
    [Birthdate]
    public DateTime? Birthdate { get; set; } = default!;
    [Required]
    public RoleEnum Role { get; set; }
}
