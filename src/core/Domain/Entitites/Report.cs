using Domain.Entities.Commons;
using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entitites;
public class Report : BaseEntity, ICreation
{
    [Required]
    public ReportTypeEnum ReportType { get; set; }
    [MaxLength(500)]
    public string Reason { get; set; } = default!;
    [Required]
    public ReportEntityEnum ReportEntity { get; set; }
    public StateEnum State { get; set; } = default!;
    [Required]
    public Guid TargetId { get; set; }
    [NotMapped]
    public object? Target { get; set; } = default!;
    [MaxLength(500)]
    public string? Note { get; set; } = default!; //luu ly do tu choi hoac chap thuan
    public Guid? CreatedBy { get; set; }
    public virtual Account AccountReport { get; set; } = default!;
    public DateTime CreatedOn { get; set; }
}
