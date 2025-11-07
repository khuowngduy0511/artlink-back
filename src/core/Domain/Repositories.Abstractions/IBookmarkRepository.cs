using Domain.Entitites;

namespace Domain.Repositories.Abstractions;
public interface IBookmarkRepository
{
    Task AddBookmarkAsync(Bookmark bookmark);
    void DeleteBookmark(Bookmark bookmark);
    Task<Bookmark?> GetByIdAsync(Guid collectionId, Guid artworkId);
    Task<bool> IsExistAsync(Bookmark bookmark);
}
