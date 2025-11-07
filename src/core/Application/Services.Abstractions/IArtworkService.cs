using Application.Filters;
using Application.Models;
using Domain.Entities.Commons;

namespace Application.Services.Abstractions;
public interface IArtworkService
{
    Task<ArtworkVM?> GetArtworkByIdAsync(Guid artworkId);
    Task<ArtworkDetailModerationVM?> GetArtworkByIdForModerationAsync(Guid artworkId);
    Task<List<ArtworkVM>> GetAllArtworksAsync();
    Task<IPagedList<ArtworkPreviewVM>> GetArtworksAsync(ArtworkCriteria criteria);
    Task<IPagedList<ArtworksV2>> SearchArtworksWithElasticSearchAsync(ArtworkElasticCriteria criteria);
    Task<IPagedList<ArtworksV2>> GetRecommenedArtworkAsync(RecommendedArtworkCriteria criteria);
    Task<IPagedList<ArtworkModerationVM>> GetAllArtworksForModerationAsync(ArtworkModerationCriteria criteria);
    Task<IPagedList<ArtworkPreviewVM>> GetAllArtworksByAccountIdAsync(Guid accountId, ArtworkModerationCriteria criteria);
    Task<IPagedList<ArtworkPreviewVM>> GetArtworksOfFollowingsAsync(PagedCriteria criteria);
    Task<IPagedList<ArtworkContainAssetVM>> GetArtworksContainAssetsOfAccountAsync(Guid accountId, PagedCriteria criteria);
    Task<List<ImageDuplicationVM>> GetArtworksDuplicateAsync(Guid artworkId);
    Task<ArtworkVM> AddArtworkAsync(ArtworkModel artwork);
    Task UpdateArtworkAsync(Guid artworkId, ArtworkEM artworkEM);
    Task UpdateArtworkStateAsync(Guid artworkId, ArtworkStateEM model);
    Task DeleteArtworkAsync(Guid artworkId);
    Task SoftDeleteArtworkAsync(Guid artworkId);
}
