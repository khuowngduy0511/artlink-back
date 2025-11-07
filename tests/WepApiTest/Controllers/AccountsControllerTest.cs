using Application.Commons;
using Application.Filters;
using Application.Models;
using Domain.Entities.Commons;
using Domain.Test;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;
using WebApi.Utils;
using Xunit.Abstractions;

namespace WepApiTest.Controllers;

public class AccountsControllerTest : SetupTest
{
    private static readonly int TOTAL_COUNT = 3;
    private static readonly int CURRENT_PAGE = 1;
    private readonly AccountsController _accountsController;
    private readonly ITestOutputHelper output;
    private readonly AccountCriteria _criteria;

    public AccountsControllerTest(ITestOutputHelper output)
    {
        this.output = output;

        _accountsController = new AccountsController(
            _accountServiceMock.Object,
            _mapperConfig,
            _claimsServiceMock.Object,
            _assetServiceMock.Object,
            _serviceServiceMock.Object,
            _artworkServiceMock.Object,
            _reviewServiceMock.Object);
        _criteria = new();
    }

    [Fact]
    public async Task GetAccounts_ShouldReturnCorrectData()
    {
        // Arrange
        var mockAccounts = MockAccountList(TOTAL_COUNT);
        var mockAccountVMs = _mapperConfig.Map<List<AccountVM>>(mockAccounts);
        var mockPagedList = new PagedList<AccountVM>(
            mockAccountVMs, TOTAL_COUNT, CURRENT_PAGE, TOTAL_COUNT);
        _accountServiceMock.Setup(service => service.GetAccountsAsync(_criteria))
            .ReturnsAsync(mockPagedList);

        // Act
        ActionResult<IPagedList<AccountVM>> result =
            await _accountsController.GetAccounts(_criteria);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        var pagedListResult = Assert.IsAssignableFrom<IPagedList<AccountVM>>(okResult.Value);
        var itemResult = pagedListResult.Items;
        Assert.NotNull(itemResult);
        Assert.Equal(mockAccounts.Count, itemResult.Count);
        Assert.Contains(mockAccountVMs[0], itemResult);
    }

    [Fact]
    public async Task GetAccounts_Returns_InternalServerError_On_Exception()
    {
        // Arrange
        _accountServiceMock.Setup(service => service.GetAccountsAsync(_criteria))
            .ThrowsAsync(new Exception("Something wrong in service"));

        // Act
        ActionResult<IPagedList<AccountVM>> result =
            await _accountsController.GetAccounts(_criteria);

        // Assert
        var objResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(StatusCodes.Status500InternalServerError, objResult.StatusCode);
        var apiResponse = Assert.IsType<ApiResponse>(objResult.Value);
        Assert.NotNull(apiResponse.ErrorMessage);
    }
}
