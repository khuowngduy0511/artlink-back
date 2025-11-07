using Application.Commons;
using Application.Services.Abstractions;
using Domain.Entities.Commons;
using Domain.Entitites;
using Domain.Repositories.Abstractions;
using Infrastructure.Database;
using Infrastructure.Repositories.Commons;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;
public class AssetRepository : GenericRepository<Asset>, IAssetRepository
{
    private IClaimService _claimService;

    public AssetRepository(AppDBContext dBContext, IClaimService claimService) : base(dBContext)
    {
        _claimService = claimService;
    }

    public override void SoftDelete(Asset asset)
    {
        asset.DeletedOn = CurrentTime.GetCurrentTime;
        asset.DeletedBy = _claimService.GetCurrentUserId;
        _dbContext.Entry(asset).State = EntityState.Modified;
    }

    public override void Update(Asset entity)
    {
        base.Update(entity);
        entity.LastModificatedBy = _claimService.GetCurrentUserId;
    }

    public override async Task<List<Asset>> GetAllUndeletedAsync()
    {
        return await _dbContext.Assets.Where(a => a.DeletedOn == null).ToListAsync();
    }

    public async Task<Asset?> GetAssetAndItsCreatorAsync(Guid assetId)
    {
        return await _dbContext.Assets
            .Include(a => a.Artwork)
            .FirstOrDefaultAsync(a => a.Id == assetId);
    }

    public async Task<IPagedList<Asset>> GetAllAssetsAsync(
        Guid? accountId, int? minPrice, int? maxPrice, string? keyword, string? sortColumn, string? sortOrder, int page, int pageSize)
    {
        var allAssets = _dbContext.Assets
            .Include(a => a.Artwork)
                .ThenInclude(a => a.Account)
            .Where(a => a.DeletedOn == null && a.Artwork.DeletedOn == null);

        if (accountId != null)
        {
            allAssets = allAssets.Where(a => a.Artwork.CreatedBy == accountId);
        }

        if (minPrice != null)
        {
            allAssets = allAssets.Where(a => a.Price >= minPrice);
        }
        if (maxPrice != null)
        {
            allAssets = allAssets.Where(a => a.Price <= maxPrice);
        }
        if (!string.IsNullOrEmpty(keyword))
        {
            keyword = keyword.ToLower();
            allAssets = allAssets.Where(a => a.AssetTitle.ToLower().Contains(keyword)
                       || (a.Description != null && a.Description.ToLower().Contains(keyword)));
        }

        #region sorting
        Expression<Func<Asset, object>> orderBy = sortColumn?.ToLower() switch
        {
            "price" => a => a.Price,
            "create" => a => a.Artwork.CreatedOn,
            _ => a => a.Artwork.CreatedOn,
        };

        if (sortOrder?.ToLower() == "asc")
        {
            allAssets = allAssets.OrderBy(orderBy);
        }
        else
        {
            allAssets = allAssets.OrderByDescending(orderBy);
        }
        #endregion

        #region paging
        var result = await ToPaginationAsync(allAssets, page, pageSize);
        #endregion

        return result;
    }
}
