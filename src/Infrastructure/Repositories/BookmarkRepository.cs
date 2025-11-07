using Domain.Entitites;
using Domain.Repositories.Abstractions;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class BookmarkRepository : IBookmarkRepository
{
    private readonly AppDBContext _dbContext;

    public BookmarkRepository(AppDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddBookmarkAsync(Bookmark bookmark)
        => await _dbContext.Bookmarks.AddAsync(bookmark);

    public void DeleteBookmark(Bookmark bookmark)
        => _dbContext.Bookmarks.Remove(bookmark);

    public async Task<bool> IsExistAsync(Bookmark bookmark)
        => await _dbContext.Bookmarks.FirstOrDefaultAsync(
                    x => x.ArtworkId == bookmark.ArtworkId
                        && x.CollectionId == bookmark.CollectionId) != null;

    public async Task<Bookmark?> GetByIdAsync(Guid collectionId, Guid artworkId)
        => await _dbContext.Bookmarks.FirstOrDefaultAsync(
                    x => x.ArtworkId == artworkId
                        && x.CollectionId == collectionId);
}
