using Domain.Entitites;
using Domain.Repositories.Abstractions;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class LikeRepository : ILikeRepository
{
    private readonly AppDBContext _dbContext;

    public LikeRepository(AppDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Like>> GetAllLikeOfArtworkAsync(Guid artworkId)
    {
        return await _dbContext.Likes
            .Where(x => x.ArtworkId == artworkId)
            .Include(x => x.Account)
            .ToListAsync();
    }

    public async Task<List<Like>> GetAllLikeOfAccountAsync(Guid accountId)
    {
        return await _dbContext.Likes
            .Where(x => x.AccountId == accountId)
            .Include(x => x.Artwork)
            .ToListAsync();
    }

    public async Task<Like?> GetByIdAsync(Guid accountId, Guid artworkId)
    {
        return await _dbContext.Likes
            .FirstOrDefaultAsync(x => x.AccountId == accountId && x.ArtworkId == artworkId);
    }

    public async Task AddLikeAsync(Like like)
     => await _dbContext.Likes.AddAsync(like);

    public void DeleteLike(Like like)
     => _dbContext.Likes.Remove(like);
}
