using Application.Models;

namespace Application.Services.Abstractions;
public interface ICategoryServiceDetailService
{
    Task AddCategoryServiceAsync(CategoryServiceModel categoryServiceModel);
    Task AddCategoryListServiceAsync(CategoryListServiceModel categoryListServiceModel, bool isSaveChanges = true);
    Task DeleteCategoryServiceAsync(Guid categoryServiceId);
}
