using Domain.Entitites;

namespace Domain.Repositories.Abstractions;
public interface IImageRepository : IGenericRepository<Image>
{
    Task<List<Image>> GetImagesDuplicateAsync(Guid imageId);
}
