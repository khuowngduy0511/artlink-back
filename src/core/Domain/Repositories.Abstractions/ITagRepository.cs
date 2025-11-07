using Domain.Entities.Commons;
using Domain.Entitites;

namespace Domain.Repositories.Abstractions;
public interface ITagRepository : IGenericRepository<Tag>
{
    Task<IPagedList<Tag>> GetAllTagsAsync(
        string? keyword, string? sortColumn, string? sortOrder, int page, int pageSize);
    Task<Tag?> GetTagByNameAsync(string tagName);
    Task<List<Tag>> SearchTagsByNameAsync(string keyword);
}
