using Application.Commons;
using FluentAssertions;

namespace Application.Test.Commons;

public class HashPasswordStringTest
{
    [Fact]
    public void TestHashPasswordString_ShouldReturnCorrectData()
    {
        // arrange
        var password = "This Is Supper Strong Password!";
        var data = password.Hash();
        // action
        var result = "This Is Supper Strong Password!".Verify(data);
        // assert
        result.Should().BeTrue();
    }

    [Fact]
    public void TestHashPasswordString_ShouldReturnFalseWhenIncorrectString()
    {
        // arrange
        var password = "This Is Supper Strong Password!";
        var data = password.Hash();
        // action
        var result = "This Is Wrong Password!".Verify(data);
        // assert
        result.Should().BeFalse();
    }
}
