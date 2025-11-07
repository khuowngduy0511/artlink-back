using Application.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Utils;

namespace WebApi.Controllers;

[ApiController]
public class WalletHistoriesController : ControllerBase
{
    private readonly IWalletHistoryService _walletHistoryService;

    public WalletHistoriesController(IWalletHistoryService walletHistoryService)
    {
        _walletHistoryService = walletHistoryService;
    }

    //GET: api/account/{accountId}/wallet-histories
    [Route("api/account/{accountId}/wallet-histories")]
    [HttpGet]
    [Authorize]
    public async Task<ActionResult> GetWalletHistoriesByAccount(Guid accountId)
    {
        try
        {
            var walletHistories = await _walletHistoryService.GetWalletHistoriesOfAccount(accountId);
            return Ok(walletHistories);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }
}
