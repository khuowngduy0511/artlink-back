using Application.Models;

namespace Application.Services.Abstractions;
public interface ICategoryService
{
    Task<CategoryVM?> GetCategoryByIdAsync(Guid categoryId);
    Task<List<CategoryVM>> GetAllCategoriesAsync();
    Task DeleteCategoryAsync(Guid categoryId);
    Task UpdateCategoryAsync(Guid categoryId, CategoryEM categoryEM);
    Task<CategoryVM> AddCategoryAsync(CategoryModel categoryModel);
}
