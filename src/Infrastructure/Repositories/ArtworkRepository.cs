using Application.Services.Abstractions;
using CoenM.ImageHash;
using Domain.Entities.Commons;
using Domain.Entitites;
using Domain.Enums;
using Domain.Repositories.Abstractions;
using Infrastructure.Database;
using Infrastructure.Repositories.Commons;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace Infrastructure.Repositories;
public class ArtworkRepository : GenericAuditableRepository<Artwork>, IArtworkRepository
{
    private readonly static int MIN_SIMILARITY = 95;
    public ArtworkRepository(AppDBContext dBContext, IClaimService claimService) : base(dBContext, claimService)
    {
    }

    public async Task<List<Artwork>> GetAllArtworks()
    {
        var allArtworks = await _dbContext.Artworks
            .Include(a => a.Account)
            .Include(l => l.LicenseType)
            .Include(c => c.CategoryArtworkDetails)
                .ThenInclude(c => c.Category)
            .Include(t => t.TagDetails)
                .ThenInclude(t => t.Tag)
            .Include(s => s.SoftwareDetails)
                .ThenInclude(s => s.SoftwareUsed)
            .Include(s => s.Assets)
            //.Where(a => a.DeletedOn == null && a.State == StateEnum.Accepted && a.Privacy == PrivacyEnum.Public)
            .ToListAsync();

        return allArtworks;
    }
    public async Task<IPagedList<Artwork>> GetArtworksAsync(
        string? keyword, string? sortColumn, string? sortOrder, int page, int pageSize,
        Guid? accountId = null, Guid? categoryId = null, Guid? tagId = null,
        StateEnum? state = null, PrivacyEnum? privacy = null)
    {
        Guid? loginId = _claimService.GetCurrentUserId;
        string loginRole = _claimService.GetCurrentRole;

        var allArtworks = _dbContext.Artworks
            .Include(a => a.Account)
            .Include(l => l.LicenseType)
            .Include(c => c.CategoryArtworkDetails)
                .ThenInclude(c => c.Category)
            .Include(t => t.TagDetails)
                .ThenInclude(t => t.Tag)
            .Include(s => s.SoftwareDetails)
                .ThenInclude(s => s.SoftwareUsed)
            .Where(a => a.DeletedOn == null);

        // if user is not login, only show public artworks
        // if user is login, role is common user, and accountId is not owned artworks, only show public artworks
        if (loginId == null || (loginId != null && loginRole.Equals(RoleEnum.CommonUser.ToString()) && loginId != accountId))
        {
            allArtworks = allArtworks.Where(a => a.State == StateEnum.Accepted && a.Privacy == PrivacyEnum.Public);
        }

        if (accountId != null)
        {
            allArtworks = allArtworks.Where(a => a.CreatedBy == accountId);
        }

        if (categoryId != null)
        {
            allArtworks = allArtworks
                .Where(a => a.CategoryArtworkDetails
                .Any(c => c.CategoryId == categoryId));
        }

        if (tagId != null)
        {
            allArtworks = allArtworks
                .Where(a => a.TagDetails
                               .Any(t => t.TagId == tagId));
        }

        if (state != null)
        {
            allArtworks = allArtworks.Where(a => a.State == state);
        }

        if (privacy != null)
        {
            allArtworks = allArtworks.Where(a => a.Privacy == privacy);
        }

        if (!string.IsNullOrEmpty(keyword))
        {
            keyword = keyword.ToLower();
            allArtworks = allArtworks.Where(a => a.Title.ToLower().Contains(keyword)
            || (a.Description != null && a.Description.ToLower().Contains(keyword)));
        }

        #region sorting
        Expression<Func<Artwork, object>> orderBy = sortColumn?.ToLower() switch
        {
            "view" => a => a.ViewCount,
            "create" => a => a.CreatedOn,
            "comment" => a => a.CommentCount,
            "like" => a => a.LikeCount,
            _ => a => a.CreatedOn,
        };

        if (sortOrder?.ToLower() == "asc")
        {
            allArtworks = allArtworks.OrderBy(orderBy);
        }
        else
        {
            allArtworks = allArtworks.OrderByDescending(orderBy);
        }
        #endregion

        #region paging
        var result = await ToPaginationAsync(allArtworks, page, pageSize);
        #endregion

        return result;
    }

    public async Task<IPagedList<Artwork>> GetArtworksContainAssetsAsync(Guid? accountId, int? minPrice, int? maxPrice,
        string? keyword, string? sortColumn, string? sortOrder, int page, int pageSize)
    {
        var allArtworks = _dbContext.Artworks
            .Include(a => a.Assets)
            .Include(a => a.Account)
            .Where(a => a.DeletedOn == null && a.Assets.Any());

        allArtworks = allArtworks.OrderByDescending(x => x.CreatedOn);

        #region paging
        var result = await ToPaginationAsync(allArtworks, page, pageSize);
        #endregion

        return result;
    }

    public async Task<IPagedList<Artwork>> GetArtworksContainAssetsOfAccountAsync(Guid creatorId, int page, int pageSize,
        StateEnum? state = null, PrivacyEnum? privacy = null)
    {
        var allArtworks = _dbContext.Artworks
            .Include(a => a.Assets.Where(a => a.DeletedOn == null))
            .Where(a => a.CreatedBy == creatorId && a.DeletedOn == null && a.Assets.Where(a => a.DeletedOn == null).Any());

        if (state != null)
        {
            allArtworks = allArtworks.Where(a => a.State == state);
        }
        else
        {
            allArtworks = allArtworks.Where(a => a.State == StateEnum.Accepted);
        }
        if (privacy != null)
        {
            allArtworks = allArtworks.Where(a => a.Privacy == privacy);
        }
        else
        {
            allArtworks = allArtworks.Where(a => a.Privacy == PrivacyEnum.Public);
        }

        allArtworks = allArtworks.OrderByDescending(x => x.CreatedOn);

        #region paging
        var result = await ToPaginationAsync(allArtworks, page, pageSize);
        #endregion

        return result;
    }

    public async Task<IPagedList<Artwork>> GetArtworksForModerationAsync(
        string? keyword, string? sortColumn, string? sortOrder, int page,
        int pageSize, Guid? accountId = null, Guid? categoryId = null,
        Guid? tagId = null, StateEnum? state = null, PrivacyEnum? privacy = null)
    {

        var allArtworks = _dbContext.Artworks
            .Include(a => a.Account)
            .Include(l => l.LicenseType)
            .Include(c => c.CategoryArtworkDetails)
                .ThenInclude(c => c.Category)
            .Include(t => t.TagDetails)
                .ThenInclude(t => t.Tag)
            .Include(s => s.SoftwareDetails)
                .ThenInclude(s => s.SoftwareUsed)
            .Where(a => a.DeletedOn == null);

        if (accountId != null)
        {
            allArtworks = allArtworks.Where(a => a.CreatedBy == accountId);
        }

        if (categoryId != null)
        {
            allArtworks = allArtworks
                .Where(a => a.CategoryArtworkDetails
                .Any(c => c.CategoryId == categoryId));
        }

        if (tagId != null)
        {
            allArtworks = allArtworks
                .Where(a => a.TagDetails
                               .Any(t => t.TagId == tagId));
        }

        if (state != null)
        {
            allArtworks = allArtworks.Where(a => a.State == state);
        }

        if (privacy != null)
        {
            allArtworks = allArtworks.Where(a => a.Privacy == privacy);
        }

        if (!string.IsNullOrEmpty(keyword))
        {
            keyword = keyword.ToLower();
            allArtworks = allArtworks.Where(a => a.Title.ToLower().Contains(keyword)
            || (a.Description != null && a.Description.ToLower().Contains(keyword)));
        }

        #region sorting
        Expression<Func<Artwork, object>> orderBy = sortColumn?.ToLower() switch
        {
            "view" => a => a.ViewCount,
            "create" => a => a.CreatedOn,
            "comment" => a => a.CommentCount,
            "like" => a => a.LikeCount,
            _ => a => a.CreatedOn,
        };

        if (sortOrder?.ToLower() == "asc")
        {
            allArtworks = allArtworks.OrderBy(orderBy);
        }
        else
        {
            allArtworks = allArtworks.OrderByDescending(orderBy);
        }
        #endregion

        #region paging
        var result = await ToPaginationAsync(allArtworks, page, pageSize);
        #endregion

        return result;
    }

    public async Task<Artwork?> GetArtworkDetailByIdAsync(Guid artworkId)
    {
        return await _dbContext.Artworks
            .Include(a => a.Account)
            .Include(l => l.LicenseType)
            .Include(i => i.Images)
            .Include(a => a.Assets.Where(a => a.DeletedOn == null))
            .Include(c => c.Comments)
            .Include(a => a.TagDetails)
                .ThenInclude(t => t.Tag)
            .Include(a => a.CategoryArtworkDetails)
                .ThenInclude(c => c.Category)
            .Include(s => s.SoftwareDetails)
                .ThenInclude(s => s.SoftwareUsed)
            .FirstOrDefaultAsync(a => a.Id == artworkId);
    }

    public async Task<List<Image>> GetArtworksDuplicateAsync(Guid artworkId)
    {
        var artwork = await _dbContext.Artworks
            .Include(x => x.Images)
            .Where(x => x.Id == artworkId).FirstOrDefaultAsync();
        var createdByOfArtwork = artwork!.CreatedBy;
        //x.Artwork.State == Domain.Enums.StateEnum.Accepted
        var listImage = await _dbContext.Images
            .Include(x => x.Artwork)
            .Where(x => x.Artwork.CreatedBy != createdByOfArtwork).ToListAsync();
        List<Image> result2 = new();
        foreach (Image item in listImage)
        {
            foreach (Image image in artwork.Images)
            {
                var similarity = CompareHash.Similarity(item.ImageHash.GetValueOrDefault(), image.ImageHash.GetValueOrDefault());
                if (similarity >= MIN_SIMILARITY)
                {
                    item.Similarity = similarity;
                    result2.Add(item);
                }
                if (result2.Count > 10)
                {
                    break;
                }
            }
        }
        return result2;
    }

    public async Task<IPagedList<Artwork>> GetArtworksOfFollowingsAsync(Guid followerId, int page, int pageSize)
    {
        var followingAccountIds = _dbContext.Follows
            .Where(f => f.FollowingId == followerId)
            .Select(f => f.FollowedId)
            .ToList();

        var allArtworks = _dbContext.Artworks
            .Include(a => a.Account)
            .Include(c => c.CategoryArtworkDetails)
                .ThenInclude(c => c.Category)
            .Include(t => t.TagDetails)
                .ThenInclude(t => t.Tag)
            .Where(a => a.State == StateEnum.Accepted && a.Privacy == PrivacyEnum.Public
                && a.DeletedOn == null && followingAccountIds.Contains(a.CreatedBy ?? default));


        // sorting
        allArtworks = allArtworks.OrderByDescending(x => x.CreatedOn);

        //paging
        var result = await ToPaginationAsync(allArtworks, page, pageSize);

        return result;
    }

    public async Task IncreaseCommentCountAsync(Guid artworkId)
    {
        var artWork = await _dbContext.Artworks.FindAsync(artworkId);
        if (artWork == null)
        {
            throw new KeyNotFoundException("ArtworkId not found!");
        }
        artWork.CommentCount++;
        this.Update(artWork);
    }

    public async Task DecreaseCommentCountAsync(Guid artworkId)
    {
        var artWork = await _dbContext.Artworks.FindAsync(artworkId);
        if (artWork == null)
        {
            throw new KeyNotFoundException("ArtworkId not found!");
        }
        artWork.CommentCount--;
        this.Update(artWork);
    }

}
