using Domain.Entitites;
using Domain.Repositories.Abstractions;
using Domain.Test;
using FluentAssertions;
using Infrastructure.Repositories.Commons;

namespace Infrastructure.Test.Repositories.Commons;

public class GenericCreationRepositoryTest : SetupTest
{
    private readonly IGenericRepository<Comment> _genericCreationRepository;

    public GenericCreationRepositoryTest()
    {
        _genericCreationRepository = new GenericCreationRepository<Comment>(
                                            _dbContext, _claimsServiceMock.Object);
    }
    [Fact]
    public async Task GenericCreationRepository_AddAsync_ShouldReturnCorrectData()
    {
        // arrange
        _claimsServiceMock.Setup(x => x.GetCurrentUserId).Returns(Guid.NewGuid());
        var currentId = _claimsServiceMock.Object.GetCurrentUserId;
        var mockData = MockCommentList(1).FirstOrDefault()!;

        // act
        await _genericCreationRepository.AddAsync(mockData);
        var result = await _dbContext.SaveChangesAsync();

        // assert
        result.Should().Be(1);
        mockData.CreatedBy.Should().Be(currentId);
    }

    [Fact]
    public async Task GenericCreationRepository_AddRangeAsync_ShouldReturnCorrectData()
    {
        // arrange
        _claimsServiceMock.Setup(x => x.GetCurrentUserId).Returns(Guid.NewGuid());
        var currentId = _claimsServiceMock.Object.GetCurrentUserId;
        var mockData = MockCommentList(5);

        // act
        await _genericCreationRepository.AddRangeAsync(mockData);
        var result = await _dbContext.SaveChangesAsync();

        // assert
        result.Should().Be(5);
        mockData.First().CreatedBy.Should().Be(currentId);
        mockData.Last().CreatedBy.Should().Be(currentId);
    }
}
