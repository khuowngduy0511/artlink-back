using Application.Services.Abstractions;
using Domain.Entities.Commons;
using Domain.Entitites;
using Domain.Enums;
using Domain.Repositories.Abstractions;
using Infrastructure.Database;
using Infrastructure.Repositories.Commons;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;
public class ReportRepository : GenericCreationRepository<Report>, IReportRepository
{
    public ReportRepository(AppDBContext dBContext, IClaimService claimService) : base(dBContext, claimService)
    {
    }

    public async Task<IPagedList<Report>> GetAllReportsAsync(ReportEntityEnum? reportEntity, StateEnum? state, string? keyword, string? sortColumn, string? sortOrder, int page, int pageSize)
    {
        var allReports = _dbContext.Reports.Include(x => x.AccountReport).AsQueryable();

        if (reportEntity != null)
        {
            allReports = allReports.Where(r => r.ReportEntity == reportEntity);
        }

        if (state != null)
        {
            allReports = allReports.Where(r => r.State == state);
        }

        if (!string.IsNullOrEmpty(keyword))
        {
            keyword = keyword.ToLower();
            allReports = allReports.Where(r => r.Reason.ToLower().Contains(keyword));
        }

        #region sorting
        Expression<Func<Report, object>> orderBy = sortColumn?.ToLower() switch
        {
            "create" => r => r.CreatedOn,
            _ => r => r.CreatedOn,
        };

        if (sortOrder?.ToLower() == "asc")
        {
            allReports = allReports.OrderBy(orderBy);
        }
        else
        {
            allReports = allReports.OrderByDescending(orderBy);
        }
        #endregion

        var pagedResult = await ToPaginationAsync(allReports, page, pageSize);

        pagedResult.Items.ForEach(r => r.Target = r.ReportEntity switch
        {
            ReportEntityEnum.Account => _dbContext.Accounts.Find(r.TargetId),
            ReportEntityEnum.Comment => _dbContext.Comments.Find(r.TargetId),
            ReportEntityEnum.Artwork => _dbContext.Artworks.Find(r.TargetId),
            _ => null
        });

        return pagedResult;

    }
}
