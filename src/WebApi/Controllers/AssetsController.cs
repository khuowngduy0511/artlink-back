using Application.Filters;
using Application.Models;
using Application.Services.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Utils;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AssetsController : ControllerBase
{
    private readonly IAssetService _assetService;
    private readonly IMapper _mapper;

    public AssetsController(
        IAssetService assetService,
        IMapper mapper)
    {
        _assetService = assetService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAssets([FromQuery] AssetCriteria criteria)
    {
        try
        {
            var result = await _assetService.GetAllAssetsAsync(criteria);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    [HttpGet("{assetId}")]
    public async Task<IActionResult> GetAssetById(Guid assetId)
    {
        try
        {
            var result = await _assetService.GetAssetByIdAsync(assetId);
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

    [HttpGet("download/{assetId}")]
    [Authorize]
    public async Task<IActionResult> GetAssetDownloadById(Guid assetId)
    {
        try
        {
            var link = await _assetService.GetDownloadUriAssetAsync(assetId);
            return Ok(new { link });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    [HttpGet("moderation/download/{assetId}")]
    [Authorize(Roles = "Moderator,Admin")]
    public async Task<IActionResult> GetAssetDownloadForModeration(Guid assetId)
    {
        try
        {
            var link = await _assetService.GetDownloadUriAssetForModerationAsync(assetId);
            return Ok(new { link });
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
    public async Task<IActionResult> AddAsset([FromForm] AssetModel assetModel)
    {
        try
        {
            var asset = await _assetService.AddAssetAsync(assetModel);
            return CreatedAtAction(nameof(GetAssetById), new { assetId = asset.Id }, asset);
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

    [HttpPut("{assetId}")]
    [Authorize]
    public async Task<IActionResult> UpdateAsset(Guid assetId, [FromBody] AssetEM assetEM)
    {
        try
        {
            await _assetService.UpdateAssetAsync(assetId, assetEM);
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

    [HttpDelete("{assetId}")]
    [Authorize]
    public async Task<IActionResult> DeleteAsset(Guid assetId)
    {
        try
        {
            await _assetService.DeleteAssetAsync(assetId);
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
