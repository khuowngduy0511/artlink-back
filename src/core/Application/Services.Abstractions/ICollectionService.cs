using Application.Models;
using Domain.Entitites;

namespace Application.Services.Abstractions;

public interface ICollectionService
{
    Task<List<Collection>> GetAllCollectionsOfAccountAsync(Guid accountId);
    Task<Collection?> GetCollectionDetailAsync(Guid collectionId);
    Task CreateCollectionAsync(Collection collection, Guid artworkId);
    Task AddArtworkToCollectionAsync(Bookmark bookmark);
    Task RemoveArtworkFromCollectionAsync(Bookmark bookmark);
    Task DeleteCollectionAsync(Guid collectionId);
    Task UpdateCollectionAsync(Guid collectionId, CollectionModificationModel updateModel);
}