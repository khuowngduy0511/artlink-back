using System.ComponentModel.DataAnnotations;

namespace Application.Models;
public class RequestVM
{
    public Guid Id { get; set; }
    public Guid ServiceId { get; set; }
    public Guid ChatBoxId { get; set; }
    [MaxLength(255)]
    public string Message { get; set; } = default!;
    [MaxLength(150)]
    public string Timeline { get; set; } = default!;
    public double Budget { get; set; } = default!;
    public string RequestStatus { get; set; } = default!;
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }

    public AccountBasicInfoVM Account { get; set; } = default!;
    public ServiceVM Service { get; set; } = default!;
}
