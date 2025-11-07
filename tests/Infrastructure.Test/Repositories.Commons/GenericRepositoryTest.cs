using AutoFixture;
using Domain.Entitites;
using Domain.Repositories.Abstractions;
using Domain.Test;
using FluentAssertions;
using Infrastructure.Repositories.Commons;

namespace Infrastructure.Test.Repositories.Commons;

public class GenericRepositoryTest : SetupTest
{
    private readonly IGenericRepository<Comment> _genericRepository;

    public GenericRepositoryTest()
    {
        _genericRepository = new GenericRepository<Comment>(_dbContext);
    }

    // Naming convention: ClassName_MethodName_ExpectedResult

    [Fact]
    public async Task GenericRepository_GetAllAsync_ShouldReturnCorrectData()
    {
        // arrange
        var mockData = MockCommentList(5);
        await _dbContext.Comments.AddRangeAsync(mockData);
        await _dbContext.SaveChangesAsync();

        // act
        var result = await _genericRepository.GetAllAsync();

        // assert
        result.Should().BeEquivalentTo(mockData);
    }

    [Fact]
    public async Task GenericRepository_GetAllAsync_ShouldReturnEmptyWhenHavingNoData()
    {
        // act
        var result = await _genericRepository.GetAllAsync();

        // assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GenericRepository_GetByIdAsync_ShouldReturnCorrectData()
    {
        // arrange
        var mockData = MockCommentList(1).FirstOrDefault()!;
        await _dbContext.Comments.AddAsync(mockData);
        await _dbContext.SaveChangesAsync();

        // act
        var result = await _genericRepository.GetByIdAsync(mockData.Id);

        // assert
        result.Should().BeEquivalentTo(mockData);
    }

    [Fact]
    public async Task GenericRepository_GetByIdAsync_ShouldReturnNullWhenNotFound()
    {
        // arrange
        var mockData = MockCommentList(5);
        await _dbContext.Comments.AddRangeAsync(mockData);
        await _dbContext.SaveChangesAsync();

        // act
        var result = await _genericRepository.GetByIdAsync(Guid.NewGuid());

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GenericRepository_AddAsync_ShouldReturnCorrectData()
    {
        // arrange
        var mockData = MockCommentList(1).FirstOrDefault()!;

        // act
        await _genericRepository.AddAsync(mockData);
        var result = await _dbContext.SaveChangesAsync();

        // assert
        result.Should().Be(1);
    }

    [Fact]
    public async Task GenericRepository_AddRangeAsync_ShouldReturnCorrectData()
    {
        // arrange
        var mockData = MockCommentList(5);

        // act
        await _genericRepository.AddRangeAsync(mockData);
        var result = await _dbContext.SaveChangesAsync();

        // assert
        result.Should().Be(5);
    }

    [Fact]
    public async Task GenericRepository_Update_ShouldReturnCorrectData()
    {
        // arrange
        var mockData = MockCommentList(1).FirstOrDefault()!;
        await _dbContext.Comments.AddAsync(mockData);
        await _dbContext.SaveChangesAsync();

        // act
        _genericRepository.Update(mockData);
        var result = await _dbContext.SaveChangesAsync();

        // assert
        result.Should().Be(1);
    }

    [Fact]
    public async Task GenericRepository_UpdateRange_ShouldReturnCorrectData()
    {
        // arrange
        var mockData = MockCommentList(5);
        await _dbContext.Comments.AddRangeAsync(mockData);
        await _dbContext.SaveChangesAsync();

        // act
        _genericRepository.UpdateRange(mockData);
        var result = await _dbContext.SaveChangesAsync();

        // assert
        result.Should().Be(5);
    }

    [Fact]
    public async Task GenericRepository_Delete_ShouldReturnCorrectData()
    {
        // arrange
        var mockData = MockCommentList(1).FirstOrDefault()!;
        await _dbContext.Comments.AddAsync(mockData);
        await _dbContext.SaveChangesAsync();

        // act
        _genericRepository.Delete(mockData);
        var result = await _dbContext.SaveChangesAsync();

        // assert
        result.Should().Be(1);
    }

    [Fact]
    public async Task GenericRepository_GetSingleByConditionAsync_ShouldReturnCorrectData()
    {
        // arrange
        var mockData = MockCommentList(1).FirstOrDefault()!;
        await _dbContext.Comments.AddAsync(mockData);
        await _dbContext.SaveChangesAsync();

        // act
        var result = await _genericRepository
                            .GetSingleByConditionAsync(x
                                => x.Content == mockData.Content);

        // assert
        result.Should().BeEquivalentTo(mockData);
    }

    [Fact]
    public async Task GenericRepository_GetListByConditionAsync_ShouldReturnCorrectData()
    {
        // arrange
        var mockData = _fixture.Build<Comment>()
                                .Without(x => x.Artwork)
                                .Without(x => x.Reply)
                                .Without(x => x.Account)
                                .Without(x => x.Replies)
                                .With(x => x.ArtworkId, Guid.NewGuid())
                                .CreateMany(5)
                                .ToList();
        await _dbContext.Comments.AddRangeAsync(mockData);
        await _dbContext.SaveChangesAsync();

        // act
        var result = await _genericRepository
                            .GetListByConditionAsync(x
                                => x.ArtworkId == mockData.First().ArtworkId);

        // assert
        result.Should().BeEquivalentTo(mockData);
    }
}
