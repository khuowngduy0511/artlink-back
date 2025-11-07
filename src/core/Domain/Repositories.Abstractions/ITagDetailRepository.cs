using Domain.Entitites;

namespace Domain.Repositories.Abstractions;
public interface ITagDetailRepository
{
    Task<List<TagDetail>> GetAllTagDetailsOfArtworkAsync(Guid artworkId);
    Task<TagDetail?> GetTagDetailAsync(Guid artworkId, Guid tagId);
    Task AddTagDetailAsync(TagDetail tagDetail);
    void DeleteTagDetail(TagDetail tagDetail);
}
