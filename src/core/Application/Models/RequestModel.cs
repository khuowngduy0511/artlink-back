using System.ComponentModel.DataAnnotations;

namespace Application.Models;
public class RequestModel
{
    [Required]
    public Guid ServiceId { get; set; } = default!;
    [MaxLength(255)]
    [Required]
    public string Message { get; set; } = default!;
    [MaxLength(150)]
    [Required]
    public string Timeline { get; set; } = default!;
    [Required]
    [Range(10000f, 10000000f, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
    public double Budget { get; set; } = default!;
}
