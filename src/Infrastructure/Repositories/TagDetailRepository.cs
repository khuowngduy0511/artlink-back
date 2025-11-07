using Domain.Entitites;
using Domain.Repositories.Abstractions;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class TagDetailRepository : ITagDetailRepository
{
    private readonly AppDBContext _dbContext;

    public TagDetailRepository(AppDBContext dBContext)
    {
        _dbContext = dBContext;
    }

    public async Task AddTagDetailAsync(TagDetail tagDetail)
    {
        await _dbContext.TagDetails.AddAsync(tagDetail);
    }

    public void DeleteTagDetail(TagDetail tagDetail)
    {
        _dbContext.TagDetails.Remove(tagDetail);
    }

    public async Task<List<TagDetail>> GetAllTagDetailsOfArtworkAsync(Guid artworkId)
    {
        return await _dbContext.TagDetails
            .Where(x => x.ArtworkId == artworkId)
            .Include(x => x.Tag)
            .ToListAsync();
    }

    public async Task<TagDetail?> GetTagDetailAsync(Guid artworkId, Guid tagId)
    {
        return await _dbContext.TagDetails.FirstOrDefaultAsync(x => x.ArtworkId == artworkId && x.TagId == tagId);
    }
}
