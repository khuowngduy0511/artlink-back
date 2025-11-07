using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models;

public class EmailModel
{
    [Required]
    public string Email { get; set; } = default!;    
}

public class EmailVerificationModel
{
    [Required]
    public string Email { get; set; } = default!;
    [Required]
    public string VerificationCode { get; set; } = default!;
}
