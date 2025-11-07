using Domain.Entitites;

namespace Domain.Repositories.Abstractions;
public interface ILikeRepository
{
    Task AddLikeAsync(Like like);
    void DeleteLike(Like like);
    Task<List<Like>> GetAllLikeOfArtworkAsync(Guid artworkId);
    Task<List<Like>> GetAllLikeOfAccountAsync(Guid accountId);
    Task<Like?> GetByIdAsync(Guid accountId, Guid artworkId);
}
