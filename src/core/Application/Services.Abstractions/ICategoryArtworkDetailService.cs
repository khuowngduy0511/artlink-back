using Application.Models;

namespace Application.Services.Abstractions;
public interface ICategoryArtworkDetailService
{
    Task AddCategoryArtworkAsync(CategoryArtworkModel categoryArtworkModel);
    Task AddCategoryListArtworkAsync(CategoryListArtworkModel categoryListArtworkModel, bool isSaveChanges = true);
    Task DeleteCategoryArtworkAsync(Guid categoryArtworkId);
}
