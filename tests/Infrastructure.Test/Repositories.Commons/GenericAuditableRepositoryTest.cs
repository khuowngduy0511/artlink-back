using Domain.Entitites;
using Domain.Repositories.Abstractions;
using Domain.Test;
using FluentAssertions;
using Infrastructure.Repositories.Commons;

namespace Infrastructure.Test.Repositories.Commons;

public class GenericAuditableRepositoryTest : SetupTest
{
    private readonly IGenericRepository<Comment> _genericAuditableRepository;

    public GenericAuditableRepositoryTest()
    {
        _genericAuditableRepository = new GenericAuditableRepository<Comment>(
                                            _dbContext, _claimsServiceMock.Object);
    }

    [Fact]
    public async Task GenericAuditableRepository_Update_ShouldReturnCorrectData()
    {
        // arrange
        _claimsServiceMock.Setup(x => x.GetCurrentUserId).Returns(Guid.NewGuid());

        var mockData = MockCommentList(1).FirstOrDefault()!;
        await _dbContext.Comments.AddAsync(mockData);
        await _dbContext.SaveChangesAsync();

        // act
        _genericAuditableRepository.Update(mockData);
        var result = await _dbContext.SaveChangesAsync();

        // assert
        result.Should().Be(1);
        mockData.LastModificatedBy.Should().Be(_claimsServiceMock.Object.GetCurrentUserId);
    }

    [Fact]
    public async Task GenericAuditableRepository_UpdateRange_ShouldReturnCorrectData()
    {
        // arrange
        _claimsServiceMock.Setup(x => x.GetCurrentUserId).Returns(Guid.NewGuid());

        var mockData = MockCommentList(5);
        await _dbContext.Comments.AddRangeAsync(mockData);
        await _dbContext.SaveChangesAsync();

        // act
        _genericAuditableRepository.UpdateRange(mockData);
        var result = await _dbContext.SaveChangesAsync();

        // assert
        result.Should().Be(5);
        mockData.First().LastModificatedBy.Should().Be(_claimsServiceMock.Object.GetCurrentUserId);
        mockData.Last().LastModificatedBy.Should().Be(_claimsServiceMock.Object.GetCurrentUserId);
    }

    [Fact]
    public async Task GenericAuditableRepository_SoftDelete_ShouldReturnCorrectData()
    {
        // arrange
        _claimsServiceMock.Setup(x => x.GetCurrentUserId).Returns(Guid.NewGuid());

        var mockData = MockCommentList(1).FirstOrDefault()!;
        await _dbContext.Comments.AddAsync(mockData);
        await _dbContext.SaveChangesAsync();

        // act
        _genericAuditableRepository.SoftDelete(mockData);
        var result = await _dbContext.SaveChangesAsync();

        // assert
        result.Should().Be(1);
        mockData.DeletedBy.Should().Be(_claimsServiceMock.Object.GetCurrentUserId);
        mockData.DeletedOn.Should().NotBeNull();
    }
}
