using Application.Models;
using Domain.Entitites;

namespace Application.Services.Abstractions;

public interface IFollowService
{
    Task CreateFollowAsync(FollowModel model);
    Task DeleteFollowAsync(FollowModel model);
    Task<FollowingVM> GetAllFollowingsAsync(Guid followerId);
    Task<FollowerVM> GetAllFollowersAsync(Guid followingId);
    Task<Follow?> GetFollowByIdAsync(Guid followingId, Guid followedId);
    Task<bool> IsFollowedAsync(Guid followedId);
}
