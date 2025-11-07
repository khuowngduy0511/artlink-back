using Domain.Entitites;

namespace Domain.Repositories.Abstractions;
public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<Category?> GetCategoryByIdAsync(Guid categoryId);
}
