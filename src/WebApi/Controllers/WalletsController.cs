using Application.Models;
using Application.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Utils;

namespace WebApi.Controllers;

[ApiController]
public class WalletsController : ControllerBase
{
    private readonly IWalletService _walletService;
    private readonly IClaimService _claimService;

    public WalletsController(IWalletService walletService, IClaimService claimService)
    {
        _walletService = walletService;
        _claimService = claimService;
    }

    [Route("api/[controller]/{walletId}")]
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetWalletById(Guid walletId)
    {
        try
        {
            var result = await _walletService.GetWalletByIdAsync(walletId);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    [Route("api/account/{userId}/[controller]")]
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetWalletByUserId(Guid userId)
    {
        try
        {
            var result = await _walletService.GetWalletByAccountIdAsync(userId);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            return BadRequest(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    [Route("api/account/current-wallet")]
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetWalletOfCurrentUser()
    {
        try
        {
            var currentUserId = _claimService.GetCurrentUserId ?? default;
            var result = await _walletService.GetWalletByAccountIdAsync(currentUserId);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    [Route("api/[controller]/{walletId}")]
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateWallet(Guid walletId, WalletEM walletEM)
    {
        try
        {
            await _walletService.UpdateWalletAsync(walletId, walletEM);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }
}
