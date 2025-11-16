using Domain.Entities.Commons;
using Domain.Entitites;
using Domain.Repositories.Abstractions;
using Infrastructure.Database;
using Infrastructure.Repositories.Commons;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class TagRepository : GenericRepository<Tag>, ITagRepository
{
    public TagRepository(AppDBContext dBContext) : base(dBContext)
    {
    }

    public async Task<IPagedList<Tag>> GetAllTagsAsync(string? keyword, string? sortColumn, string? sortOrder, int page, int pageSize)
    {
        var allTags = _dbContext.Tags.AsQueryable();

        if (!string.IsNullOrEmpty(keyword))
        {
            keyword = keyword.ToLower();
            allTags = allTags.Where(x => x.TagName.ToLower().Contains(keyword));
        }

        #region sorting
        if (sortOrder?.ToLower() == "desc")
        {
            allTags = allTags.OrderByDescending(x => x.TagName);
        }
        else
        {
            allTags = allTags.OrderBy(x => x.TagName);
        }
        #endregion

        #region paging
        var result = await ToPaginationAsync(allTags, page, pageSize);
        #endregion

        return result;
    }

    public async Task<Tag?> GetTagByNameAsync(string tagName)
    {
        return await _dbContext.Tags.FirstOrDefaultAsync(x => x.TagName.ToLower().Equals(tagName.ToLower()));
    }

    public async Task<List<Tag>> SearchTagsByNameAsync(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
        {
            return await _dbContext.Tags
                .OrderBy(x => x.TagName)
                .Take(20)
                .ToListAsync();
        }
        
        // Case-insensitive search using ToLower for better matching
        keyword = keyword.ToLower().Trim();
        return await _dbContext.Tags
            .Where(x => x.TagName.ToLower().Contains(keyword))
            .OrderBy(x => x.TagName)
            .Take(20)
            .ToListAsync();
    }
}
