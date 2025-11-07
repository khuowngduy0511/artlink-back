using Domain.Entitites;
using Domain.Repositories.Abstractions;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class FollowRepository : IFollowRepository
{
    private readonly AppDBContext _dbContext;

    public FollowRepository(AppDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Follow?> GetByIdAsync(Guid followingId, Guid followedId)
    {
        return await _dbContext.Follows
            .FirstOrDefaultAsync(x => x.FollowingId == followingId
                                    && x.FollowedId == followedId);
    }
    public async Task<List<Follow>> GetAllFollowingsAsync(Guid followerId)
    {
        return await _dbContext.Follows
            .Where(x => x.FollowingId == followerId)
            .Include(x => x.Followed)
            .ToListAsync();
    }
    public async Task<List<Follow>> GetAllFollowersAsync(Guid followingId)
    {
        return await _dbContext.Follows
            .Where(x => x.FollowedId == followingId)
            .Include(x => x.Following)
            .ToListAsync();
    }
    public async Task AddFollowAsync(Follow follow)
        => await _dbContext.Follows.AddAsync(follow);

    public void DeleteFollow(Follow follow)
        => _dbContext.Follows.Remove(follow);
}
