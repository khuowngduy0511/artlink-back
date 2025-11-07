using Application.Services.Abstractions;
using Domain.Entitites;
using Domain.Repositories.Abstractions;
using Infrastructure.Database;
using Infrastructure.Repositories.Commons;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class CollectionRepository : GenericCreationRepository<Collection>, ICollectionRepository
{
    public CollectionRepository(AppDBContext dBContext, IClaimService claimService) : base(dBContext, claimService)
    {
    }

    public async Task<List<Collection>> GetAllCollectionsOfAccountWithArtworksAsync(Guid accountId)
    {
        return await _dbContext.Collections
                                .Where(x => x.CreatedBy == accountId)
                                .Include(x => x.Account)
                                .Include(x => x.Bookmarks)
                                    .ThenInclude(x => x.Artwork)
                                        .ThenInclude(a => a.Account)
                                .ToListAsync();

        //    var query = await _dbContext.Collections
        //.Where(collection => collection.CreatedBy == accountId)
        //.Join(
        //    _dbContext.Accounts,
        //    collection => collection.CreatedBy,
        //    account => account.Id,
        //    (collection, account) => new { Collection = collection, Account = account }
        //)
        //.Join(
        //    _dbContext.Bookmarks,
        //    joined => joined.Collection.Id,
        //    bookmark => bookmark.CollectionId,
        //    (joined, bookmark) => new { joined.Collection, joined.Account, Bookmark = bookmark }
        //)
        //.Join(
        //    _dbContext.Artworks,
        //    joined => joined.Bookmark.ArtworkId,
        //    artwork => artwork.Id,
        //    (joined, artwork) => new { joined.Collection, joined.Account, joined.Bookmark, Artwork = artwork }
        //)
        //.ToListAsync();
        //    return query;
    }

    public async Task<Collection?> GetCollectionWithArtworksAsync(Guid collectionId)
    {
        return await _dbContext.Collections
                                .Include(x => x.Account)
                                .Include(x => x.Bookmarks)
                                    .ThenInclude(b => b.Artwork)
                                        .ThenInclude(a => a.Account)
                                .FirstOrDefaultAsync(x => x.Id == collectionId);
    }
}