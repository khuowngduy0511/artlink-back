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
            var filePath = await _proposalAssetService.GetDownloadUriProposalAssetAsync(proposalAssetId);
            
            if (string.IsNullOrEmpty(filePath))
            {
                return NotFound(new ApiResponse { ErrorMessage = "File không tồn tại." });
            }

            // Remove leading slash if present
            var localPath = filePath.TrimStart('/');
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", localPath);

            if (!System.IO.File.Exists(fullPath))
            {
                return NotFound(new ApiResponse { ErrorMessage = "File không tồn tại trên server." });
            }

            var fileBytes = await System.IO.File.ReadAllBytesAsync(fullPath);
            var fileName = Path.GetFileName(fullPath);
            var contentType = GetContentType(fileName);

            return File(fileBytes, contentType, fileName);
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

    private string GetContentType(string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLowerInvariant();
        return extension switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".pdf" => "application/pdf",
            ".zip" => "application/zip",
            ".psd" => "image/vnd.adobe.photoshop",
            ".ai" => "application/postscript",
            _ => "application/octet-stream",
        };
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
