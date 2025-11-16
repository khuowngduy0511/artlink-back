using Application.Models;
using Application.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Utils;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProposalAssetsController : ControllerBase
{
    private readonly IProposalAssetService _proposalAssetService;

    public ProposalAssetsController(IProposalAssetService proposalAssetService)
    {
        _proposalAssetService = proposalAssetService;
    }

    [HttpGet("{proposalId}")]
    [Authorize]
    public async Task<IActionResult> GetProposalAssetsOfProposal(Guid proposalId)
    {
        try
        {
            var result = await _proposalAssetService.GetProposalAssetsOfProposalAsync(proposalId);
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

    [HttpGet("download/{proposalId}")]
    [Authorize]
    public async Task<IActionResult> GetProposalAssetDownloadById(Guid proposalId)
    {
        try
        {
            var link = await _proposalAssetService.GetDownloadUriProposalAssetAsync(proposalId);
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

    [HttpGet("file/{proposalAssetId}")]
    [Authorize]
    public async Task<IActionResult> DownloadProposalAssetFile(Guid proposalAssetId)
    {
        try
        {
            var result = await _proposalAssetService.DownloadProposalAssetAsync(proposalAssetId);
            
            if (result.stream == null || string.IsNullOrEmpty(result.fileName))
            {
                return NotFound(new ApiResponse { ErrorMessage = "File không tồn tại." });
            }

            return File(result.stream, result.contentType, result.fileName);
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
            Console.WriteLine($"[DownloadProposalAssetFile] Error: {ex.Message}");
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateProposalAssets([FromForm] ProposalAssetModel model)
    {
        try
        {
            Console.WriteLine($"[CreateProposalAssets] Received request");
            Console.WriteLine($"[CreateProposalAssets] ProposalId: {model?.ProposalId}");
            var result = await _proposalAssetService.AddProposalAssetAsync(model);
            Console.WriteLine($"[CreateProposalAssets] Success!");
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine($"[CreateProposalAssets] KeyNotFoundException: {ex.Message}");
            return NotFound(new ApiResponse { ErrorMessage = ex.Message });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[CreateProposalAssets] Exception: {ex.Message}");
            Console.WriteLine($"[CreateProposalAssets] StackTrace: {ex.StackTrace}");
            return StatusCode(500, new ApiResponse { ErrorMessage = ex.Message });
        }
    }
}
