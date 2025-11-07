using Domain.Entitites;
using Domain.Repositories.Abstractions;
using Domain.Test;
using FluentAssertions;
using Infrastructure.Repositories;

namespace Infrastructure.Test.Repositories;

public class TagRepositoryTest : SetupTest
{
    private readonly ITagRepository _tagRepository;

    public TagRepositoryTest()
    {
        _tagRepository = new TagRepository(_dbContext);
    }

    public static List<Tag> TagTestData()
    {
        return new List<Tag>()
        {
            new() { TagName = "Nghệ thuật đương đại" },
            new() { TagName = "Kỹ thuật số" },
            new() { TagName = "Nghệ thuật số hóa" },
            new() { TagName = "Anime" },
            new() { TagName = "Ảo giác" }
        };
    }

    [Fact]
    public async Task TagRepository_GetAllAsync_ShouldReturnCorrectData()
    {
        // arrange
        var mockData = MockTagList(5);
        await _dbContext.Tags.AddRangeAsync(mockData);
        await _dbContext.SaveChangesAsync();

        // act
        var result = await _tagRepository.GetAllAsync();

        // assert
        result.Should().BeEquivalentTo(mockData);
    }

    [Fact]
    public async Task TagRepository_SearchTagsByNameAsync_ShouldReturnCorrectData()
    {
        // arrange
        var mockData = TagTestData();
        var resultData = new List<Tag>()
        {
            new() { TagName = "Nghệ thuật đương đại" },
            new() { TagName = "Kỹ thuật số" },
            new() { TagName = "Nghệ thuật số hóa" },
        };
        await _dbContext.Tags.AddRangeAsync(mockData);
        await _dbContext.SaveChangesAsync();

        // act
        var result = await _tagRepository.SearchTagsByNameAsync("thuật");

        // assert
        // loai tru Id khi so sanh
        result.Should().BeEquivalentTo(resultData, opt => opt.Excluding(x => x.Id));
    }

    [Fact]
    public async Task TagRepository_SearchTagsByNameAsync_ShouldReturnEmptyListWhenNotFound()
    {
        // arrange
        var mockData = TagTestData();
        await _dbContext.Tags.AddRangeAsync(mockData);
        await _dbContext.SaveChangesAsync();

        // act
        var result = await _tagRepository.SearchTagsByNameAsync("notfound");

        // assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task TagRepository_GetByIdAsync_ShouldReturnCorrectData()
    {
        // arrange
        var mockData = MockTagList(5);
        var resultData = mockData.FirstOrDefault()!;
        await _dbContext.Tags.AddRangeAsync(mockData);
        await _dbContext.SaveChangesAsync();

        // act
        var result = await _tagRepository.GetByIdAsync(resultData.Id);

        // assert
        result.Should().BeEquivalentTo(resultData);
    }

    [Fact]
    public async Task TagRepository_GetByIdAsync_ShouldReturnNullWhenNotFound()
    {
        // arrange
        var mockData = MockTagList(5);
        await _dbContext.Tags.AddRangeAsync(mockData);
        await _dbContext.SaveChangesAsync();

        // act
        // xac suat trung random id rat thaps
        var result = await _tagRepository.GetByIdAsync(Guid.NewGuid());

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task TagRepository_AddAsync_ShouldReturnCorrectData()
    {
        // arrange
        var mockData = MockTagList(1).FirstOrDefault()!;

        // act
        await _tagRepository.AddAsync(mockData);
        var result = await _dbContext.SaveChangesAsync();

        // assert
        result.Should().Be(1);
    }
}
