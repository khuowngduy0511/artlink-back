using Domain.Enums;

namespace Application.Filters;

public class ReportCriteria : BaseCriteria
{
    public ReportEntityEnum? ReportEntity { get; set; }
    public StateEnum? State { get; set; }
}
