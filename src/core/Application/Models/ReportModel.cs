using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Models;

public class ReportModel
{
    [Required]
    public ReportTypeEnum ReportType { get; set; } = default!;
    [MaxLength(500), MinLength(10)]
    public string Reason { get; set; } = default!;
    public ReportEntityEnum ReportEntity { get; set; } = default!;
    [Required]
    public Guid TargetId { get; set; }
}
public class ReportStateEM
{
    [Required]
    public StateEnum State { get; set; } = default!;
    [MaxLength(500)]
    public string? Note { get; set; }
}