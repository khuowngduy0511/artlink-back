using Domain.Entitites;

namespace Domain.Repositories.Abstractions;
public interface ICategoryArtworkDetailRepository
{
    Task<List<CategoryArtworkDetail>> GetAllCategoryOfArtworkAsync(Guid artworkId);
    Task AddCategoryArtworkAsync(CategoryArtworkDetail categoryArtwork);
    void DeleteCategoryArtwork(CategoryArtworkDetail categoryArtwork);
}
