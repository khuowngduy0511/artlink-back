using Domain.Entitites;
using Domain.Repositories.Abstractions;
using Infrastructure.Database;
using Infrastructure.Repositories.Commons;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDBContext dbContext) : base(dbContext)
    {
    }

    public async Task<Category?> GetCategoryByIdAsync(Guid categoryId)
    {
        return await _dbContext.Categories.Include(x => x.Parent)
            .FirstOrDefaultAsync(x => x.Id == categoryId);
    }
}
