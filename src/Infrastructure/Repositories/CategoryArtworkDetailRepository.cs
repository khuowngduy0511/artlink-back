using Domain.Entitites;
using Domain.Repositories.Abstractions;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class CategoryArtworkDetailRepository : ICategoryArtworkDetailRepository
{
    private readonly AppDBContext _dbContext;

    public CategoryArtworkDetailRepository(AppDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddCategoryArtworkAsync(CategoryArtworkDetail categoryArtwork)
    {
        await _dbContext.CategoryArtworkDetails.AddAsync(categoryArtwork);
    }

    public void DeleteCategoryArtwork(CategoryArtworkDetail categoryArtwork)
    {
        _dbContext.CategoryArtworkDetails.Remove(categoryArtwork);
    }

    public async Task<List<CategoryArtworkDetail>> GetAllCategoryOfArtworkAsync(Guid artworkId)
    {
        return await _dbContext.CategoryArtworkDetails
            .Where(x => x.ArtworkId == artworkId)
            .Include(x => x.Category)
            .ToListAsync();
    }
}
