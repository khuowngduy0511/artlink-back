using AutoFixture;
using Domain.Entitites;
using Domain.Repositories.Abstractions;
using Domain.Test;
using FluentAssertions;
using Infrastructure.Repositories;

namespace Infrastructure.Test.Repositories;

public class CommentRepositoryTest : SetupTest
{
    private readonly ICommentRepository _commentRepository;

    public CommentRepositoryTest()
    {
        _commentRepository = new CommentRepository(_dbContext,
                        _claimsServiceMock.Object);
    }

    [Fact]
    public async Task CommentRepository_SoftDeleteReplies_ShouldReturnCorrectData()
    {
        // arrange
        _claimsServiceMock.Setup(x => x.GetCurrentUserId).Returns(Guid.NewGuid());
        var currentUserId = _claimsServiceMock.Object.GetCurrentUserId;
        var commentId = Guid.NewGuid();
        var mockReplies = _fixture.Build<Comment>()
                               .With(x => x.ReplyId, commentId)
                               .With(x => x.CreatedBy, currentUserId)
                               .Without(x => x.Artwork)
                               .Without(x => x.Replies)
                               .Without(x => x.Reply)
                               .Without(x => x.Account)
                               .CreateMany(5)
                               .ToList();
        var mockData = _fixture.Build<Comment>()
                               .With(x => x.Id, commentId)
                               .With(x => x.CreatedBy, currentUserId)
                               .With(x => x.Replies, mockReplies)
                               .Without(x => x.Artwork)
                               .Without(x => x.Reply)
                               .Without(x => x.Account)
                               .Create();
        await _dbContext.Comments.AddAsync(mockData);
        await _dbContext.SaveChangesAsync();

        // action
        _commentRepository.SoftDeleteReplies(mockData.Id);
        var result = await _dbContext.SaveChangesAsync();

        // assert
        result.Should().Be(5);
        mockReplies.First().DeletedBy.Should().Be(currentUserId);
        mockReplies.First().DeletedOn.Should().NotBeNull();
        mockReplies.Last().DeletedBy.Should().Be(currentUserId);
        mockReplies.Last().DeletedOn.Should().NotBeNull();
    }

}
