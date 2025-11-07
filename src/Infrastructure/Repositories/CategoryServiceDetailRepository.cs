using Domain.Entitites;
using Domain.Repositories.Abstractions;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class CategoryServiceDetailRepository : ICategoryServiceDetailRepository
{
    private readonly AppDBContext _dbContext;

    public CategoryServiceDetailRepository(AppDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddCategoryServiceAsync(CategoryServiceDetail categoryService)
    {
        await _dbContext.CategoryServiceDetails.AddAsync(categoryService);
    }

    public async Task DeleteAllCategoryServiceByServiceIdAsync(Guid serviceId)
    {
        var categoryService = await _dbContext.CategoryServiceDetails
            .Where(x => x.ServiceId == serviceId)
            .ToListAsync();
        _dbContext.CategoryServiceDetails.RemoveRange(categoryService);
    }

    public void DeleteCategoryService(CategoryServiceDetail categoryService)
    {
        _dbContext.CategoryServiceDetails.Remove(categoryService);
    }

    public async Task<List<CategoryServiceDetail>> GetAllCategoryOfServiceAsync(Guid artworkId)
    {
        return await _dbContext.CategoryServiceDetails
            .Where(x => x.ServiceId == artworkId)
            .Include(x => x.Category)
            .ToListAsync();
    }
}
