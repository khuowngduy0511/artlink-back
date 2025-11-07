using Domain.Entitites;

namespace Domain.Repositories.Abstractions;

public interface IFollowRepository
{
    Task AddFollowAsync(Follow follow);
    void DeleteFollow(Follow follow);
    Task<List<Follow>> GetAllFollowersAsync(Guid followingId);
    Task<List<Follow>> GetAllFollowingsAsync(Guid followerId);
    Task<Follow?> GetByIdAsync(Guid followingId, Guid followedId);
}
