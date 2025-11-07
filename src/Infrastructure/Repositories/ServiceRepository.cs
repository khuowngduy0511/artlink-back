using Application.Services.Abstractions;
using Domain.Entities.Commons;
using Domain.Entitites;
using Domain.Repositories.Abstractions;
using Infrastructure.Database;
using Infrastructure.Repositories.Commons;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;
public class ServiceRepository : GenericAuditableRepository<Service>, IServiceRepository
{
    public ServiceRepository(AppDBContext dBContext, IClaimService claimService) : base(dBContext, claimService)
    {
    }

    public async Task<Service?> GetServiceByIdAsync(Guid id)
    {
        return await _dbContext.Services
            .Include(x => x.Account)
            .Include(x => x.ServiceDetails)
                .ThenInclude(x => x.Artwork)
            .Include(x => x.CategoryServiceDetails)
                .ThenInclude(x => x.Category)
            .SingleOrDefaultAsync(x => x.Id == id && x.DeletedOn == null);
    }

    public async Task<IPagedList<Service>?> GetAllServicesAsync(
        Guid? accountId, int? minPrice, int? maxPrice, string? keyword, string? sortColumn, string? sortOrder, int page, int pageSize)
    {
        var allServices = _dbContext.Services
            .Include(x => x.Account)
            .Include(x => x.ServiceDetails)
                .ThenInclude(x => x.Artwork)
            .Include(x => x.CategoryServiceDetails)
                .ThenInclude(x => x.Category)
            .Where(x => x.DeletedOn == null);
        if (accountId != null)
        {
            allServices = allServices.Where(x => x.Account.Id == accountId);
        }
        if (minPrice != null)
        {
            allServices = allServices.Where(x => x.StartingPrice >= minPrice);
        }
        if (maxPrice != null)
        {
            allServices = allServices.Where(x => x.StartingPrice <= maxPrice);
        }
        if (!string.IsNullOrEmpty(keyword))
        {
            keyword = keyword.ToLower();
            allServices = allServices.Where(x => x.Description.ToLower().Contains(keyword)
                || x.ServiceName.ToLower().Contains(keyword));
        }

        #region sorting
        Expression<Func<Service, object>> orderBy = sortColumn?.ToLower() switch
        {
            "price" => a => a.StartingPrice,
            "create" => a => a.CreatedOn,
            _ => a => a.CreatedOn,
        };

        if (sortOrder?.ToLower() == "asc")
        {
            allServices = allServices.OrderBy(orderBy);
        }
        else
        {
            allServices = allServices.OrderByDescending(orderBy);
        }
        #endregion

        #region paging
        var result = await ToPaginationAsync(allServices, page, pageSize);
        #endregion

        return result;
    }

    public async Task<double> GetAverageRatingOfServiceAsync(Guid serviceId)
    {
        var reviews = _dbContext.Reviews
            .Include(x => x.Proposal)
            .Where(x => x.Proposal.ServiceId == serviceId)
            .Select(x => x.Vote);
        if (!reviews.Any())
        {
            return 0;
        }
        return await reviews.AverageAsync();
    }
}
