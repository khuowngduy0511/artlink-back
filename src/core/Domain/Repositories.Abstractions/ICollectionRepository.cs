using Domain.Entitites;

namespace Domain.Repositories.Abstractions;
public interface ICollectionRepository : IGenericRepository<Collection>
{
    Task<List<Collection>> GetAllCollectionsOfAccountWithArtworksAsync(Guid accountId);
    Task<Collection?> GetCollectionWithArtworksAsync(Guid collectionId);
}
