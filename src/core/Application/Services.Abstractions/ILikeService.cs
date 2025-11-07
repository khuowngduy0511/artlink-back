using Application.Models;
using Domain.Entitites;

namespace Application.Services.Abstractions;
public interface ILikeService
{
    Task CreateLikeAsync(LikeModel likeModel);
    Task DeleteLikeAsync(Guid artworkId);
    Task<AccountLikeVM> GetAllLikesOfArtworkAsync(Guid artworkId);
    Task<ArtworkLikeVM> GetAllLikesOfAccountAsync(Guid accountId);
    Task<Like?> GetLikeByIdAsync(Guid accountId, Guid artworkId);
    Task<IsLikedVM> GetIsLikedArtworkAsync(Guid accountId, Guid artworkId);
    Task<List<IsLikedVM>> GetListIsLikedArtworkAsync(Guid accountId, List<Guid> artworkIds);
}
