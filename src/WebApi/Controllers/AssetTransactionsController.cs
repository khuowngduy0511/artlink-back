using Application.Models;
using Application.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Utils;
namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetTransactionsController : ControllerBase
    {
        private readonly IAssetTransactionService _assetTransactionService;

        public AssetTransactionsController(IAssetTransactionService assetTransactionService)
        {
            _assetTransactionService = assetTransactionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAssetTransactions()
        {
            try
            {
                var assetTransactions = await _assetTransactionService.GetAllAssetTransactionsAsync();
                return Ok(assetTransactions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
            }
        }

        [HttpGet("{assetTransactionId}")]
        public async Task<IActionResult> GetAssetTransactionById(Guid assetTransactionId)
        {
            try
            {
                var assetTransaction = await _assetTransactionService.GetAssetTransactionByIdAsync(assetTransactionId);
                return Ok(assetTransaction);
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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAssetTransaction([FromBody] AssetTransactionModel assetTransactionModel)
        {
            try
            {
                var assetTransaction = await _assetTransactionService.CreateAssetTransactionAsync(assetTransactionModel);
                return CreatedAtAction(
                    nameof(GetAssetTransactionById), new { assetTransactionId = assetTransaction.Id }
                    , assetTransaction);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ApiResponse { ErrorMessage = ex.Message });
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(new ApiResponse { ErrorMessage = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
            }
        }
    }
}
