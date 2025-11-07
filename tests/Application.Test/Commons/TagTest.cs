using Application.Services;
using Application.Services.Abstractions;
using Domain.Test;
using FluentAssertions;

namespace Application.Test.Commons;

public class TagTest : SetupTest
{
    private readonly ITagService _tagService;

    public TagTest()
    {
        _tagService = new TagService(_unitOfWorkMock.Object, _mapperConfig);
    }

    public static IEnumerable<object[]> TagTestData()
    {
        // true
        yield return new object[] { "Bầu trời", true };
        yield return new object[] { "núi", true };
        yield return new object[] { "1 piece", true };
        yield return new object[] { "GTA 5", true };

        // false
        yield return new object[] { "important!", false };
        yield return new object[] { "a", false };
        yield return new object[] { "RandomStringWith31Chars12345678", false };
        yield return new object[] { "@gmail", false };
        yield return new object[] { "any%", false };
        yield return new object[] { "Bầu_trời", false };
        yield return new object[] { "in-house", false };
        yield return new object[] { "Steins;Gate", false };
        yield return new object[] { "遥かか", false };
    }

    [Theory]
    [MemberData(nameof(TagTestData))]
    public void TestTag_ShouldVerifySuccessfully(string tag, bool expectedResult)
    {
        // action
        var result = _tagService.IsTagValid(tag);
        // assert
        result.Should().Be(expectedResult, because: $"Tag '{tag}' verification failed. Expected: {expectedResult}, Actual: {result}");
    }
}
