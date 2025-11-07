using System.ComponentModel.DataAnnotations;

namespace Application.Models;

public class LoginModel
{
    [Required]
    public string Username { get; set; } = default!;
    [Required]
    public string Password { get; set; } = default!;
}
