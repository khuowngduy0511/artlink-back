using Application.Filters;
using Application.Models;
using Domain.Entities.Commons;
namespace Application.Services.Abstractions;

public interface IReportService
{
    Task<ReportVM?> GetReportByIdAsync(Guid reportId);
    Task<IPagedList<ReportVM>> GetAllReportsAsync(ReportCriteria criteria);
    Task<ReportVM> AddReportAsync(ReportModel reportModel);
    Task UpdateReportState(Guid reportId, ReportStateEM reportStateEM);
    Task DeleteReportAsync(Guid reportId);
}
