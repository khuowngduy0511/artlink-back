using Domain.Entities.Commons;
using Domain.Entitites;
using Domain.Enums;

namespace Domain.Repositories.Abstractions;
public interface IReportRepository : IGenericRepository<Report>
{
    Task<IPagedList<Report>> GetAllReportsAsync(ReportEntityEnum? reportEntity, StateEnum? state, string? keyword, string? sortColumn, string? sortOrder, int page, int pageSize);

}
