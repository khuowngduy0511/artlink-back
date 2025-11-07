using Domain.Entitites;

namespace Domain.Repositories.Abstractions;
public interface ICategoryServiceDetailRepository
{
    Task<List<CategoryServiceDetail>> GetAllCategoryOfServiceAsync(Guid artworkId);
    Task AddCategoryServiceAsync(CategoryServiceDetail categoryService);
    void DeleteCategoryService(CategoryServiceDetail categoryService);
    Task DeleteAllCategoryServiceByServiceIdAsync(Guid serviceId);
}
